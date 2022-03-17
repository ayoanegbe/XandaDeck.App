using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using XandaApp.Data.Enums;
using XandaApp.Infra.Services;

namespace XandaApp.App
{
    public partial class Main : Form
    {
        private TimeSpan startTime;
        private Timer timer1;
        private int youtubeVideoLength;
        private bool isCopyrighted = false;
        private bool isWebViewInitialized = false;

        private readonly IConfiguration _configuration;
        private readonly ILogger<Main> _logger;

        private static readonly string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //private static Main _instance;

        public Main(ILogger<Main> logger)
        {
            InitializeComponent();

            _logger = logger;

            _configuration = AppServices.GetConfiguration();
           
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        public int YoutubeVideoDuration(string videoId)
        {
            string youtubekey = _configuration["Settings:YoutubeApiKey"];

            WebClient myDownloader = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            string jsonResponse = myDownloader.DownloadString(
                "https://www.googleapis.com/youtube/v3/videos?id=" + videoId + "&key="
                + youtubekey + "&part=contentDetails");
            dynamic dynamicObject = JsonConvert.DeserializeObject(jsonResponse);
            string tmp = dynamicObject.items[0].contentDetails.duration;
            var Duration = Convert.ToInt32(System.Xml.XmlConvert.ToTimeSpan(tmp).TotalMilliseconds);

            tmp = dynamicObject.items[0].contentDetails.licensedContent;
            isCopyrighted = bool.Parse(tmp);

            return Duration;
        }

        private bool _loopVideo;
        //private bool _stopVideo = false;
        //private IEnumerator<string> _videos;
        private List<string> _videos = new List<string>();
        private Timer videoTimer;

        public void ProcessVideos(bool isInfinite)
        {
            _loopVideo = isInfinite;
            _videos = GetVideoFileNames();           
            PlayVideos();
        }

        private List<string> GetVideoFileNames()
        {
            var path = baseDirectory + _configuration["AppSettings:VideoPath"];
            return Directory.EnumerateFiles(path, "*.mp4", SearchOption.TopDirectoryOnly)
                .ToList();
        }

        string _item;
        private void PlayVideos()
        {
            _item = _videos.FirstOrDefault();           
            PlayVideoAsync(_item);            

        }


        public void PlayVideoAsync(string filePath)
        {
            ScreenToggle(ContentType.Video);

            try
            {
                Core.Initialize();

                var libvlc = new LibVLC(enableDebugLogs: false);
                var uri = new Uri(filePath);
                var media = new Media(libvlc, uri);

                xandaVideoPlayer.Dock = DockStyle.Fill;
                xandaVideoPlayer.MediaPlayer = new MediaPlayer(libvlc) { EnableHardwareDecoding = true };
                videoTimer = new Timer();
                videoTimer.Start();
                videoTimer.Tick += new EventHandler(OnVideoTimerEvent);

                startTime = DateTime.Now.TimeOfDay;

                xandaVideoPlayer.MediaPlayer.Play(media);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error playing video: {ex}");
            }

        }

        private void OnVideoTimerEvent(object sender, EventArgs e)
        {
            TimeSpan timeSpan = (DateTime.Now.TimeOfDay - startTime);
            if (xandaVideoPlayer.MediaPlayer.Length < timeSpan.TotalMilliseconds)
            {
                //Video has finished playing!

                videoTimer.Stop();
                videoTimer.Enabled = false;
                xandaVideoPlayer.MediaPlayer.Stop();
                if ((_videos.IndexOf(_item) + 1) < _videos.Count())
                {
                    // Start the next one, if any.
                    _item = _videos[_videos.IndexOf(_item) + 1];
                    PlayVideoAsync(_item);
                }
                else
                {
                    DisplayMessage("Videos have finished playing", 10000);
                }
            }
        }

        public void StopVideo()
        {
            xandaVideoPlayer.MediaPlayer.Stop();
        }       

        private async void OnYoutubeTimerEvent(object sender, EventArgs e)
        {
            TimeSpan timeSpan = (DateTime.Now.TimeOfDay - startTime);
            if (youtubeVideoLength < timeSpan.TotalMilliseconds)
            {
                //Video has finished playing!

                timer1.Stop();
                timer1.Enabled = false;

                await Task.Delay(3000);

                DisplayMessage("Videos have finished playing", 10000); 
            }
        }

        // A file without the desired EXIF property record will throw ArgumentException.
        private static PropertyItem SafeGetPropertyItem(Image image, int propid)
        {
            try
            {
                return image.GetPropertyItem(propid);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public async void PlayYoutube(string youtubeVideoId)
        {

            youtubeVideoLength = YoutubeVideoDuration(youtubeVideoId);

            if (isCopyrighted)
            {
                DisplayMessage("This youtube video can't be played because it's copyrighted", 20000);
                return;
            }



            webView21.Dock = DockStyle.Fill;

            //int height = webView21.Height - 50;

            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}?&autoplay=1' allow='autoplay' width='100%' height='100%' frameborder='0' allowFullScreen='false'></iframe>";
            html += "</body></html>";

            var options = new CoreWebView2EnvironmentOptions("--autoplay-policy=no-user-gesture-required");
            var environment = await CoreWebView2Environment.CreateAsync(null, Environment.MachineName, options);
            if (!isWebViewInitialized)
            {
                await webView21.EnsureCoreWebView2Async(environment);
                isWebViewInitialized = true;
            }

            //CoreWebView2 webBrowser = new CoreWebView2();

            webView21.NavigateToString(string.Format(html, youtubeVideoId));

            //webView21.Dispose();

            timer1 = new Timer();

            timer1.Start();
            timer1.Tick += new EventHandler(OnYoutubeTimerEvent);

            startTime = DateTime.Now.TimeOfDay;

            return;
        }

        private async void InitializeWebView(string html)
        {
            ScreenToggle(ContentType.Youtube);
            webView21.Dock = DockStyle.Fill;

            var options = new CoreWebView2EnvironmentOptions("--autoplay-policy=no-user-gesture-required");
            var environment = await CoreWebView2Environment.CreateAsync(null, Environment.UserName, options);
            if (!isWebViewInitialized)
            {
                await webView21.EnsureCoreWebView2Async(environment);
                isWebViewInitialized = true;
            }
            //webView21.Scri
            webView21.NavigateToString(html);

            //webView21.Dispose();
        }

        public void StopRefresh()
        {
            webView21.Reload();
        }

        public void StopYoutube()
        {
            ScreenToggle(ContentType.Youtube);
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "</body></html>";

            webView21.NavigateToString(html);
        }

        public void ScreenToggle(ContentType contentType)
        {
            switch (contentType)
            {                
                case ContentType.Video:
                    xandaVideoPlayer.Visible = true;
                    webView21.Visible = false;
                    break;
                case ContentType.Youtube:
                    xandaVideoPlayer.Visible = false;
                    webView21.Visible = true;
                    break;
            }

            return;
        }

        public void SetVideoMode(DisplayMode mode)
        {
            switch (mode)
            {
                case DisplayMode.Video:
                    xandaSplitter.Panel1Collapsed = true;
                    xandaSplitter.Panel1.Hide();
                    break;
                case DisplayMode.Picture:
                    xandaSplitter.Panel2Collapsed = true;
                    xandaSplitter.Panel2.Hide();
                    break;
                default:
                    xandaSplitter.Panel1Collapsed = false;
                    xandaSplitter.Panel2Collapsed = false;
                    xandaSplitter.Panel1.Show();
                    xandaSplitter.Panel2.Show();
                    break;
            }
        }

        Timer msgTimer;
        public void DisplayMessage(string message, int duration)
        {

            //AnimateControl.Animate(messagePanel, AnimateControl.Effect.Slide, 100, 180);
            AnimateControl.BottomToUp(messagePanel, 5000);

            msgTimer = new Timer
            {
                Interval = duration
            };

            msgTimer.Start();
            msgTimer.Tick += new EventHandler(MessageTimerEvent);

            messagePanel.Visible = true;
            messageLabel.Text = message;

        }

        public void MessageTimerEvent(object sender, EventArgs e)
        {
            msgTimer.Stop();
            msgTimer.Enabled = false;
            AnimateControl.TopToBottom(messagePanel, 5000);
            messagePanel.Visible = false;
        }

        public int GetVideoPanelWidth() { return xandaSplitter.Panel2.Width; }
        public int GetPicturePanelWidth() { return xandaSplitter.Panel1.Width; }
        
        public void AdjustWidth(int value)
        {
            xandaSplitter.SplitterDistance = xandaSplitter.Panel1.Width + value;
        }
        public void CloseForm()
        {
            Close();
        }

        public void Youtube()
        {
            ScreenToggle(ContentType.Youtube);
            string youtubeVideoId = "h8cNnxLsvdk"; //"tiMD7FdhbPo"; //h8cNnxLsvdk
            PlayYoutube(youtubeVideoId);
        }

        public void Youtube(string videoId)
        {
            ScreenToggle(ContentType.Youtube);
            string youtubeVideoId = videoId;
            PlayYoutube(youtubeVideoId);
        }

        public void ProcessPictures(int duration, bool isInfinite)
        {
            _loopPix = isInfinite;
            _stopPix = false;
            _duration = duration;
            _images = GetImageFileNames();
            ShowImages();
        }

        public void LoadPicture(string imageFile)
        {
            xandaPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            Image image = Image.FromFile(imageFile);
            xandaPicture.Image = image;

            if (GetOrientation(image) != ImageOrientation.Original)
                xandaPicture.Image.RotateFlip(RotateFlipType.Rotate90FlipX);

        }

        private static ImageOrientation GetOrientation(Image image)
        {
            PropertyItem pi = SafeGetPropertyItem(image, 0x112);

            if (pi == null || pi.Type != 3)
            {
                return ImageOrientation.Original;
            }

            return (ImageOrientation)BitConverter.ToInt16(pi.Value, 0);
        }

        private List<string> GetImageFileNames()
        {
            var path = baseDirectory + _configuration["AppSettings:PicturePath"];
            return Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".jpeg"))
                .ToList();
        }

        private bool _loopPix;
        private bool _stopPix = false;
        private List<string> _images = new List<string>();
        private int _duration = 0;
        private async void ShowImages()
        {
            int iteration = 0;
            foreach (var item in _images)
            {
                if (_stopPix)
                {
                    return;
                }

                LoadPicture(item);
                await Task.Delay(_duration);
                iteration++;
                if (_loopPix && (iteration >= _images.Count()))
                {
                    ShowImages();
                }
            }

            DisplayMessage("Images have finished playing", 10000);
        }

        public void StopPixLoop()
        {
            _loopPix = false;
        }

        public void StopPix()
        {
            _stopPix = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            xandaPicture.Load(baseDirectory + @"\xandadocs\xandadeck.png");

            string html = "<html>";
            html += "<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css'";
            html += "integrity='sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u' crossorigin='anonymous'>";
            html += "<div class='container-fluid'>";
            html += "<div class='row'>";
            html += "<div class='col-md-2 col-sm-12 col-xs-12'>";
            html += "<nav id='navbar'>";
            html += "<h3>Technical Documentation</h3>";
            html += "<ul class='nav nav-pills nav-stacked'>";
            html += "<a class='nav-link' href='#Introduction' rel='internal'>";
            html += "<li>Introduction</li>";
            html += "</a>";
            html += "<a class='nav-link' href='#What_you_should_already_know' rel='internal'>";
            html += "<li>What you should already know</li>";
            html += "</a>";
            html += "<a class='nav-link' href='#About_Topic' rel='internal'>";
            html += "<li>About the topic</li>";
            html += "</a>";
            html += "<a class='nav-link' href='#Topic_1' rel='internal'>";
            html += "<li>Topic 1</li>";
            html += "</a>";
            html += "<a class='nav-link' href='#Topic_2' rel='internal'>";
            html += "<li>Topic 2</li>";
            html += "</a>";
            html += "</ul>";
            html += "</nav>";
            html += "</div>";
            html += "<div class='col-md-10 col-sm-12 col-xs-12'>";
            html += "<main id='main-doc'>";
            html += "<section class='main-section' id='Introduction'>";
            html += "<h3 style = 'background: red; color: white'>Introduction</h3>";
            html += "<article>";
            html += "<p>Some content about the main topic, for example Java documentation introduction about the language</p>";
            html += "</article>";
            html += "</section>";
            html += "<section class='main-section' id='What_you_should_already_know'>";
            html += "<h3 style = 'background: red; color: white'>What you should already know</h3>";
            html += "<article>";
            html += "<p>Background information before getting into the topic:</p>";
            html += "<li>Some list content</li>";
            html += "<li>Prerequisites.</li>";
            html += "<li>Workings and assumptions</li>";
            html += "<p>Any other content to be covered before learning this topic</p>";
            html += "</artice>";
            html += "</section>";
            html += "<section class='main-section' id='About_Topic'>";
            html += "<h3 style = 'background: red; color: white'>About topic</h3>";
            html += "<article>";
            html += "<p>More lines about the topic. For example, how the basic functionality works, features etc...</p>";
            html += "<p>Technical documentation should be thorough and to the point</p>";
            html += "<p>Write about features, comparisons with other languages etc</p>";
            html += "</article>";
            html += "</section>";
            html += "<section class='main-section' id='Topic_1'>";
            html += "<h3 style = 'background: red; color: white'>Topic 1</h3>";
            html += "<article>";
            html += "Getting started with the actual documentation content";
            html += "<code>This would come in a different color and font indicating lines of code</code>";
            html += "</article>";
            html += "</section>";
            html += "<section class='main-section' id='Topic_2'>";
            html += "<h3 style = 'background: red; color: white'>Topic 2</h3>";
            html += "<p>Another topic about the main topic, for example, if the topic is Java, this could be variables or data types in Java</p>";
            html += "</section>";
            html += "</main>";
            html += "</div>";
            html += "</div>";
            html += "</div>";
            html += "</html>";

            InitializeWebView(html);
        }


    }

}

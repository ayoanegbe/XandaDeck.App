using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using XandaApp.Data.Context;
using XandaApp.Data.Contexts;
using XandaApp.Data.Models;
using XandaApp.Infra.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using XandaApp.Data.Enums;

namespace XandaApp.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {
        private readonly IConfiguration config;

        public AppSettingsController()
        {
            config = AppServices.GetConfiguration();
        }

        //[HttpGet("startup")]
        //public IActionResult Startup()
        //{
            
        //    bool successful = false;
        //    try
        //    {
                
        //        string appName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
        //        int count = Process.GetProcessesByName(appName).Count();
        //        var exists = count >= 1; 
        //        if (exists)
        //        {
        //            return Ok("An instance is already running");
        //        }

        //        string fileName = config["AppSettings:AppName"];
        //        //string filePath = config["AppSettings:AppPath"]; // This will be used eventually
        //        string path = Path.Combine(AppContext.BaseDirectory, fileName);

        //        Process process = new Process();

        //        process.StartInfo.FileName = path;

        //        process.StartInfo.CreateNoWindow = true;
        //        process.StartInfo.UseShellExecute = false;
        //        process.StartInfo.RedirectStandardOutput = true;
        //        process.Start();
        //        successful = process.ExitCode == 0;
        //        process.Dispose();

        //        return Ok(successful);
        //    }
        //    catch (Exception /*ex*/)
        //    {
        //        return Ok(successful);
        //    }
        //}

        [HttpGet("shutdown")]
        public IActionResult Shutdown()
     {

            Application.Exit();
            return Ok(new { message = "Successful" });
  
        }

        //[HttpGet("main")]
        //public IActionResult MainForm()
        //{
        //    Program.MainForm.BeginInvoke(new Action(() =>
        //    {
        //        Program.MainForm.Show();
        //    }));

        //    return Ok(new { message = "Successful" });
        //}

        [HttpGet("play-video")]
        public IActionResult PlayVideo(bool isInfinite = false) 
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.ProcessVideos(isInfinite);
            }));

            return Ok(new { message = "Successful" }); 
        }        

        [HttpGet("stop-video")]
        public IActionResult StopVideo()
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.StopVideo();
            }));

            return Ok(new { message = "Successful" });
        }

        [HttpGet("play-youtube")]
        public IActionResult PlayYoutube()
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.Youtube();
            }));

            return Ok(new { message = "Successful" });
        }

        [HttpGet("play-youtube/{id}")]
        public IActionResult PlayYoutube(string videoId)
        {
            if (videoId == null) { return BadRequest(); }

            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.Youtube(videoId);
            }));

            return Ok(new { message = "Successful" });
        }

        [HttpGet("stop-youtube")]
        public IActionResult StopYoutube()
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.StopYoutube();
            }));

            return Ok(new { message = "Successful" });
        }

        [HttpGet("message/{id}")]
        public ActionResult Get(string id, int duration)
        {
            if (id == null) { return BadRequest(); }

            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.DisplayMessage(id, duration);
            }));

            return Ok();
        }

        [HttpGet("set-display")]
        public ActionResult Set(DisplayMode mode)
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.SetVideoMode(mode);
            }));
            return Ok();
        }

        [HttpGet("getpicture-width")]
        public ActionResult GetPicturePanelWidth()
        {
            int width = 0;
            Program.MainForm.Invoke(new Action(() =>
            {
                width = Program.MainForm.GetPicturePanelWidth();
            }));

            return Ok(width);
        }

        [HttpGet("adjust-width/{value}")]
        public ActionResult AdjustPicturePanelWidth(int value)
        {

            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.AdjustWidth(value);
            }));

            return Ok();
        }

        [HttpGet("getvideo-width")]
        public ActionResult GetVideoPanelWidth()
        {
            int width = 0;
            Program.MainForm.Invoke(new Action(() =>
            {
                width = Program.MainForm.GetVideoPanelWidth();
            }));

            return Ok(width);
        }

        [HttpGet("Load-picture/{duration}")]
        public ActionResult LoadPix(int duration, bool isInfinite = false)
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.ProcessPictures(duration, isInfinite);
            }));

            return Ok();
        }

        [HttpGet("stop-picture-loop")]
        public ActionResult ResetPixLoop()
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.StopPixLoop();
            }));

            return Ok();
        }

        [HttpGet("stop-picture")]
        public ActionResult StopPix()
        {
            Program.MainForm.BeginInvoke(new Action(() =>
            {
                Program.MainForm.StopPix();
            }));

            return Ok();
        }

    }
}

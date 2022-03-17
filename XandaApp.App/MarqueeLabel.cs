using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XandaApp.App
{
    public class MarqueeLabel : Label
    {
        Timer timer;
        int? left;
        int textWidth = 0;

        public MarqueeLabel()
        {
            DoubleBuffered = true;
            timer = new Timer() { Interval = 25 };
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (RightToLeft == RightToLeft.Yes)
            {
                left += 3;
                if (left > Width)
                    left = -textWidth;
            }
            else
            {
                left -= 3;
                if (left < -textWidth)
                    left = Width;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            var s = TextRenderer.MeasureText(Text, Font, new Size(0, 0),
                TextFormatFlags.TextBoxControl | TextFormatFlags.SingleLine);
            textWidth = s.Width;
            if (!left.HasValue) left = Width;
            var format = TextFormatFlags.TextBoxControl | TextFormatFlags.SingleLine |
                TextFormatFlags.VerticalCenter;
            if (RightToLeft == RightToLeft.Yes)
            {
                format |= TextFormatFlags.RightToLeft;
                if (!left.HasValue) left = -textWidth;
            }
            TextRenderer.DrawText(e.Graphics, Text, Font,
                new Rectangle(left.Value, 0, textWidth, Height),
                ForeColor, BackColor, format);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                timer.Dispose();
            base.Dispose(disposing);
        }
    }
}

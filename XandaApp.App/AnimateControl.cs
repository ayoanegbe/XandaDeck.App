using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using XandaApp.Data.Constants;

namespace XandaApp.App
{
    public class AnimateControl
    {
        private static int iflags;

        public enum Effect { Roll, Slide, Center, Blend }

        public static void Animate(Control ctl, Effect effect, int msec, int angle)
        {
            int flags = effmap[(int)effect];
            if (ctl.Visible) { flags |= 0x10000; angle += 180; }
            else
            {
                if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                else if (effect == Effect.Blend) throw new ArgumentException();
            }
            flags |= dirmap[(angle % 360) / 45];
            bool ok = AnimateWindow(ctl.Handle, msec, flags);
            if (!ok) throw new Exception("Animation failed");
            ctl.Visible = !ctl.Visible;
        }

        private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);

        public static void LeftToRight(Control ctl, int msec)
        {
            iflags = AnimationConstants.AW_ACTIVATE | AnimationConstants.AW_HOR_POSITIVE;

            iflags |= AnimationConstants.AW_SLIDE;

            ctl.Visible = true;

            AnimateWindow(ctl.Handle, msec, iflags);
        }

        public static void RightToLeft(Control ctl, int msec)
        {
            iflags = AnimationConstants.AW_ACTIVATE | AnimationConstants.AW_HOR_NEGATIVE;

            iflags |= AnimationConstants.AW_SLIDE;

            ctl.Visible = true;

            AnimateWindow(ctl.Handle, msec, iflags);
        }

        public static void TopToBottom(Control ctl, int msec)
        {
            iflags = AnimationConstants.AW_ACTIVATE | AnimationConstants.AW_VER_POSITIVE;

            iflags |= AnimationConstants.AW_SLIDE;

            ctl.Visible = true;

            AnimateWindow(ctl.Handle, msec, iflags);
        }

        public static void BottomToUp(Control ctl, int msec)
        {
            iflags = AnimationConstants.AW_ACTIVATE | AnimationConstants.AW_VER_NEGATIVE;

            iflags |= AnimationConstants.AW_SLIDE;

            ctl.Visible = true;

            AnimateWindow(ctl.Handle, msec, iflags);
        }

        public static void CollapseInward(Control ctl, int msec)
        {
            iflags = AnimationConstants.AW_ACTIVATE | AnimationConstants.AW_CENTER;

            ctl.Visible = true;

            AnimateWindow(ctl.Handle, msec, iflags);
        }

        public static void Fade(Control ctl, int msec)
        {
            iflags = AnimationConstants.AW_ACTIVATE | AnimationConstants.AW_BLEND;

            ctl.Visible = true;

            AnimateWindow(ctl.Handle, msec, iflags);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BSFG_AutoFarm
{
    public static class RColor
    {
        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

           public static string Get(IntPtr hWnd, int x, int y)
            {
                IntPtr hdc = GetDC(hWnd);
                uint pixel = GetPixel(hdc, x, y);
                ReleaseDC(hWnd, hdc);
                Color color = Color.FromRgb(
                    (byte)(pixel & 0x000000FF),
                    (byte)((pixel & 0x0000FF00) >> 8),
                    (byte)((pixel & 0x00FF0000) >> 16));
                return color.R.ToString();
            }
            
        


       // F2_START = GetPixelColor(BSFGhWnd, 1839, 321);
       // F2test_START = F2_START.R.ToString();
    }
}

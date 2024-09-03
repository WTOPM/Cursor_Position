using ImageMagick;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace BSFG_AutoFarm
{
    internal class Iconic
    {


        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
    int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);
        public static BitmapImage Ico(IntPtr BSFGhWnd, int xSrc, int ySrc, int xDest, int yDest)
        {
            IntPtr hdcSrc = GetWindowDC(BSFGhWnd);
            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);

            int nWidth = xDest - xSrc;
            int nHeight = yDest - ySrc;

            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, nWidth, nHeight);
            // select the bitmap object
            IntPtr hOld = SelectObject(hdcDest, hBitmap);
            // bitblt over
            BitBlt(hdcDest, 0, 0, xDest, yDest, hdcSrc, xSrc, ySrc, SRCCOPY);
            // restore selection
            SelectObject(hdcDest, hOld);
            // clean up 
            DeleteDC(hdcDest);
            ReleaseDC(BSFGhWnd, hdcSrc);
            System.Drawing.Image img = System.Drawing.Image.FromHbitmap(hBitmap);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            using (MagickImage image = new MagickImage(bmp))
            {
                image.Negate();
                image.Threshold(new Percentage(60)); // 60 is OK 
                image.Depth = 1;
                using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
                {
                    bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
            }
        }
    }
}

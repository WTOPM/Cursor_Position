using NHotkey.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BSFG_AutoFarm
{
    public class GameResObject : INotifyPropertyChanged
    {
        private string _name;
        private string _color;

        public string GameResolutionColor
        {
            get
            {
                return _color;
            }

            set
            {
                if (_color == value) return;

                _color = value;
                OnPropertyChanged("GameResolutionColor");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //bool isPartyAssist = false;
        //bool isHeroTarget = false;
        bool screenSkill = false;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string windowName);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessageMouse(
            IntPtr hWnd,               // handle to destination window
            int Msg,                // message
            int wParam,             // first message parameter
            int lParam);			// second message parameter

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        // ******************************************************************
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

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

        IntPtr BSFGhWnd;
        string BSFGTitle;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref RECT rectangle);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        private const UInt32 WM_CLOSE = 0x0010;
        const UInt32 WM_KEYDOWN = 0x0100;
        const UInt32 WM_KEYUP = 0x0101;

        private static ushort VK_F1 = 0x70;
        private static ushort VK_F2 = 0x71;
        private static ushort VK_F3 = 0x72;
        private static ushort VK_F4 = 0x73;
        private static ushort VK_F5 = 0x74;
        private static ushort VK_F6 = 0x75;
        private static ushort VK_F7 = 0x76;
        private static ushort VK_F8 = 0x77;
        private static ushort VK_F9 = 0x78;
        private static ushort VK_F10 = 0x79;
        private static ushort VK_F11 = 0x7A;
        private static ushort VK_F12 = 0x7B;
        private static ushort VK_ESC = 0x1B;
        private static ushort VK_PAGE_UP = 0x21;
        private static ushort VK_PAGE_DOWN = 0x22;
        private static ushort WM_LBUTTONDOWN = 0x0201;
        private static ushort WM_LBUTTONUP = 0x0202;
        private const uint MK_LBUTTON = 0x0001;
        private const uint MK_RBUTTON = 0x0002;
        private static ushort WM_RBUTTONDOWN = 0x0204;
        private static ushort WM_RBUTTONUP = 0x0205;

        private static ushort VK_1 = 0x31;
        private static ushort VK_2 = 0x32;
        private static ushort VK_3 = 0x33;
        private static ushort VK_4 = 0x34;
        private static ushort VK_5 = 0x35;
        private static ushort VK_6 = 0x36;
        private static ushort VK_7 = 0x37;
        private static ushort VK_8 = 0x38;
        private static ushort VK_9 = 0x39;
        private static ushort VK_0 = 0x30;
        private static ushort VK_MINUS = 0xBD;
        private static ushort VK_PLUS = 0xBB;

        private static ushort VK_NUM1 = 0x61;
        private static ushort VK_NUM2 = 0x62;
        private static ushort VK_NUM3 = 0x63;
        private static ushort VK_NUM4 = 0x64;
        private static ushort VK_NUM5 = 0x65;
        private static ushort VK_NUM6 = 0x66;
        private static ushort VK_NUM7 = 0x67;
        private static ushort VK_NUM8 = 0x68;
        private static ushort VK_NUM9 = 0x69;
        private static ushort VK_NUM0 = 0x60;
        private static ushort VK_NUM_MULT = 0x6A;
        private static ushort VK_NUM_DIV = 0x6F;

        public class PicClass
        {
            public BitmapImage Img { get; set; }
            public string ImgName { get; set; }
        }

        public System.Collections.ObjectModel.ObservableCollection<PicClass> Images { get; set; }

        public bool turnOnButton = false;

        public string currentGameWindowResolution;

        public const string GameWindowResolutionRule1 = "1768x992";
        public const string GameWindowResolutionRule2 = "1366x768";
        public const string GameWindowResolutionRule3 = "1920x1080";
        public const string GameWindowResolutionRule4 = "1600x900";
        public const string GameWindowResolutionRule5 = "1280x720";
        public const int GameWindowResolutionRule1Value = 816;

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);


        static void BtnPress(IntPtr hwnd, int x, int y, int resx, int resy)
        {
            System.Threading.Thread.Sleep(300);
            SetForegroundWindow(hwnd);
            System.Threading.Thread.Sleep(600);

            int X = (x * 65535) / resx;
            int Y = (y * 65535) / resy;

            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
            System.Threading.Thread.Sleep(100);
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
            System.Threading.Thread.Sleep(100);
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
            System.Threading.Thread.Sleep(100);
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
            System.Threading.Thread.Sleep(400);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(4);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(4);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(300);
        }

#pragma warning disable 649

        internal struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

#pragma warning restore 649

        public static void ClickOnPoint(IntPtr wndHandle, Point clientPoint)
        {
            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

        }

        private void clickerActivate(object sender, NHotkey.HotkeyEventArgs e)
        {
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void clickerDeactivate(object sender, NHotkey.HotkeyEventArgs e)
        {
            buttonOff.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        public MainWindow()
    {

            HotkeyManager.Current.AddOrReplace("Act", Key.PageUp, ModifierKeys.None, clickerActivate);
            HotkeyManager.Current.AddOrReplace("Deact", Key.PageDown, ModifierKeys.None, clickerDeactivate);

            InitializeComponent();


            button.IsEnabled = false;

        }

        public int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_MOVE = 0x01;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_ClickOff(object sender, RoutedEventArgs e)
        {
           
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
    }
}

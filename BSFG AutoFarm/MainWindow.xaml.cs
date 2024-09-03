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


        public string GameResolution
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value) return;

                _name = value;
                OnPropertyChanged("GameResolution");
            }
        }

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
        int monsterCount;
        int attackCount;
        int massCount;


        int skillX;
        int skillY;

        int resX;
        int resY;

        int skillCdX;
        int cdX;
        int skillCdY;
        int cdY;
        int cdY_;

        int hpMonsterX;
        int hpMonsterY;
        int hpEnemyPlayerY;

        int halfHpMonsterX;
        int fullHpMonsterX;

        int activeBuffX;
        int activeBuffY;
        int activeBuffAdd;

        int tpOnDeathX;
        int tpOnDeathXAlternative;
        int tpOnDeathY;

        int fullCPPlayerX;
        int fullCPPlayerY;

        int checkHuntX;
        int checkHuntY;

        int huntEnergyX;      //21  color 99
        int huntEnergyY;      //72

        //int huntOneOneX;
        //int huntOneOneY;
        //int huntOneTwoX;
        //int huntOneTwoY;

        //int huntTwoOneX;
        //int huntTwoOneY;
        //int huntTwoTwoX;
        //int huntTwoTwoY;

        //int huntThreeOneX;
        //int huntThreeOneY;
        //int huntThreeTwoX;
        //int huntThreeTwoY;

        int huntFourX;  //169 color   (4)-45 : (3)- 48     (2)-64               (1) - 168   color 45
        int huntFourOneY;  //97                                                  (1)-97
        int huntFourTwoY;   //104               (3)- 51     (2)-48                (1)-104     color 44

        int giveQuestWindowX;
        int giveQuestWindowY;

        int questWindowY;

        int completeQuestWindowY;

        int levelUpwindow30X;
        int levelUpwindow30Y;

        int levelUpwindow40X;
        int levelUpwindow40Y;

        int mpHalfX;
        int mpHalfY;

        int huntBtn4X;
        int huntBtn4Y;

        int huntBtn3X;
        int huntBtn3Y;

        int huntBtn2X;
        int huntBtn2Y;

        int huntBtn1X;
        int huntBtn1Y;

        int huntBtnX;
        int huntBtnY;

        int huntWindowCloseX;
        int huntWindowCloseY;

        string M_F1, M_F2, M_F3, M_F4, M_F5, M_F6, M_F7, M_F8, M_F9, M_F10, M_F11, M_F12;
        string M_K1, M_K2, M_K3, M_K4, M_K5, M_K6, M_K7, M_K8, M_K9, M_K10, M_K11, M_K12;
        string M_N1, M_N2, M_N3, M_N4, M_N5, M_N6, M_N7, M_N8, M_N9, M_N10, M_N11, M_N12;

        int partyFirstMemberX;
        int partyFirstActiveX;
        int partyFirstMemberY;
        int partyFirstActiveY;

        bool one;
        bool two;
        bool three;
        bool four;

        string playerMP;
        string playerMP2;

        int chestWindowCloseX;         //1302         color 225
        int chestWindowCloseY;         //252



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


        //private void checkBoxPartyAssist_Checked(object sender, RoutedEventArgs e)
        //{
        //    isPartyAssist = true;
        //}
        public bool IsPartyAssist()
        {
            bool result = false;

            if (checkBoxPartyAssist.IsChecked == true) result = true;

            return result;
        }

        //private void checkBoxResetSkill_Checked(object sender, RoutedEventArgs e)
        //{
        //    screenSkill = false;
        //}
        public bool IsScreenSkill()
        {
            bool result = false;

            if (checkBoxResetSkill.IsChecked == true) result = true;

            return result;
        }

        //private void checkBoxHeroTarget_Checked(object sender, RoutedEventArgs e)  // RESET SKILLS
        //{
        //    isHeroTarget = true;
        //}
        public bool IsHeroTarget()
        {
            bool result = false;

            if (checkBoxHeroTarget.IsChecked == true) result = true;

            return result;
        }
        //private void checkBoxSaveMana_Checked(object sender, RoutedEventArgs e)
        //{

        //}
        public bool IsSaveMana()
        {
            bool result = false;

            if (checkBoxSaveMana.IsChecked == true) result = true;

            return result;
        }

        public bool IsBuff1Hour()
        {
            bool result = false;

            if (checkBoxBuff1Hour.IsChecked == true) result = true;

            return result;
        }

        public bool IsCheckHunt()
        {
            bool result = false;

            if (checkBoxHunt.IsChecked == true) result = true;

            return result;
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
            findMonster.Tick += new EventHandler(findMonster_Tick); ///EXAMPLE - 3
            findMonster.Interval = new TimeSpan(0, 0, 0, 0, 200);

            attackMonster.Tick += new EventHandler(attackMonster_Tick);
            attackMonster.Interval = new TimeSpan(0, 0, 0, 0, 1);

            massMonster.Tick += new EventHandler(massMonster_Tick);
            massMonster.Interval = new TimeSpan(0, 0, 0, 0, 200);

            selfBuff.Tick += new EventHandler(selfBuff_Tick);
            selfBuff.Interval = new TimeSpan(0, 0, 0, 1, 0);

            buff1Hour.Tick += new EventHandler(buff1Hour_Tick);
            buff1Hour.Interval = new TimeSpan(0, 0, 0, 1, 0);


            pickUp.Tick += new EventHandler(pickUp_Tick);
            pickUp.Interval = new TimeSpan(0, 0, 0, 0, 1);

            checkHunt.Tick += new EventHandler(checkHunt_Tick);
            checkHunt.Interval = new TimeSpan(0, 0, 0, 6, 0);

            selfHeal.Tick += new EventHandler(selfHeal_Tick);
            selfHeal.Interval = new TimeSpan(0, 0, 0, 1, 0);

            cutCreep.Tick += new EventHandler(cutCreep_Tick);
            cutCreep.Interval = new TimeSpan(0, 0, 0, 0, 250);

            tpOnDeath.Tick += new EventHandler(tpOnDeath_Tick);
            tpOnDeath.Interval = new TimeSpan(0, 0, 0, 5, 0);

            partyAssist.Tick += new EventHandler(partyAssist_Tick);
            partyAssist.Interval = new TimeSpan(0, 0, 0, 0, 250);

            partyFollow.Tick += new EventHandler(partyFollow_Tick);
            partyFollow.Interval = new TimeSpan(0, 0, 0, 4, 0);

            fightHero.Tick += new EventHandler(fightHero_Tick);
            fightHero.Interval = new TimeSpan(0, 0, 0, 0, 100);

            closeWindow.Tick += new EventHandler(closeWindow_Tick);
            closeWindow.Interval = new TimeSpan(0, 0, 0, 1, 0);

            HotkeyManager.Current.AddOrReplace("Act", Key.PageUp, ModifierKeys.None, clickerActivate);
            HotkeyManager.Current.AddOrReplace("Deact", Key.PageDown, ModifierKeys.None, clickerDeactivate);

            InitializeComponent();

            BSFGTitle = this.Title;

            button.IsEnabled = false;

            gameWindowResLabel.DataContext = GWRO; // This is the whole bind operation

            Images = new System.Collections.ObjectModel.ObservableCollection<PicClass>();

            comboBox.SelectedValuePath = "Value";


            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains("EndlessWar"))
                {
                    if (pList.ProcessName == "EndlessWar")
                    {
                        BSFGhWnd = pList.MainWindowHandle;

                        comboBox.Items.Add(new { Text = "EW:  ", Img = Iconic.Ico(BSFGhWnd, 19, 4, 234, 77), Value = BSFGhWnd });
                    }
                }
            }
        }

        public int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }
        /// /////////////////////////////////////////////////////////////////////////////// START CODE
        ///EXAMPLE 2
        private void findMonster_Tick(object sender, EventArgs e)
        {
            string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster

            string secondTarget = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 10) + (cdY_ * 2)); //N11
            string firstTarget = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 11) + (cdY_ * 2)); //N12
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER

            if (hpMonster != "140" || (hpEnemy != "140" && IsHeroTarget()))
            {
                if (firstTarget == "188" && monsterCount == 0) //--R = 188-- - have skill color
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM_MULT, 0);
                }
                if (secondTarget == "188" && monsterCount == 1)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM_DIV, 0);
                }
                monsterCount++;
            }

            if (monsterCount >= 2)
            {
                monsterCount = 0;
            }

        }

        private void attackMonster_Tick(object sender, EventArgs e)
        {
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER


            if (hpEnemy != "140")
            {
                string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster
                string fullHpMonster = RColor.Get(BSFGhWnd, fullHpMonsterX, hpMonsterY);    //HP Monster

             //   string attack = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY);             //Attack - F1
                string firstSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + cdY);  //Fight 1 - F2
                string secondSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY);                     //Fight 2 - 1
                string fourthSkill = RColor.Get(BSFGhWnd, skillX, skillY);                     //Fight 4 - N1
                string fivehSkill = RColor.Get(BSFGhWnd, skillX, skillY);
                string sixhSkill = RColor.Get(BSFGhWnd, skillX, skillY + cdY);

               // string _F1 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY);                             // F1 cooldown
                string _F2 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + cdY);         // F2 cooldown
                string _K1 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY);                             // K1 cooldown
                string _N1 = RColor.Get(BSFGhWnd, skillCdX, skillCdY);                       // N1 cooldown
                string _K2 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + cdY);
                string _N2 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + cdY);

                if (IsSaveMana())
                {
                    playerMP = RColor.Get(BSFGhWnd, mpHalfX, mpHalfY);    //MP PLAYER HALF  COLOR - 17
                    playerMP2 = RColor.Get(BSFGhWnd, mpHalfX - 4, mpHalfY);    //MP PLAYER HALF  COLOR - 17
                }
                else 
                {
                    playerMP = "17";
                }


                if (hpMonster == "140")

                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);

                    if (playerMP == "17" || playerMP2 == "17")
                    {
                        if (firstSkill == "188" && _F2 == M_F2) //--R = 188-- - have skill color
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F2, 0);
                        }
                        if (secondSkill == "188" && _K1 == M_K1 && fullHpMonster == "140")   //Jump skill
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_1, 0);
                        }
                        if (fourthSkill == "188" && _K2 == M_K2)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_2, 0);
                        }
                        if (fivehSkill == "188" && _N1 == M_N1)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM1, 0);
                        }
                        if (sixhSkill == "188" && _N2 == M_N2)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM2, 0);
                        }

                    }
                }


          
            }

        }

        private void massMonster_Tick(object sender, EventArgs e)
        {
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER
            if (hpEnemy != "140")
            {


                if (IsSaveMana())
                {
                    playerMP = RColor.Get(BSFGhWnd, mpHalfX, mpHalfY);    //MP PLAYER HALF  COLOR - 17
                    playerMP2 = RColor.Get(BSFGhWnd, mpHalfX - 4, mpHalfY);    //MP PLAYER HALF  COLOR - 17
                }
                else
                {
                    playerMP = "17";
                }

                string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster
                    string halfHpMonster = RColor.Get(BSFGhWnd, halfHpMonsterX, hpMonsterY - 1);    //half HP Monster  99%  //Color 151 (152)

                    string firstSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 2));     //Mass 1 - Key3
                    string secondSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 2));    //Mass 2 - F3
                    string thirdSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 3));     //Mass 3 - Key4
                    string fourthSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 3));  //Mass 4 - F4    
                string fiveSkill = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 2));
                string sixSkill = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 3));

                string _K3 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 2));
                    string _F3 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 2));
                    string _K4 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 3));
                    string _F4 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 3));

                    string _F2 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + cdY);

                string _N3 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 2));
                string _N4 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 3));

                if (_F2 != M_F2 && hpMonster == "140" && halfHpMonster != "155" && (playerMP == "17" || playerMP2 == "17"))
                    {
                        if (firstSkill == "188" && _K3 == M_K3) //--R = 188-- - have skill color
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_3, 0);
                        }
                        if (secondSkill == "188" && _F3 == M_F3)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F3, 0);
                        }
                        if (thirdSkill == "188" && _K4 == M_K4)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_4, 0);
                        }
                        if (fourthSkill == "188" && _F4 == M_F4)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F4, 0);
                        }
                        if (fiveSkill == "188" && _N3 == M_N3)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM3, 0);
                        }
                        if (sixSkill == "188" && _N4 == M_N4)
                        {
                            PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM4, 0);
                        }
                    }
              
            }
        }



        private void selfBuff_Tick(object sender, EventArgs e)
        {
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER
            string isFirstPartyActive = RColor.Get(BSFGhWnd, partyFirstActiveX, partyFirstActiveY);    //IS FIRST PATY MEMBER ACTIVE  COLOR-255
            string cpNotFull = RColor.Get(BSFGhWnd, fullCPPlayerX, fullCPPlayerY);    //IS MY CHARACTER CP NOT FULL   165 COLOR


            string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster

            string firstSelfBuff = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 7) + cdY_);      //K8
            string secondSelfBuff = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 8) + (cdY_ * 2)); //K7
            string thirdSelfBuff = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 9) + (cdY_ * 2)); //K10
            string fourthSelfBuff = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 10) + (cdY_ * 2)); //K11
            string fiveSelfBuff = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 11) + (cdY_ * 2)); //K12

            string _K8 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 7) + cdY_);         //K8
            string _K9 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 8) + (cdY_ * 2));   //K7
            string _K10 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 9) + (cdY_ * 2));   //K10
            string _K11 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 10) + (cdY_ * 2)); //K11
            string _K12 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 11) + (cdY_ * 2)); //K12

            string _K1 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY);

            string _F2 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + cdY); // CD FIRST skill

            if (IsSaveMana())
            {
                playerMP = RColor.Get(BSFGhWnd, mpHalfX, mpHalfY);    //MP PLAYER HALF  COLOR - 17
                playerMP2 = RColor.Get(BSFGhWnd, mpHalfX - 4, mpHalfY);    //MP PLAYER HALF  COLOR - 17
            }
            else
            {
                playerMP = "17";
            }

            if ((hpEnemy == "140" && isFirstPartyActive != "255") || (hpMonster == "140" && (playerMP == "17" || playerMP2 == "17")))
            {
                if (fiveSelfBuff == "188" && _K12 == M_K12 && _K1 == M_K1)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_PLUS, 0); // DASH
                }
                if (fourthSelfBuff == "188" && _K11 == M_K11 && _F2 != M_F2)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_MINUS, 0);
                }
                if (thirdSelfBuff == "188" && _K10 == M_K10 && _F2 != M_F2)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_0, 0);
                }
                if (secondSelfBuff == "188" && _K9 == M_K9 && _F2 != M_F2)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_9, 0);
                }
                if (firstSelfBuff == "188" && _K8 == M_K8 && _F2 != M_F2)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_8, 0);
                }
                
            }         

            if (cpNotFull != "165")
            {
                PostMessage(BSFGhWnd, WM_KEYDOWN, VK_7, 0);
                PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F7, 0);

                PostMessage(BSFGhWnd, WM_KEYDOWN, VK_7, 0);
                PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F7, 0);
            }
        }



        private void buff1Hour_Tick(object sender, EventArgs e)
        {
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER
            string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster

            string firstBuff1Hour = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 4) + cdY_); //N5
            string secondBuff1Hour = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 5) + cdY_); //N6
            string thirdBuff1Hour = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 6) + cdY_); //N7
            string fourthBuff1Hour = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 7) + cdY_); //N8
            string fiveBuff1Hour = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 8) + (cdY_ * 2)); //N9
            string sixBuff1Hour = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 9) + (cdY_ * 2)); //N10

            string fourActiveBuff = RColor.Get(BSFGhWnd, activeBuffX + (activeBuffAdd * 3), activeBuffY); //four ACTIVE BUFF  color=132 x=339;y=0

            if (IsBuff1Hour() && fourActiveBuff != "132" && (hpMonster == "140" || hpEnemy == "140"))
            {
    
                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM5, 0);
                    System.Threading.Thread.Sleep(500);

                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM6, 0);
                    System.Threading.Thread.Sleep(500);
 
                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM7, 0);
                    System.Threading.Thread.Sleep(500);
                

                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM8, 0);
                    System.Threading.Thread.Sleep(500);
             

                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM9, 0);
                    System.Threading.Thread.Sleep(500);
                
 
                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM0, 0);
                    System.Threading.Thread.Sleep(500);
                

                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F5, 0);
                    System.Threading.Thread.Sleep(500);

                    System.Threading.Thread.Sleep(500);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F6, 0);
                    System.Threading.Thread.Sleep(500);

            }
        }
        private void pickUp_Tick(object sender, EventArgs e)
        {
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER

            string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster

            string pickUpSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 11) + (cdY_ * 2));      //pickUp F12

            string keySkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 9) + (cdY_ * 2));     //F10 KEY
            string _F10 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), (skillCdY + (cdY * 9) + (cdY_ * 2)) + 5);    //F10 KEY CD

            if (hpMonster != "140" && hpEnemy != "140")
            {
                if (pickUpSkill == "188")
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);

                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F12, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F12, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F12, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F12, 0);

                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F10, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F10, 0);
                    System.Threading.Thread.Sleep(250);
                    if (keySkill == "188" && _F10 == M_F10)
                    {
                        PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F12, 0);
                        PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F12, 0);
                    }

                }
            }
        }
        
        private void checkHunt_Tick(object sender, EventArgs e)   
        {  
            string checkHunt = RColor.Get(BSFGhWnd, checkHuntX, checkHuntY);   // checkHunt color 246 (x=203;y=106)
            string huntPower = RColor.Get(BSFGhWnd, huntEnergyX, huntEnergyY);     //   21 72  color 99
            if (checkHunt != "246" && huntPower == "99")
            {
                BtnPress(BSFGhWnd, checkHuntX, checkHuntY, resX, resY);
            }

            if (checkHunt != "246" && huntPower != "99")
            {
                       huntPower = RColor.Get(BSFGhWnd, huntEnergyX, huntEnergyY);     //   21 72  color 99

                string fourOne = RColor.Get(BSFGhWnd, huntFourX, huntFourOneY);     //   color 45
                string fourTwo = RColor.Get(BSFGhWnd, huntFourX, huntFourTwoY);     //   color 45
                bool IsFourHuntEmpty()
                {
                    bool result = false;
                    if (fourOne == "45" && fourTwo == "45" && huntPower != "99") result = true;
                    return result;
                }

                string threeOne = RColor.Get(BSFGhWnd, huntFourX, huntFourOneY);     //   color 48
                string threeTwo = RColor.Get(BSFGhWnd, huntFourX, huntFourTwoY);     //   color 51
                bool IsThreeHuntEmpty()
                {
                    bool result = false;
                    if (threeOne == "48" && threeTwo == "51" && huntPower != "99") result = true;
                    return result;
                }

                string twoOne = RColor.Get(BSFGhWnd, huntFourX, huntFourOneY);     //     169 97  color 64
                string twoTwo = RColor.Get(BSFGhWnd, huntFourX, huntFourTwoY);     //     169 104  color 48
                bool IsTwoHuntEmpty()
                {
                    bool result = false;
                    if (twoOne == "64" && twoTwo == "48" && huntPower != "99") result = true;
                    return result;
                }

                string oneOne = RColor.Get(BSFGhWnd, huntFourX-1, huntFourOneY);     //   168 97  color 45
                string oneTwo = RColor.Get(BSFGhWnd, huntFourX-1, huntFourTwoY);     //   168 104  color 44              
                bool IsOneHuntEmpty()
            {
                bool result = false;
                if (oneOne == "45" && oneTwo == "44" && huntPower != "99") result = true;
                return result;
            }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (IsFourHuntEmpty() && four == false)
                {

                    BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    BtnPress(BSFGhWnd, huntBtn4X - 50, huntBtn4Y, resX, resY);
                    BtnPress(BSFGhWnd, checkHuntX, checkHuntY, resX, resY);

                    string close = RColor.Get(BSFGhWnd, huntWindowCloseX, huntWindowCloseY);     //color 247
                    if (close == "247")
                    {
                        BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    }
                    four = true;
                }

                if (IsThreeHuntEmpty() && three == false)
                {

                    BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    BtnPress(BSFGhWnd, huntBtn4X - 100, huntBtn4Y, resX, resY);
                    BtnPress(BSFGhWnd, checkHuntX, checkHuntY, resX, resY);

                    string close = RColor.Get(BSFGhWnd, huntWindowCloseX, huntWindowCloseY);     //color 247
                    if (close == "247")
                    {
                        BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    }
                    three = true;
                }

                if (IsTwoHuntEmpty() && two == false)
                {

                    BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    BtnPress(BSFGhWnd, huntBtn4X - 150, huntBtn4Y, resX, resY);
                    BtnPress(BSFGhWnd, checkHuntX, checkHuntY, resX, resY);

                    string close = RColor.Get(BSFGhWnd, huntWindowCloseX, huntWindowCloseY);     //color 247
                    if (close == "247")
                    {
                        BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    }
                    two = true;
                }

                if (IsOneHuntEmpty() && one == false)
                {

                    BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    BtnPress(BSFGhWnd, huntBtn4X, huntBtn4Y, resX, resY);
                    BtnPress(BSFGhWnd, checkHuntX, checkHuntY, resX, resY);

                    string close = RColor.Get(BSFGhWnd, huntWindowCloseX, huntWindowCloseY);     //color 247
                    if (close == "247")
                    {
                        BtnPress(BSFGhWnd, huntBtnX, huntBtnY, resX, resY);
                    }
                    one = false;
                }





            }
        }



        private void selfHeal_Tick(object sender, EventArgs e)
        {
            string selfHP = RColor.Get(BSFGhWnd, 220, 46);    //selfHP        x=220y=46Color=152

            string firstSelfHeal = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 5) + cdY_); //firstSelfHeal SKILL

            string _K6 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 5) + cdY_); //firstSelfHeal SKILL Cooldown Key6  


            if (selfHP != "152")
            {
                if (firstSelfHeal == "188" && _K6 == M_K6)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_6, 0);
                    System.Threading.Thread.Sleep(50);
                }
            }
        }

        private void cutCreep_Tick(object sender, EventArgs e)
        {
            string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster

            string cutCreepSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 10) + (cdY_ * 2)); //cutCreep SKILL

            string _F11 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 10) + (cdY_ * 2)); //cutCreep SKILL Cooldown Key6  

            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER

            if (hpMonster != "140" && hpEnemy != "140")   // 78 85 80 90 79 81 72 - no HP colors
            {
                if (cutCreepSkill == "188" && _F11 == M_F11)
                {
                    System.Threading.Thread.Sleep(50);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F11, 0);
                }
            }
        }

        private void tpOnDeath_Tick(object sender, EventArgs e)
        {
            string checkDeath = RColor.Get(BSFGhWnd, tpOnDeathX, tpOnDeathY); // check Death screen activeColor=237 (x=936;y=448)
            string checkDeathAlternative = RColor.Get(BSFGhWnd, tpOnDeathXAlternative, tpOnDeathY); // check Death screen activeColor=237 (x=936;y=448)
            string checkNoHP = RColor.Get(BSFGhWnd, 20, 46); // check Death screen haveHPCOLOR=140 (x=20;y=46)

            string checkHunt = RColor.Get(BSFGhWnd, checkHuntX, checkHuntY);   // checkHunt color 246 (x=203;y=106)

            if ((checkDeath == "237" || checkDeathAlternative == "237") && checkNoHP != "140")
            {
                System.Threading.Thread.Sleep(200);
                SetForegroundWindow(BSFGhWnd);
                System.Threading.Thread.Sleep(700);

                if (checkHunt == "246")
                {
                    //int Xx = ((checkHuntX + 3) * 65535) / resX;
                    //int Yy = ((checkHuntY + 3) * 65535) / resY;

                    //System.Threading.Thread.Sleep(200);
                    //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, Xx, Yy, 0, 0);
                    //System.Threading.Thread.Sleep(700);
                    //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                    //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    //System.Threading.Thread.Sleep(700);

                    BtnPress(BSFGhWnd, checkHuntX + 3, checkHuntY + 3, resX, resY);

                }
               // System.Threading.Thread.Sleep(200);
               // int X = (tpOnDeathX * 65535) / resX;
               //int Y = (tpOnDeathY * 65535) / resY;
               // mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
               // System.Threading.Thread.Sleep(500);
               // mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
               // System.Threading.Thread.Sleep(65);
               // mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                BtnPress(BSFGhWnd, tpOnDeathX, tpOnDeathY, resX, resY);

                System.Threading.Thread.Sleep(2000);
                buttonOff.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); // CLICKER OFF PROGRAM
            }

            if (checkDeathAlternative == "237" && checkNoHP != "140")
            {
                SetForegroundWindow(BSFGhWnd);
                System.Threading.Thread.Sleep(500);

                if (checkHunt == "246")
                {
                    //int Xx = (checkHuntX * 65535) / resX;
                    //int Yy = (checkHuntY * 65535) / resY;

                    //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, Xx, Yy, 0, 0);
                    //System.Threading.Thread.Sleep(500);
                    //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

                    //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    //System.Threading.Thread.Sleep(500);

                    BtnPress(BSFGhWnd, checkHuntX + 3, checkHuntY + 3, resX, resY);
                }

                //int X = (tpOnDeathXAlternative * 65535) / resX;
                //int Y = (tpOnDeathY * 65535) / resY;
                //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
                //System.Threading.Thread.Sleep(500);
                //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                //System.Threading.Thread.Sleep(65);
                //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                BtnPress(BSFGhWnd, tpOnDeathXAlternative, tpOnDeathY + 3, resX, resY);

                System.Threading.Thread.Sleep(2000);
                buttonOff.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); // CLICKER OFF PROGRAM
            }
        }

        private void partyAssist_Tick(object sender, EventArgs e)
        {
            string hpMonster = RColor.Get(BSFGhWnd, hpMonsterX, hpMonsterY);    //HP Monster
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER
            string isFirstPartyActive = RColor.Get(BSFGhWnd, partyFirstActiveX, partyFirstActiveY);    //IS FIRST PATY MEMBER ACTIVE  COLOR-255
            string isFirstMemberAlive = RColor.Get(BSFGhWnd, partyFirstMemberX, partyFirstMemberY);

            if (hpMonster != "140" && isFirstMemberAlive == "143")
            {
                int X = ((partyFirstMemberX + 5) * 65535) / resX;
                int Y = ((partyFirstMemberY + 5) * 65535) / resY;

                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);

                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                System.Threading.Thread.Sleep(15);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);

                if (hpEnemy == "140" && isFirstPartyActive == "255") //target first member
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                }

            }
            if (isFirstMemberAlive != "143")
            {
                PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM_DIV, 0);
            }


        }



        private void partyFollow_Tick(object sender, EventArgs e)
        {
            //int X = (partyFirstMemberX * 65535) / resX;
            //int Y = (partyFirstMemberY * 65535) / resY;

            //mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);

            //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //System.Threading.Thread.Sleep(5);
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //System.Threading.Thread.Sleep(5);
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //System.Threading.Thread.Sleep(5);
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            BtnPress(BSFGhWnd, partyFirstMemberX, partyFirstMemberY, resX, resY);
            BtnPress(BSFGhWnd, partyFirstMemberX, partyFirstMemberY, resX, resY);
            BtnPress(BSFGhWnd, partyFirstMemberX, partyFirstMemberY, resX, resY);
            BtnPress(BSFGhWnd, partyFirstMemberX, partyFirstMemberY, resX, resY);

        }
        




            private void fightHero_Tick(object sender, EventArgs e)
        {
            string hpEnemy = RColor.Get(BSFGhWnd, hpMonsterX, hpEnemyPlayerY);    //HP ENEMY PLAYER
            string isFirstPartyActive = RColor.Get(BSFGhWnd, partyFirstActiveX, partyFirstActiveY);    //IS FIRST PATY MEMBER ACTIVE  COLOR-255

            string attack = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY);             //Attack - F1
            string firstSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + cdY);  //Fight 1 - F2
            string secondSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY);                     //Fight 2 - 1
            string thirdSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY + cdY);  //Fight 3 - 2
            string fourthSkill = RColor.Get(BSFGhWnd, skillX, skillY);                     //Fight 4 - N1
            string fiveSkill = RColor.Get(BSFGhWnd, skillX, skillY + cdY); //Fight 5 - N2

            string _F1 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY);                             // F1 cooldown
            string _F2 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + cdY);         // F2 cooldown
            string _K1 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY);                             // K1 cooldown
            string _K2 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + cdY);         // K2 cooldown
            string _N1 = RColor.Get(BSFGhWnd, skillCdX, skillCdY);                             // N1 cooldown
            string _N2 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + cdY);         // N2 cooldown                              PVP SKILL

            if (hpEnemy == "140" && isFirstPartyActive != "255")             
            {
                if (attack == "188" && attackCount == 0 && _F1 == M_F1)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F1, 0);
                }
                if (firstSkill == "188" && attackCount == 1 && _F2 == M_F2) //--R = 188-- - have skill color
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F2, 0);
                }
                if (secondSkill == "188" && attackCount == 2 && _K1 == M_K1)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_1, 0);
                }
                if (thirdSkill == "188" && attackCount == 3 && _K2 == M_K2)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_2, 0);
                }
                if (fourthSkill == "188" && attackCount == 4 && _N1 == M_N1)   //Jump skill
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM1, 0);
                }
                if (fiveSkill == "188" && attackCount == 5 && _N2 == M_N2)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM2, 0);
                }
                attackCount++;
            }

            if (attackCount >= 6)
            {
                attackCount = 0;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// MASS HERO

            string massFirstSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 2));     //Mass 1 - Key3
            string massSecondSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 2));    //Mass 2 - F3
            string massThirdSkill = RColor.Get(BSFGhWnd, skillX + cdX, skillY + (cdY * 3));     //Mass 3 - Key4
            string massFourthSkill = RColor.Get(BSFGhWnd, skillX + (cdX * 2), skillY + (cdY * 3));  //Mass 4 - F4
            string massFiveSkill = RColor.Get(BSFGhWnd, skillX, skillY + (cdY * 2));  //Mass 5 - N3             // PVP SKILL
            string massSixSkill = RColor.Get(BSFGhWnd, 1761, 400);  //Mass 6 - N4                               // PVP SKILL

            string _K3 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 2));
            string _F3 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 2));
            string _K4 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 3));
            string _F4 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 3));
            string _N3 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 2));                                     // PVP SKILL
            string _N4 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 3));                                     // PVP SKILL


            if (hpEnemy == "140" && isFirstPartyActive != "255")
            {
                if (massFirstSkill == "188" && massCount == 0 && _K3 == M_K3) //--R = 188-- - have skill color
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_3, 0);
                }
                if (massSecondSkill == "188" && massCount == 1 && _F3 == M_F3)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F3, 0);
                }
                if (massThirdSkill == "188" && massCount == 2 && _K4 == M_K4)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_4, 0);
                }
                if (massFourthSkill == "188" && massCount == 3 && _F4 == M_F4)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_F4, 0);
                }
                if (massFiveSkill == "188" && massCount == 4 && _N3 == M_N3)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM3, 0);              //PVP SKILL
                }
                if (massSixSkill == "188" && massCount == 5 && _N4 == M_N4)
                {
                    PostMessage(BSFGhWnd, WM_KEYDOWN, VK_NUM4, 0);              //PVP SKILL
                }
                massCount++;
            }
            if (massCount >= 6)
            {
                massCount = 0;
            }

        }

        private void closeWindow_Tick(object sender, EventArgs e)
        {
            string checkGiveQuest = RColor.Get(BSFGhWnd, giveQuestWindowX, giveQuestWindowY);   // check give quest COLOR 225
            string checkCompleteQuest = RColor.Get(BSFGhWnd, giveQuestWindowX, completeQuestWindowY);   // check complete quest COLOR 225
            string questWindow = RColor.Get(BSFGhWnd, giveQuestWindowX, questWindowY);   // check color 225
            string checkLevelUpWindow30 = RColor.Get(BSFGhWnd, levelUpwindow30X, levelUpwindow30Y);   // color 188
            string checkLevelUpWindow40 = RColor.Get(BSFGhWnd, levelUpwindow30X, levelUpwindow40Y);   // color 188
            string closeChestWindow = RColor.Get(BSFGhWnd, chestWindowCloseX, chestWindowCloseY);   // color 188

            if (closeChestWindow == "225")
            {
                BtnPress(BSFGhWnd, chestWindowCloseX + 3, chestWindowCloseY + 3, resX, resY);
            }


            if (questWindow == "225")
            {
                BtnPress(BSFGhWnd, giveQuestWindowX + 3, questWindowY + 3, resX, resY);
            }

            if (checkLevelUpWindow30 == "188")
            {
                BtnPress(BSFGhWnd, levelUpwindow30X + 10, levelUpwindow30Y + 10, resX, resY);
            }

            if (checkLevelUpWindow40 == "188")
            {
                BtnPress(BSFGhWnd, levelUpwindow30X + 10, levelUpwindow40Y + 10, resX, resY);
            }

            if (checkGiveQuest == "225")
                {
                BtnPress(BSFGhWnd, giveQuestWindowX + 3, giveQuestWindowY + 3, resX, resY);
                }
                if (checkCompleteQuest == "225")
                {
                BtnPress(BSFGhWnd, giveQuestWindowX + 3, completeQuestWindowY + 3, resX, resY);
                }          
            }

        /// /////////////////////////////////////////////////////////////////////////////     STOP CODE
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

        // System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Wwindows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer findMonster = new System.Windows.Threading.DispatcherTimer();  ///EXAMPLE - 1
        System.Windows.Threading.DispatcherTimer attackMonster = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer massMonster = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer selfBuff = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer buff1Hour = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer pickUp = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer checkHunt = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer selfHeal = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer cutCreep = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer tpOnDeath = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer partyAssist = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer partyFollow = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer fightHero = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer closeWindow = new System.Windows.Threading.DispatcherTimer();



        GameResObject GWRO = new GameResObject();

        private void btnTurnOn(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageUp)
            {

                MessageBox.Show("PIDARAS");

            }
        }

        public void FirstMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            // btn1
            MessageBox.Show("PIDARAS");
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////// START
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (IsScreenSkill() || screenSkill == false)
            {
                if (currentGameWindowResolution == GameWindowResolutionRule2) //1366 768
                {
                    halfHpMonsterX = 610; // half HP
                    fullHpMonsterX = 752;

                    hpMonsterX = 474;
                    hpMonsterY = 405;
                    hpEnemyPlayerY = 421;   ///////// TO DO

                    skillX = 1207;
                    skillY = 124;

                    skillCdX = 1189;
                    cdX = 48;
                    skillCdY = 126;
                    cdY = 40;
                    cdY_ = 10;

                    activeBuffX = 261;
                    activeBuffY = 0;
                    activeBuffAdd = 26;

                    resX = 1366;
                    tpOnDeathX = 632;  //659 OR 632
                    tpOnDeathXAlternative = 659;  //659 OR 632
                    resY = 768;
                    tpOnDeathY = 292;

                    partyFirstMemberX = 48; 
                    partyFirstMemberY = 306;

                    partyFirstActiveX = 16;
                    partyFirstActiveY = 271;

                    fullCPPlayerX = 200;       //COLOR 165
                    fullCPPlayerY = 33;

                    checkHuntX = 203;
                    checkHuntY = 106;

                    giveQuestWindowX = 303; //
                    giveQuestWindowY = 193;   //   COLOR 225

                    completeQuestWindowY = 143;

                    mpHalfX = 0;    //     ?????? MP on NOTE HALF
                    mpHalfY = 0;    //     ?????
                }

                if (currentGameWindowResolution == GameWindowResolutionRule3)   // 1920 1080
                {
                    halfHpMonsterX = 887;     // half HP
                    fullHpMonsterX = 1029;

                    hpMonsterX = 751;
                    hpMonsterY = 561;
                    hpEnemyPlayerY = 577;     /// TO DO

                    skillX = 1761;
                    skillY = 280;

                    skillCdX = 1743;
                    cdX = 48;
                    skillCdY = 282;
                    cdY = 40;
                    cdY_ = 10;

                    activeBuffX = 261;
                    activeBuffY = 0;
                    activeBuffAdd = 26;

                    resX = 1920;
                    tpOnDeathX = 909;    //1920    (  909 * 65535 ) / 1920       // 936 или 909
                                         //    (tpOnDeathX * 65535) / resX;
                    tpOnDeathXAlternative = 936;

                    resY = 1080;
                    tpOnDeathY = 448;    //1080    ( 448*65535)  /1080
                                           //   (tpOnDeathY * 65535) / resY;

                      partyFirstMemberX = 48;
                      partyFirstMemberY = 306;

                    partyFirstActiveX = 16;
                    partyFirstActiveY = 271;

                    fullCPPlayerX = 200;       //COLOR 165
                    fullCPPlayerY = 33;

                    checkHuntX = 203; //color 246
                    checkHuntY = 106;

                    giveQuestWindowX = 303;   //COLOR 225
                    giveQuestWindowY = 349;
                    questWindowY = 243;
                    completeQuestWindowY = 299;

                    mpHalfX = 137;
                    mpHalfY = 59;

                    huntEnergyX = 21;      //21  color 99
                    huntEnergyY = 72;      //72

                    huntFourX = 169;  //169 color   (4)-45 : (3)- 48     (2)-64               (1) - 168   color 45
                    huntFourOneY = 97;  //97                                                  (1)-97
                    huntFourTwoY = 104;   //104               (3)- 51     (2)-48                (1)-104     color 44

                    levelUpwindow30X = 1638; //color 188
                    levelUpwindow30Y = 581;

                    levelUpwindow40X = 1638; //color 188
                    levelUpwindow40Y = 398;

                    huntBtnX = 30;
                    huntBtnY = 99;

                    huntBtn4X = 1500;
                    huntBtn4Y = 50;

                    huntWindowCloseX = 1903;   // color 247
                    huntWindowCloseY = 12;

                    chestWindowCloseX = 1302;         //1302         color 225
                    chestWindowCloseY = 252;         //252
                }


                M_F1 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY);
                M_F2 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + cdY);
                M_F3 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 2));
                M_F4 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 3));
                M_F5 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 4) + cdY_);
                M_F6 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 5) + cdY_);
                M_F7 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 6) + cdY_);
                M_F8 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 7) + cdY_);
                M_F9 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 8) + (cdY_ * 2));
                M_F10 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), (skillCdY + (cdY * 9) + (cdY_ * 2)) + 5);
                M_F11 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 10) + (cdY_ * 2));
                M_F12 = RColor.Get(BSFGhWnd, skillCdX + (cdX * 2), skillCdY + (cdY * 11) + (cdY_ * 2));

                M_K1 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY);
                M_K2 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + cdY);
                M_K3 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 2));
                M_K4 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 3));
                M_K5 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 4) + cdY_);
                M_K6 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 5) + cdY_);
                M_K7 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 6) + cdY_);
                M_K8 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 7) + cdY_);
                M_K9 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 8) + (cdY_ * 2));
                M_K10 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 9) + (cdY_ * 2));
                M_K11 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 10) + (cdY_ * 2));
                M_K12 = RColor.Get(BSFGhWnd, skillCdX + cdX, skillCdY + (cdY * 11) + (cdY_ * 2));

                M_N1 = RColor.Get(BSFGhWnd, skillCdX, skillCdY);
                M_N2 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + cdY);
                M_N3 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 2));
                M_N4 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 3));
                M_N5 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 4) + cdY_);
                M_N6 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 5) + cdY_);
                M_N7 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 6) + cdY_);
                M_N8 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 7) + cdY_);
                M_N9 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 8) + (cdY_ * 2));
                M_N10 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 9) + (cdY_ * 2));
                M_N11 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 10) + (cdY_ * 2));
                M_N12 = RColor.Get(BSFGhWnd, skillCdX, skillCdY + (cdY * 11) + (cdY_ * 2));

                screenSkill = true;
                checkBoxResetSkill.IsChecked = false;
            }


            monsterCount = 0;
            attackCount = 0;
            massCount = 0;

            comboBox.IsEnabled = false;
            button.IsEnabled = false;
            button.Visibility = System.Windows.Visibility.Hidden;

            buttonOff.IsEnabled = true;
            buttonOff.Visibility = System.Windows.Visibility.Visible;

            int value;
            if (comboBox.SelectedValue != null)
            {
                if (int.TryParse(comboBox.SelectedValue.ToString(), out value))
                {
                    foreach (Process pList in Process.GetProcesses())
                    {
                        if (pList.MainWindowTitle.Contains("EndlessWar"))
                        {
                            if (pList.MainWindowHandle.ToInt32() == Int32.Parse(comboBox.SelectedValue.ToString()))
                            {
                                BSFGhWnd = pList.MainWindowHandle;
                                RECT rect = new RECT();
                                GetClientRect(pList.MainWindowHandle, ref rect);

                                if (rect.Right > 0)
                                {
                                    if ((rect.Right == 1366 && rect.Bottom == 768) || (rect.Right == 1920 && rect.Bottom == 1080))
                                        GWRO.GameResolutionColor = "Green";
                                    else if ((rect.Right == 1768 && rect.Bottom == 992) || (rect.Right == 1600 && rect.Bottom == 900) || (rect.Right == 1280 && rect.Bottom == 720)) 
                                            GWRO.GameResolutionColor = "Blue"; //blue - to do
                                    else   GWRO.GameResolutionColor = "Red";   // NO WORK

                                    GWRO.GameResolution = "Разрешение: " + rect.Right + "x" + rect.Bottom;
                                }
                                IntPtr hWnd2 = pList.MainWindowHandle;
                                SetForegroundWindow(hWnd2);
                                System.Threading.Thread.Sleep(1200);

                               if (!IsPartyAssist())
                                {
                                    findMonster.Start();         ///EXAMPLE - 4
                                }

                                attackMonster.Start();
                                massMonster.Start();
                                selfBuff.Start();
                                buff1Hour.Start();
                                pickUp.Start();
                                if (IsCheckHunt())
                                {
                                     checkHunt.Start();
                                }
                                    selfHeal.Start();
                                cutCreep.Start();
                                tpOnDeath.Start();
                                closeWindow.Start();

                                if (IsHeroTarget())
                                {
                                    fightHero.Start();
                                }

                                if (IsPartyAssist())
                                {
                                    partyAssist.Start();
                                    partyFollow.Start();
                                }                       
                            }
                        }
                    }
                }
            }
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////    STOP
        private void button_ClickOff(object sender, RoutedEventArgs e)
        {
            one = false;
            two = false;
            three = false;
            four = false;

            comboBox.IsEnabled = true;

            button.IsEnabled = true;
            button.Visibility = System.Windows.Visibility.Visible;

            buttonOff.IsEnabled = false;
            buttonOff.Visibility = System.Windows.Visibility.Hidden;

           if (!IsPartyAssist())
            {
                findMonster.Stop();
            }                                                  ///EXAMPLE - 5
            attackMonster.Stop();
            massMonster.Stop();
            selfBuff.Stop();
            buff1Hour.Stop();
            pickUp.Stop();
            if (IsCheckHunt())
            {
                checkHunt.Stop();
            }
            selfHeal.Stop();
            cutCreep.Stop();
            tpOnDeath.Stop();
            closeWindow.Stop();
            if (IsHeroTarget())
            {
                fightHero.Stop();
            }
            if (IsPartyAssist())
            {
                partyAssist.Stop();
                partyFollow.Stop();
            }
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

        //public void partyAssist()
        //{
        //    RECT WindowLa2Rect = new RECT();
        //    Point mp;
        //    GetWindowRect(BSFGhWnd, ref WindowLa2Rect);

        //    int MarginTop = 0;

        //    if (checkBox.IsChecked == true)
        //    {
        //        if (currentGameWindowResolution == GameWindowResolutionRule1)
        //        {
        //            mp = GetMousePosition();

        //            int X = 110 + WindowLa2Rect.Left - Convert.ToInt32(mp.X);
        //            int Y = 380 + MarginTop + WindowLa2Rect.Top - Convert.ToInt32(mp.Y);
        //            //110x380
        //            mouse_event(MOUSEEVENTF_MOVE, X, Y, 0, 0);
        //            System.Threading.Thread.Sleep(100);
        //            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        //            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        //        }
        //    }
        //}



        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GWRO.GameResolution = "";
            GWRO.GameResolutionColor = "White";

            int value;
            if (comboBox.SelectedValue != null && int.TryParse(comboBox.SelectedValue.ToString(), out value))
            {
                if (!turnOnButton)
                {
                    turnOnButton = true;
                    button.IsEnabled = true;
                }

                foreach (Process pList in Process.GetProcesses())
                {
                    if (pList.MainWindowTitle.Contains("EndlessWar"))
                    {
                        if (pList.MainWindowHandle.ToInt32() == Int32.Parse(comboBox.SelectedValue.ToString()))
                        {
                            RECT rect = new RECT();
                            GetClientRect(pList.MainWindowHandle, ref rect);

                            this.Title = BSFGTitle + " " + comboBox.SelectedValue.ToString();

                            currentGameWindowResolution = rect.Right + "x" + rect.Bottom;

                            gameWindowResLabel.Visibility = Visibility.Visible;
                        }
                    }
                }
            }

        }
    }
}

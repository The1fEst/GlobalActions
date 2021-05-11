using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GlobalActions {
  public static class Win32Interop {
    public delegate IntPtr LowLevelKeyboardProc(
      int nCode, IntPtr wParam, IntPtr lParam);

    [Flags]
    public enum InputType {
      Mouse = 0,

      Keyboard = 1,

      Hardware = 2,
    }

    [Flags]
    public enum KeyEventF {
      KeyDown = 0x0000,

      ExtendedKey = 0x0001,

      KeyUp = 0x0002,

      Unicode = 0x0004,

      Scancode = 0x0008,
    }

    [Flags]
    public enum KEYEVENTF : uint {
      KEYEVENTF_EXTENDEDKEY = 1,

      KEYEVENTF_KEYUP = 2,

      KEYEVENTF_UNICODE = 4,

      KEYEVENTF_SCANCODE = 8,
    }

    [Flags]
    public enum MouseEventF {
      Absolute = 0x8000,

      HWheel = 0x01000,

      Move = 0x0001,

      MoveNoCoalesce = 0x2000,

      LeftDown = 0x0002,

      LeftUp = 0x0004,

      RightDown = 0x0008,

      RightUp = 0x0010,

      MiddleDown = 0x0020,

      MiddleUp = 0x0040,

      VirtualDesk = 0x4000,

      Wheel = 0x0800,

      XDown = 0x0080,

      XUp = 0x0100,
    }

    [Flags]
    public enum MOUSEEVENTF : uint {
      MOUSEEVENTF_MOVE = 1,

      MOUSEEVENTF_LEFTDOWN = 2,

      MOUSEEVENTF_LEFTUP = 4,

      MOUSEEVENTF_RIGHTDOWN = 8,

      MOUSEEVENTF_RIGHTUP = 16, // 0x00000010

      MOUSEEVENTF_MIDDLEDOWN = 32, // 0x00000020

      MOUSEEVENTF_MIDDLEUP = 64, // 0x00000040

      MOUSEEVENTF_XDOWN = 128, // 0x00000080

      MOUSEEVENTF_XUP = 256, // 0x00000100

      MOUSEEVENTF_WHEEL = 2048, // 0x00000800

      MOUSEEVENTF_HWHEEL = 4096, // 0x00001000

      MOUSEEVENTF_MOVE_NOCOALESCE = 8192, // 0x00002000

      MOUSEEVENTF_VIRTUALDESK = 16384, // 0x00004000

      MOUSEEVENTF_ABSOLUTE = 32768, // 0x00008000
    }

    [Flags]
    public enum SWP : uint {
      SWP_NOSIZE = 1,

      SWP_NOMOVE = 2,

      SWP_NOZORDER = 4,

      SWP_NOREDRAW = 8,

      SWP_NOACTIVATE = 16, // 0x00000010

      SWP_FRAMECHANGED = 32, // 0x00000020

      SWP_SHOWWINDOW = 64, // 0x00000040

      SWP_HIDEWINDOW = 128, // 0x00000080

      SWP_NOCOPYBITS = 256, // 0x00000100

      SWP_NOOWNERZORDER = 512, // 0x00000200

      SWP_NOSENDCHANGING = 1024, // 0x00000400

      SWP_DRAWFRAME = SWP_FRAMECHANGED, // 0x00000020

      SWP_NOREPOSITION = SWP_NOOWNERZORDER, // 0x00000200

      SWP_DEFERERASE = 8192, // 0x00002000

      SWP_ASYNCWINDOWPOS = 16384, // 0x00004000
    }

    public enum WM : uint {
      WM_NULL = 0,

      WM_CREATE = 1,

      WM_DESTROY = 2,

      WM_MOVE = 3,

      WM_SIZE = 5,

      WM_ACTIVATE = 6,

      WM_SETFOCUS = 7,

      WM_KILLFOCUS = 8,

      WM_ENABLE = 10, // 0x0000000A

      WM_SETREDRAW = 11, // 0x0000000B

      WM_SETTEXT = 12, // 0x0000000C

      WM_GETTEXT = 13, // 0x0000000D

      WM_GETTEXTLENGTH = 14, // 0x0000000E

      WM_PAINT = 15, // 0x0000000F

      WM_CLOSE = 16, // 0x00000010

      WM_QUERYENDSESSION = 17, // 0x00000011

      WM_QUIT = 18, // 0x00000012

      WM_QUERYOPEN = 19, // 0x00000013

      WM_ERASEBKGND = 20, // 0x00000014

      WM_SYSCOLORCHANGE = 21, // 0x00000015

      WM_ENDSESSION = 22, // 0x00000016

      WM_SHOWWINDOW = 24, // 0x00000018

      WM_SETTINGCHANGE = 26, // 0x0000001A

      WM_WININICHANGE = 26, // 0x0000001A

      WM_DEVMODECHANGE = 27, // 0x0000001B

      WM_ACTIVATEAPP = 28, // 0x0000001C

      WM_FONTCHANGE = 29, // 0x0000001D

      WM_TIMECHANGE = 30, // 0x0000001E

      WM_CANCELMODE = 31, // 0x0000001F

      WM_SETCURSOR = 32, // 0x00000020

      WM_MOUSEACTIVATE = 33, // 0x00000021

      WM_CHILDACTIVATE = 34, // 0x00000022

      WM_QUEUESYNC = 35, // 0x00000023

      WM_GETMINMAXINFO = 36, // 0x00000024

      WM_PAINTICON = 38, // 0x00000026

      WM_ICONERASEBKGND = 39, // 0x00000027

      WM_NEXTDLGCTL = 40, // 0x00000028

      WM_SPOOLERSTATUS = 42, // 0x0000002A

      WM_DRAWITEM = 43, // 0x0000002B

      WM_MEASUREITEM = 44, // 0x0000002C

      WM_DELETEITEM = 45, // 0x0000002D

      WM_VKEYTOITEM = 46, // 0x0000002E

      WM_CHARTOITEM = 47, // 0x0000002F

      WM_SETFONT = 48, // 0x00000030

      WM_GETFONT = 49, // 0x00000031

      WM_SETHOTKEY = 50, // 0x00000032

      WM_GETHOTKEY = 51, // 0x00000033

      WM_QUERYDRAGICON = 55, // 0x00000037

      WM_COMPAREITEM = 57, // 0x00000039

      WM_GETOBJECT = 61, // 0x0000003D

      WM_COMPACTING = 65, // 0x00000041

      [Obsolete] WM_COMMNOTIFY = 68, // 0x00000044

      WM_WINDOWPOSCHANGING = 70, // 0x00000046

      WM_WINDOWPOSCHANGED = 71, // 0x00000047

      [Obsolete] WM_POWER = 72, // 0x00000048

      WM_COPYDATA = 74, // 0x0000004A

      WM_CANCELJOURNAL = 75, // 0x0000004B

      WM_NOTIFY = 78, // 0x0000004E

      WM_INPUTLANGCHANGEREQUEST = 80, // 0x00000050

      WM_INPUTLANGCHANGE = 81, // 0x00000051

      WM_TCARD = 82, // 0x00000052

      WM_HELP = 83, // 0x00000053

      WM_USERCHANGED = 84, // 0x00000054

      WM_NOTIFYFORMAT = 85, // 0x00000055

      WM_CONTEXTMENU = 123, // 0x0000007B

      WM_STYLECHANGING = 124, // 0x0000007C

      WM_STYLECHANGED = 125, // 0x0000007D

      WM_DISPLAYCHANGE = 126, // 0x0000007E

      WM_GETICON = 127, // 0x0000007F

      WM_SETICON = 128, // 0x00000080

      WM_NCCREATE = 129, // 0x00000081

      WM_NCDESTROY = 130, // 0x00000082

      WM_NCCALCSIZE = 131, // 0x00000083

      WM_NCHITTEST = 132, // 0x00000084

      WM_NCPAINT = 133, // 0x00000085

      WM_NCACTIVATE = 134, // 0x00000086

      WM_GETDLGCODE = 135, // 0x00000087

      WM_SYNCPAINT = 136, // 0x00000088

      WM_NCMOUSEMOVE = 160, // 0x000000A0

      WM_NCLBUTTONDOWN = 161, // 0x000000A1

      WM_NCLBUTTONUP = 162, // 0x000000A2

      WM_NCLBUTTONDBLCLK = 163, // 0x000000A3

      WM_NCRBUTTONDOWN = 164, // 0x000000A4

      WM_NCRBUTTONUP = 165, // 0x000000A5

      WM_NCRBUTTONDBLCLK = 166, // 0x000000A6

      WM_NCMBUTTONDOWN = 167, // 0x000000A7

      WM_NCMBUTTONUP = 168, // 0x000000A8

      WM_NCMBUTTONDBLCLK = 169, // 0x000000A9

      WM_NCXBUTTONDOWN = 171, // 0x000000AB

      WM_NCXBUTTONUP = 172, // 0x000000AC

      WM_NCXBUTTONDBLCLK = 173, // 0x000000AD

      WM_INPUT_DEVICE_CHANGE = 254, // 0x000000FE

      WM_INPUT = 255, // 0x000000FF

      WM_KEYDOWN = 256, // 0x00000100

      WM_KEYUP = 257, // 0x00000101

      WM_CHAR = 258, // 0x00000102

      WM_DEADCHAR = 259, // 0x00000103

      WM_SYSKEYDOWN = 260, // 0x00000104

      WM_SYSKEYUP = 261, // 0x00000105

      WM_SYSCHAR = 262, // 0x00000106

      WM_SYSDEADCHAR = 263, // 0x00000107

      WM_UNICHAR = 265, // 0x00000109

      WM_IME_STARTCOMPOSITION = 269, // 0x0000010D

      WM_IME_ENDCOMPOSITION = 270, // 0x0000010E

      WM_IME_COMPOSITION = 271, // 0x0000010F

      WM_INITDIALOG = 272, // 0x00000110

      WM_COMMAND = 273, // 0x00000111

      WM_SYSCOMMAND = 274, // 0x00000112

      WM_TIMER = 275, // 0x00000113

      WM_HSCROLL = 276, // 0x00000114

      WM_VSCROLL = 277, // 0x00000115

      WM_INITMENU = 278, // 0x00000116

      WM_INITMENUPOPUP = 279, // 0x00000117

      WM_SYSTIMER = 280, // 0x00000118

      WM_MENUSELECT = 287, // 0x0000011F

      WM_MENUCHAR = 288, // 0x00000120

      WM_ENTERIDLE = 289, // 0x00000121

      WM_MENURBUTTONUP = 290, // 0x00000122

      WM_MENUDRAG = 291, // 0x00000123

      WM_MENUGETOBJECT = 292, // 0x00000124

      WM_UNINITMENUPOPUP = 293, // 0x00000125

      WM_MENUCOMMAND = 294, // 0x00000126

      WM_CHANGEUISTATE = 295, // 0x00000127

      WM_UPDATEUISTATE = 296, // 0x00000128

      WM_QUERYUISTATE = 297, // 0x00000129

      WM_CTLCOLORMSGBOX = 306, // 0x00000132

      WM_CTLCOLOREDIT = 307, // 0x00000133

      WM_CTLCOLORLISTBOX = 308, // 0x00000134

      WM_CTLCOLORBTN = 309, // 0x00000135

      WM_CTLCOLORDLG = 310, // 0x00000136

      WM_CTLCOLORSCROLLBAR = 311, // 0x00000137

      WM_CTLCOLORSTATIC = 312, // 0x00000138

      WM_MOUSEFIRST = 512, // 0x00000200

      WM_MOUSEMOVE = 512, // 0x00000200

      WM_LBUTTONDOWN = 513, // 0x00000201

      WM_LBUTTONUP = 514, // 0x00000202

      WM_LBUTTONDBLCLK = 515, // 0x00000203

      WM_RBUTTONDOWN = 516, // 0x00000204

      WM_RBUTTONUP = 517, // 0x00000205

      WM_RBUTTONDBLCLK = 518, // 0x00000206

      WM_MBUTTONDOWN = 519, // 0x00000207

      WM_MBUTTONUP = 520, // 0x00000208

      WM_MBUTTONDBLCLK = 521, // 0x00000209

      WM_MOUSEWHEEL = 522, // 0x0000020A

      WM_XBUTTONDOWN = 523, // 0x0000020B

      WM_XBUTTONUP = 524, // 0x0000020C

      WM_XBUTTONDBLCLK = 525, // 0x0000020D

      WM_MOUSEHWHEEL = 526, // 0x0000020E

      WM_MOUSELAST = 526, // 0x0000020E

      WM_PARENTNOTIFY = 528, // 0x00000210

      WM_ENTERMENULOOP = 529, // 0x00000211

      WM_EXITMENULOOP = 530, // 0x00000212

      WM_NEXTMENU = 531, // 0x00000213

      WM_SIZING = 532, // 0x00000214

      WM_CAPTURECHANGED = 533, // 0x00000215

      WM_MOVING = 534, // 0x00000216

      WM_POWERBROADCAST = 536, // 0x00000218

      WM_DEVICECHANGE = 537, // 0x00000219

      WM_MDICREATE = 544, // 0x00000220

      WM_MDIDESTROY = 545, // 0x00000221

      WM_MDIACTIVATE = 546, // 0x00000222

      WM_MDIRESTORE = 547, // 0x00000223

      WM_MDINEXT = 548, // 0x00000224

      WM_MDIMAXIMIZE = 549, // 0x00000225

      WM_MDITILE = 550, // 0x00000226

      WM_MDICASCADE = 551, // 0x00000227

      WM_MDIICONARRANGE = 552, // 0x00000228

      WM_MDIGETACTIVE = 553, // 0x00000229

      WM_MDISETMENU = 560, // 0x00000230

      WM_ENTERSIZEMOVE = 561, // 0x00000231

      WM_EXITSIZEMOVE = 562, // 0x00000232

      WM_DROPFILES = 563, // 0x00000233

      WM_MDIREFRESHMENU = 564, // 0x00000234

      WM_IME_SETCONTEXT = 641, // 0x00000281

      WM_IME_NOTIFY = 642, // 0x00000282

      WM_IME_CONTROL = 643, // 0x00000283

      WM_IME_COMPOSITIONFULL = 644, // 0x00000284

      WM_IME_SELECT = 645, // 0x00000285

      WM_IME_CHAR = 646, // 0x00000286

      WM_IME_REQUEST = 648, // 0x00000288

      WM_IME_KEYDOWN = 656, // 0x00000290

      WM_IME_KEYUP = 657, // 0x00000291

      WM_NCMOUSEHOVER = 672, // 0x000002A0

      WM_MOUSEHOVER = 673, // 0x000002A1

      WM_NCMOUSELEAVE = 674, // 0x000002A2

      WM_MOUSELEAVE = 675, // 0x000002A3

      WM_WTSSESSION_CHANGE = 689, // 0x000002B1

      WM_TABLET_FIRST = 704, // 0x000002C0

      WM_TABLET_LAST = 735, // 0x000002DF

      WM_CUT = 768, // 0x00000300

      WM_COPY = 769, // 0x00000301

      WM_PASTE = 770, // 0x00000302

      WM_CLEAR = 771, // 0x00000303

      WM_UNDO = 772, // 0x00000304

      WM_RENDERFORMAT = 773, // 0x00000305

      WM_RENDERALLFORMATS = 774, // 0x00000306

      WM_DESTROYCLIPBOARD = 775, // 0x00000307

      WM_DRAWCLIPBOARD = 776, // 0x00000308

      WM_PAINTCLIPBOARD = 777, // 0x00000309

      WM_VSCROLLCLIPBOARD = 778, // 0x0000030A

      WM_SIZECLIPBOARD = 779, // 0x0000030B

      WM_ASKCBFORMATNAME = 780, // 0x0000030C

      WM_CHANGECBCHAIN = 781, // 0x0000030D

      WM_HSCROLLCLIPBOARD = 782, // 0x0000030E

      WM_QUERYNEWPALETTE = 783, // 0x0000030F

      WM_PALETTEISCHANGING = 784, // 0x00000310

      WM_PALETTECHANGED = 785, // 0x00000311

      WM_HOTKEY = 786, // 0x00000312

      WM_PRINT = 791, // 0x00000317

      WM_PRINTCLIENT = 792, // 0x00000318

      WM_APPCOMMAND = 793, // 0x00000319

      WM_THEMECHANGED = 794, // 0x0000031A

      WM_CLIPBOARDUPDATE = 797, // 0x0000031D

      WM_DWMCOMPOSITIONCHANGED = 798, // 0x0000031E

      WM_DWMNCRENDERINGCHANGED = 799, // 0x0000031F

      WM_DWMCOLORIZATIONCOLORCHANGED = 800, // 0x00000320

      WM_DWMWINDOWMAXIMIZEDCHANGE = 801, // 0x00000321

      WM_GETTITLEBARINFOEX = 831, // 0x0000033F

      WM_HANDHELDFIRST = 856, // 0x00000358

      WM_HANDHELDLAST = 863, // 0x0000035F

      WM_AFXFIRST = 864, // 0x00000360

      WM_AFXLAST = 895, // 0x0000037F

      WM_PENWINFIRST = 896, // 0x00000380

      WM_PENWINLAST = 911, // 0x0000038F

      WM_USER = 1024, // 0x00000400

      WM_CPL_LAUNCH = 5120, // 0x00001400

      WM_CPL_LAUNCHED = 5121, // 0x00001401

      WM_APP = 32768, // 0x00008000
    }

    [Flags]
    public enum WS_EX : uint {
      WS_EX_DLGMODALFRAME = 1,

      WS_EX_NOPARENTNOTIFY = 4,

      WS_EX_TOPMOST = 8,

      WS_EX_ACCEPTFILES = 16, // 0x00000010

      WS_EX_TRANSPARENT = 32, // 0x00000020

      WS_EX_MDICHILD = 64, // 0x00000040

      WS_EX_TOOLWINDOW = 128, // 0x00000080

      WS_EX_WINDOWEDGE = 256, // 0x00000100

      WS_EX_CLIENTEDGE = 512, // 0x00000200

      WS_EX_CONTEXTHELP = 1024, // 0x00000400

      WS_EX_RIGHT = 4096, // 0x00001000

      WS_EX_LEFT = 0,

      WS_EX_RTLREADING = 8192, // 0x00002000

      WS_EX_LTRREADING = 0,

      WS_EX_LEFTSCROLLBAR = 16384, // 0x00004000

      WS_EX_RIGHTSCROLLBAR = 0,

      WS_EX_CONTROLPARENT = 65536, // 0x00010000

      WS_EX_STATICEDGE = 131072, // 0x00020000

      WS_EX_APPWINDOW = 262144, // 0x00040000

      WS_EX_OVERLAPPEDWINDOW = WS_EX_CLIENTEDGE | WS_EX_WINDOWEDGE, // 0x00000300

      WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST, // 0x00000188

      WS_EX_LAYERED = 524288, // 0x00080000

      WS_EX_NOINHERITLAYOUT = 1048576, // 0x00100000

      WS_EX_LAYOUTRTL = 4194304, // 0x00400000

      WS_EX_COMPOSITED = 33554432, // 0x02000000

      WS_EX_NOACTIVATE = 134217728, // 0x08000000
    }

    public const int WH_KEYBOARD_LL = 13;

    [DllImport("User32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifer, int vlc);

    [DllImport("User32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetMessage(
      out MSG lpMsg,
      [In] IntPtr hWnd,
      uint wMsgFilterMin,
      uint wMsgFilterMax);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow([In] string lpClassName, [In] string lpWindowName);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern void mouse_event(
      MOUSEEVENTF dwFlags,
      uint dx,
      uint dy,
      uint dwData,
      uint dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern void keybd_event(
      byte bVk,
      byte bScan,
      KEYEVENTF dwFlags,
      uint dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong([In] IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern int SetWindowLong([In] IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(
      [In] IntPtr hWnd,
      [In] IntPtr hWndInsertAfter,
      int X,
      int Y,
      int cx,
      int cy,
      SWP uFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetWindowsHookEx(int idHook,
                                                 LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                                               IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    private static extern IntPtr GetMessageExtraInfo();

    [DllImport("user32.dll")]
    public static extern uint MapVirtualKeyA(uint uCode, uint uMapType);

    private static void key_event(params (ushort key, bool isDown)[] inputParams) {
      var inputs = inputParams.Select(x => new Input {
        type = (int) InputType.Keyboard,
        u = new InputUnion {
          ki = new KeyboardInput {
            wVk = 0,
            wScan = x.key,
            dwFlags = x.isDown
              ? (uint) (KeyEventF.KeyDown | KeyEventF.Scancode)
              : (uint) (KeyEventF.KeyUp | KeyEventF.Scancode),
            dwExtraInfo = GetMessageExtraInfo(),
          },
        },
      }).ToArray();

      SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
    }

    public static void key_down(ushort kbdScan) {
      key_event((kbdScan, true));
    }

    public static void key_up(ushort kbdScan) {
      key_event((kbdScan, false));
    }

    public static void key_press(ushort kbdScan) {
      key_event((kbdScan, true), (kbdScan, false));
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput {
      public ushort wVk;

      public ushort wScan;

      public uint dwFlags;

      public uint time;

      public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput {
      public int dx;

      public int dy;

      public uint mouseData;

      public uint dwFlags;

      public uint time;

      public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput {
      public uint uMsg;

      public ushort wParamL;

      public ushort wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion {
      [FieldOffset(0)] public MouseInput mi;
      [FieldOffset(0)] public KeyboardInput ki;
      [FieldOffset(0)] public HardwareInput hi;
    }

    public struct Input {
      public int type;

      public InputUnion u;
    }

    public struct POINT {
      public int x;

      public int y;
    }

    public struct MSG {
      public IntPtr hwnd;

      public WM message;

      public uint wParam;

      public int lParam;

      public uint time;

      public POINT pt;
    }
  }
}

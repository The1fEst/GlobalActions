using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GlobalActions {
  public static class Win32Interop {
    public delegate IntPtr LowLevelKeyboardProc(
      int nCode, IntPtr wParam, IntPtr lParam);

    [Flags]
    public enum KeyEventF : uint {
      KeyDown = 0,

      KeyUp = 2,

      Scancode = 8,
    }

    [Flags]
    public enum MouseEventF : uint {
      Move = 1,

      LeftDown = 2,

      LeftUp = 4,

      RightDown = 8,

      RightUp = 16, // 0x00000010

      MiddleDown = 32, // 0x00000020

      MiddleUp = 64, // 0x00000040

      XDown = 128, // 0x00000080

      Xup = 256, // 0x00000100

      Wheel = 2048, // 0x00000800

      HWheel = 4096, // 0x00001000

      MoveNoCoalesce = 8192, // 0x00002000

      VirtualDesk = 16384, // 0x00004000

      Absolute = 32768, // 0x00008000
    }

    public enum Wm : uint {
      Keydown = 256, // 0x00000100

      Keyup = 257, // 0x00000101

      SysKeyDown = 260, // 0x00000104

      SysKeyUp = 261, // 0x00000105
    }

    public const int WhKeyboardLl = 13;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetMessage(
      out Msg lpMsg,
      [In]
      IntPtr hWnd,
      uint wMsgFilterMin,
      uint wMsgFilterMax);

    [DllImport("user32.dll")]
    public static extern void mouse_event(
      MouseEventF dwFlags,
      uint dx,
      uint dy,
      uint dwData,
      uint dwExtraInfo);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetCursorPos(int x, int y);

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
    public static extern ushort MapVirtualKeyA(uint uCode, uint uMapType);

    private static void key_event(params (ushort key, bool isDown)[] inputParams) {
      var inputs = inputParams.Select(x => new Input {
        Type = 1,
        U = new InputUnion {
          ki = new KeyboardInput {
            wVk = 0,
            wScan = MapVirtualKeyA(x.key, 0),
            dwFlags = x.isDown
              ? (uint) (KeyEventF.KeyDown | KeyEventF.Scancode)
              : (uint) (KeyEventF.KeyUp | KeyEventF.Scancode),
            dwExtraInfo = GetMessageExtraInfo(),
          },
        },
      }).ToArray();

      SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
    }

    public static void key_down(ushort key) {
      key_event((key, true));
    }

    public static void key_up(ushort key) {
      key_event((key, false));
    }

    public static void key_press(ushort key) {
      key_event((key, true), (key, false));
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput {
      public ushort wVk;

      public ushort wScan;

      public uint dwFlags;

      public uint time;

      public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion {
      [FieldOffset(0)]
      public MouseInput mi;
      [FieldOffset(0)]
      public KeyboardInput ki;
      [FieldOffset(0)]
      public HardwareInput hi;
    }

    public struct Input {
      public int Type;

      public InputUnion U;
    }

    public struct Point {
      public int X;

      public int Y;
    }

    public struct Msg {
      public IntPtr Hwnd;

      public Wm Message;

      public uint WParam;

      public int LParam;

      public uint Time;

      public Point Pt;
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
  }
}

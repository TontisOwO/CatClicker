using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class InfernalMagic : MonoBehaviour
{

    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd,  int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32")]
    static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);
    private struct Margins
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);

    const int Gwl_ExStyle = -20;

    const uint Ws_Ex_Layered = 0x00080000;
    const uint Ws_Ex_Transparent = 0x00000020;

    static readonly IntPtr HWnd_Topmost = new IntPtr(-1);

    const uint Lwa_Colorkey = 0x00000001;

    public void Start()
    {
#if !UNITY_EDITOR
        IntPtr hWnd = GetActiveWindow();

        Margins margins = new Margins { Left = -1};
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowLong(hWnd, Gwl_ExStyle, Ws_Ex_Layered);
        SetLayeredWindowAttributes(hWnd, 0,0, Lwa_Colorkey);
        
        SetWindowPos(hWnd, HWnd_Topmost, 0 ,0 ,0 ,0 ,0);
#endif
    }
}

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class InfernalMagic : MonoBehaviour
{

    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    private struct Margins
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cxTopHeight;
        public int cxBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);

    private void Start()
    {
        MessageBox(new IntPtr(0), "Hello World", "Hello Windows", 0);

        IntPtr hWnd = GetActiveWindow();

        Margins margins = new Margins { cxLeftWidth = -1};
        DwmExtendFrameIntoClientArea (hWnd, ref margins);
    }

}

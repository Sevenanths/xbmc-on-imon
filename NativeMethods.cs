// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.NativeMethods
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using System;
using System.Runtime.InteropServices;

namespace iMon.XBMC
{
  internal class NativeMethods
  {
    public static readonly int WM_SHOWME = NativeMethods.RegisterWindowMessage(nameof (WM_SHOWME));
    public const int HWND_BROADCAST = 65535;

    [DllImport("user32")]
    public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    [DllImport("user32")]
    public static extern int RegisterWindowMessage(string message);
  }
}

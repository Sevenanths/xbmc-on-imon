// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.Program
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using System;
using System.Threading;
using System.Windows.Forms;

namespace iMon.XBMC
{
  internal static class Program
  {
    private static Mutex mutex = new Mutex(true, Application.ProductName);

    [STAThread]
    private static void Main()
    {
      try
      {
        if (Program.mutex.WaitOne(TimeSpan.Zero, true))
        {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run((Form) new iMon.XBMC.XBMC());
          Program.mutex.ReleaseMutex();
        }
        else
          NativeMethods.PostMessage((IntPtr) ((int) ushort.MaxValue), NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
      }
      catch (Exception ex)
      {
        Logging.Error("Unhandled exception", ex);
        int num = (int) MessageBox.Show("An unhandled exception has occured." + Environment.NewLine + "Please check the debug/error log for more details", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
  }
}

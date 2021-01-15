// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.Logging
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using iMon.XBMC.Properties;
using System;
using System.IO;
using System.Text;

namespace iMon.XBMC
{
  internal static class Logging
  {
    internal const string ErrorLog = "error.log";
    internal const string DebugLog = "debug.log";
    internal const string OldLog = ".old";

    public static void Error(string message)
    {
      Logging.Error("GUI", message, (Exception) null);
    }

    public static void Error(string area, string message)
    {
      Logging.Error(area, message, (Exception) null);
    }

    public static void Error(string message, Exception exception)
    {
      Logging.Error("GUI", message, exception);
    }

    public static void Error(string area, string message, Exception exception)
    {
      if (Settings.Default.GeneralDebugEnable)
      {
        Logging.Log(area, "ERROR " + message, exception);
      }
      else
      {
        try
        {
          using (StreamWriter streamWriter = new StreamWriter("error.log", true, Encoding.UTF8))
          {
            streamWriter.WriteLine("{0} [{1}] {2}", (object) DateTime.Now, (object) area, (object) message);
            if (exception == null)
              return;
            streamWriter.WriteLine("    {0}: {1}" + Environment.NewLine + "         {2}", (object) exception.GetType().Name, (object) exception.Message, (object) exception.StackTrace);
          }
        }
        catch (Exception ex)
        {
        }
      }
    }

    public static void Log(string message)
    {
      Logging.Log("GUI", message, (Exception) null);
    }

    public static void Log(string area, string message)
    {
      Logging.Log(area, message, (Exception) null);
    }

    public static void Log(string message, Exception exception)
    {
      Logging.Log("GUI", message, exception);
    }

    public static void Log(string area, string message, Exception exception)
    {
      if (!Settings.Default.GeneralDebugEnable)
        return;
      try
      {
        using (StreamWriter streamWriter = new StreamWriter("debug.log", true, Encoding.UTF8))
        {
          streamWriter.WriteLine("{0} [{1}] {2}", (object) DateTime.Now, (object) area, (object) message);
          if (exception == null)
            return;
          streamWriter.WriteLine("    {0}: {1}" + Environment.NewLine + "         {2}", (object) exception.GetType().Name, (object) exception.Message, (object) exception.StackTrace);
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.DisplayHandler
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using iMon.DisplayApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace iMon.XBMC
{
  internal class DisplayHandler : BackgroundWorker
  {
    private bool vfd = DisplayHandler.DEBUG;
    private object displayLock = new object();
    private object queueLock = new object();
    private static bool DEBUG;
    private Semaphore semReady;
    private Semaphore semWork;
    private iMonWrapperApi imon;
    private List<DisplayHandler.Text> queue;
    private int position;

    public DisplayHandler(iMonWrapperApi imon)
    {
      if (imon == null)
        throw new ArgumentNullException(nameof (imon));
      this.imon = imon;
      this.imon.StateChanged += new EventHandler<iMonStateChangedEventArgs>(this.stateChanged);
      this.queue = new List<DisplayHandler.Text>();
      this.WorkerReportsProgress = false;
      this.WorkerSupportsCancellation = true;
      this.semReady = new Semaphore(0, 1);
      this.semWork = new Semaphore(0, 1);
      this.semReady.Release();
    }

    protected override void OnDoWork(DoWorkEventArgs e)
    {
      while (!this.CancellationPending)
      {
        this.semReady.WaitOne();
        Logging.Log("Display Handler", "Start working");
        if (this.queue.Count > 0)
        {
          this.display(this.queue[0]);
          if (this.queue.Count > 1)
            this.position = 1;
        }
        while (!this.CancellationPending && this.vfd)
        {
          this.semWork.WaitOne();
          lock (this.queueLock)
          {
            if (this.position >= this.queue.Count)
              this.position = 0;
            this.display(this.queue[this.position]);
            if (this.queue.Count > this.position + 1)
              ++this.position;
          }
        }
        Logging.Log("Display Handler", "Stop working");
      }
      Logging.Log("Display Handler", "Cancelled");
    }

    public void SetText(string text)
    {
      this.SetText(text, text, string.Empty, 0);
    }

    public void SetText(string text, int delay)
    {
      this.SetText(text, text, string.Empty, delay);
    }

    public void SetText(string lcd, string vfdUpper, string vfdLower)
    {
      this.SetText(lcd, vfdUpper, vfdLower, 0);
    }

    public void SetText(string lcd, string vfdUpper, string vfdLower, int delay)
    {
      lock (this.queueLock)
      {
        Logging.Log("Display Handler", "Setting text to \"" + lcd + "\"");
        this.queue.Clear();
        this.queue.Add(new DisplayHandler.Text(lcd, vfdUpper, vfdLower, delay));
        this.position = 0;
        this.update();
      }
    }

    public void AddText(string text)
    {
      this.AddText(text, text, string.Empty, 0);
    }

    public void AddText(string text, int delay)
    {
      this.AddText(text, text, string.Empty, delay);
    }

    public void AddText(string lcd, string vfdUpper, string vfdLower)
    {
      this.AddText(lcd, vfdUpper, vfdLower, 0);
    }

    public void AddText(string lcd, string vfdUpper, string vfdLower, int delay)
    {
      lock (this.queueLock)
      {
        Logging.Log("Display Handler", "Adding text \"" + lcd + "\" to the queue");
        this.queue.Add(new DisplayHandler.Text(lcd, vfdUpper, vfdLower, delay));
        if (this.queue.Count != 1)
          return;
        this.update();
      }
    }

    public void SetProgress(TimeSpan position, TimeSpan total, string text, char icon)
    {
      this.SetText(text, text, (icon).ToString() + ((int) position.TotalHours).ToString() + ":" + position.Minutes.ToString().PadLeft(2, '0') + ":" + position.Seconds.ToString().PadLeft(2, '0') + "/" + ((int) total.TotalHours).ToString() + ":" + total.Minutes.ToString().PadLeft(2, '0') + ":" + total.Seconds.ToString().PadLeft(2, '0'));
    }

    private void update()
    {
      try
      {
        this.semWork.Release();
      }
      catch (SemaphoreFullException ex)
      {
      }
    }

    private void stateChanged(object sender, iMonStateChangedEventArgs e)
    {
      lock (this.displayLock)
      {
        if (e.IsInitialized || DisplayHandler.DEBUG)
        {
          this.vfd = true;
          this.semReady.Release();
        }
        else
        {
          this.update();
          this.vfd = false;
        }
      }
    }

    private void display(DisplayHandler.Text text)
    {
      this.display(text, false);
    }

    private void display(DisplayHandler.Text text, bool noscroll)
    {
      lock (this.displayLock)
      {
        Logging.Log("Display Handler", "VFD.SetText: " + text.VfdUpper + "; " + text.VfdLower);
        if (this.vfd)
        {
          if (!DisplayHandler.DEBUG)
          {
            this.imon.VFD.SetText(this.centerText(text.VfdUpper), this.centerText(text.VfdLower));
          }
          else
          {
            Console.WriteLine("----------------");
            Console.WriteLine(this.centerText(text.VfdUpper));
            Console.WriteLine(this.centerText(text.VfdLower));
            Console.WriteLine("----------------");
          }
        }
        if (text.Delay <= 0)
          return;
        Logging.Log("Display Handler", "Showing text for " + (object) text.Delay + "ms");
        Thread.Sleep(text.Delay);
      }
    }

    private string centerText(string text)
    {
      text = text.Substring(0, Math.Min(text.Length, 16));
      int length = text.Length;
      if (length < 15)
        text = text.PadLeft(length + (16 - length) / 2, ' ');
      return text;
    }

    private struct Text
    {
      private string lcd;
      private string vfdUpper;
      private string vfdLower;
      private int delay;

      public string Lcd
      {
        get
        {
          return this.lcd;
        }
      }

      public string VfdUpper
      {
        get
        {
          return this.vfdUpper;
        }
      }

      public string VfdLower
      {
        get
        {
          return this.vfdLower;
        }
      }

      public int Delay
      {
        get
        {
          return this.delay;
        }
      }

      public Text(string lcd, string vfdUpper, string vfdLower)
      {
        this.lcd = lcd != null ? lcd : string.Empty;
        this.vfdUpper = vfdUpper != null ? vfdUpper : string.Empty;
        this.vfdLower = vfdLower != null ? vfdLower : string.Empty;
        this.delay = 0;
      }

      public Text(string lcd, string vfdUpper, string vfdLower, int delay)
      {
        this = new DisplayHandler.Text(lcd, vfdUpper, vfdLower);
        if (this.delay < 0)
          return;
        this.delay = delay;
      }
    }
  }
}

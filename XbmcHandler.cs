// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.XbmcHandler
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using iMon.XBMC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using XBMC.JsonRpc;

namespace iMon.XBMC
{
  internal class XbmcHandler : BackgroundWorker
  {
    private static object connectionLocking = new object();
    private int ControlModeUnchangedDelay = 60000;
    private int ProgressBarTimeSwitchDelay = 5000;
    private DateTime lastUpdatePlayingDisplayMode = DateTime.Now;
    private int numBlock = 16;
    private char[] progressBarChars1 = new char[6]
    {
      '¥',
      '\x0010',
      '\x0011',
      '\x0012',
      '\x0013',
      '\x0014'
    };
    private char[] progressBarChars2 = new char[8]
    {
      '\b',
      '\x0001',
      '\x0002',
      '\x0003',
      '\x0004',
      '\x0005',
      '\x0006',
      '\a'
    };
    private char spaceChar = ' ';
    private char playIconChar = '\x001D';
    private char pauseIconChar = '\x0097';
    private DateTime lastUpdate = DateTime.Now;
    private const int SystemTimeUpdateInterval = 1000;
    private int currentPlayingDisplayMode;
    private char[] progressBarChars;
    private int numStateBlock;
    private Semaphore semReady;
    private Semaphore semWork;
    private bool connected;
    private XbmcJsonRpcConnection xbmc;
    private DisplayHandler display;
    private XbmcMediaPlayer player;
    private XbmcPlayable currentlyPlaying;
    private XbmcHandler.PlayerState playerState;
    private TimeSpan length;
    private TimeSpan position;
    private double percent;
    private System.Timers.Timer progressTimer;
    private System.Timers.Timer scrollingTimer;
    private bool showSystemTime;
    private System.Timers.Timer systemTimeTimer;
    private XbmcHandler.ControlState controlModeState;
    private System.Timers.Timer controlModeTimer;
    private int controlModeUnchanged;
    private string currentLabel;
    private int currentLabelPos;
    private bool startPlaying;
    private bool isIdle;

    public XbmcHandler(XbmcJsonRpcConnection xbmc, DisplayHandler display)
    {
      if (xbmc == null)
        throw new ArgumentNullException(nameof (xbmc));
      if (display == null)
        throw new ArgumentNullException(nameof (display));
      this.xbmc = xbmc;
      this.display = display;
      this.xbmc.Connected += new EventHandler(this.xbmcConnected);
      this.xbmc.Aborted += new EventHandler(this.xbmcAborted);
      this.xbmc.Player.PlaybackStarted += new EventHandler<XbmcPlayerPlaybackChangedEventArgs>(this.xbmcPlaybackStarted);
      this.xbmc.Player.PlaybackResumed += new EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs>(this.xbmcPlaybackResumed);
      this.xbmc.Player.PlaybackPaused += new EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs>(this.xbmcPlaybackPaused);
      this.xbmc.Player.PlaybackStopped += new EventHandler(this.xbmcPlaybackStopped);
      this.xbmc.Player.PlaybackEnded += new EventHandler(this.xbmcPlaybackEnded);
      this.progressTimer = new System.Timers.Timer();
      this.progressTimer.Interval = (double) Math.Max(Settings.Default.ImonLcdScrollingDelay, 50);
      this.progressTimer.Elapsed += new ElapsedEventHandler(this.progressTimerUpdate);
      this.progressTimer.AutoReset = true;
      this.scrollingTimer = new System.Timers.Timer();
      this.scrollingTimer.Interval = (double) Math.Max(Settings.Default.ImonLcdScrollingDelay, 50);
      this.scrollingTimer.Elapsed += new ElapsedEventHandler(this.progressNavigateUpdate);
      this.scrollingTimer.AutoReset = true;
      this.systemTimeTimer = new System.Timers.Timer();
      this.systemTimeTimer.Interval = 1000.0;
      this.systemTimeTimer.Elapsed += new ElapsedEventHandler(this.systemTimeTimerUpdate);
      this.systemTimeTimer.AutoReset = true;
      this.WorkerReportsProgress = false;
      this.WorkerSupportsCancellation = true;
      this.controlModeState = new XbmcHandler.ControlState();
      this.controlModeTimer = new System.Timers.Timer();
      this.controlModeTimer.Interval = (double) Math.Max(Settings.Default.XbmcControlModeUpdateInterval, 50);
      this.controlModeTimer.Elapsed += new ElapsedEventHandler(this.controlModeTimerUpdate);
      this.controlModeTimer.AutoReset = true;
      this.currentLabel = (string) null;
      this.currentLabelPos = 0;
      this.semReady = new Semaphore(0, 1);
      this.semWork = new Semaphore(0, 1);
    }

    public void Update()
    {
      Logging.Log("XBMC Handler", nameof (Update));
      if (!this.connected)
        return;
      this.showSystemTime = Settings.Default.XbmcIdleTimeEnable;
      this.scrollingTimer.Interval = (double) Math.Max(Settings.Default.ImonLcdScrollingDelay, 50);
      this.progressTimer.Interval = (double) Math.Max(Settings.Default.ImonLcdScrollingDelay, 50);
      this.ControlModeUnchangedDelay = Math.Max(Settings.Default.XbmcControlModeIdleDelay, 1) * 60000;
      this.ProgressBarTimeSwitchDelay = Math.Max(Settings.Default.XbmcAlternateProgressDisplayDelay, 1) * 1000;
      this.progressBarChars = Settings.Default.XbmcProgressbarStyle == 1 ? this.progressBarChars1 : this.progressBarChars2;
      this.numStateBlock = Settings.Default.XbmcProgressbarStyle == 1 ? 5 : 7;
      if (this.player != null)
      {
        this.GetLabel();
        this.systemTimeTimer.Stop();
        this.controlModeTimer.Stop();
        this.progressTimer.Start();
      }
      else if (!Settings.Default.XbmcControlModeEnable)
      {
        if (Settings.Default.XbmcIdleStaticTextEnable || Settings.Default.XbmcIdleTimeEnable)
          this.systemTimeTimer.Start();
        this.scrollingTimer.Stop();
        this.progressTimer.Stop();
      }
      else
        this.progressTimer.Stop();
      this.controlModeState.Window = (string) null;
      this.controlModeState.Control = (string) null;
      this.controlModeState.Label1 = (string) null;
      this.controlModeState.Pos1 = 0;
      this.controlModeState.Label2 = (string) null;
      this.controlModeState.Pos2 = 0;
      if (Settings.Default.XbmcControlModeEnable && !this.controlModeTimer.Enabled)
      {
        this.controlModeTimer.Interval = (double) Math.Max(Settings.Default.XbmcControlModeUpdateInterval, 50);
        this.controlModeTimer.Start();
      }
      else
      {
        if (Settings.Default.XbmcControlModeEnable || !this.controlModeTimer.Enabled)
          return;
        this.controlModeTimer.Stop();
      }
    }

    protected override void OnDoWork(DoWorkEventArgs e)
    {
      while (!this.CancellationPending)
      {
        this.semReady.WaitOne();
        Logging.Log("XBMC Handler", "Start working");
        if (Settings.Default.XbmcControlModeEnable)
          this.controlModeTimer.Start();
        while (!this.CancellationPending && this.connected)
        {
          this.semWork.WaitOne();
          this.Update();
        }
        Logging.Log("XBMC Handler", "Stop working");
      }
      Logging.Log("XBMC Handler", "Cancelled");
      this.xbmc.Player.PlaybackStarted -= new EventHandler<XbmcPlayerPlaybackChangedEventArgs>(this.xbmcPlaybackStarted);
      this.xbmc.Player.PlaybackPaused -= new EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs>(this.xbmcPlaybackPaused);
      this.xbmc.Player.PlaybackStopped -= new EventHandler(this.xbmcPlaybackStopped);
      this.xbmc.Player.PlaybackResumed -= new EventHandler<XbmcPlayerPlaybackPositionChangedEventArgs>(this.xbmcPlaybackResumed);
      this.xbmc.Player.PlaybackEnded -= new EventHandler(this.xbmcPlaybackEnded);
    }

    private void xbmcConnected(object sender, EventArgs e)
    {
      lock (XbmcHandler.connectionLocking)
      {
        this.connected = true;
        bool video;
        bool audio;
        bool picture;
        int id;
        this.xbmc.Player.GetActivePlayers(out video, out audio, out picture, out id);
        if (video)
          this.player = (XbmcMediaPlayer) this.xbmc.Player.Video;
        else if (audio)
          this.player = (XbmcMediaPlayer) this.xbmc.Player.Audio;
        else if (picture)
          this.player = (XbmcMediaPlayer) this.xbmc.Player.Pictures;
      }
      this.semReady.Release();
      if (this.player != null)
      {
        this.playerState = this.player.Speed == 0 ? XbmcHandler.PlayerState.Paused : XbmcHandler.PlayerState.Playing;
        this.Update();
      }
      else
        this.systemTimeTimer.Start();
      this.update();
    }

    private void xbmcAborted(object sender, EventArgs e)
    {
      lock (XbmcHandler.connectionLocking)
        this.connected = false;
      this.scrollingTimer.Stop();
      this.systemTimeTimer.Stop();
      this.controlModeTimer.Stop();
      this.playbackStopped();
      this.update();
    }

    private void GetLabel()
    {
      this.updateCurrentlyPlaying();
      this.currentLabelPos = 0;
    }

    private void xbmcPlaybackStarted(object sender, XbmcPlayerPlaybackChangedEventArgs e)
    {
      if (e == null || e.Player == null)
      {
        this.startPlaying = false;
      }
      else
      {
        Logging.Log("XBMC Handler", "Playback started");
        this.startPlaying = true;
        this.isIdle = false;
        this.scrollingTimer.Stop();
        this.player = e.Player;
        this.playerState = XbmcHandler.PlayerState.Playing;
        this.GetLabel();
        this.currentPlayingDisplayMode = 0;
        this.lastUpdatePlayingDisplayMode = DateTime.Now;
        this.progressTimer.Start();
        this.update();
      }
    }

    private void xbmcPlaybackPaused(object sender, XbmcPlayerPlaybackPositionChangedEventArgs e)
    {
      if (e == null || e.Player == null)
        return;
      Logging.Log("XBMC Handler", "Playback paused");
      this.controlModeTimer.Stop();
      this.playerState = XbmcHandler.PlayerState.Paused;
      this.updateProgress();
      this.update();
    }

    private void xbmcPlaybackResumed(object sender, XbmcPlayerPlaybackPositionChangedEventArgs e)
    {
      if (e == null || e.Player == null)
        return;
      Logging.Log("XBMC Handler", "Playback resumed");
      this.xbmcPlaybackStarted(sender, (XbmcPlayerPlaybackChangedEventArgs) e);
    }

    private void xbmcPlaybackStopped(object sender, EventArgs e)
    {
      Logging.Log("XBMC Handler", "Playback stopped");
      this.playbackStopped();
      this.update();
    }

    private void xbmcPlaybackEnded(object sender, EventArgs e)
    {
      Logging.Log("XBMC Handler", "Playback ended");
      this.xbmcPlaybackStopped(sender, e);
    }

    private void progressTimerUpdate(object sender, ElapsedEventArgs e)
    {
      if (!this.connected)
        return;
      if (DateTime.Now.Subtract(this.lastUpdate).TotalMilliseconds > 1000.0)
      {
        this.lastUpdate = DateTime.Now;
        IDictionary<string, string> infoLabels = this.xbmc.System.GetInfoLabels("Player.Time", "Player.Duration");
        string s1 = infoLabels["Player.Time"];
        if (string.IsNullOrEmpty(s1))
          s1 = "00:00:00";
        else if (s1.Length < 6)
          s1 = "00:" + s1;
        string s2 = infoLabels["Player.Duration"];
        if (string.IsNullOrEmpty(s2))
          s2 = "00:00:00";
        else if (s2.Length < 6)
          s2 = "00:" + s2;
        this.position = TimeSpan.Parse(s1);
        this.length = TimeSpan.Parse(s2);
        this.percent = Math.Floor(this.position.TotalMilliseconds / this.length.TotalMilliseconds * 100.0);
      }
      this.updateProgress();
    }

    private void progressNavigateUpdate(object sender, ElapsedEventArgs e)
    {
      if (!this.connected || this.startPlaying)
        return;
      string vfdUpper = this.controlModeState.Label1;
      string str1 = this.controlModeState.Label2;
      if (vfdUpper != null && vfdUpper.Length > 16)
      {
        int length1 = vfdUpper.Length;
        string str2 = vfdUpper + " - " + vfdUpper;
        int length2 = str2.Length;
        if (length2 - this.controlModeState.Pos1 < length1)
          this.controlModeState.Pos1 = -1;
        vfdUpper = str2.Substring(Math.Min(Math.Max(this.controlModeState.Pos1++, 0), length2 - 16));
      }
      if (str1 != null && str1.Length > 16)
      {
        int length1 = str1.Length;
        string str2 = str1 + " - " + str1;
        int length2 = str2.Length;
        if (length2 - this.controlModeState.Pos2 < length1)
          this.controlModeState.Pos2 = -1;
        str1 = str2.Substring(Math.Min(Math.Max(this.controlModeState.Pos2++, 0), length2 - 16));
      }
      this.display.SetText(str1, vfdUpper, str1);
    }

    private void systemTimeTimerUpdate(object sender, ElapsedEventArgs e)
    {
      if (!this.connected || Settings.Default.XbmcControlModeEnable && (!Settings.Default.XbmcControlModeIdleEnable || !this.isIdle))
        return;
      DateTime now = DateTime.Now;
      string str = Settings.Default.XbmcIdleStaticTextEnable ? Settings.Default.XbmcIdleStaticText : (string) null;
      if (this.showSystemTime)
      {
        if (Settings.Default.XbmcIdleTimeShowSeconds)
        {
          if (str != null)
            this.display.SetText(str, str, now.ToLongTimeString());
          else
            this.display.SetText(now.ToLongTimeString());
        }
        else if (str != null)
          this.display.SetText(str, str, now.ToShortTimeString());
        else
          this.display.SetText(now.ToShortTimeString());
      }
      else
      {
        if (!Settings.Default.XbmcIdleStaticTextEnable)
          return;
        this.display.SetText(str);
      }
    }

    private void controlModeTimerUpdate(object sender, ElapsedEventArgs e)
    {
      this.controlModeTimer.Stop();
      lock (XbmcHandler.connectionLocking)
      {
        if (Settings.Default.XbmcControlModeEnable)
        {
          if (this.connected)
            goto label_5;
        }
        this.controlModeTimer.Stop();
        this.scrollingTimer.Stop();
        return;
      }
label_5:
      if (this.controlModeUnchanged >= 0)
        this.controlModeUnchanged += Convert.ToInt32(this.controlModeTimer.Interval);
      if (this.playerState == XbmcHandler.PlayerState.Stopped)
      {
        IDictionary<string, string> infoLabels = this.xbmc.System.GetInfoLabels("System.CurrentWindow", "System.CurrentControl");
        if (infoLabels.Count != 2 || string.IsNullOrEmpty(infoLabels["System.CurrentControl"]))
        {
          this.checkControlModeUnchanged();
        }
        else
        {
          string str1 = this.replaceChars(!string.IsNullOrEmpty(infoLabels["System.CurrentWindow"]) ? infoLabels["System.CurrentWindow"] : "XBMC");
          string vfdLower = this.replaceChars(infoLabels["System.CurrentControl"]);
          if (vfdLower.StartsWith("[") && vfdLower.EndsWith("]"))
          {
            string str2 = vfdLower.Remove(0, 1);
            vfdLower = str2.Remove(str2.Length - 1, 1);
          }
          if (this.controlModeState.Window == str1 && this.controlModeState.Control == vfdLower)
          {
            this.checkControlModeUnchanged();
          }
          else
          {
            this.controlModeState.Label1 = (string) null;
            this.controlModeState.Label2 = (string) null;
            this.controlModeState.Pos1 = 0;
            this.controlModeState.Pos2 = 0;
            if (!this.startPlaying)
            {
              this.progressTimer.Stop();
              this.display.SetText(str1, str1, vfdLower);
              this.controlModeUnchanged = 0;
              this.isIdle = false;
            }
            else
              this.controlModeUnchanged = this.ControlModeUnchangedDelay;
            this.controlModeState.Window = str1;
            this.controlModeState.Control = vfdLower;
            if (!this.startPlaying && (this.controlModeState.Control != null && this.controlModeState.Control.Length > 16 || this.controlModeState.Window != null && this.controlModeState.Window.Length > 16))
            {
              this.controlModeState.Label1 = this.controlModeState.Window;
              this.controlModeState.Label2 = this.controlModeState.Control;
              this.scrollingTimer.Start();
            }
            else
            {
              this.controlModeState.Label1 = (string) null;
              this.controlModeState.Label2 = (string) null;
              this.controlModeState.Pos1 = 0;
              this.controlModeState.Pos2 = 0;
              this.scrollingTimer.Stop();
            }
          }
        }
      }
      this.controlModeTimer.Start();
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

    private void updateProgress()
    {
      string str1 = this.currentLabel;
      if (str1 != null && str1.Length > 16)
      {
        int length1 = str1.Length;
        string str2 = str1 + " - " + str1;
        int length2 = str2.Length;
        if (length2 - this.currentLabelPos < length1)
          this.currentLabelPos = 0;
        str1 = str2.Substring(Math.Min(Math.Max(this.currentLabelPos++, 0), length2 - 16));
      }
      if (Settings.Default.XbmcPlaybackProgress == 2)
      {
        if (DateTime.Now.Subtract(this.lastUpdatePlayingDisplayMode).TotalMilliseconds > (double) this.ProgressBarTimeSwitchDelay)
        {
          this.lastUpdatePlayingDisplayMode = DateTime.Now;
          this.currentPlayingDisplayMode = this.currentPlayingDisplayMode == 0 ? 1 : 0;
        }
      }
      else
        this.currentPlayingDisplayMode = Settings.Default.XbmcPlaybackProgress;
      if (this.currentPlayingDisplayMode == 1)
      {
        double num = this.percent > 0.0 ? Math.Floor((double) this.numBlock * (double) this.numStateBlock / 100.0 * this.percent) : 0.0;
        string vfdLower = (new string(this.progressBarChars[this.numStateBlock], Convert.ToInt32(Math.Floor(num / (double) this.numStateBlock))) + (object) this.progressBarChars[Convert.ToInt32(num) % this.numStateBlock]).PadRight(this.numBlock, this.progressBarChars[0]);
        this.display.SetText(str1, str1, vfdLower);
      }
      else
      {
        char icon = this.spaceChar;
        if (this.playerState == XbmcHandler.PlayerState.Playing)
          icon = this.playIconChar;
        else if (this.playerState == XbmcHandler.PlayerState.Paused)
          icon = this.pauseIconChar;
        this.display.SetProgress(this.position, this.length, str1, icon);
      }
    }

    private void playbackStopped()
    {
      this.startPlaying = false;
      this.playerState = XbmcHandler.PlayerState.Stopped;
      this.player = (XbmcMediaPlayer) null;
      this.currentlyPlaying = (XbmcPlayable) null;
      this.progressTimer.Stop();
      this.position = new TimeSpan();
      this.length = new TimeSpan();
    }

    private void updateCurrentlyPlaying()
    {
      if (this.player == null)
        return;
      Logging.Log("XBMC Handler", "Updating currently playing file");
      this.scrollingTimer.Stop();
      this.progressTimer.Start();
      if (this.player is XbmcAudioPlayer)
      {
        this.currentlyPlaying = (XbmcPlayable) this.xbmc.Playlist.Audio.GetCurrentItem();
        this.displaySong();
      }
      else if (this.player is XbmcPicturePlayer)
      {
        this.displaySlideshow();
      }
      else
      {
        this.currentlyPlaying = (XbmcPlayable) this.xbmc.Playlist.Video.GetCurrentItem();
        if (this.currentlyPlaying is XbmcTvEpisode)
          this.displayTvEpisode();
        else if (this.currentlyPlaying is XbmcMusicVideo)
          this.displayMusicVideo();
        else
          this.displayMovie();
      }
    }

    private void getTime(out TimeSpan position, out TimeSpan length)
    {
      position = new TimeSpan();
      length = new TimeSpan();
      XbmcPlayerState xbmcPlayerState = XbmcPlayerState.Unavailable;
      if (this.player is XbmcVideoPlayer)
        xbmcPlayerState = ((XbmcVideoPlayer) this.player).GetTime(out this.position, out this.length);
      if (this.player is XbmcAudioPlayer)
        xbmcPlayerState = ((XbmcAudioPlayer) this.player).GetTime(out this.position, out this.length);
      switch (xbmcPlayerState)
      {
        case XbmcPlayerState.Playing:
        case XbmcPlayerState.PartyMode:
          this.playerState = XbmcHandler.PlayerState.Playing;
          break;
        case XbmcPlayerState.Paused:
          this.playerState = XbmcHandler.PlayerState.Paused;
          break;
        default:
          this.playerState = XbmcHandler.PlayerState.Stopped;
          break;
      }
    }

    private void checkControlModeUnchanged()
    {
      if (this.controlModeUnchanged < this.ControlModeUnchangedDelay)
        return;
      this.controlModeUnchanged = -1;
      this.startPlaying = false;
      if (this.player != null)
      {
        this.scrollingTimer.Stop();
        this.progressTimer.Start();
      }
      else
      {
        if (!Settings.Default.XbmcControlModeIdleEnable || !Settings.Default.XbmcControlModeEnable)
          return;
        this.systemTimeTimer.Start();
        if (this.scrollingTimer.Enabled)
          this.scrollingTimer.Stop();
        this.isIdle = true;
      }
    }

    private static bool listContains(IEnumerable<string> list, string value)
    {
      foreach (string str in list)
      {
        if (str.Equals(value, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      return false;
    }

    private void displayMovie()
    {
      List<string> texts = new List<string>();
      string[] strArray = new string[1]
      {
        Settings.Default.XbmcMovieSingleText
      };
      if (this.currentlyPlaying != null)
      {
        texts = XbmcHandler.buildMovieInfo((ICollection<string>) strArray, (XbmcMovie) this.currentlyPlaying);
      }
      else
      {
        IDictionary<string, string> playerInfoLabels = this.getVideoPlayerInfoLabels();
        if (playerInfoLabels.Count > 0)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>(playerInfoLabels.Count);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Title"))
            dictionary.Add("title", playerInfoLabels["VideoPlayer.Title"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Year"))
            dictionary.Add("year", playerInfoLabels["VideoPlayer.Year"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Rating"))
            dictionary.Add("rating", playerInfoLabels["VideoPlayer.Rating"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Genre"))
            dictionary.Add("genre", playerInfoLabels["VideoPlayer.Genre"]);
          try
          {
            if (playerInfoLabels.ContainsKey("VideoPlayer.Duration"))
              dictionary.Add("duration", TimeSpan.Parse(playerInfoLabels["VideoPlayer.Duration"]).TotalMinutes.ToString("0"));
          }
          catch (Exception ex)
          {
          }
          if (playerInfoLabels.ContainsKey("VideoPlayer.mpaa"))
            dictionary.Add("mpaa", playerInfoLabels["VideoPlayer.mpaa"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Tagline"))
            dictionary.Add("tagline", playerInfoLabels["VideoPlayer.Tagline"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Studio"))
            dictionary.Add("studio", playerInfoLabels["VideoPlayer.Studio"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Director"))
            dictionary.Add("director", playerInfoLabels["VideoPlayer.Director"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Writer"))
            dictionary.Add("writer", playerInfoLabels["VideoPlayer.Writer"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.PlotOutline"))
            dictionary.Add("outline", playerInfoLabels["VideoPlayer.PlotOutline"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Plot"))
            dictionary.Add("plot", playerInfoLabels["VideoPlayer.Plot"]);
          texts = XbmcHandler.buildInfoText((ICollection<string>) strArray, (IDictionary<string, string>) dictionary);
        }
      }
      this.displayNowPlaying(texts);
    }

    private void displayTvEpisode()
    {
      List<string> texts = new List<string>();
      string[] strArray = new string[1]
      {
        Settings.Default.XbmcTvSingleText
      };
      if (this.currentlyPlaying != null)
      {
        texts = XbmcHandler.buildTvEpisodeInfo((ICollection<string>) strArray, (XbmcTvEpisode) this.currentlyPlaying);
      }
      else
      {
        IDictionary<string, string> playerInfoLabels = this.getVideoPlayerInfoLabels();
        if (playerInfoLabels.Count > 0)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>(playerInfoLabels.Count);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Title"))
            dictionary.Add("title", playerInfoLabels["VideoPlayer.Title"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.TVShowTitle"))
            dictionary.Add("show", playerInfoLabels["VideoPlayer.TVShowTitle"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Year"))
            dictionary.Add("year", playerInfoLabels["VideoPlayer.Year"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Rating"))
            dictionary.Add("rating", playerInfoLabels["VideoPlayer.Rating"]);
          try
          {
            if (playerInfoLabels.ContainsKey("VideoPlayer.Duration"))
              dictionary.Add("duration", TimeSpan.Parse(playerInfoLabels["VideoPlayer.Duration"]).TotalMinutes.ToString("0"));
          }
          catch (Exception ex)
          {
          }
          if (playerInfoLabels.ContainsKey("VideoPlayer.mpaa"))
            dictionary.Add("mpaa", playerInfoLabels["VideoPlayer.mpaa"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Studio"))
            dictionary.Add("studio", playerInfoLabels["VideoPlayer.Studio"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Director"))
            dictionary.Add("director", playerInfoLabels["VideoPlayer.Director"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Writer"))
            dictionary.Add("writer", playerInfoLabels["VideoPlayer.Writer"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Plot"))
            dictionary.Add("plot", playerInfoLabels["VideoPlayer.Plot"]);
          texts = XbmcHandler.buildInfoText((ICollection<string>) strArray, (IDictionary<string, string>) dictionary);
        }
      }
      this.displayNowPlaying(texts);
    }

    private void displayMusicVideo()
    {
      List<string> texts = new List<string>();
      string[] strArray = new string[1]
      {
        Settings.Default.XbmcMusicVideoSingleText
      };
      if (this.currentlyPlaying != null)
      {
        texts = XbmcHandler.buildMusicVideoInfo((ICollection<string>) strArray, (XbmcMusicVideo) this.currentlyPlaying);
      }
      else
      {
        IDictionary<string, string> playerInfoLabels = this.getVideoPlayerInfoLabels();
        if (playerInfoLabels.Count > 0)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>(playerInfoLabels.Count);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Title"))
            dictionary.Add("title", playerInfoLabels["VideoPlayer.Title"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Artist"))
            dictionary.Add("artist", playerInfoLabels["VideoPlayer.Artist"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Album"))
            dictionary.Add("album", playerInfoLabels["VideoPlayer.Album"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Year"))
            dictionary.Add("year", playerInfoLabels["VideoPlayer.Year"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Rating"))
            dictionary.Add("rating", playerInfoLabels["VideoPlayer.Rating"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Genre"))
            dictionary.Add("genre", playerInfoLabels["VideoPlayer.Genre"]);
          try
          {
            if (playerInfoLabels.ContainsKey("VideoPlayer.Duration"))
              dictionary.Add("duration", TimeSpan.Parse(playerInfoLabels["VideoPlayer.Duration"]).TotalMinutes.ToString("0"));
          }
          catch (Exception ex)
          {
          }
          if (playerInfoLabels.ContainsKey("VideoPlayer.Studio"))
            dictionary.Add("studio", playerInfoLabels["VideoPlayer.Studio"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Director"))
            dictionary.Add("director", playerInfoLabels["VideoPlayer.Director"]);
          if (playerInfoLabels.ContainsKey("VideoPlayer.Plot"))
            dictionary.Add("plot", playerInfoLabels["VideoPlayer.Plot"]);
          texts = XbmcHandler.buildInfoText((ICollection<string>) strArray, (IDictionary<string, string>) dictionary);
        }
      }
      this.displayNowPlaying(texts);
    }

    private void displaySong()
    {
      List<string> texts = new List<string>();
      string[] strArray = new string[1]
      {
        Settings.Default.XbmcMusicSingleText
      };
      if (this.currentlyPlaying != null)
      {
        texts = XbmcHandler.buildMusicInfo((ICollection<string>) strArray, (XbmcSong) this.currentlyPlaying);
      }
      else
      {
        IDictionary<string, string> playerInfoLabels = this.getAudioPlayerInfoLabels();
        if (playerInfoLabels.Count > 0)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>(playerInfoLabels.Count);
          if (playerInfoLabels.ContainsKey("MusicPlayer.Title"))
            dictionary.Add("title", playerInfoLabels["MusicPlayer.Title"]);
          if (playerInfoLabels.ContainsKey("MusicPlayer.Artist"))
            dictionary.Add("artist", playerInfoLabels["MusicPlayer.Artist"]);
          if (playerInfoLabels.ContainsKey("MusicPlayer.Album"))
            dictionary.Add("album", playerInfoLabels["MusicPlayer.Album"]);
          if (playerInfoLabels.ContainsKey("MusicPlayer.Year"))
            dictionary.Add("year", playerInfoLabels["MusicPlayer.Year"]);
          if (playerInfoLabels.ContainsKey("MusicPlayer.Rating"))
            dictionary.Add("rating", playerInfoLabels["MusicPlayer.Rating"]);
          if (playerInfoLabels.ContainsKey("MusicPlayer.Genre"))
            dictionary.Add("genre", playerInfoLabels["MusicPlayer.Genre"]);
          try
          {
            if (playerInfoLabels.ContainsKey("MusicPlayer.Duration"))
              dictionary.Add("duration", TimeSpan.Parse(playerInfoLabels["MusicPlayer.Duration"]).TotalMinutes.ToString("0"));
          }
          catch (Exception ex)
          {
          }
          if (playerInfoLabels.ContainsKey("MusicPlayer.DiscNumber"))
            dictionary.Add("disc", playerInfoLabels["MusicPlayer.DiscNumber"]);
          if (playerInfoLabels.ContainsKey("MusicPlayer.TrackNumber"))
            dictionary.Add("track", playerInfoLabels["MusicPlayer.TrackNumber"]);
          texts = XbmcHandler.buildInfoText((ICollection<string>) strArray, (IDictionary<string, string>) dictionary);
        }
      }
      this.displayNowPlaying(texts);
    }

    private void displayNowPlaying(List<string> texts)
    {
      if (texts == null)
        texts = new List<string>();
      if (texts.Count == 0)
      {
        string infoLabel = this.xbmc.System.GetInfoLabel("Player.Filenameandpath");
        if (!string.IsNullOrEmpty(infoLabel))
          texts.Add(Path.GetFileNameWithoutExtension(infoLabel));
      }
      if (texts.Count <= 0)
        return;
      this.currentLabel = this.replaceChars(texts[0]);
    }

    private string replaceChars(string text)
    {
      if (text != null)
      {
        text = text.Replace("é", "e");
        text = text.Replace("è", "e");
        text = text.Replace("ê", "e");
        text = text.Replace("ë", "e");
        text = text.Replace("ç", "c");
        text = text.Replace("à", "a");
        text = text.Replace("â", "a");
        text = text.Replace("ä", "a");
        text = text.Replace("û", "u");
        text = text.Replace("ü", "u");
        text = text.Replace("ô", "o");
        text = text.Replace("ö", "o");
        text = text.Replace("î", "i");
        text = text.Replace("ï", "i");
        text = text.Replace("É", "E");
        text = text.Replace("È", "E");
        text = text.Replace("Ê", "E");
        text = text.Replace("Ë", "E");
        text = text.Replace("Ç", "C");
        text = text.Replace("À", "A");
        text = text.Replace("Â", "A");
        text = text.Replace("Ä", "A");
        text = text.Replace("Û", "U");
        text = text.Replace("Ü", "U");
        text = text.Replace("Ô", "O");
        text = text.Replace("Ö", "O");
        text = text.Replace("Î", "I");
        text = text.Replace("Ï", "I");
      }
      return text;
    }

    private void displaySlideshow()
    {
      this.display.SetText("SLIDESHOW", "Picture", "Slideshow");
    }

    private IDictionary<string, string> getVideoPlayerInfoLabels()
    {
      return this.xbmc.System.GetInfoLabels("VideoPlayer.Title", "VideoPlayer.Year", "VideoPlayer.Rating", "VideoPlayer.Genre", "VideoPlayer.Duration", "VideoPlayer.mpaa", "VideoPlayer.Tagline", "VideoPlayer.Studio", "VideoPlayer.Director", "VideoPlayer.Writer", "VideoPlayer.PlotOutline", "VideoPlayer.Plot", "VideoPlayer.TVShowTitle", "VideoPlayer.Album", "VideoPlayer.Artist");
    }

    private IDictionary<string, string> getAudioPlayerInfoLabels()
    {
      return this.xbmc.System.GetInfoLabels("MusicPlayer.Title", "MusicPlayer.Year", "MusicPlayer.Rating", "MusicPlayer.Genre", "MusicPlayer.Duration", "MusicPlayer.Artist", "MusicPlayer.Album", "MusicPlayer.DiscNumber", "MusicPlayer.TrackNumber");
    }

    private static List<string> buildMovieInfo(ICollection<string> patterns, XbmcMovie movie)
    {
      return XbmcHandler.buildInfoText(patterns, (IDictionary<string, string>) new Dictionary<string, string>(12)
      {
        {
          "title",
          movie.Title
        },
        {
          "year",
          movie.Year.ToString()
        },
        {
          "rating",
          movie.Rating.ToString("0.#")
        },
        {
          "genre",
          movie.Genre
        },
        {
          "duration",
          movie.Duration.TotalMinutes.ToString("0")
        },
        {
          "mpaa",
          movie.Mpaa
        },
        {
          "tagline",
          movie.Tagline
        },
        {
          "studio",
          movie.Studio
        },
        {
          "director",
          movie.Director
        },
        {
          "writer",
          movie.Writer
        },
        {
          "outline",
          movie.Outline
        },
        {
          "plot",
          movie.Plot
        }
      });
    }

    private static List<string> buildTvEpisodeInfo(
      ICollection<string> patterns,
      XbmcTvEpisode episode)
    {
      return XbmcHandler.buildInfoText(patterns, (IDictionary<string, string>) new Dictionary<string, string>(12)
      {
        {
          "title",
          episode.Title
        },
        {
          nameof (episode),
          ((XbmcVideo) episode).Episodes.ToString()
        },
        {
          "season",
          episode.Season.ToString()
        },
        {
          "show",
          episode.ShowTitle
        },
        {
          "year",
          episode.Year.ToString()
        },
        {
          "rating",
          episode.Rating.ToString("0.#")
        },
        {
          "duration",
          episode.Duration.TotalMinutes.ToString("0")
        },
        {
          "mpaa",
          episode.Mpaa
        },
        {
          "studio",
          episode.Studio
        },
        {
          "director",
          episode.Director
        },
        {
          "writer",
          episode.Writer
        },
        {
          "plot",
          episode.Plot
        }
      });
    }

    private static List<string> buildMusicVideoInfo(
      ICollection<string> patterns,
      XbmcMusicVideo musicVideo)
    {
      return XbmcHandler.buildInfoText(patterns, (IDictionary<string, string>) new Dictionary<string, string>(10)
      {
        {
          "title",
          musicVideo.Title
        },
        {
          "artist",
          musicVideo.Artist
        },
        {
          "album",
          musicVideo.Album
        },
        {
          "year",
          musicVideo.Year.ToString()
        },
        {
          "rating",
          musicVideo.Rating.ToString("0.#")
        },
        {
          "genre",
          musicVideo.Genre
        },
        {
          "duration",
          musicVideo.Duration.Minutes.ToString("0") + ":" + musicVideo.Duration.Seconds.ToString("00")
        },
        {
          "studio",
          musicVideo.Studio
        },
        {
          "director",
          musicVideo.Director
        },
        {
          "plot",
          musicVideo.Plot
        }
      });
    }

    private static List<string> buildMusicInfo(ICollection<string> patterns, XbmcSong song)
    {
      return XbmcHandler.buildInfoText(patterns, (IDictionary<string, string>) new Dictionary<string, string>(10)
      {
        {
          "title",
          song.Title
        },
        {
          "artist",
          song.Artist
        },
        {
          "album",
          song.Album
        },
        {
          "track",
          song.Track.ToString()
        },
        {
          "year",
          song.Year.ToString()
        },
        {
          "rating",
          song.Rating.ToString("0.#")
        },
        {
          "genre",
          song.Genre
        },
        {
          "duration",
          song.Duration.Minutes.ToString("0") + ":" + song.Duration.Seconds.ToString("00")
        },
        {
          "lyrics",
          song.Lyrics
        }
      });
    }

    private static List<string> buildInfoText(
      ICollection<string> patterns,
      IDictionary<string, string> data)
    {
      if (patterns == null || data == null)
        throw new ArgumentNullException();
      List<string> stringList = new List<string>(patterns.Count);
      foreach (string pattern in (IEnumerable<string>) patterns)
      {
        StringBuilder stringBuilder = new StringBuilder(pattern);
        string str1 = pattern;
        if (str1.LastIndexOf("%", str1.Length, str1.Length) > 0)
        {
          int num1 = 0;
          int num2;
          for (int startIndex1 = 0; startIndex1 >= 0 && startIndex1 < str1.Length; startIndex1 = num2 + 1)
          {
            num2 = str1.IndexOf("%", startIndex1, str1.Length - startIndex1);
            if (num2 >= startIndex1)
            {
              ++num1;
              if (num1 > 1)
              {
                --num1;
                string key = str1.Substring(startIndex1, num2 - startIndex1).ToLowerInvariant();
                if (!key.Contains(" "))
                {
                  --num1;
                  stringBuilder.Remove(startIndex1 - 1, num2 - startIndex1 + 2);
                  bool flag = false;
                  int result = 0;
                  if (key.Contains(":"))
                  {
                    int startIndex2 = key.IndexOf(":") + 1;
                    string s = key.Substring(startIndex2, key.Length - startIndex2);
                    key = key.Remove(startIndex2 - 1);
                    if (int.TryParse(s, out result) && result > 0)
                      flag = true;
                  }
                  if (data.ContainsKey(key) && !string.IsNullOrEmpty(data[key]))
                  {
                    string str2 = data[key];
                    if (flag && str2.Length < result)
                      str2 = str2.PadLeft(result, '0');
                    stringBuilder.Insert(startIndex1 - 1, str2);
                    num2 += data[key].Length;
                  }
                  str1 = stringBuilder.ToString();
                }
                num2 -= key.Length + 2;
              }
            }
            else
              break;
          }
        }
        if (stringBuilder.Length > 0)
          stringList.Add(stringBuilder.ToString());
      }
      return stringList;
    }

    private enum PlayerState
    {
      Stopped,
      Playing,
      Paused,
    }

    public struct ControlState
    {
      private string window;
      private string control;
      private string label1;
      private string label2;
      private int pos1;
      private int pos2;

      public string Window
      {
        get
        {
          return this.window;
        }
        set
        {
          this.window = value != null ? value : string.Empty;
        }
      }

      public string Control
      {
        get
        {
          return this.control;
        }
        set
        {
          this.control = value != null ? value : string.Empty;
        }
      }

      public string Label1
      {
        get
        {
          return this.label1;
        }
        set
        {
          this.label1 = value != null ? value : string.Empty;
        }
      }

      public string Label2
      {
        get
        {
          return this.label2;
        }
        set
        {
          this.label2 = value != null ? value : string.Empty;
        }
      }

      public int Pos1
      {
        get
        {
          return this.pos1;
        }
        set
        {
          this.pos1 = value;
        }
      }

      public int Pos2
      {
        get
        {
          return this.pos2;
        }
        set
        {
          this.pos2 = value;
        }
      }

      public ControlState(string window, string control)
      {
        if (window == null)
          window = string.Empty;
        if (control == null)
          control = string.Empty;
        this.window = window;
        this.control = control;
        this.label1 = string.Empty;
        this.label2 = string.Empty;
        this.pos1 = -5;
        this.pos2 = -5;
      }
    }
  }
}

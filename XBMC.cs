// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.XBMC
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using iMon.DisplayApi;
using iMon.XBMC.Dialogs;
using iMon.XBMC.Properties;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using XBMC.JsonRpc;

namespace iMon.XBMC
{
  public class XBMC : Form
  {
    private const string AutostartRegistry = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
    private bool closing;
    private iMonWrapperApi imon;
    private DisplayHandler displayHandler;
    private XbmcJsonRpcConnection xbmc;
    private XbmcHandler xbmcHandler;
    private iMon.XBMC.XBMC.XbmcConnectingDelegate xbmcConnectingDeletage;
    private System.Windows.Forms.Timer xbmcConnectionTimer;
    private IContainer components;
    private MenuStrip menu;
    private ToolStripMenuItem miGeneral;
    private ToolStripMenuItem miXbmc;
    private ToolStripMenuItem miXbmcConnect;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem miXbmcInfo;
    private ToolStripMenuItem miAbout;
    private ToolStripMenuItem miAboutXbmcOniMon;
    private ToolStripMenuItem miGeneralClose;
    private ToolStripMenuItem miImon;
    private ToolStripMenuItem miImonInitialize;
    private ToolStripMenuItem miImonUninitialize;
    private SplitContainer splitter;
    private ImageList iLOptions;
    private TabControl tabOptions;
    private TabPage tpGeneral;
    private TabPage tpImon;
    private TabPage tpXBMC;
    private GroupBox groupBox2;
    private ToolStripMenuItem miXbmcDisconnect;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private CheckBox cbGeneralTrayStartMinimized;
    private CheckBox cbGeneralTrayEnabled;
    private CheckBox cbGeneralStartupConnect;
    private CheckBox cbGeneralStartupAuto;
    private CheckBox cbGeneralTrayHideOnClose;
    private NotifyIcon trayIcon;
    private ContextMenuStrip trayMenu;
    private ToolStripMenuItem trayMenuOpen;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem trayMenuXBMC;
    private ToolStripMenuItem trayMenuImon;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem trayMenuClose;
    private CheckBox cbGeneralTrayHideOnMinimize;
    private CheckBox cbImonGeneralUninitializeOnError;
    private CheckBox cbImonGeneralAutoInitialize;
    private Panel pNavigation;
    private NavigationButton bNavigationGeneral;
    private NavigationButton bNavigationXbmc;
    private NavigationButton bNavigationImon;
    private GroupBox groupBox13;
    private NumericUpDown nudImonLcdScrollingDelay;
    private Label label8;
    private GroupBox groupBox15;
    private CheckBox cbGeneralDebugEnable;
    private Label label12;
    private GroupBox groupBox1;
    private Label label6;
    private NumericUpDown nudXbmcConnectionInterval;
    private Label label5;
    private TextBox tbXbmcConnectionPassword;
    private Label label4;
    private TextBox tbXbmcConnectionUsername;
    private Label label3;
    private TextBox tbXbmcConnectionPort;
    private Label label2;
    private TextBox tbXbmcConnectionIp;
    private Label label1;
    private GroupBox groupBox11;
    private GroupBox groupBox5;
    private TextBox tbXbmcIdleStaticText;
    private GroupBox groupBox6;
    private GroupBox groupBox10;
    private GroupBox groupBox8;
    private GroupBox groupBox9;
    private GroupBox groupBox16;
    private CheckBox cbXbmcControlModeEnable;
    private CheckBox cbGeneralTrayDisableBalloonTips;
    private SuggestionBox tbXbmcMovieSingleText;
    private SuggestionBox tbXbmcTvSingleText;
    private SuggestionBox tbXbmcMusicSingleText;
    private SuggestionBox tbXbmcMusicVideoSingleText;
    private CheckBox cbXbmcIdleShowSeconds;
    private Label label9;
    private NumericUpDown nudXbmcControlModeUpdateInterval;
    private Label label11;
    private Label label7;
    private Label label10;
    private Label label14;
    private Label label13;
    private CheckBox cbXbmcIdleStaticTextEnable;
    private CheckBox cbXbmcIdleTime;
    private RadioButton rbShowInfoPlayingTime;
    private RadioButton rbShowInfoPlayingProgressBar;
    private RadioButton rbShowInfoPlayingBoth;
    private Label label15;
    private NumericUpDown nudAlternateDisplayDelay;
    private Label label16;
    private Label label17;
    private NumericUpDown nudControlModeIdleDelay;
    private CheckBox cbControlModeIdle;
    private Label label18;
    private Label label19;
    private Label label21;
    private Label label20;
    private Label label22;
    private RadioButton rbProgressStyle2;
    private RadioButton rbProgressStyle1;
    private Panel groupBox7;
    private AboutDialog aboutDialog;

    private void constructor()
    {
      this.tbXbmcMovieSingleText.Suggestions.Add("%title%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%year%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%rating%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%genre%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%duration%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%mpaa%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%tagline%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%studio%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%director%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%writer%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%outline%");
      this.tbXbmcMovieSingleText.Suggestions.Add("%plot%");
      this.tbXbmcTvSingleText.Suggestions.Add("%title%");
      this.tbXbmcTvSingleText.Suggestions.Add("%episode%");
      this.tbXbmcTvSingleText.Suggestions.Add("%season%");
      this.tbXbmcTvSingleText.Suggestions.Add("%show%");
      this.tbXbmcTvSingleText.Suggestions.Add("%year%");
      this.tbXbmcTvSingleText.Suggestions.Add("%rating%");
      this.tbXbmcTvSingleText.Suggestions.Add("%duration%");
      this.tbXbmcTvSingleText.Suggestions.Add("%mpaa%");
      this.tbXbmcTvSingleText.Suggestions.Add("%studio%");
      this.tbXbmcTvSingleText.Suggestions.Add("%director%");
      this.tbXbmcTvSingleText.Suggestions.Add("%writer%");
      this.tbXbmcTvSingleText.Suggestions.Add("%plot%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%title%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%artist%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%album%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%track%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%year%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%rating%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%genre%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%duration%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%disc%");
      this.tbXbmcMusicSingleText.Suggestions.Add("%lyrics%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%title%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%artist%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%album%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%year%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%rating%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%genre%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%duration%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%studio%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%director%");
      this.tbXbmcMusicVideoSingleText.Suggestions.Add("%plot%");
      try
      {
        if (File.Exists("error.log"))
        {
          if (File.Exists("error.log.old"))
            File.Delete("error.log.old");
          File.Move("error.log", "error.log.old");
        }
        if (File.Exists("debug.log"))
        {
          if (File.Exists("debug.log.old"))
            File.Delete("debug.log.old");
          File.Move("debug.log", "debug.log.old");
        }
      }
      catch (Exception ex)
      {
      }
      this.xbmcConnectionTimer = new System.Windows.Forms.Timer();
      this.xbmcConnectionTimer.Tick += new EventHandler(this.xbmcTryConnect);
      this.settingsUpdate();
      this.setupSettingsChanges((Control) this.tabOptions);
      Logging.Log("Setting up iMON");
      this.imon = new iMonWrapperApi();
      this.imon.StateChanged += new EventHandler<iMonStateChangedEventArgs>(this.wrapperApi_StateChanged);
      this.imon.Error += new EventHandler<iMonErrorEventArgs>(this.wrapperApi_Error);
      this.imon.LogError += new EventHandler<iMonLogErrorEventArgs>(iMon.XBMC.XBMC.wrapperApiIMonLogError);
      if (Settings.Default.GeneralDebugEnable)
        this.imon.Log += new EventHandler<iMonLogEventArgs>(iMon.XBMC.XBMC.wrapperApiIMonLog);
      this.displayHandler = new DisplayHandler(this.imon);
      this.displayHandler.RunWorkerAsync();
      this.xbmcConnectingDeletage = new iMon.XBMC.XBMC.XbmcConnectingDelegate(this.xbmcConnecting);
      this.xbmcSetup();
    }

    private void show()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Action(() => this.show()));
      }
      else
      {
        this.Show();
        this.WindowState = FormWindowState.Normal;
      }
    }

    private void close(bool force)
    {
      if (!force && Settings.Default.GeneralTrayEnabled && Settings.Default.GeneralTrayHideOnClose)
      {
        this.Hide();
      }
      else
      {
        Logging.Log("Closing the application...");
        this.closing = true;
        Logging.Log("Cancelling the display handler...");
        this.displayHandler.CancelAsync();
        this.iMonUninitialize();
        Logging.Log("Cancelling the XBMC handler...");
        this.xbmcHandler.CancelAsync();
        this.xbmcDisconnect(true);
        this.Close();
      }
    }

    private void setupSettingsChanges(Control control)
    {
      foreach (Control control1 in (ArrangedElementCollection) control.Controls)
      {
        if (control1 is CheckBox)
          ((CheckBox) control1).CheckedChanged += new EventHandler(this.settingsChanged);
        else if (control1 is RadioButton)
          ((RadioButton) control1).CheckedChanged += new EventHandler(this.settingsChanged);
        else if (control1 is ComboBox)
          ((ListControl) control1).SelectedValueChanged += new EventHandler(this.settingsChanged);
        else if (control1 is TextBox)
          control1.Leave += new EventHandler(this.settingsChanged);
        else if (control1 is NumericUpDown)
          ((NumericUpDown) control1).ValueChanged += new EventHandler(this.settingsChanged);
        else if (control1.Controls.Count > 0)
          this.setupSettingsChanges(control1);
      }
    }

    private void showBalloonTip(string text, ToolTipIcon icon)
    {
      if (Settings.Default.GeneralTrayDisableBalloonTips)
        return;
      this.trayIcon.ShowBalloonTip(5000, "XBMC on iMON", text, icon);
    }

    private void settingsChanged(object sender, EventArgs e)
    {
      this.settingsSave();
    }

    private void settingsUpdate()
    {
      this.cbGeneralStartupAuto.Checked = Settings.Default.GeneralStartupAuto;
      this.cbGeneralStartupConnect.Checked = Settings.Default.GeneralStartupConnect;
      this.cbGeneralTrayEnabled.Checked = Settings.Default.GeneralTrayEnabled;
      this.cbGeneralTrayStartMinimized.Checked = Settings.Default.GeneralTrayStartMinimized;
      this.cbGeneralTrayHideOnMinimize.Checked = Settings.Default.GeneralTrayHideOnMinimize;
      this.cbGeneralTrayHideOnClose.Checked = Settings.Default.GeneralTrayHideOnClose;
      this.cbGeneralTrayDisableBalloonTips.Checked = Settings.Default.GeneralTrayDisableBalloonTips;
      this.cbGeneralDebugEnable.Checked = Settings.Default.GeneralDebugEnable;
      this.cbImonGeneralAutoInitialize.Checked = Settings.Default.ImonAutoInitialize;
      this.cbImonGeneralUninitializeOnError.Checked = Settings.Default.ImonUninitializeOnError;
      this.nudImonLcdScrollingDelay.Value = (Decimal) Settings.Default.ImonLcdScrollingDelay;
      this.tbXbmcConnectionIp.Text = Settings.Default.XbmcIp;
      this.tbXbmcConnectionPort.Text = Settings.Default.XbmcPort.ToString();
      this.tbXbmcConnectionUsername.Text = Settings.Default.XbmcUsername;
      this.tbXbmcConnectionPassword.Text = Settings.Default.XbmcPassword;
      this.nudXbmcConnectionInterval.Value = (Decimal) Settings.Default.XbmcConnectionInterval;
      this.cbXbmcIdleStaticTextEnable.Checked = Settings.Default.XbmcIdleStaticTextEnable;
      this.tbXbmcIdleStaticText.Text = Settings.Default.XbmcIdleStaticText;
      this.cbXbmcIdleTime.Checked = Settings.Default.XbmcIdleTimeEnable;
      this.cbXbmcIdleShowSeconds.Checked = Settings.Default.XbmcIdleTimeShowSeconds;
      this.cbXbmcControlModeEnable.Checked = Settings.Default.XbmcControlModeEnable;
      this.nudXbmcControlModeUpdateInterval.Value = (Decimal) Settings.Default.XbmcControlModeUpdateInterval;
      this.nudXbmcControlModeUpdateInterval.Enabled = Settings.Default.XbmcControlModeEnable;
      this.cbControlModeIdle.Enabled = Settings.Default.XbmcControlModeEnable;
      this.cbControlModeIdle.Checked = Settings.Default.XbmcControlModeIdleEnable;
      this.nudControlModeIdleDelay.Enabled = Settings.Default.XbmcControlModeIdleEnable && Settings.Default.XbmcControlModeEnable;
      this.nudControlModeIdleDelay.Value = (Decimal) Settings.Default.XbmcControlModeIdleDelay;
      this.rbShowInfoPlayingTime.Checked = Settings.Default.XbmcPlaybackProgress == 0;
      this.rbShowInfoPlayingProgressBar.Checked = Settings.Default.XbmcPlaybackProgress == 1;
      this.rbShowInfoPlayingBoth.Checked = Settings.Default.XbmcPlaybackProgress == 2;
      this.nudXbmcControlModeUpdateInterval.Enabled = Settings.Default.XbmcPlaybackProgress == 2;
      this.nudAlternateDisplayDelay.Value = (Decimal) Settings.Default.XbmcAlternateProgressDisplayDelay;
      this.groupBox7.Enabled = Settings.Default.XbmcPlaybackProgress == 2 || Settings.Default.XbmcPlaybackProgress == 1;
      this.rbProgressStyle1.Checked = Settings.Default.XbmcProgressbarStyle == 1;
      this.rbProgressStyle2.Checked = Settings.Default.XbmcProgressbarStyle == 2;
      this.tbXbmcMovieSingleText.Text = Settings.Default.XbmcMovieSingleText;
      this.tbXbmcTvSingleText.Text = Settings.Default.XbmcTvSingleText;
      this.tbXbmcMusicSingleText.Text = Settings.Default.XbmcMusicSingleText;
      this.tbXbmcMusicVideoSingleText.Text = Settings.Default.XbmcMusicVideoSingleText;
      this.trayIcon.Visible = Settings.Default.GeneralTrayEnabled;
      this.xbmcConnectionTimer.Interval = Settings.Default.XbmcConnectionInterval * 1000;
      Logging.Log("Settings successfully applied to the GUI");
      if (!Settings.Default.ImonAutoInitialize || this.imon == null || (this.imon.IsInitialized || this.xbmc == null) || !this.xbmc.IsAlive)
        return;
      Logging.Log("Auto-initializing iMON");
      this.iMonInitialize();
    }

    private void settingsSave()
    {
      bool flag = false;
      if (Settings.Default.GeneralStartupAuto != this.cbGeneralStartupAuto.Checked)
      {
        RegistryKey subKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
        if (this.cbGeneralStartupAuto.Checked)
        {
          Logging.Log("Adding Windows Registry entry for autostart on windows start...");
          subKey.SetValue(Application.ProductName, (object) Application.ExecutablePath);
        }
        else if (registryKey != null && !string.IsNullOrEmpty((string) registryKey.GetValue(Application.ProductName)))
        {
          Logging.Log("Removing Windows Registry entry for autostart on windows start...");
          subKey.DeleteValue(Application.ProductName);
        }
      }
      if (Settings.Default.GeneralStartupConnect != this.cbGeneralStartupConnect.Checked)
      {
        if (this.cbGeneralStartupConnect.Checked && !this.xbmc.IsAlive)
        {
          Logging.Log("Auto-connecting to XBMC");
          this.xbmcConnect(true);
        }
        else if (!this.cbGeneralStartupConnect.Checked && this.xbmcConnectionTimer.Enabled)
        {
          Logging.Log("Stopping XBMC auto-connection interval");
          this.xbmcConnectionTimer.Stop();
        }
      }
      if (Settings.Default.XbmcIp != this.tbXbmcConnectionIp.Text || Settings.Default.XbmcPort != Convert.ToInt32(this.tbXbmcConnectionPort.Text) || (Settings.Default.XbmcUsername != this.tbXbmcConnectionUsername.Text || Settings.Default.XbmcPassword != this.tbXbmcConnectionPassword.Text))
      {
        flag = true;
        this.xbmcHandler.CancelAsync();
        this.xbmcHandler.Dispose();
        if (this.xbmc.IsAlive)
        {
          this.xbmcDisconnect(true);
          this.xbmc.System.Hibernating -= new EventHandler(this.xbmcShutdown);
          this.xbmc.System.ShuttingDown -= new EventHandler(this.xbmcShutdown);
          this.xbmc.System.Rebooting -= new EventHandler(this.xbmcShutdown);
          this.xbmc.System.Sleeping -= new EventHandler(this.xbmcShutdown);
          this.xbmc.System.Suspending -= new EventHandler(this.xbmcShutdown);
          this.xbmc.Aborted -= new EventHandler(this.xbmcShutdown);
          this.xbmc.LogError -= new EventHandler<XbmcJsonRpcLogErrorEventArgs>(iMon.XBMC.XBMC.wrapperApiXbmcLogError);
          if (Settings.Default.GeneralDebugEnable)
            this.xbmc.Log -= new EventHandler<XbmcJsonRpcLogEventArgs>(iMon.XBMC.XBMC.wrapperApiXbmcLog);
          this.xbmc.Dispose();
        }
      }
      if (Settings.Default.XbmcConnectionInterval != Convert.ToInt32(this.nudXbmcConnectionInterval.Value))
        this.xbmcConnectionTimer.Interval = Convert.ToInt32(this.nudXbmcConnectionInterval.Value) * 1000;
      if (Settings.Default.GeneralDebugEnable != this.cbGeneralDebugEnable.Checked)
      {
        if (this.cbGeneralDebugEnable.Checked)
        {
          this.imon.Log += new EventHandler<iMonLogEventArgs>(iMon.XBMC.XBMC.wrapperApiIMonLog);
          this.xbmc.Log += new EventHandler<XbmcJsonRpcLogEventArgs>(iMon.XBMC.XBMC.wrapperApiXbmcLog);
        }
        else
        {
          this.imon.Log -= new EventHandler<iMonLogEventArgs>(iMon.XBMC.XBMC.wrapperApiIMonLog);
          this.xbmc.Log -= new EventHandler<XbmcJsonRpcLogEventArgs>(iMon.XBMC.XBMC.wrapperApiXbmcLog);
        }
      }
      Settings.Default.GeneralStartupAuto = this.cbGeneralStartupAuto.Checked;
      Settings.Default.GeneralStartupConnect = this.cbGeneralStartupConnect.Checked;
      Settings.Default.GeneralTrayEnabled = this.cbGeneralTrayEnabled.Checked;
      Settings.Default.GeneralTrayStartMinimized = this.cbGeneralTrayStartMinimized.Checked;
      Settings.Default.GeneralTrayHideOnMinimize = this.cbGeneralTrayHideOnMinimize.Checked;
      Settings.Default.GeneralTrayHideOnClose = this.cbGeneralTrayHideOnClose.Checked;
      Settings.Default.GeneralTrayDisableBalloonTips = this.cbGeneralTrayDisableBalloonTips.Checked;
      Settings.Default.GeneralDebugEnable = this.cbGeneralDebugEnable.Checked;
      Settings.Default.ImonAutoInitialize = this.cbImonGeneralAutoInitialize.Checked;
      Settings.Default.ImonUninitializeOnError = this.cbImonGeneralUninitializeOnError.Checked;
      Settings.Default.ImonLcdScrollingDelay = Convert.ToInt32(this.nudImonLcdScrollingDelay.Value);
      Settings.Default.XbmcIp = this.tbXbmcConnectionIp.Text;
      Settings.Default.XbmcPort = int.Parse(this.tbXbmcConnectionPort.Text);
      Settings.Default.XbmcUsername = this.tbXbmcConnectionUsername.Text;
      Settings.Default.XbmcPassword = this.tbXbmcConnectionPassword.Text;
      Settings.Default.XbmcConnectionInterval = Convert.ToInt32(this.nudXbmcConnectionInterval.Value);
      Settings.Default.XbmcIdleStaticTextEnable = this.cbXbmcIdleStaticTextEnable.Checked;
      Settings.Default.XbmcIdleTimeEnable = this.cbXbmcIdleTime.Checked;
      Settings.Default.XbmcIdleStaticText = this.tbXbmcIdleStaticText.Text;
      Settings.Default.XbmcIdleTimeShowSeconds = this.cbXbmcIdleShowSeconds.Checked;
      Settings.Default.XbmcControlModeEnable = this.cbXbmcControlModeEnable.Checked;
      Settings.Default.XbmcControlModeUpdateInterval = Convert.ToInt32(this.nudXbmcControlModeUpdateInterval.Value);
      Settings.Default.XbmcControlModeIdleEnable = this.cbControlModeIdle.Checked;
      Settings.Default.XbmcControlModeIdleDelay = Convert.ToInt32(this.nudControlModeIdleDelay.Value);
      Settings.Default.XbmcPlaybackProgress = this.rbShowInfoPlayingTime.Checked ? 0 : (this.rbShowInfoPlayingProgressBar.Checked ? 1 : 2);
      Settings.Default.XbmcAlternateProgressDisplayDelay = Convert.ToInt32(this.nudAlternateDisplayDelay.Value);
      Settings.Default.XbmcProgressbarStyle = Convert.ToInt32(this.rbProgressStyle1.Checked ? 1 : 2);
      Settings.Default.XbmcMovieSingleText = this.tbXbmcMovieSingleText.Text;
      Settings.Default.XbmcTvSingleText = this.tbXbmcTvSingleText.Text;
      Settings.Default.XbmcMusicSingleText = this.tbXbmcMusicSingleText.Text;
      Settings.Default.XbmcMusicVideoSingleText = this.tbXbmcMusicVideoSingleText.Text;
      Settings.Default.Save();
      Logging.Log("Settings saved");
      if (flag)
        this.xbmcSetup();
      this.xbmcHandler.Update();
      this.trayIcon.Visible = Settings.Default.GeneralTrayEnabled;
    }

    private void iMonInitialize()
    {
      Logging.Log("Initializing iMON");
      this.imon.Initialize();
    }

    private void iMonUninitialize()
    {
      if (this.imon == null || !this.imon.IsInitialized)
        return;
      Logging.Log("Uninitializing iMON");
      this.imon.Uninitialize();
    }

    private void iMonStateChanged(bool isInitialized)
    {
      this.miImonInitialize.Enabled = !isInitialized;
      this.miImonUninitialize.Enabled = isInitialized;
      if (isInitialized)
      {
        string str = string.Empty;
        if ((this.imon.DisplayType & iMonDisplayType.VFD) == iMonDisplayType.VFD)
          str = !string.IsNullOrEmpty(str) ? str + " & VFD" : "VFD";
        Logging.Log("iMON " + str + " initialized");
        this.trayIcon.Text = "XBMC on iMON" + Environment.NewLine + "Running";
        this.showBalloonTip("Connected to XBMC at " + Settings.Default.XbmcIp + ":" + (object) Settings.Default.XbmcPort + Environment.NewLine + "iMON " + str + " initialized", ToolTipIcon.Info);
        this.trayMenuImon.Text = "iMON: Uninitialize";
      }
      else
      {
        Logging.Log("iMON uninitialized");
        this.trayIcon.Text = "XBMC on iMON" + Environment.NewLine + "Connected";
        this.showBalloonTip("iMON uninitialized", ToolTipIcon.Warning);
        this.trayMenuImon.Text = "iMON: Initialize";
      }
    }

    private void iMonError(iMonErrorType error)
    {
      Logging.Error("iMON reports an error of type " + (object) error);
      switch (error)
      {
        case iMonErrorType.Unknown:
          this.showBalloonTip("Unknown error in iMON", ToolTipIcon.Error);
          break;
        case iMonErrorType.OutOfMemory:
          this.showBalloonTip("iMON is out of memory!" + Environment.NewLine + "Please restart XBMC on iMON.", ToolTipIcon.Warning);
          break;
        case iMonErrorType.InvalidArguments:
        case iMonErrorType.InvalidPointer:
          this.showBalloonTip("Invalid arguments in a call to iMON", ToolTipIcon.Warning);
          break;
        case iMonErrorType.NotInitialized:
        case iMonErrorType.ApiNotInitialized:
          this.showBalloonTip("Invalid operation because" + Environment.NewLine + "iMON is not initialized.", ToolTipIcon.Error);
          break;
        case iMonErrorType.NotInPluginMode:
          this.showBalloonTip("Invalid operation because" + Environment.NewLine + "iMON is not in plugin mode.", ToolTipIcon.Error);
          break;
        case iMonErrorType.iMonClosed:
          this.showBalloonTip("iMON Manager has been closed.", ToolTipIcon.Info);
          break;
        case iMonErrorType.HardwareDisconnected:
          this.showBalloonTip("The iMON Display has been disconnected.", ToolTipIcon.Warning);
          break;
        case iMonErrorType.PluginModeAlreadyInUse:
          this.showBalloonTip("Cannot use iMON Display because it" + Environment.NewLine + "is already used by another application.", ToolTipIcon.Error);
          break;
        case iMonErrorType.HardwareNotConnected:
          this.showBalloonTip("Cannot use iMON Display because" + Environment.NewLine + "it is not connected.", ToolTipIcon.Error);
          break;
        case iMonErrorType.HardwareNotSupported:
          this.showBalloonTip("Cannot use iMON Display because" + Environment.NewLine + "this hardware is not supported.", ToolTipIcon.Error);
          break;
        case iMonErrorType.PluginModeDisabled:
          this.showBalloonTip("Plugin mode must be" + Environment.NewLine + "enabled in iMON Manager.", ToolTipIcon.Error);
          break;
        case iMonErrorType.iMonNotResponding:
          this.showBalloonTip("iMON is not responding." + Environment.NewLine + "Please restart iMON Manager.", ToolTipIcon.Error);
          break;
      }
      if (!Settings.Default.ImonUninitializeOnError)
        return;
      this.iMonUninitialize();
    }

    private void xbmcSetup()
    {
      Logging.Log("Setting up XBMC connection to " + Settings.Default.XbmcIp + ":" + (object) Settings.Default.XbmcPort);
      this.xbmc = new XbmcJsonRpcConnection(Settings.Default.XbmcIp, Settings.Default.XbmcPort, Settings.Default.XbmcUsername, Settings.Default.XbmcPassword);
      this.xbmc.System.Hibernating += new EventHandler(this.xbmcShutdown);
      this.xbmc.System.ShuttingDown += new EventHandler(this.xbmcShutdown);
      this.xbmc.System.Rebooting += new EventHandler(this.xbmcShutdown);
      this.xbmc.System.Sleeping += new EventHandler(this.xbmcShutdown);
      this.xbmc.System.Suspending += new EventHandler(this.xbmcShutdown);
      this.xbmc.Aborted += new EventHandler(this.xbmcShutdown);
      this.xbmc.LogError += new EventHandler<XbmcJsonRpcLogErrorEventArgs>(iMon.XBMC.XBMC.wrapperApiXbmcLogError);
      if (Settings.Default.GeneralDebugEnable)
        this.xbmc.Log += new EventHandler<XbmcJsonRpcLogEventArgs>(iMon.XBMC.XBMC.wrapperApiXbmcLog);
      this.xbmcHandler = new XbmcHandler(this.xbmc, this.displayHandler);
      this.xbmcHandler.RunWorkerAsync();
      if (!Settings.Default.GeneralStartupConnect)
        return;
      Logging.Log("Auto-connecting to XBMC at startup");
      this.xbmcConnect(true);
    }

    private void xbmcTryConnect(object sender, EventArgs e)
    {
      this.xbmcConnectionTimer.Stop();
      Logging.Log("Trying to auto-connect with XBMC");
      this.xbmcConnect(true);
    }

    private void xbmcConnect(bool auto)
    {
      if (this.xbmc.IsAlive)
        return;
      Logging.Log("Asynchronous starting to connect with XBMC");
      this.xbmcConnectingDeletage.BeginInvoke(auto, new AsyncCallback(this.xbmcConnectingFinished), (object) auto);
    }

    private bool xbmcConnecting(bool auto)
    {
      return this.xbmc.Open(9090);
    }

    private void xbmcConnectingFinished(IAsyncResult ar)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Action(() => this.xbmcConnectingFinished(ar)));
      }
      else
      {
        bool asyncState = (bool) ar.AsyncState;
        if (this.xbmcConnectingDeletage.EndInvoke(ar) && this.xbmc.IsAlive)
        {
          Logging.Log("Connection with XBMC established");
          this.trayIcon.Text = "XBMC on iMON" + Environment.NewLine + "Connected";
          if (!Settings.Default.ImonAutoInitialize)
            this.showBalloonTip("Connected to XBMC at " + Settings.Default.XbmcIp + ":" + (object) Settings.Default.XbmcPort, ToolTipIcon.Info);
          this.miXbmcConnect.Enabled = false;
          this.miXbmcDisconnect.Enabled = true;
          this.miXbmcInfo.Text = "Build " + this.xbmc.Xbmc.BuildVersion + " (" + this.xbmc.Xbmc.BuildDate.ToShortDateString() + ")";
          this.miImon.Enabled = true;
          this.miImonInitialize.Enabled = true;
          this.miImonUninitialize.Enabled = false;
          this.trayMenuXBMC.Text = "XBMC: Disconnect";
          this.trayMenuImon.Text = "iMON: Initialize";
          this.trayMenuImon.Enabled = true;
          if (!Settings.Default.ImonAutoInitialize)
            return;
          this.iMonInitialize();
        }
        else if (!asyncState)
        {
          Logging.Log("Connection with XBMC failed");
          if (MessageBox.Show("Cannot connect to XBMC at" + Environment.NewLine + Settings.Default.XbmcIp + ":" + (object) Settings.Default.XbmcPort, "XBMC Connection", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) != DialogResult.Retry)
            return;
          this.xbmcConnect(false);
        }
        else
        {
          Logging.Log("Connection with XBMC failed");
          this.xbmcConnectionTimer.Start();
        }
      }
    }

    private void xbmcDisconnect(bool forceClose)
    {
      this.iMonUninitialize();
      if (forceClose)
      {
        Logging.Log("Disconnecting from XBMC");
        this.xbmc.Close();
      }
      else
      {
        this.trayIcon.Text = "XBMC on iMON" + Environment.NewLine + "Disconnected";
        this.showBalloonTip("XBMC disconnected", ToolTipIcon.Warning);
      }
      this.miXbmcConnect.Enabled = true;
      this.miXbmcDisconnect.Enabled = false;
      this.miXbmcInfo.Text = "Build unknown";
      this.miImon.Enabled = false;
      this.trayMenuXBMC.Text = "XBMC: Connect";
      this.trayMenuImon.Text = "iMON: Initialize";
      this.trayMenuImon.Enabled = false;
      if (!Settings.Default.GeneralStartupConnect || forceClose)
        return;
      this.xbmcConnectionTimer.Start();
    }

    private void xbmcShutdown(object sender, EventArgs args)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Action(() => this.xbmcShutdown(sender, args)));
      }
      else
      {
        Logging.Log("XBMC has been closed");
        this.displayHandler.SetText("XBMC Shutdown", nameof (XBMC), "Shutdown");
        Thread.Sleep(2000);
        this.xbmcDisconnect(false);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (iMon.XBMC.XBMC));
      this.menu = new MenuStrip();
      this.miGeneral = new ToolStripMenuItem();
      this.miGeneralClose = new ToolStripMenuItem();
      this.miXbmc = new ToolStripMenuItem();
      this.miXbmcConnect = new ToolStripMenuItem();
      this.miXbmcDisconnect = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.miXbmcInfo = new ToolStripMenuItem();
      this.miImon = new ToolStripMenuItem();
      this.miImonInitialize = new ToolStripMenuItem();
      this.miImonUninitialize = new ToolStripMenuItem();
      this.miAbout = new ToolStripMenuItem();
      this.miAboutXbmcOniMon = new ToolStripMenuItem();
      this.splitter = new SplitContainer();
      this.pNavigation = new Panel();
      this.iLOptions = new ImageList(this.components);
      this.tabOptions = new TabControl();
      this.tpImon = new TabPage();
      this.groupBox13 = new GroupBox();
      this.label12 = new Label();
      this.nudImonLcdScrollingDelay = new NumericUpDown();
      this.label8 = new Label();
      this.groupBox3 = new GroupBox();
      this.cbImonGeneralUninitializeOnError = new CheckBox();
      this.cbImonGeneralAutoInitialize = new CheckBox();
      this.tpGeneral = new TabPage();
      this.groupBox15 = new GroupBox();
      this.cbGeneralDebugEnable = new CheckBox();
      this.groupBox4 = new GroupBox();
      this.cbGeneralTrayDisableBalloonTips = new CheckBox();
      this.cbGeneralTrayHideOnMinimize = new CheckBox();
      this.cbGeneralTrayHideOnClose = new CheckBox();
      this.cbGeneralTrayStartMinimized = new CheckBox();
      this.cbGeneralTrayEnabled = new CheckBox();
      this.groupBox2 = new GroupBox();
      this.cbGeneralStartupConnect = new CheckBox();
      this.cbGeneralStartupAuto = new CheckBox();
      this.tpXBMC = new TabPage();
      this.groupBox9 = new GroupBox();
      this.label19 = new Label();
      this.groupBox1 = new GroupBox();
      this.label6 = new Label();
      this.nudXbmcConnectionInterval = new NumericUpDown();
      this.label5 = new Label();
      this.tbXbmcConnectionPassword = new TextBox();
      this.label4 = new Label();
      this.tbXbmcConnectionUsername = new TextBox();
      this.label3 = new Label();
      this.tbXbmcConnectionPort = new TextBox();
      this.label2 = new Label();
      this.tbXbmcConnectionIp = new TextBox();
      this.label1 = new Label();
      this.groupBox8 = new GroupBox();
      this.label18 = new Label();
      this.groupBox16 = new GroupBox();
      this.label17 = new Label();
      this.label11 = new Label();
      this.nudControlModeIdleDelay = new NumericUpDown();
      this.cbControlModeIdle = new CheckBox();
      this.label9 = new Label();
      this.nudXbmcControlModeUpdateInterval = new NumericUpDown();
      this.cbXbmcControlModeEnable = new CheckBox();
      this.groupBox11 = new GroupBox();
      this.label21 = new Label();
      this.groupBox10 = new GroupBox();
      this.label20 = new Label();
      this.groupBox5 = new GroupBox();
      this.cbXbmcIdleTime = new CheckBox();
      this.cbXbmcIdleStaticTextEnable = new CheckBox();
      this.cbXbmcIdleShowSeconds = new CheckBox();
      this.tbXbmcIdleStaticText = new TextBox();
      this.groupBox6 = new GroupBox();
      this.groupBox7 = new Panel();
      this.label22 = new Label();
      this.rbProgressStyle2 = new RadioButton();
      this.rbProgressStyle1 = new RadioButton();
      this.label16 = new Label();
      this.label15 = new Label();
      this.nudAlternateDisplayDelay = new NumericUpDown();
      this.rbShowInfoPlayingBoth = new RadioButton();
      this.rbShowInfoPlayingTime = new RadioButton();
      this.rbShowInfoPlayingProgressBar = new RadioButton();
      this.label7 = new Label();
      this.label10 = new Label();
      this.label14 = new Label();
      this.label13 = new Label();
      this.trayIcon = new NotifyIcon(this.components);
      this.trayMenu = new ContextMenuStrip(this.components);
      this.trayMenuOpen = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.trayMenuXBMC = new ToolStripMenuItem();
      this.trayMenuImon = new ToolStripMenuItem();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.trayMenuClose = new ToolStripMenuItem();
      this.bNavigationXbmc = new NavigationButton();
      this.bNavigationImon = new NavigationButton();
      this.bNavigationGeneral = new NavigationButton();
      this.tbXbmcTvSingleText = new SuggestionBox();
      this.tbXbmcMovieSingleText = new SuggestionBox();
      this.tbXbmcMusicVideoSingleText = new SuggestionBox();
      this.tbXbmcMusicSingleText = new SuggestionBox();
      this.menu.SuspendLayout();
      this.splitter.Panel1.SuspendLayout();
      this.splitter.Panel2.SuspendLayout();
      this.splitter.SuspendLayout();
      this.pNavigation.SuspendLayout();
      this.tabOptions.SuspendLayout();
      this.tpImon.SuspendLayout();
      this.groupBox13.SuspendLayout();
      this.nudImonLcdScrollingDelay.BeginInit();
      this.groupBox3.SuspendLayout();
      this.tpGeneral.SuspendLayout();
      this.groupBox15.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tpXBMC.SuspendLayout();
      this.groupBox9.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.nudXbmcConnectionInterval.BeginInit();
      this.groupBox8.SuspendLayout();
      this.groupBox16.SuspendLayout();
      this.nudControlModeIdleDelay.BeginInit();
      this.nudXbmcControlModeUpdateInterval.BeginInit();
      this.groupBox11.SuspendLayout();
      this.groupBox10.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.groupBox7.SuspendLayout();
      this.nudAlternateDisplayDelay.BeginInit();
      this.trayMenu.SuspendLayout();
      this.SuspendLayout();
      this.menu.BackColor = System.Drawing.Color.Transparent;
      this.menu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.miGeneral,
        (ToolStripItem) this.miXbmc,
        (ToolStripItem) this.miImon,
        (ToolStripItem) this.miAbout
      });
      this.menu.Location = new Point(0, 0);
      this.menu.Name = "menu";
      this.menu.Size = new Size(485, 24);
      this.menu.TabIndex = 0;
      this.menu.Text = "menuStrip1";
      this.miGeneral.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.miGeneralClose
      });
      this.miGeneral.Name = "miGeneral";
      this.miGeneral.Size = new Size(59, 20);
      this.miGeneral.Text = "General";
      this.miGeneralClose.Name = "miGeneralClose";
      this.miGeneralClose.ShortcutKeys = Keys.W | Keys.Control;
      this.miGeneralClose.Size = new Size(148, 22);
      this.miGeneralClose.Text = "Close";
      this.miGeneralClose.Click += new EventHandler(this.miGeneralClose_Click);
      this.miXbmc.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.miXbmcConnect,
        (ToolStripItem) this.miXbmcDisconnect,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.miXbmcInfo
      });
      this.miXbmc.Name = "miXbmc";
      this.miXbmc.Size = new Size(52, 20);
      this.miXbmc.Text = nameof (XBMC);
      this.miXbmcConnect.Name = "miXbmcConnect";
      this.miXbmcConnect.Size = new Size(154, 22);
      this.miXbmcConnect.Text = "Connect";
      this.miXbmcConnect.Click += new EventHandler(this.miXbmcConnect_Click);
      this.miXbmcDisconnect.Enabled = false;
      this.miXbmcDisconnect.Name = "miXbmcDisconnect";
      this.miXbmcDisconnect.Size = new Size(154, 22);
      this.miXbmcDisconnect.Text = "Disconnect";
      this.miXbmcDisconnect.Click += new EventHandler(this.miXbmcDisconnect_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(151, 6);
      this.miXbmcInfo.Enabled = false;
      this.miXbmcInfo.Name = "miXbmcInfo";
      this.miXbmcInfo.Size = new Size(154, 22);
      this.miXbmcInfo.Text = "Build unknown";
      this.miImon.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.miImonInitialize,
        (ToolStripItem) this.miImonUninitialize
      });
      this.miImon.Enabled = false;
      this.miImon.Name = "miImon";
      this.miImon.Size = new Size(51, 20);
      this.miImon.Text = "iMON";
      this.miImonInitialize.Name = "miImonInitialize";
      this.miImonInitialize.Size = new Size(132, 22);
      this.miImonInitialize.Text = "Initialize";
      this.miImonInitialize.Click += new EventHandler(this.miImonInitialize_Click);
      this.miImonUninitialize.Name = "miImonUninitialize";
      this.miImonUninitialize.Size = new Size(132, 22);
      this.miImonUninitialize.Text = "Uninitialize";
      this.miImonUninitialize.Click += new EventHandler(this.miImonUninitialize_Click);
      this.miAbout.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.miAboutXbmcOniMon
      });
      this.miAbout.Name = "miAbout";
      this.miAbout.Size = new Size(52, 20);
      this.miAbout.Text = "About";
      this.miAboutXbmcOniMon.Name = "miAboutXbmcOniMon";
      this.miAboutXbmcOniMon.Size = new Size(191, 22);
      this.miAboutXbmcOniMon.Text = "About XBMC on iMon";
      this.miAboutXbmcOniMon.Click += new EventHandler(this.miAboutXbmcOniMon_Click);
      this.splitter.Dock = DockStyle.Fill;
      this.splitter.FixedPanel = FixedPanel.Panel1;
      this.splitter.Location = new Point(0, 24);
      this.splitter.Name = "splitter";
      this.splitter.Panel1.Controls.Add((Control) this.pNavigation);
      this.splitter.Panel2.Controls.Add((Control) this.tabOptions);
      this.splitter.Size = new Size(485, 429);
      this.splitter.SplitterDistance = 83;
      this.splitter.TabIndex = 1;
      this.pNavigation.BackColor = System.Drawing.Color.White;
      this.pNavigation.BorderStyle = BorderStyle.FixedSingle;
      this.pNavigation.Controls.Add((Control) this.bNavigationXbmc);
      this.pNavigation.Controls.Add((Control) this.bNavigationImon);
      this.pNavigation.Controls.Add((Control) this.bNavigationGeneral);
      this.pNavigation.Location = new Point(4, 4);
      this.pNavigation.Name = "pNavigation";
      this.pNavigation.Size = new Size(76, 425);
      this.pNavigation.TabIndex = 0;
      this.iLOptions.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("iLOptions.ImageStream");
      this.iLOptions.TransparentColor = System.Drawing.Color.Transparent;
      this.iLOptions.Images.SetKeyName(0, "General");
      this.iLOptions.Images.SetKeyName(1, "GeneralActive");
      this.iLOptions.Images.SetKeyName(2, "GeneralHover");
      this.iLOptions.Images.SetKeyName(3, "iMON");
      this.iLOptions.Images.SetKeyName(4, "iMONActive");
      this.iLOptions.Images.SetKeyName(5, "iMONHover");
      this.iLOptions.Images.SetKeyName(6, nameof (XBMC));
      this.iLOptions.Images.SetKeyName(7, "XBMCActive");
      this.iLOptions.Images.SetKeyName(8, "XBMCHover");
      this.tabOptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.tabOptions.Controls.Add((Control) this.tpImon);
      this.tabOptions.Controls.Add((Control) this.tpGeneral);
      this.tabOptions.Controls.Add((Control) this.tpXBMC);
      this.tabOptions.Location = new Point(-13, -23);
      this.tabOptions.Name = "tabOptions";
      this.tabOptions.SelectedIndex = 0;
      this.tabOptions.Size = new Size(416, 455);
      this.tabOptions.TabIndex = 0;
      this.tpImon.BackColor = SystemColors.Control;
      this.tpImon.Controls.Add((Control) this.groupBox13);
      this.tpImon.Controls.Add((Control) this.groupBox3);
      this.tpImon.Location = new Point(4, 22);
      this.tpImon.Name = "tpImon";
      this.tpImon.Padding = new Padding(3);
      this.tpImon.Size = new Size(408, 429);
      this.tpImon.TabIndex = 1;
      this.tpImon.Text = "iMON";
      this.groupBox13.Controls.Add((Control) this.label12);
      this.groupBox13.Controls.Add((Control) this.nudImonLcdScrollingDelay);
      this.groupBox13.Controls.Add((Control) this.label8);
      this.groupBox13.Location = new Point(10, 81);
      this.groupBox13.Name = "groupBox13";
      this.groupBox13.Size = new Size(386, 47);
      this.groupBox13.TabIndex = 1;
      this.groupBox13.TabStop = false;
      this.groupBox13.Text = "VFD text scrolling";
      this.label12.AutoSize = true;
      this.label12.Location = new Point(106, 21);
      this.label12.Name = "label12";
      this.label12.Size = new Size(63, 13);
      this.label12.TabIndex = 13;
      this.label12.Text = "milliseconds";
      this.nudImonLcdScrollingDelay.Increment = new Decimal(new int[4]
      {
        50,
        0,
        0,
        0
      });
      this.nudImonLcdScrollingDelay.Location = new Point(50, 19);
      this.nudImonLcdScrollingDelay.Maximum = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.nudImonLcdScrollingDelay.Name = "nudImonLcdScrollingDelay";
      this.nudImonLcdScrollingDelay.Size = new Size(50, 20);
      this.nudImonLcdScrollingDelay.TabIndex = 1;
      this.nudImonLcdScrollingDelay.TextAlign = HorizontalAlignment.Right;
      this.nudImonLcdScrollingDelay.Value = new Decimal(new int[4]
      {
        500,
        0,
        0,
        0
      });
      this.label8.AutoSize = true;
      this.label8.Location = new Point(7, 21);
      this.label8.Name = "label8";
      this.label8.Size = new Size(37, 13);
      this.label8.TabIndex = 0;
      this.label8.Text = "Delay:";
      this.groupBox3.Controls.Add((Control) this.cbImonGeneralUninitializeOnError);
      this.groupBox3.Controls.Add((Control) this.cbImonGeneralAutoInitialize);
      this.groupBox3.Location = new Point(10, 6);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(386, 68);
      this.groupBox3.TabIndex = 0;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "General";
      this.cbImonGeneralUninitializeOnError.AutoSize = true;
      this.cbImonGeneralUninitializeOnError.Location = new Point(7, 44);
      this.cbImonGeneralUninitializeOnError.Name = "cbImonGeneralUninitializeOnError";
      this.cbImonGeneralUninitializeOnError.Size = new Size(209, 17);
      this.cbImonGeneralUninitializeOnError.TabIndex = 1;
      this.cbImonGeneralUninitializeOnError.Text = "Uninitialize when iMON reports an error";
      this.cbImonGeneralUninitializeOnError.UseVisualStyleBackColor = true;
      this.cbImonGeneralAutoInitialize.AutoSize = true;
      this.cbImonGeneralAutoInitialize.Location = new Point(7, 20);
      this.cbImonGeneralAutoInitialize.Name = "cbImonGeneralAutoInitialize";
      this.cbImonGeneralAutoInitialize.Size = new Size(253, 17);
      this.cbImonGeneralAutoInitialize.TabIndex = 0;
      this.cbImonGeneralAutoInitialize.Text = "Automatically initialize when XBMC is connected";
      this.cbImonGeneralAutoInitialize.UseVisualStyleBackColor = true;
      this.tpGeneral.BackColor = SystemColors.Control;
      this.tpGeneral.Controls.Add((Control) this.groupBox15);
      this.tpGeneral.Controls.Add((Control) this.groupBox4);
      this.tpGeneral.Controls.Add((Control) this.groupBox2);
      this.tpGeneral.Location = new Point(4, 22);
      this.tpGeneral.Name = "tpGeneral";
      this.tpGeneral.Padding = new Padding(3);
      this.tpGeneral.Size = new Size(408, 429);
      this.tpGeneral.TabIndex = 0;
      this.tpGeneral.Text = "General";
      this.groupBox15.Controls.Add((Control) this.cbGeneralDebugEnable);
      this.groupBox15.Location = new Point(10, 220);
      this.groupBox15.Name = "groupBox15";
      this.groupBox15.Size = new Size(385, 45);
      this.groupBox15.TabIndex = 2;
      this.groupBox15.TabStop = false;
      this.groupBox15.Text = "Debugging";
      this.cbGeneralDebugEnable.AutoSize = true;
      this.cbGeneralDebugEnable.Location = new Point(6, 20);
      this.cbGeneralDebugEnable.Name = "cbGeneralDebugEnable";
      this.cbGeneralDebugEnable.Size = new Size(206, 17);
      this.cbGeneralDebugEnable.TabIndex = 0;
      this.cbGeneralDebugEnable.Text = "Enable detailed logging into debug.log";
      this.cbGeneralDebugEnable.UseVisualStyleBackColor = true;
      this.groupBox4.Controls.Add((Control) this.cbGeneralTrayDisableBalloonTips);
      this.groupBox4.Controls.Add((Control) this.cbGeneralTrayHideOnMinimize);
      this.groupBox4.Controls.Add((Control) this.cbGeneralTrayHideOnClose);
      this.groupBox4.Controls.Add((Control) this.cbGeneralTrayStartMinimized);
      this.groupBox4.Controls.Add((Control) this.cbGeneralTrayEnabled);
      this.groupBox4.Location = new Point(10, 80);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(386, 134);
      this.groupBox4.TabIndex = 1;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Tray Icon";
      this.cbGeneralTrayDisableBalloonTips.AutoSize = true;
      this.cbGeneralTrayDisableBalloonTips.Location = new Point(25, 112);
      this.cbGeneralTrayDisableBalloonTips.Name = "cbGeneralTrayDisableBalloonTips";
      this.cbGeneralTrayDisableBalloonTips.Size = new Size(122, 17);
      this.cbGeneralTrayDisableBalloonTips.TabIndex = 4;
      this.cbGeneralTrayDisableBalloonTips.Text = "Disable Balloon Tips";
      this.cbGeneralTrayDisableBalloonTips.UseVisualStyleBackColor = true;
      this.cbGeneralTrayHideOnMinimize.AutoSize = true;
      this.cbGeneralTrayHideOnMinimize.Location = new Point(25, 66);
      this.cbGeneralTrayHideOnMinimize.Name = "cbGeneralTrayHideOnMinimize";
      this.cbGeneralTrayHideOnMinimize.Size = new Size(205, 17);
      this.cbGeneralTrayHideOnMinimize.TabIndex = 3;
      this.cbGeneralTrayHideOnMinimize.Text = "Hide in Windows tray when minimizing";
      this.cbGeneralTrayHideOnMinimize.UseVisualStyleBackColor = true;
      this.cbGeneralTrayHideOnClose.AutoSize = true;
      this.cbGeneralTrayHideOnClose.Location = new Point(25, 89);
      this.cbGeneralTrayHideOnClose.Name = "cbGeneralTrayHideOnClose";
      this.cbGeneralTrayHideOnClose.Size = new Size(191, 17);
      this.cbGeneralTrayHideOnClose.TabIndex = 2;
      this.cbGeneralTrayHideOnClose.Text = "Hide in Windows tray when closing";
      this.cbGeneralTrayHideOnClose.UseVisualStyleBackColor = true;
      this.cbGeneralTrayStartMinimized.AutoSize = true;
      this.cbGeneralTrayStartMinimized.Location = new Point(25, 43);
      this.cbGeneralTrayStartMinimized.Name = "cbGeneralTrayStartMinimized";
      this.cbGeneralTrayStartMinimized.Size = new Size(175, 17);
      this.cbGeneralTrayStartMinimized.TabIndex = 1;
      this.cbGeneralTrayStartMinimized.Text = "Start minimized to Windows tray";
      this.cbGeneralTrayStartMinimized.UseVisualStyleBackColor = true;
      this.cbGeneralTrayEnabled.AutoSize = true;
      this.cbGeneralTrayEnabled.Location = new Point(7, 20);
      this.cbGeneralTrayEnabled.Name = "cbGeneralTrayEnabled";
      this.cbGeneralTrayEnabled.Size = new Size(232, 17);
      this.cbGeneralTrayEnabled.TabIndex = 0;
      this.cbGeneralTrayEnabled.Text = "Show Windows tray icon with current status";
      this.cbGeneralTrayEnabled.UseVisualStyleBackColor = true;
      this.cbGeneralTrayEnabled.CheckedChanged += new EventHandler(this.cbGeneralTrayEnabled_CheckedChanged);
      this.groupBox2.Controls.Add((Control) this.cbGeneralStartupConnect);
      this.groupBox2.Controls.Add((Control) this.cbGeneralStartupAuto);
      this.groupBox2.Location = new Point(10, 6);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(386, 68);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Startup";
      this.cbGeneralStartupConnect.AutoSize = true;
      this.cbGeneralStartupConnect.Location = new Point(7, 44);
      this.cbGeneralStartupConnect.Name = "cbGeneralStartupConnect";
      this.cbGeneralStartupConnect.Size = new Size(171, 17);
      this.cbGeneralStartupConnect.TabIndex = 1;
      this.cbGeneralStartupConnect.Text = "Connect with XBMC on startup";
      this.cbGeneralStartupConnect.UseVisualStyleBackColor = true;
      this.cbGeneralStartupAuto.AutoSize = true;
      this.cbGeneralStartupAuto.Location = new Point(7, 20);
      this.cbGeneralStartupAuto.Name = "cbGeneralStartupAuto";
      this.cbGeneralStartupAuto.Size = new Size(140, 17);
      this.cbGeneralStartupAuto.TabIndex = 0;
      this.cbGeneralStartupAuto.Text = "Auto start with Windows";
      this.cbGeneralStartupAuto.UseVisualStyleBackColor = true;
      this.tpXBMC.AutoScroll = true;
      this.tpXBMC.BackColor = SystemColors.Control;
      this.tpXBMC.Controls.Add((Control) this.groupBox9);
      this.tpXBMC.Controls.Add((Control) this.groupBox1);
      this.tpXBMC.Controls.Add((Control) this.groupBox8);
      this.tpXBMC.Controls.Add((Control) this.groupBox16);
      this.tpXBMC.Controls.Add((Control) this.groupBox11);
      this.tpXBMC.Controls.Add((Control) this.groupBox10);
      this.tpXBMC.Controls.Add((Control) this.groupBox5);
      this.tpXBMC.Controls.Add((Control) this.groupBox6);
      this.tpXBMC.Location = new Point(4, 22);
      this.tpXBMC.Name = "tpXBMC";
      this.tpXBMC.Padding = new Padding(3);
      this.tpXBMC.Size = new Size(408, 429);
      this.tpXBMC.TabIndex = 2;
      this.tpXBMC.Text = nameof (XBMC);
      this.groupBox9.Controls.Add((Control) this.label19);
      this.groupBox9.Controls.Add((Control) this.tbXbmcTvSingleText);
      this.groupBox9.Location = new Point(10, 483);
      this.groupBox9.Name = "groupBox9";
      this.groupBox9.Size = new Size(375, 74);
      this.groupBox9.TabIndex = 15;
      this.groupBox9.TabStop = false;
      this.groupBox9.Text = "Playing a tv episode";
      this.label19.AutoSize = true;
      this.label19.Location = new Point(15, 42);
      this.label19.Name = "label19";
      this.label19.Size = new Size(60, 13);
      this.label19.TabIndex = 10;
      this.label19.Text = "Single Text";
      this.groupBox1.Controls.Add((Control) this.label6);
      this.groupBox1.Controls.Add((Control) this.nudXbmcConnectionInterval);
      this.groupBox1.Controls.Add((Control) this.label5);
      this.groupBox1.Controls.Add((Control) this.tbXbmcConnectionPassword);
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.tbXbmcConnectionUsername);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.tbXbmcConnectionPort);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.tbXbmcConnectionIp);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(10, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(375, 99);
      this.groupBox1.TabIndex = 10;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Connection";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(140, 76);
      this.label6.Name = "label6";
      this.label6.Size = new Size(47, 13);
      this.label6.TabIndex = 10;
      this.label6.Text = "seconds";
      this.nudXbmcConnectionInterval.Location = new Point(94, 73);
      this.nudXbmcConnectionInterval.Maximum = new Decimal(new int[4]
      {
        60,
        0,
        0,
        0
      });
      this.nudXbmcConnectionInterval.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.nudXbmcConnectionInterval.Name = "nudXbmcConnectionInterval";
      this.nudXbmcConnectionInterval.Size = new Size(40, 20);
      this.nudXbmcConnectionInterval.TabIndex = 9;
      this.nudXbmcConnectionInterval.Value = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.label5.AutoSize = true;
      this.label5.Location = new Point(6, 76);
      this.label5.Name = "label5";
      this.label5.Size = new Size(72, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "Retry interval:";
      this.tbXbmcConnectionPassword.Location = new Point(251, 47);
      this.tbXbmcConnectionPassword.Name = "tbXbmcConnectionPassword";
      this.tbXbmcConnectionPassword.PasswordChar = '*';
      this.tbXbmcConnectionPassword.Size = new Size(118, 20);
      this.tbXbmcConnectionPassword.TabIndex = 7;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(189, 50);
      this.label4.Name = "label4";
      this.label4.Size = new Size(56, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Password:";
      this.tbXbmcConnectionUsername.Location = new Point(70, 47);
      this.tbXbmcConnectionUsername.Name = "tbXbmcConnectionUsername";
      this.tbXbmcConnectionUsername.Size = new Size(113, 20);
      this.tbXbmcConnectionUsername.TabIndex = 5;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(6, 50);
      this.label3.Name = "label3";
      this.label3.Size = new Size(58, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Username:";
      this.tbXbmcConnectionPort.Location = new Point(296, 19);
      this.tbXbmcConnectionPort.Name = "tbXbmcConnectionPort";
      this.tbXbmcConnectionPort.Size = new Size(73, 20);
      this.tbXbmcConnectionPort.TabIndex = 3;
      this.label2.Location = new Point((int) byte.MaxValue, 23);
      this.label2.Name = "label2";
      this.label2.Size = new Size(35, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Port:";
      this.tbXbmcConnectionIp.Location = new Point(34, 20);
      this.tbXbmcConnectionIp.Name = "tbXbmcConnectionIp";
      this.tbXbmcConnectionIp.Size = new Size(215, 20);
      this.tbXbmcConnectionIp.TabIndex = 1;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(6, 23);
      this.label1.Name = "label1";
      this.label1.Size = new Size(20, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "IP:";
      this.groupBox8.Controls.Add((Control) this.label18);
      this.groupBox8.Controls.Add((Control) this.tbXbmcMovieSingleText);
      this.groupBox8.Location = new Point(10, 403);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new Size(375, 74);
      this.groupBox8.TabIndex = 14;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "Playing a movie";
      this.label18.AutoSize = true;
      this.label18.Location = new Point(17, 42);
      this.label18.Name = "label18";
      this.label18.Size = new Size(60, 13);
      this.label18.TabIndex = 6;
      this.label18.Text = "Single Text";
      this.groupBox16.Controls.Add((Control) this.label17);
      this.groupBox16.Controls.Add((Control) this.label11);
      this.groupBox16.Controls.Add((Control) this.nudControlModeIdleDelay);
      this.groupBox16.Controls.Add((Control) this.cbControlModeIdle);
      this.groupBox16.Controls.Add((Control) this.label9);
      this.groupBox16.Controls.Add((Control) this.nudXbmcControlModeUpdateInterval);
      this.groupBox16.Controls.Add((Control) this.cbXbmcControlModeEnable);
      this.groupBox16.Location = new Point(10, 188);
      this.groupBox16.Name = "groupBox16";
      this.groupBox16.Size = new Size(375, 100);
      this.groupBox16.TabIndex = 19;
      this.groupBox16.TabStop = false;
      this.groupBox16.Text = "Control mode (highly experimental)";
      this.label17.AutoSize = true;
      this.label17.Location = new Point(194, 70);
      this.label17.Name = "label17";
      this.label17.Size = new Size(43, 13);
      this.label17.TabIndex = 20;
      this.label17.Text = "minut(s)";
      this.label11.AutoSize = true;
      this.label11.Location = new Point(194, 42);
      this.label11.Name = "label11";
      this.label11.Size = new Size(63, 13);
      this.label11.TabIndex = 16;
      this.label11.Text = "milliseconds";
      this.nudControlModeIdleDelay.Enabled = false;
      this.nudControlModeIdleDelay.Location = new Point(145, 67);
      this.nudControlModeIdleDelay.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.nudControlModeIdleDelay.Name = "nudControlModeIdleDelay";
      this.nudControlModeIdleDelay.Size = new Size(47, 20);
      this.nudControlModeIdleDelay.TabIndex = 19;
      this.nudControlModeIdleDelay.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.cbControlModeIdle.AutoSize = true;
      this.cbControlModeIdle.Enabled = false;
      this.cbControlModeIdle.Location = new Point(9, 70);
      this.cbControlModeIdle.Name = "cbControlModeIdle";
      this.cbControlModeIdle.Size = new Size(103, 17);
      this.cbControlModeIdle.TabIndex = 18;
      this.cbControlModeIdle.Text = "Display idle after";
      this.cbControlModeIdle.UseVisualStyleBackColor = true;
      this.cbControlModeIdle.CheckedChanged += new EventHandler(this.cbControlModeIdle_CheckedChanged);
      this.label9.AutoSize = true;
      this.label9.Location = new Point(63, 42);
      this.label9.Name = "label9";
      this.label9.Size = new Size(79, 13);
      this.label9.TabIndex = 15;
      this.label9.Text = "Update interval";
      this.nudXbmcControlModeUpdateInterval.Enabled = false;
      this.nudXbmcControlModeUpdateInterval.Increment = new Decimal(new int[4]
      {
        50,
        0,
        0,
        0
      });
      this.nudXbmcControlModeUpdateInterval.Location = new Point(145, 39);
      this.nudXbmcControlModeUpdateInterval.Maximum = new Decimal(new int[4]
      {
        10000,
        0,
        0,
        0
      });
      this.nudXbmcControlModeUpdateInterval.Name = "nudXbmcControlModeUpdateInterval";
      this.nudXbmcControlModeUpdateInterval.Size = new Size(46, 20);
      this.nudXbmcControlModeUpdateInterval.TabIndex = 14;
      this.nudXbmcControlModeUpdateInterval.Value = new Decimal(new int[4]
      {
        500,
        0,
        0,
        0
      });
      this.cbXbmcControlModeEnable.AutoSize = true;
      this.cbXbmcControlModeEnable.Location = new Point(8, 20);
      this.cbXbmcControlModeEnable.Name = "cbXbmcControlModeEnable";
      this.cbXbmcControlModeEnable.Size = new Size(59, 17);
      this.cbXbmcControlModeEnable.TabIndex = 0;
      this.cbXbmcControlModeEnable.Text = "Enable";
      this.cbXbmcControlModeEnable.UseVisualStyleBackColor = true;
      this.cbXbmcControlModeEnable.CheckedChanged += new EventHandler(this.cbXbmcControlModeEnable_CheckedChanged);
      this.groupBox11.Controls.Add((Control) this.label21);
      this.groupBox11.Controls.Add((Control) this.tbXbmcMusicVideoSingleText);
      this.groupBox11.Location = new Point(10, 643);
      this.groupBox11.Name = "groupBox11";
      this.groupBox11.Size = new Size(375, 74);
      this.groupBox11.TabIndex = 17;
      this.groupBox11.TabStop = false;
      this.groupBox11.Text = "Playing a music video";
      this.label21.AutoSize = true;
      this.label21.Location = new Point(17, 42);
      this.label21.Name = "label21";
      this.label21.Size = new Size(60, 13);
      this.label21.TabIndex = 11;
      this.label21.Text = "Single Text";
      this.groupBox10.Controls.Add((Control) this.label20);
      this.groupBox10.Controls.Add((Control) this.tbXbmcMusicSingleText);
      this.groupBox10.Location = new Point(10, 563);
      this.groupBox10.Name = "groupBox10";
      this.groupBox10.Size = new Size(375, 74);
      this.groupBox10.TabIndex = 16;
      this.groupBox10.TabStop = false;
      this.groupBox10.Text = "Playing a song";
      this.label20.AutoSize = true;
      this.label20.Location = new Point(15, 42);
      this.label20.Name = "label20";
      this.label20.Size = new Size(60, 13);
      this.label20.TabIndex = 11;
      this.label20.Text = "Single Text";
      this.groupBox5.Controls.Add((Control) this.cbXbmcIdleTime);
      this.groupBox5.Controls.Add((Control) this.cbXbmcIdleStaticTextEnable);
      this.groupBox5.Controls.Add((Control) this.cbXbmcIdleShowSeconds);
      this.groupBox5.Controls.Add((Control) this.tbXbmcIdleStaticText);
      this.groupBox5.Location = new Point(10, 111);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new Size(375, 70);
      this.groupBox5.TabIndex = 11;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Idle";
      this.cbXbmcIdleTime.AutoSize = true;
      this.cbXbmcIdleTime.Location = new Point(219, 19);
      this.cbXbmcIdleTime.Name = "cbXbmcIdleTime";
      this.cbXbmcIdleTime.Size = new Size(111, 17);
      this.cbXbmcIdleTime.TabIndex = 6;
      this.cbXbmcIdleTime.Text = "Show current time";
      this.cbXbmcIdleTime.UseVisualStyleBackColor = true;
      this.cbXbmcIdleTime.CheckedChanged += new EventHandler(this.cbXbmcIdleTime_CheckedChanged);
      this.cbXbmcIdleStaticTextEnable.AutoSize = true;
      this.cbXbmcIdleStaticTextEnable.Location = new Point(20, 19);
      this.cbXbmcIdleStaticTextEnable.Name = "cbXbmcIdleStaticTextEnable";
      this.cbXbmcIdleStaticTextEnable.Size = new Size(101, 17);
      this.cbXbmcIdleStaticTextEnable.TabIndex = 5;
      this.cbXbmcIdleStaticTextEnable.Text = "Show static text";
      this.cbXbmcIdleStaticTextEnable.UseVisualStyleBackColor = true;
      this.cbXbmcIdleStaticTextEnable.CheckedChanged += new EventHandler(this.cbXbmcIdleStaticTextEnable_CheckedChanged);
      this.cbXbmcIdleShowSeconds.AutoSize = true;
      this.cbXbmcIdleShowSeconds.Enabled = false;
      this.cbXbmcIdleShowSeconds.Location = new Point(219, 45);
      this.cbXbmcIdleShowSeconds.Name = "cbXbmcIdleShowSeconds";
      this.cbXbmcIdleShowSeconds.Size = new Size(96, 17);
      this.cbXbmcIdleShowSeconds.TabIndex = 4;
      this.cbXbmcIdleShowSeconds.Text = "Show seconds";
      this.cbXbmcIdleShowSeconds.UseVisualStyleBackColor = true;
      this.tbXbmcIdleStaticText.Enabled = false;
      this.tbXbmcIdleStaticText.Location = new Point(34, 43);
      this.tbXbmcIdleStaticText.Name = "tbXbmcIdleStaticText";
      this.tbXbmcIdleStaticText.Size = new Size(149, 20);
      this.tbXbmcIdleStaticText.TabIndex = 2;
      this.groupBox6.Controls.Add((Control) this.groupBox7);
      this.groupBox6.Controls.Add((Control) this.label16);
      this.groupBox6.Controls.Add((Control) this.label15);
      this.groupBox6.Controls.Add((Control) this.nudAlternateDisplayDelay);
      this.groupBox6.Controls.Add((Control) this.rbShowInfoPlayingBoth);
      this.groupBox6.Controls.Add((Control) this.rbShowInfoPlayingTime);
      this.groupBox6.Controls.Add((Control) this.rbShowInfoPlayingProgressBar);
      this.groupBox6.Location = new Point(10, 295);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new Size(375, 102);
      this.groupBox6.TabIndex = 12;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Infos during playback";
      this.groupBox7.Controls.Add((Control) this.label22);
      this.groupBox7.Controls.Add((Control) this.rbProgressStyle2);
      this.groupBox7.Controls.Add((Control) this.rbProgressStyle1);
      this.groupBox7.Enabled = false;
      this.groupBox7.Location = new Point(10, 68);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new Size(360, 29);
      this.groupBox7.TabIndex = 22;
      this.label22.AutoSize = true;
      this.label22.Location = new Point(66, 7);
      this.label22.Name = "label22";
      this.label22.Size = new Size(90, 13);
      this.label22.TabIndex = 20;
      this.label22.Text = "Progress bar style";
      this.rbProgressStyle2.AutoSize = true;
      this.rbProgressStyle2.Location = new Point(268, 5);
      this.rbProgressStyle2.Name = "rbProgressStyle2";
      this.rbProgressStyle2.Size = new Size(89, 17);
      this.rbProgressStyle2.TabIndex = 19;
      this.rbProgressStyle2.TabStop = true;
      this.rbProgressStyle2.Text = "Vertical block";
      this.rbProgressStyle2.UseVisualStyleBackColor = true;
      this.rbProgressStyle1.AutoSize = true;
      this.rbProgressStyle1.Location = new Point(163, 5);
      this.rbProgressStyle1.Name = "rbProgressStyle1";
      this.rbProgressStyle1.Size = new Size(99, 17);
      this.rbProgressStyle1.TabIndex = 18;
      this.rbProgressStyle1.TabStop = true;
      this.rbProgressStyle1.Text = "horizontal block";
      this.rbProgressStyle1.UseVisualStyleBackColor = true;
      this.label16.AutoSize = true;
      this.label16.Location = new Point(285, 45);
      this.label16.Name = "label16";
      this.label16.Size = new Size(47, 13);
      this.label16.TabIndex = 17;
      this.label16.Text = "seconds";
      this.label15.AutoSize = true;
      this.label15.Location = new Point(110, 45);
      this.label15.Name = "label15";
      this.label15.Size = new Size(112, 13);
      this.label15.TabIndex = 5;
      this.label15.Text = "Alternate display delay";
      this.nudAlternateDisplayDelay.Enabled = false;
      this.nudAlternateDisplayDelay.Location = new Point(225, 42);
      this.nudAlternateDisplayDelay.Maximum = new Decimal(new int[4]
      {
        60,
        0,
        0,
        0
      });
      this.nudAlternateDisplayDelay.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.nudAlternateDisplayDelay.Name = "nudAlternateDisplayDelay";
      this.nudAlternateDisplayDelay.Size = new Size(57, 20);
      this.nudAlternateDisplayDelay.TabIndex = 4;
      this.nudAlternateDisplayDelay.Value = new Decimal(new int[4]
      {
        60,
        0,
        0,
        0
      });
      this.rbShowInfoPlayingBoth.AutoSize = true;
      this.rbShowInfoPlayingBoth.Location = new Point(208, 19);
      this.rbShowInfoPlayingBoth.Name = "rbShowInfoPlayingBoth";
      this.rbShowInfoPlayingBoth.Size = new Size(144, 17);
      this.rbShowInfoPlayingBoth.TabIndex = 3;
      this.rbShowInfoPlayingBoth.TabStop = true;
      this.rbShowInfoPlayingBoth.Text = "Show time && progress bar";
      this.rbShowInfoPlayingBoth.UseVisualStyleBackColor = true;
      this.rbShowInfoPlayingBoth.CheckedChanged += new EventHandler(this.rbShowInfoPlayingBoth_CheckedChanged);
      this.rbShowInfoPlayingTime.AutoSize = true;
      this.rbShowInfoPlayingTime.Location = new Point(9, 19);
      this.rbShowInfoPlayingTime.Name = "rbShowInfoPlayingTime";
      this.rbShowInfoPlayingTime.Size = new Size(74, 17);
      this.rbShowInfoPlayingTime.TabIndex = 2;
      this.rbShowInfoPlayingTime.TabStop = true;
      this.rbShowInfoPlayingTime.Text = "Show time";
      this.rbShowInfoPlayingTime.UseVisualStyleBackColor = true;
      this.rbShowInfoPlayingProgressBar.AutoSize = true;
      this.rbShowInfoPlayingProgressBar.Location = new Point(89, 19);
      this.rbShowInfoPlayingProgressBar.Name = "rbShowInfoPlayingProgressBar";
      this.rbShowInfoPlayingProgressBar.Size = new Size(113, 17);
      this.rbShowInfoPlayingProgressBar.TabIndex = 1;
      this.rbShowInfoPlayingProgressBar.TabStop = true;
      this.rbShowInfoPlayingProgressBar.Text = "Show progress bar";
      this.rbShowInfoPlayingProgressBar.UseVisualStyleBackColor = true;
      this.rbShowInfoPlayingProgressBar.CheckedChanged += new EventHandler(this.rbShowInfoPlayingProgressBar_CheckedChanged);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(4, 24);
      this.label7.Name = "label7";
      this.label7.Size = new Size(28, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Text";
      this.label10.AutoSize = true;
      this.label10.Location = new Point(3, 24);
      this.label10.Name = "label10";
      this.label10.Size = new Size(28, 13);
      this.label10.TabIndex = 7;
      this.label10.Text = "Text";
      this.label14.AutoSize = true;
      this.label14.Location = new Point(3, 22);
      this.label14.Name = "label14";
      this.label14.Size = new Size(28, 13);
      this.label14.TabIndex = 10;
      this.label14.Text = "Text";
      this.label13.AutoSize = true;
      this.label13.Location = new Point(3, 22);
      this.label13.Name = "label13";
      this.label13.Size = new Size(28, 13);
      this.label13.TabIndex = 10;
      this.label13.Text = "Text";
      this.trayIcon.Icon = (Icon) componentResourceManager.GetObject("trayIcon.Icon");
      this.trayIcon.Text = "XBMC on iMon\r\nDisconnected";
      this.trayIcon.DoubleClick += new EventHandler(this.trayIcon_DoubleClick);
      this.trayMenu.Items.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.trayMenuOpen,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.trayMenuXBMC,
        (ToolStripItem) this.trayMenuImon,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.trayMenuClose
      });
      this.trayMenu.Name = "trayMenu";
      this.trayMenu.RenderMode = ToolStripRenderMode.Professional;
      this.trayMenu.Size = new Size(159, 104);
      this.trayMenuOpen.Name = "trayMenuOpen";
      this.trayMenuOpen.Size = new Size(158, 22);
      this.trayMenuOpen.Text = "Open";
      this.trayMenuOpen.Click += new EventHandler(this.trayMenuOpen_Click);
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(155, 6);
      this.trayMenuXBMC.Name = "trayMenuXBMC";
      this.trayMenuXBMC.Size = new Size(158, 22);
      this.trayMenuXBMC.Text = "XBMC: Connect";
      this.trayMenuXBMC.Click += new EventHandler(this.trayMenuXBMC_Click);
      this.trayMenuImon.Enabled = false;
      this.trayMenuImon.Name = "trayMenuImon";
      this.trayMenuImon.Size = new Size(158, 22);
      this.trayMenuImon.Text = "iMON: Initialize";
      this.trayMenuImon.Click += new EventHandler(this.trayMenuImon_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(155, 6);
      this.trayMenuClose.Name = "trayMenuClose";
      this.trayMenuClose.Size = new Size(158, 22);
      this.trayMenuClose.Text = "Close";
      this.trayMenuClose.Click += new EventHandler(this.trayMenuClose_Click);
      this.bNavigationXbmc.ActiveImageIndex = 7;
      this.bNavigationXbmc.BackColor = System.Drawing.Color.Transparent;
      this.bNavigationXbmc.DefaultImageIndex = 6;
      this.bNavigationXbmc.FlatAppearance.BorderSize = 0;
      this.bNavigationXbmc.FlatStyle = FlatStyle.Flat;
      this.bNavigationXbmc.HoverImageIndex = 8;
      this.bNavigationXbmc.ImageIndex = 6;
      this.bNavigationXbmc.ImageList = this.iLOptions;
      this.bNavigationXbmc.Location = new Point(-1, 147);
      this.bNavigationXbmc.Margin = new Padding(0);
      this.bNavigationXbmc.Name = "bNavigationXbmc";
      this.bNavigationXbmc.Size = new Size(74, 74);
      this.bNavigationXbmc.TabIndex = 5;
      this.bNavigationXbmc.UseVisualStyleBackColor = false;
      this.bNavigationXbmc.Click += new EventHandler(this.bNavigationXbmc_Click);
      this.bNavigationImon.ActiveImageIndex = 4;
      this.bNavigationImon.BackColor = System.Drawing.Color.Transparent;
      this.bNavigationImon.DefaultImageIndex = 3;
      this.bNavigationImon.FlatAppearance.BorderSize = 0;
      this.bNavigationImon.FlatStyle = FlatStyle.Flat;
      this.bNavigationImon.HoverImageIndex = 5;
      this.bNavigationImon.ImageIndex = 3;
      this.bNavigationImon.ImageList = this.iLOptions;
      this.bNavigationImon.Location = new Point(-1, 73);
      this.bNavigationImon.Margin = new Padding(0);
      this.bNavigationImon.Name = "bNavigationImon";
      this.bNavigationImon.Size = new Size(74, 74);
      this.bNavigationImon.TabIndex = 4;
      this.bNavigationImon.UseVisualStyleBackColor = false;
      this.bNavigationImon.Click += new EventHandler(this.bNavigationImon_Click);
      this.bNavigationGeneral.ActiveImageIndex = 1;
      this.bNavigationGeneral.BackColor = System.Drawing.Color.Transparent;
      this.bNavigationGeneral.DefaultImageIndex = 0;
      this.bNavigationGeneral.FlatAppearance.BorderSize = 0;
      this.bNavigationGeneral.FlatStyle = FlatStyle.Flat;
      this.bNavigationGeneral.HoverImageIndex = 2;
      this.bNavigationGeneral.ImageIndex = 0;
      this.bNavigationGeneral.ImageList = this.iLOptions;
      this.bNavigationGeneral.Location = new Point(-1, -1);
      this.bNavigationGeneral.Margin = new Padding(0);
      this.bNavigationGeneral.Name = "bNavigationGeneral";
      this.bNavigationGeneral.Size = new Size(74, 74);
      this.bNavigationGeneral.TabIndex = 3;
      this.bNavigationGeneral.UseVisualStyleBackColor = false;
      this.bNavigationGeneral.Click += new EventHandler(this.bNavigationGeneral_Click);
      this.tbXbmcTvSingleText.Delimiter = "%";
      this.tbXbmcTvSingleText.Location = new Point(18, 19);
      this.tbXbmcTvSingleText.MaximumRows = 2;
      this.tbXbmcTvSingleText.Name = "tbXbmcTvSingleText";
      this.tbXbmcTvSingleText.Size = new Size(336, 20);
      this.tbXbmcTvSingleText.StartAndEnd = true;
      this.tbXbmcTvSingleText.TabIndex = 9;
      this.tbXbmcMovieSingleText.Delimiter = "%";
      this.tbXbmcMovieSingleText.Location = new Point(20, 19);
      this.tbXbmcMovieSingleText.MaximumRows = 2;
      this.tbXbmcMovieSingleText.Name = "tbXbmcMovieSingleText";
      this.tbXbmcMovieSingleText.Size = new Size(336, 20);
      this.tbXbmcMovieSingleText.StartAndEnd = true;
      this.tbXbmcMovieSingleText.TabIndex = 5;
      this.tbXbmcMusicVideoSingleText.Delimiter = "%";
      this.tbXbmcMusicVideoSingleText.Location = new Point(20, 19);
      this.tbXbmcMusicVideoSingleText.MaximumRows = 2;
      this.tbXbmcMusicVideoSingleText.Name = "tbXbmcMusicVideoSingleText";
      this.tbXbmcMusicVideoSingleText.Size = new Size(336, 20);
      this.tbXbmcMusicVideoSingleText.StartAndEnd = true;
      this.tbXbmcMusicVideoSingleText.TabIndex = 9;
      this.tbXbmcMusicSingleText.Delimiter = "%";
      this.tbXbmcMusicSingleText.Location = new Point(20, 19);
      this.tbXbmcMusicSingleText.MaximumRows = 2;
      this.tbXbmcMusicSingleText.Name = "tbXbmcMusicSingleText";
      this.tbXbmcMusicSingleText.Size = new Size(336, 20);
      this.tbXbmcMusicSingleText.StartAndEnd = true;
      this.tbXbmcMusicSingleText.TabIndex = 9;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(485, 453);
      this.Controls.Add((Control) this.splitter);
      this.Controls.Add((Control) this.menu);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menu;
      this.MaximizeBox = false;
      this.Name = nameof (XBMC);
      this.Text = "XBMC on iMON";
      this.FormClosing += new FormClosingEventHandler(this.xbmc_FormClosing);
      this.Load += new EventHandler(this.xbmc_Load);
      this.Resize += new EventHandler(this.xbmc_Resize);
      this.menu.ResumeLayout(false);
      this.menu.PerformLayout();
      this.splitter.Panel1.ResumeLayout(false);
      this.splitter.Panel2.ResumeLayout(false);
      this.splitter.ResumeLayout(false);
      this.pNavigation.ResumeLayout(false);
      this.tabOptions.ResumeLayout(false);
      this.tpImon.ResumeLayout(false);
      this.groupBox13.ResumeLayout(false);
      this.groupBox13.PerformLayout();
      this.nudImonLcdScrollingDelay.EndInit();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.tpGeneral.ResumeLayout(false);
      this.groupBox15.ResumeLayout(false);
      this.groupBox15.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.tpXBMC.ResumeLayout(false);
      this.groupBox9.ResumeLayout(false);
      this.groupBox9.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.nudXbmcConnectionInterval.EndInit();
      this.groupBox8.ResumeLayout(false);
      this.groupBox8.PerformLayout();
      this.groupBox16.ResumeLayout(false);
      this.groupBox16.PerformLayout();
      this.nudControlModeIdleDelay.EndInit();
      this.nudXbmcControlModeUpdateInterval.EndInit();
      this.groupBox11.ResumeLayout(false);
      this.groupBox11.PerformLayout();
      this.groupBox10.ResumeLayout(false);
      this.groupBox10.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.groupBox7.ResumeLayout(false);
      this.groupBox7.PerformLayout();
      this.nudAlternateDisplayDelay.EndInit();
      this.trayMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public XBMC()
    {
      this.InitializeComponent();
      this.Opacity = 0.0;
      this.tabOptions.SelectTab(this.tpGeneral);
      this.bNavigationGeneral.Activate();
      this.trayIcon.ContextMenuStrip = this.trayMenu;
      this.constructor();
      this.aboutDialog = new AboutDialog();
    }

    [DllImport("user32")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    protected override void WndProc(ref Message msg)
    {
      if (msg.Msg == NativeMethods.WM_SHOWME)
      {
        this.show();
        iMon.XBMC.XBMC.SetForegroundWindow(msg.HWnd);
      }
      base.WndProc(ref msg);
    }

    private void xbmc_Load(object sender, EventArgs e)
    {
      if (Settings.Default.GeneralTrayEnabled && Settings.Default.GeneralTrayStartMinimized)
        this.BeginInvoke(new Action(() =>
        {
          Logging.Log("Minimizing application to tray at startup");
          this.Hide();
          this.Opacity = 1.0;
        }));
      else
        this.Opacity = 1.0;
    }

    private void xbmc_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.closing || Environment.HasShutdownStarted)
        return;
      this.close(false);
      e.Cancel = true;
    }

    private void xbmc_Resize(object sender, EventArgs e)
    {
      if (!Settings.Default.GeneralTrayEnabled || !Settings.Default.GeneralTrayHideOnMinimize)
        return;
      this.Hide();
    }

    private void miGeneralClose_Click(object sender, EventArgs e)
    {
      this.close(true);
    }

    private void miImonInitialize_Click(object sender, EventArgs e)
    {
      this.iMonInitialize();
    }

    private void miImonUninitialize_Click(object sender, EventArgs e)
    {
      this.iMonUninitialize();
    }

    private void miXbmcConnect_Click(object sender, EventArgs e)
    {
      this.xbmcConnect(false);
    }

    private void miXbmcDisconnect_Click(object sender, EventArgs e)
    {
      this.xbmcDisconnect(true);
    }

    private void bNavigationGeneral_Click(object sender, EventArgs e)
    {
      this.tabOptions.SelectTab(this.tpGeneral);
      this.bNavigationImon.Deactivate();
      this.bNavigationXbmc.Deactivate();
    }

    private void bNavigationImon_Click(object sender, EventArgs e)
    {
      this.tabOptions.SelectTab(this.tpImon);
      this.bNavigationGeneral.Deactivate();
      this.bNavigationXbmc.Deactivate();
    }

    private void bNavigationXbmc_Click(object sender, EventArgs e)
    {
      this.tabOptions.SelectTab(this.tpXBMC);
      this.bNavigationGeneral.Deactivate();
      this.bNavigationImon.Deactivate();
    }

    private void cbGeneralTrayEnabled_CheckedChanged(object sender, EventArgs e)
    {
      this.cbGeneralTrayStartMinimized.Enabled = this.cbGeneralTrayEnabled.Checked;
      this.cbGeneralTrayHideOnMinimize.Enabled = this.cbGeneralTrayEnabled.Checked;
      this.cbGeneralTrayHideOnClose.Enabled = this.cbGeneralTrayEnabled.Checked;
      this.cbGeneralTrayDisableBalloonTips.Enabled = this.cbGeneralTrayEnabled.Checked;
    }

    private void trayIcon_DoubleClick(object sender, EventArgs e)
    {
      this.show();
    }

    private void trayMenuOpen_Click(object sender, EventArgs e)
    {
      this.trayIcon_DoubleClick(sender, e);
    }

    private void trayMenuClose_Click(object sender, EventArgs e)
    {
      this.close(true);
    }

    private void trayMenuXBMC_Click(object sender, EventArgs e)
    {
      if (this.xbmc.IsAlive)
        this.xbmcDisconnect(true);
      else
        this.xbmcConnect(false);
    }

    private void trayMenuImon_Click(object sender, EventArgs e)
    {
      if (this.imon.IsInitialized)
        this.iMonUninitialize();
      else
        this.iMonInitialize();
    }

    private void cbXbmcIdleStaticTextEnable_CheckedChanged(object sender, EventArgs e)
    {
      this.tbXbmcIdleStaticText.Enabled = this.cbXbmcIdleStaticTextEnable.Checked;
    }

    private void cbXbmcIdleTime_CheckedChanged(object sender, EventArgs e)
    {
      this.cbXbmcIdleShowSeconds.Enabled = this.cbXbmcIdleTime.Checked;
    }

    private void cbXbmcControlModeEnable_CheckedChanged(object sender, EventArgs e)
    {
      this.nudXbmcControlModeUpdateInterval.Enabled = this.cbXbmcControlModeEnable.Checked;
      this.cbControlModeIdle.Enabled = this.cbXbmcControlModeEnable.Checked;
      this.nudControlModeIdleDelay.Enabled = this.cbControlModeIdle.Checked && this.cbXbmcControlModeEnable.Checked;
    }

    private void cbControlModeIdle_CheckedChanged(object sender, EventArgs e)
    {
      this.nudControlModeIdleDelay.Enabled = this.cbControlModeIdle.Checked;
    }

    private void rbShowInfoPlayingBoth_CheckedChanged(object sender, EventArgs e)
    {
      this.nudAlternateDisplayDelay.Enabled = this.rbShowInfoPlayingBoth.Checked;
      this.groupBox7.Enabled = ((RadioButton) sender).Checked;
    }

    private void rbShowInfoPlayingProgressBar_CheckedChanged(object sender, EventArgs e)
    {
      this.groupBox7.Enabled = ((RadioButton) sender).Checked;
    }

    private void miAboutXbmcOniMon_Click(object sender, EventArgs e)
    {
      int num = (int) this.aboutDialog.ShowDialog();
    }

    private void wrapperApi_StateChanged(object sender, iMonStateChangedEventArgs e)
    {
      this.iMonStateChanged(e.IsInitialized);
    }

    private void wrapperApi_Error(object sender, iMonErrorEventArgs e)
    {
      this.iMonError(e.Type);
    }

    private static void wrapperApiIMonLogError(object sender, iMonLogErrorEventArgs e)
    {
      Logging.Error("iMON", e.Message, e.Exception);
    }

    private static void wrapperApiIMonLog(object sender, iMonLogEventArgs e)
    {
      Logging.Log("iMON", e.Message, (Exception) null);
    }

    private static void wrapperApiXbmcLogError(object sender, XbmcJsonRpcLogErrorEventArgs e)
    {
      if (e.Exception != null && e.Exception is SocketException)
        return;
      Logging.Error(nameof (XBMC), e.Message, e.Exception);
    }

    private static void wrapperApiXbmcLog(object sender, XbmcJsonRpcLogEventArgs e)
    {
      Logging.Log(nameof (XBMC), e.Message, (Exception) null);
    }

    private delegate bool XbmcConnectingDelegate(bool auto);
  }
}

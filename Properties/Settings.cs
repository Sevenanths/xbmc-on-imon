// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.Properties.Settings
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace iMon.XBMC.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("127.0.0.1")]
    public string XbmcIp
    {
      get
      {
        return (string) this[nameof (XbmcIp)];
      }
      set
      {
        this[nameof (XbmcIp)] = (object) value;
      }
    }

    [DefaultSettingValue("8080")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public int XbmcPort
    {
      get
      {
        return (int) this[nameof (XbmcPort)];
      }
      set
      {
        this[nameof (XbmcPort)] = (object) value;
      }
    }

    [DefaultSettingValue("xbmc")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string XbmcUsername
    {
      get
      {
        return (string) this[nameof (XbmcUsername)];
      }
      set
      {
        this[nameof (XbmcUsername)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string XbmcPassword
    {
      get
      {
        return (string) this[nameof (XbmcPassword)];
      }
      set
      {
        this[nameof (XbmcPassword)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool GeneralStartupAuto
    {
      get
      {
        return (bool) this[nameof (GeneralStartupAuto)];
      }
      set
      {
        this[nameof (GeneralStartupAuto)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool GeneralStartupConnect
    {
      get
      {
        return (bool) this[nameof (GeneralStartupConnect)];
      }
      set
      {
        this[nameof (GeneralStartupConnect)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    public bool GeneralTrayEnabled
    {
      get
      {
        return (bool) this[nameof (GeneralTrayEnabled)];
      }
      set
      {
        this[nameof (GeneralTrayEnabled)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool GeneralTrayStartMinimized
    {
      get
      {
        return (bool) this[nameof (GeneralTrayStartMinimized)];
      }
      set
      {
        this[nameof (GeneralTrayStartMinimized)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("5")]
    public int XbmcConnectionInterval
    {
      get
      {
        return (int) this[nameof (XbmcConnectionInterval)];
      }
      set
      {
        this[nameof (XbmcConnectionInterval)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool GeneralTrayHideOnClose
    {
      get
      {
        return (bool) this[nameof (GeneralTrayHideOnClose)];
      }
      set
      {
        this[nameof (GeneralTrayHideOnClose)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    [UserScopedSetting]
    public bool GeneralTrayHideOnMinimize
    {
      get
      {
        return (bool) this[nameof (GeneralTrayHideOnMinimize)];
      }
      set
      {
        this[nameof (GeneralTrayHideOnMinimize)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    public bool ImonAutoInitialize
    {
      get
      {
        return (bool) this[nameof (ImonAutoInitialize)];
      }
      set
      {
        this[nameof (ImonAutoInitialize)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool ImonUninitializeOnError
    {
      get
      {
        return (bool) this[nameof (ImonUninitializeOnError)];
      }
      set
      {
        this[nameof (ImonUninitializeOnError)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool XbmcIdleStaticTextEnable
    {
      get
      {
        return (bool) this[nameof (XbmcIdleStaticTextEnable)];
      }
      set
      {
        this[nameof (XbmcIdleStaticTextEnable)] = (object) value;
      }
    }

    [DefaultSettingValue("XBMC")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string XbmcIdleStaticText
    {
      get
      {
        return (string) this[nameof (XbmcIdleStaticText)];
      }
      set
      {
        this[nameof (XbmcIdleStaticText)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("500")]
    [UserScopedSetting]
    public int ImonLcdScrollingDelay
    {
      get
      {
        return (int) this[nameof (ImonLcdScrollingDelay)];
      }
      set
      {
        this[nameof (ImonLcdScrollingDelay)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("10")]
    public int XbmcGeneralUpdateInterval
    {
      get
      {
        return (int) this[nameof (XbmcGeneralUpdateInterval)];
      }
      set
      {
        this[nameof (XbmcGeneralUpdateInterval)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    [UserScopedSetting]
    public bool GeneralDebugEnable
    {
      get
      {
        return (bool) this[nameof (GeneralDebugEnable)];
      }
      set
      {
        this[nameof (GeneralDebugEnable)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool XbmcControlModeEnable
    {
      get
      {
        return (bool) this[nameof (XbmcControlModeEnable)];
      }
      set
      {
        this[nameof (XbmcControlModeEnable)] = (object) value;
      }
    }

    [DefaultSettingValue("0")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public int XbmcPlaybackProgress
    {
      get
      {
        return (int) this[nameof (XbmcPlaybackProgress)];
      }
      set
      {
        this[nameof (XbmcPlaybackProgress)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool GeneralTrayDisableBalloonTips
    {
      get
      {
        return (bool) this[nameof (GeneralTrayDisableBalloonTips)];
      }
      set
      {
        this[nameof (GeneralTrayDisableBalloonTips)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("%title%")]
    public string XbmcMovieSingleText
    {
      get
      {
        return (string) this[nameof (XbmcMovieSingleText)];
      }
      set
      {
        this[nameof (XbmcMovieSingleText)] = (object) value;
      }
    }

    [DefaultSettingValue("%show%: S%season%E%episode% - %title%")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string XbmcTvSingleText
    {
      get
      {
        return (string) this[nameof (XbmcTvSingleText)];
      }
      set
      {
        this[nameof (XbmcTvSingleText)] = (object) value;
      }
    }

    [DefaultSettingValue("%artist%: %title%")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string XbmcMusicSingleText
    {
      get
      {
        return (string) this[nameof (XbmcMusicSingleText)];
      }
      set
      {
        this[nameof (XbmcMusicSingleText)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("%artist%: %title%")]
    [DebuggerNonUserCode]
    public string XbmcMusicVideoSingleText
    {
      get
      {
        return (string) this[nameof (XbmcMusicVideoSingleText)];
      }
      set
      {
        this[nameof (XbmcMusicVideoSingleText)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool XbmcIdleTimeShowSeconds
    {
      get
      {
        return (bool) this[nameof (XbmcIdleTimeShowSeconds)];
      }
      set
      {
        this[nameof (XbmcIdleTimeShowSeconds)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("500")]
    public int XbmcControlModeUpdateInterval
    {
      get
      {
        return (int) this[nameof (XbmcControlModeUpdateInterval)];
      }
      set
      {
        this[nameof (XbmcControlModeUpdateInterval)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool XbmcIdleTimeEnable
    {
      get
      {
        return (bool) this[nameof (XbmcIdleTimeEnable)];
      }
      set
      {
        this[nameof (XbmcIdleTimeEnable)] = (object) value;
      }
    }

    [DefaultSettingValue("5")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public int XbmcAlternateProgressDisplayDelay
    {
      get
      {
        return (int) this[nameof (XbmcAlternateProgressDisplayDelay)];
      }
      set
      {
        this[nameof (XbmcAlternateProgressDisplayDelay)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool XbmcControlModeIdleEnable
    {
      get
      {
        return (bool) this[nameof (XbmcControlModeIdleEnable)];
      }
      set
      {
        this[nameof (XbmcControlModeIdleEnable)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("1")]
    [UserScopedSetting]
    public int XbmcControlModeIdleDelay
    {
      get
      {
        return (int) this[nameof (XbmcControlModeIdleDelay)];
      }
      set
      {
        this[nameof (XbmcControlModeIdleDelay)] = (object) value;
      }
    }

    [DefaultSettingValue("1")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public int XbmcProgressbarStyle
    {
      get
      {
        return (int) this[nameof (XbmcProgressbarStyle)];
      }
      set
      {
        this[nameof (XbmcProgressbarStyle)] = (object) value;
      }
    }
  }
}

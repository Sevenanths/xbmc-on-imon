// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.NavigationButton
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace iMon.XBMC
{
  internal class NavigationButton : Button
  {
    private int defaultImageIndex;
    private int hoverImageIndex;
    private int activeImageIndex;
    private int currentImageIndex;

    [DefaultValue(-1)]
    [Browsable(true)]
    public int DefaultImageIndex
    {
      get
      {
        return this.defaultImageIndex;
      }
      set
      {
        if (value < -1)
          value = -1;
        this.defaultImageIndex = value;
      }
    }

    [Browsable(false)]
    public Image DefaultImage
    {
      get
      {
        if (this.ImageList == null || this.defaultImageIndex < 0 || this.defaultImageIndex >= this.ImageList.Images.Count)
          return (Image) null;
        return this.ImageList.Images[this.defaultImageIndex];
      }
    }

    [Browsable(true)]
    [DefaultValue(-1)]
    public int HoverImageIndex
    {
      get
      {
        return this.hoverImageIndex;
      }
      set
      {
        if (value < -1)
          value = -1;
        this.hoverImageIndex = value;
      }
    }

    [Browsable(false)]
    public Image HoverImage
    {
      get
      {
        if (this.ImageList == null || this.hoverImageIndex < 0 || this.hoverImageIndex >= this.ImageList.Images.Count)
          return (Image) null;
        return this.ImageList.Images[this.hoverImageIndex];
      }
    }

    [DefaultValue(-1)]
    [Browsable(true)]
    public int ActiveImageIndex
    {
      get
      {
        return this.activeImageIndex;
      }
      set
      {
        if (value < -1)
          value = -1;
        this.activeImageIndex = value;
      }
    }

    [Browsable(false)]
    public Image ActiveImage
    {
      get
      {
        if (this.ImageList == null || this.activeImageIndex < 0 || this.activeImageIndex >= this.ImageList.Images.Count)
          return (Image) null;
        return this.ImageList.Images[this.activeImageIndex];
      }
    }

    public NavigationButton()
    {
      this.defaultImageIndex = -1;
      this.hoverImageIndex = -1;
      this.activeImageIndex = -1;
      this.currentImageIndex = -1;
    }

    public void Activate()
    {
      if (this.activeImageIndex < 0 || this.ImageList == null || this.ImageList.Images.Count <= this.activeImageIndex)
        return;
      this.ImageIndex = this.activeImageIndex;
      this.currentImageIndex = this.activeImageIndex;
    }

    public void Deactivate()
    {
      if (this.defaultImageIndex < 0 || this.ImageList == null || this.ImageList.Images.Count <= this.defaultImageIndex)
        return;
      this.ImageIndex = this.defaultImageIndex;
    }

    protected override void OnClick(EventArgs e)
    {
      this.Activate();
      base.OnClick(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.hoverImageIndex < 0 || this.ImageList == null || this.ImageList.Images.Count <= this.hoverImageIndex)
      {
        base.OnMouseEnter(e);
      }
      else
      {
        this.currentImageIndex = this.ImageIndex;
        this.ImageIndex = this.hoverImageIndex;
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.currentImageIndex < 0 || this.ImageList == null || this.ImageList.Images.Count <= this.currentImageIndex)
      {
        base.OnMouseLeave(e);
      }
      else
      {
        this.ImageIndex = this.currentImageIndex;
        this.currentImageIndex = -1;
      }
    }
  }
}

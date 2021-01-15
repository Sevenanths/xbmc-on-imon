// Decompiled with JetBrains decompiler
// Type: iMon.XBMC.SuggestionBox
// Assembly: XbmcOniMonVFD, Version=0.1.4.0, Culture=neutral, PublicKeyToken=null
// MVID: FD635132-6090-4CCA-8BF1-6A9F960CDD3B
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\XbmcOniMonVFD.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace iMon.XBMC
{
  public class SuggestionBox : TextBox
  {
    private const int InitialHeight = 17;
    private const int HeightIncrement = 13;
    private ListBox list;
    private List<string> suggestions;
    private int maximumRows;
    private string delimiter;
    private bool startAndEnd;
    private bool focused;

    [Browsable(true)]
    public ICollection<string> Suggestions
    {
      get
      {
        return (ICollection<string>) this.suggestions;
      }
    }

    [Browsable(true)]
    public int MaximumRows
    {
      get
      {
        return this.maximumRows;
      }
      set
      {
        if (value <= 0)
          throw new IndexOutOfRangeException();
        this.maximumRows = value;
      }
    }

    [Browsable(true)]
    public string Delimiter
    {
      get
      {
        return this.delimiter;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          throw new ArgumentException();
        this.delimiter = value;
      }
    }

    [Browsable(true)]
    public bool StartAndEnd
    {
      get
      {
        return this.startAndEnd;
      }
      set
      {
        this.startAndEnd = value;
      }
    }

    public SuggestionBox()
    {
      this.suggestions = new List<string>();
      this.delimiter = " ";
      this.startAndEnd = false;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      this.checkList();
      if (this.focused || this.Text.Length == 0)
        this.suggest();
      this.focused = true;
      base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      this.checkList();
      if (!this.list.Focused)
        this.list.Visible = false;
      base.OnLostFocus(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      this.checkList();
      if (!this.list.Visible || keyData != Keys.Up && keyData != Keys.Down)
        return base.ProcessCmdKey(ref msg, keyData);
      switch (keyData)
      {
        case Keys.Up:
          if (this.list.SelectedIndex > 0)
          {
            --this.list.SelectedIndex;
            break;
          }
          break;
        case Keys.Down:
          if (this.list.SelectedIndex + 1 < this.list.Items.Count)
          {
            ++this.list.SelectedIndex;
            break;
          }
          break;
      }
      return true;
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      this.checkList();
      if (this.list.Visible && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Return))
      {
        if (e.KeyCode == Keys.Return && this.list.SelectedIndex >= 0)
        {
          this.insert(this.list.SelectedItem.ToString());
          this.list.Visible = false;
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      if (!e.SuppressKeyPress)
        this.suggest();
      this.OnKeyDown(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this.checkList();
        this.suggest();
      }
      base.OnMouseClick(e);
    }

    private void checkList()
    {
      if (this.list != null)
        return;
      this.list = new ListBox();
      this.list.Visible = false;
      this.Parent.Controls.Add((Control) this.list);
      this.list.SelectionMode = SelectionMode.One;
      this.list.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
      this.list.Size = new Size(this.Size.Width, 17);
      this.list.DoubleClick += new EventHandler(this.suggestionDoubleClick);
      this.list.LostFocus += new EventHandler(this.listLostFocus);
    }

    private void insert(string value)
    {
      int selectionStart = this.SelectionStart;
      if (selectionStart <= 0)
      {
        this.Text = this.Text.Insert(0, value);
        this.SelectionStart = value.Length;
      }
      else
      {
        int delimiterPosition = this.getDelimiterPosition();
        this.Text = this.Text.Remove(delimiterPosition, selectionStart - delimiterPosition).Insert(delimiterPosition, value);
        this.SelectionStart = delimiterPosition + value.Length;
      }
    }

    private void suggest()
    {
      int selectionStart = this.SelectionStart;
      string text = this.Text;
      if (selectionStart <= 0)
      {
        this.list.Items.Clear();
        foreach (object suggestion in this.suggestions)
          this.list.Items.Add(suggestion);
      }
      else
      {
        int delimiterPosition = this.getDelimiterPosition();
        this.list.Items.Clear();
        if (delimiterPosition >= 0)
        {
          string lowerInvariant = text.Substring(delimiterPosition, selectionStart - delimiterPosition).ToLowerInvariant();
          foreach (string suggestion in this.suggestions)
          {
            if (string.IsNullOrEmpty(lowerInvariant) || suggestion.ToLowerInvariant().StartsWith(lowerInvariant))
              this.list.Items.Add((object) suggestion);
          }
        }
      }
      if (this.list.Items.Count > 0)
      {
        this.list.Height = 17 + (Math.Min(this.list.Items.Count, this.maximumRows) - 1) * 13;
        this.list.Visible = true;
        this.list.BringToFront();
      }
      else
        this.list.Visible = false;
      this.SelectionStart = selectionStart;
    }

    private int getDelimiterPosition()
    {
      int selectionStart = this.SelectionStart;
      string text = this.Text;
      int num1 = text.LastIndexOf(this.delimiter, selectionStart, selectionStart + 1);
      if (num1 < 0)
        return -1;
      if (this.startAndEnd)
      {
        int num2 = 0;
        int num3;
        for (int startIndex = 0; startIndex >= 0 && startIndex < selectionStart; startIndex = num3 + 1)
        {
          num3 = text.IndexOf(this.delimiter, startIndex, selectionStart - startIndex);
          if (num3 >= startIndex)
          {
            ++num2;
            if (num2 > 1)
            {
              --num2;
              if (!text.Substring(startIndex, num3 - startIndex).Contains(" "))
                --num2;
            }
          }
          else
            break;
        }
        if (num2 == 0)
          return -1;
      }
      return num1;
    }

    private void suggestionDoubleClick(object sender, EventArgs e)
    {
      if (this.list.SelectedIndex < 0)
        return;
      this.insert(this.list.SelectedItem.ToString());
      this.list.Visible = false;
      this.Focus();
    }

    private void listLostFocus(object sender, EventArgs e)
    {
      if (this.Focused)
        return;
      this.list.Visible = false;
    }
  }
}

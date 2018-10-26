// Decompiled with JetBrains decompiler
// Type: COM2PRINTER.Form1
// Assembly: COM2PRINTER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F693582-CFDA-4074-B22C-B8E024E92F33
// Assembly location: E:\COM2PRINTER\COM2PRINTER\bin\Debug\COM2PRINTER.exe

using SerialPortLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COM2PRINTER
{
  public class Form1 : Form
  {
    private SerialPortInput serialPort = new SerialPortInput();
    private string PrinterName = string.Empty;
    private string Filepath = "F:\\测试.docx";
    private IContainer components = (IContainer) null;
    private Button button1;
    private ComboBox comboBox1;
    private ComboBox comboBox2;
    private Button button2;
    private TextBox textBox1;
    private Button button3;

    public Form1()
    {
      this.InitializeComponent();
      this.Closing += new CancelEventHandler(this.OnClosing);
      this.comboBox1.TextChanged += new EventHandler(this.ComboBox1OnTextChanged);
    }

    private void ComboBox1OnTextChanged(object sender, EventArgs e)
    {
      this.PrinterName = this.comboBox1.Text;
    }

    private void OnClosing(object sender, CancelEventArgs e)
    {
      this.serialPort.Disconnect();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Init();
    }

    public void Init()
    {
      this.comboBox1.Items.Clear();
      this.comboBox2.Items.Clear();
      foreach (object print in HardwareMng.GetPrints())
        this.comboBox1.Items.Add(print);
      foreach (object com in HardwareMng.GetComs())
        this.comboBox2.Items.Add(com);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.serialPort.ConnectionStatusChanged += (SerialPortInput.ConnectionStatusChangedEventHandler) ((s, args) =>
      {
        this.ChangeText(string.Format("{0}Connected = {1}", (object) Environment.NewLine, (object) args.Connected));
        Console.WriteLine("Connected = {0}", (object) args.Connected);
      });
      this.serialPort.MessageReceived += (SerialPortInput.MessageReceivedEventHandler) ((s, args) =>
      {
        string str = Encoding.Default.GetString(args.Data);
        this.ChangeText(string.Format("{0}Received message: {1}", (object) Environment.NewLine, (object) str));
        if (str.Contains("打印"))
          HardwareMng.PrintFile(this.Filepath, this.PrinterName);
        Console.WriteLine("Received message: {0}", (object) BitConverter.ToString(args.Data));
      });
      this.serialPort.SetPort(this.comboBox2.Text, 19200, StopBits.One, Parity.None);
      this.serialPort.Connect();
      this.serialPort.SendMessage(Encoding.UTF8.GetBytes("Hello World!"));
    }

    private void ChangeText(string pstr)
    {
      this.textBox1.BeginInvoke((Action) delegate { this.textBox1.Text += pstr; });
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.serialPort.SendMessage(((IEnumerable<string>) "AA BB 06 00 00 00 06 01 06 01".Split(' ')).AsParallel<string>().Select<string, byte>((Func<string, byte>) (x => Convert.ToByte(x, 16))).ToArray<byte>());
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.button1 = new Button();
      this.comboBox1 = new ComboBox();
      this.comboBox2 = new ComboBox();
      this.button2 = new Button();
      this.textBox1 = new TextBox();
      this.button3 = new Button();
      this.SuspendLayout();
      this.button1.Location = new Point(46, 21);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(46, 50);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(396, 20);
      this.comboBox1.TabIndex = 1;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Location = new Point(46, 76);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new Size(396, 20);
      this.comboBox2.TabIndex = 2;
      this.button2.Location = new Point(177, 21);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 3;
      this.button2.Text = "连接";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.textBox1.Location = new Point(46, 122);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(396, 175);
      this.textBox1.TabIndex = 4;
      this.button3.Location = new Point(309, 20);
      this.button3.Name = "button3";
      this.button3.Size = new Size(75, 23);
      this.button3.TabIndex = 5;
      this.button3.Text = "button3";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(454, 319);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.comboBox2);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.button1);
      this.Name = nameof (Form1);
      this.Text = nameof (Form1);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

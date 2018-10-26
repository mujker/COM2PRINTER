// Decompiled with JetBrains decompiler
// Type: COM2PRINTER.Program
// Assembly: COM2PRINTER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F693582-CFDA-4074-B22C-B8E024E92F33
// Assembly location: E:\COM2PRINTER\COM2PRINTER\bin\Debug\COM2PRINTER.exe

using System;
using System.Windows.Forms;

namespace COM2PRINTER
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}

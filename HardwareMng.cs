// Decompiled with JetBrains decompiler
// Type: COM2PRINTER.HardwareMng
// Assembly: COM2PRINTER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F693582-CFDA-4074-B22C-B8E024E92F33
// Assembly location: E:\COM2PRINTER\COM2PRINTER\bin\Debug\COM2PRINTER.exe

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace COM2PRINTER
{
  public class HardwareMng
  {
    public static List<string> AvailablePrinters()
    {
      List<string> stringList = new List<string>();
      ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);
      scope.Connect();
      SelectQuery selectQuery = new SelectQuery();
      selectQuery.QueryString = "SELECT Name FROM Win32_Printer";
      foreach (ManagementObject managementObject in new ManagementObjectSearcher(scope, (ObjectQuery) selectQuery).Get())
        stringList.Add(managementObject["Name"].ToString());
      return stringList;
    }

    public static List<string> GetPrints()
    {
      List<string> stringList = new List<string>();
      foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
        stringList.Add(installedPrinter);
      return stringList;
    }

    public static List<string> GetComs()
    {
      return ((IEnumerable<string>) SerialPort.GetPortNames()).ToList<string>();
    }

    public static void PrintFile(string Filepath, string PrinterName)
    {
      Process process = new Process();
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.UseShellExecute = true;
      process.StartInfo.FileName = Filepath;
      process.StartInfo.Verb = "print";
      string defaultPrinter = HardwareMng.GetDefaultPrinter();
      HardwareMng.SetDefaultPrinter(PrinterName);
      process.Start();
      process.WaitForExit(10000);
      HardwareMng.SetDefaultPrinter(defaultPrinter);
    }

    private static string GetDefaultPrinter()
    {
      return new PrintDocument().PrinterSettings.PrinterName;
    }

    [DllImport("winspool.drv")]
    public static extern bool SetDefaultPrinter(string Name);
  }
}

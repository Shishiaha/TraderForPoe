﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraderForPoe.Classes
{
    static class Updater
    {
        /// <summary>
        /// Check if a newer version is available
        /// </summary>
        /// <returns>Returns true if update is available</returns>
        public static bool UpdateIsAvailable()
        {
            WebClient webClient = new WebClient();

            string[] updateString = webClient.DownloadString("https://raw.githubusercontent.com/Shishiaha/TraderForPoe/TraderForPoe2/update").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            double thisVersion = Convert.ToDouble(Assembly.GetEntryAssembly().GetName().Version.ToString().Substring(0, 3), System.Globalization.CultureInfo.InvariantCulture);

            double onlineVersion = Convert.ToDouble(updateString[0], System.Globalization.CultureInfo.InvariantCulture);

            if (onlineVersion > thisVersion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void StartUpdate()
        {
            WebClient webClient = new WebClient();

            string[] updateString = webClient.DownloadString("https://raw.githubusercontent.com/Shishiaha/TraderForPoe/TraderForPoe2/update").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            string downloadLink = updateString[1];

            string newExePath = Path.GetTempPath() + "TraderForPoe2.exe";

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new System.Uri(downloadLink), newExePath);
            }
        }

        private static void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            File.Delete(Path.GetTempPath() + "TraderForPoe.bak");

            File.Move(Assembly.GetEntryAssembly().Location, Path.GetTempPath() + "TraderForPoe.bak");

            File.Move(Path.GetTempPath() + "TraderForPoe2.exe", AppDomain.CurrentDomain.BaseDirectory + "TraderForPoe2.exe");

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

            Application.Current.Shutdown();
        }

    }
}

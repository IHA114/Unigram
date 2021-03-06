﻿using System;
using System.Globalization;
using System.IO;
using System.Text;
using Unigram.Common;

namespace Unigram.Logs
{
    public class Log
    {
        public static bool IsPrivateBeta
        {
            get
            {
#if DEBUG
                return true;
#endif
#if WP81
                return Windows.ApplicationModel.Package.Current.Id.Name == "TelegramMessengerLLP.TelegramMessengerPrivateBeta";
#endif
                return true;
            }
        }

        public static bool WriteSync { get; set; }

        public static bool IsEnabled
        {
            get { return IsPrivateBeta; }
        }

        private static readonly object _fileSyncRoot = new object();

        public static void Write(string str, Action callback = null)
        {
            if (!IsEnabled)
            {
                return;
            }

            if (WriteSync)
            {
                WriteInternal(str, callback);
            }
            else
            {
                Execute.BeginOnThreadPool(() =>
                {
                    WriteInternal(str, callback);
                });
            }
        }

        public static void SyncWrite(string str, Action callback = null)
        {
            if (!IsEnabled)
            {
                return;
            }

            //if (WriteSync)
            {
                WriteInternal(str, callback);
            }
        }

        private static void WriteInternal(string str, Action callback = null)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            str = string.Format("{0} {1}{2}", timestamp, str, Environment.NewLine);
            FileUtils.Write(_fileSyncRoot, DirectoryName, FileName, str);
            callback?.Invoke();
        }

        private const string DirectoryName = "Logs";

        public static string FileName
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".txt"; }
        }

        public static string Format(string format, params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i] ?? "null";
            }

            return string.Format(format, args);
        }
    }
}
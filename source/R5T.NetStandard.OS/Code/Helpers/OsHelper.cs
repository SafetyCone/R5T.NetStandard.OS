using System;
using System.IO;
using System.Runtime.InteropServices;


namespace R5T.NetStandard.OS
{
    public static class OsHelper
    {
        public static void PlatformSwitch(Action windows, Action osx, Action linux)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                windows();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                osx();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                linux();
            }
            else
            {
                throw new Exception(@"Unknown operating system (not Windows, OSX, or Linux).");
            }
        }

        public static T PlatformSwitch<T>(Func<T> windows, Func<T> osx, Func<T> linux)
        {
            T output = default;
            OsHelper.PlatformSwitch(
                () =>
                {
                    output = windows();
                },
                () =>
                {
                    output = osx();
                },
                () =>
                {
                    output = linux();
                });
            return output;
        }

        public static void DisplayOsName(StreamWriter writer)
        {
            OsHelper.PlatformSwitch(
                () =>
                {
                    writer.WriteLine(@"OS: Windows");
                },
                () =>
                {
                    writer.WriteLine(@"OS: OSX (Mac)");
                },
                () =>
                {
                    writer.WriteLine(@"OS: Linux");
                });
        }
    }
}

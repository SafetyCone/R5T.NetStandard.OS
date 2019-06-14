using System;
using System.IO;
using System.Runtime.InteropServices;


namespace R5T.NetStandard.OS
{
    public static class OSHelper
    {
        public static OSPlatform GetOSPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }
            else
            {
                throw new Exception(@"Unknown operating system (not Windows, OSX, or Linux).");
            }
        }

        public static void OSPlatformSwitch(OSPlatform osPlatform, Action windows, Action osx, Action linux)
        {
            if (osPlatform == OSPlatform.Windows)
            {
                windows();
            }
            else if (osPlatform == OSPlatform.OSX)
            {
                osx();
            }
            else if (osPlatform == OSPlatform.Linux)
            {
                linux();
            }
            else
            {
                throw new Exception(@"Unknown operating system (not Windows, OSX, or Linux).");
            }
        }

        public static void OSPlatformSwitch(Action windows, Action osx, Action linux)
        {
            var osPlatform = OSHelper.GetOSPlatform();

            OSHelper.OSPlatformSwitch(osPlatform, windows, osx, linux);
        }

        public static T OSPlatformSwitch<T>(OSPlatform osPlatform, Func<T> windows, Func<T> osx, Func<T> linux)
        {
            T output = default;
            OSHelper.OSPlatformSwitch(osPlatform,
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

        public static T OSPlatformSwitch<T>(Func<T> windows, Func<T> osx, Func<T> linux)
        {
            var osPlatform = OSHelper.GetOSPlatform();

            var output = OSHelper.OSPlatformSwitch(osPlatform, windows, osx, linux);
            return output;
        }

        public static Platform GetPlatform(OSPlatform osPlatform)
        {
            var platform = OSHelper.OSPlatformSwitch(osPlatform,
                () =>
                {
                    return Platform.Windows;
                },
                () =>
                {
                    return Platform.NonWindows;
                },
                () =>
                {
                    return Platform.NonWindows;
                });

            return platform;
        }

        public static Platform GetPlatform()
        {
            var osPlatform = OSHelper.GetOSPlatform();

            var platform = OSHelper.GetPlatform(osPlatform);
            return platform;
        }

        public static void PlatformSwitch(Platform platform, Action windows, Action nonWindows)
        {
            switch(platform)
            {
                case Platform.NonWindows:
                    nonWindows();
                    break;

                case Platform.Windows:
                    windows();
                    break;

                default:
                    throw new ArgumentException(EnumHelper.UnexpectedEnumerationValueMessage(platform));
            }
        }

        public static void PlatformSwitch(Action windows, Action nonWindows)
        {
            var platform = OSHelper.GetPlatform();

            OSHelper.PlatformSwitch(platform, windows, nonWindows);
        }

        public static T PlatformSwitch<T>(Platform platform, Func<T> windows, Func<T> nonWindows)
        {
            T output = default;
            OSHelper.PlatformSwitch(platform,
                () =>
                {
                    output = windows();
                },
                () =>
                {
                    output = nonWindows();
                });

            return output;
        }

        public static T PlatformSwitch<T>(Func<T> windows, Func<T> nonWindows)
        {
            var platform = OSHelper.GetPlatform();

            var output = OSHelper.PlatformSwitch(platform, windows, nonWindows);
            return output;
        }

        public static void DisplayOSName(StreamWriter writer)
        {
            OSHelper.OSPlatformSwitch(
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

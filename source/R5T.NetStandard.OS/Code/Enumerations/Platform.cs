using System;


namespace R5T.NetStandard.OS
{
    public enum Platform
    {
        Windows,
        NonWindows,
    }


    public static class PlatformExtensions
    {
        public const string WindowsStandardRepresentation = @"Windows";
        public const string NonWindowsStandardRepresentation = @"NonWindows";


        /// <summary>
        /// Returns the standard string representation of the platform enumeration value.
        /// </summary>
        public static string ToStringStandard(this Platform platform)
        {
            switch (platform)
            {
                case Platform.NonWindows:
                    return PlatformExtensions.NonWindowsStandardRepresentation;

                case Platform.Windows:
                    return PlatformExtensions.WindowsStandardRepresentation;

                default:
                    var message = EnumHelper.GetUnexpectedEnumerationValueMessage(platform);
                    throw new Exception(message);
            }
        }

        /// <summary>
        /// Returns the alternate platform.
        /// </summary>
        public static Platform GetAlternatePlatform(this Platform platform)
        {
            var alternatePlatform = Utilities.GetAlternatePlatform(platform);
            return alternatePlatform;
        }
    }
}

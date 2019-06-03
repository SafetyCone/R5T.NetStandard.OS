using System;


namespace R5T.NetStandard.OS.Construction
{
    public static class Construction
    {
        public static void SubMain()
        {
            Construction.GetCurrentMachineName();
        }

        public static void GetCurrentMachineName()
        {
            Console.WriteLine($"{nameof(Environment)}.{nameof(MachineName)}: {Environment.MachineName}");
        }
    }
}

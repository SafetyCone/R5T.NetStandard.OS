using System;
using System.Diagnostics;


namespace R5T.NetStandard.OS
{
    public class ProcessRunOptions
    {
        public string Command { get; set; }
        public string Arguments { get; set; }
        public DataReceivedEventHandler ReceiveOutputData { get; set; }
        public DataReceivedEventHandler ReceiveErrorData { get; set; }
    }
}

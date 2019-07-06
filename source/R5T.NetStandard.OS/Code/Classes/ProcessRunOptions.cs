using System;
using System.Diagnostics;


namespace R5T.NetStandard.OS
{
    public class ProcessRunOptions
    {
        #region Static

        public static void DefaultReceiveErrorData(object sender, DataReceivedEventArgs e)
        {
            if(e.Data is null)
            {
                return;
            }

            throw new Exception($"An error occurred: {e.Data}");
        }

        #endregion


        public string Command { get; set; }
        public string Arguments { get; set; }
        public DataReceivedEventHandler ReceiveOutputData { get; set; }
        public DataReceivedEventHandler ReceiveErrorData { get; set; }
    }
}

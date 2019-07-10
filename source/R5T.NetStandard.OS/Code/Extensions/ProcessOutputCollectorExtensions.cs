using System;
using System.Diagnostics;
using System.IO;


namespace R5T.NetStandard.OS
{
    public static class ProcessOutputCollectorExtensions
    {
        public static void ReceiveOutputData(this ProcessOutputCollector collector, object sender, DataReceivedEventArgs e)
        {
            if(e.Data is null)
            {
                return;
            }

            collector.AddOutput(e.Data);
        }

        public static StringReader GetOutputReader(this ProcessOutputCollector collector)
        {
            var reader = new StringReader(collector.GetOutputText());
            return reader;
        }

        public static void ReceiveErrorData(this ProcessOutputCollector collector, object sender, DataReceivedEventArgs e)
        {
            if (e.Data is null)
            {
                return;
            }

            collector.AddError(e.Data);
        }

        public static StringReader GetErrorReader(this ProcessOutputCollector collector)
        {
            var reader = new StringReader(collector.GetErrorText());
            return reader;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using R5T.NetStandard.IO;


namespace R5T.NetStandard.OS
{
    public static class ProcessOutputCollectorExtensions
    {
        /// <summary>
        /// Trims the output text (including any ending \r\n from the output).
        /// </summary>
        public static string GetOutputTextTrimmed(this ProcessOutputCollector collector)
        {
            var output = collector.GetOutputText().Trim();
            return output;
        }

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

        public static IEnumerable<string> GetOutputLines(this ProcessOutputCollector collector)
        {
            using (var reader = collector.GetOutputReader())
            {
                while (!reader.ReadLineIsEnd(out var line))
                {
                    yield return line;
                }
            }
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

        public static IEnumerable<string> GetErrorLines(this ProcessOutputCollector collector)
        {
            using (var reader = collector.GetErrorReader())
            {
                while (!reader.ReadLineIsEnd(out var line))
                {
                    yield return line;
                }
            }
        }
    }
}

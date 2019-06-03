using System;
using System.Diagnostics;
using System.IO;


namespace R5T.NetStandard.OS
{
    public static class ProcessRunner
    {
        public static string GetCommandLine(string command, string arguments)
        {
            var commandLine = $"{command} {arguments}";
            return commandLine;
        }

        public static int Run(string command, string arguments)
        {
            var output = ProcessRunner.Run(command, arguments, Console.Out);
            return output;
        }

        public static int Run(string command, string arguments, TextWriter outputWriter)
        {
            bool errorOccurred = false;
            var runOptions = new ProcessRunOptions
            {
                Command = command,
                Arguments = arguments,
                ReceiveOutputData = (sender, e) =>
                {
                    outputWriter.WriteLine(e.Data);
                },
                ReceiveErrorData = (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        outputWriter.WriteLine(e.Data);

                        errorOccurred = true;
                    }
                }
            };

            int returnValue = ProcessRunner.Run(runOptions);

            outputWriter.Flush();

            if (errorOccurred)
            {
                outputWriter.WriteLine();

                var commandLine = ProcessRunner.GetCommandLine(runOptions.Command, runOptions.Arguments);
                outputWriter.WriteLine($"Command:\n{commandLine}");

                throw new Exception("Process run error occurred.");
            }

            return returnValue;
        }

        public static int Run(ProcessRunOptions runOptions)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(runOptions.Command, runOptions.Arguments)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.OutputDataReceived += runOptions.ReceiveOutputData;
            process.ErrorDataReceived += runOptions.ReceiveErrorData;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            process.OutputDataReceived -= runOptions.ReceiveOutputData;
            process.ErrorDataReceived -= runOptions.ReceiveErrorData;

            int exitCode = process.ExitCode; // For debugging.

            process.Close();

            return exitCode;
        }
    }
}

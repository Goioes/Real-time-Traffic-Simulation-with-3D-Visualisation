using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RunPythonScript
{
    public class RunPython
    {
        public static Thread mainThread; //Traci
        public static Thread childThread; //Runs sumo
        private static string filePythonExePath = @"C:/Users/gielo/anaconda3/envs/Python3_7/python.exe";
        private static string filePythonNamePath = "C://Users//gielo//OneDrive//Documenten//Arbeit//RealDeal//Unity//TestCSCode//BasicRunSumo.py";
        private static string netPath = "--netfile C:/Users/gielo/OneDrive/Documenten/Arbeit/RealDeal/Unity/TestCSCode/grid.net.xml ";
        private static string cfgPath = "--cfgfile C:/Users/gielo/OneDrive/Documenten/Arbeit/RealDeal/Unity/TestCSCode/grid.sumocfg ";
        private static string port = "--port 4001";
        private static string filePythonParameterName = netPath + cfgPath + port;
        public void CreateChildThread()  // Main creates child thread
        {
            mainThread = Thread.CurrentThread;
            mainThread.Name = "MainThread";
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Console.WriteLine("In Main: Creating the Child thread");
            childThread = new Thread(childref);
            childThread.Name = "ChildThread";
            //childThread.IsBackground = true;
            childThread.Start();
        }
        public static void CallToChildThread() // Child thread
        {
            Console.WriteLine("Child thread starts");
            string fileNameParameter = $"{filePythonNamePath} {filePythonParameterName}";
            ExecutePythonScript(fileNameParameter);
        }

        private static System.Text.StringBuilder outputText = null;
        private static int lineCount = 0;
        
        public static void ExecutePythonScript(string fileNameParameter)
        {
            outputText = new System.Text.StringBuilder();
            string standardError = string.Empty;
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo(filePythonExePath)
                    {
                        Arguments = fileNameParameter,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        //RedirectStandardError = true,
                        CreateNoWindow = true
                    };
                    process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        // Prepend line numbers to each line of the output.
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            lineCount++;
                            outputText.Append("\n[" + lineCount + "]: " + e.Data);
                            Console.WriteLine(e.Data);
                        }
                    });
                    process.Start();
                    process.BeginOutputReadLine();
                    Console.WriteLine(outputText);
                    Console.WriteLine("Error");
                    Thread.Sleep(6000);
                    process.Kill();
                    process.WaitForExit();
                    Console.WriteLine(outputText);
                }
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
            }
        }
    }
}

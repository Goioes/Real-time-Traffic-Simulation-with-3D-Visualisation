using System;
using System.Collections;
//using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Diagnostics;
using CodingConnected.TraCI.NET;

public class RunPythonScript 
{
    private static string filePythonExePath = @"C:/Users/20210124/Anaconda/python.exe";
    public string filePythonNamePath = "C://Users//20210124//Documents//RealDeal//Unity//Real-time-Traffic-Simulation-with-3D-Visualisation//SumoSimulation//BasicRunSumo.py ";
    public string netPath = "--netfile C:/Users/20210124//Documents/RealDeal/Unity/Real-time-Traffic-Simulation-with-3D-Visualisation/SumoSimulation/map.net.xml ";
    public string cfgPath = "--cfgfile C:/Users/20210124//Documents/RealDeal/Unity/Real-time-Traffic-Simulation-with-3D-Visualisation/SumoSimulation/map.sumocfg ";
    public int portNumber = 4001;

    public static Thread mainThread; //Runs Unity
    public static Thread childThread; //Runs python 
    public static RunPythonScript runPython;
    public Process pythonProcess = new Process();
    public TraCIClient client;

    //public void Test()
    //{
    //    mainThread = Thread.CurrentThread;
    //    CreateChildThread();

    //    UnityEngine.Debug.Log("Done");
    //}

    //public void Stop()
    //{
    //    runPython.pythonProcess.Kill();
    //}
    public void CreateChildThread()  // Main creates child thread
    {
        UnityEngine.Debug.Log("In Main: Creating the Child thread");
        Thread childThread = new Thread(ExecutePythonScript);
        childThread.Name = "ChildThread";
        childThread.Start();
    }

    public void KillProcess()
    {
        runPython.pythonProcess.Kill();
    }

    private static System.Text.StringBuilder outputText = null;
    private static int lineCount = 0;

    public void ExecutePythonScript()
    {

        string port = "--port " + portNumber;
        string filePythonParameterName = netPath + cfgPath + port;
        UnityEngine.Debug.Log(filePythonParameterName);
        UnityEngine.Debug.Log(filePythonNamePath);
        string fileNameParameter = $"{filePythonNamePath + filePythonParameterName}";
        runPython = this;
        outputText = new System.Text.StringBuilder();
        string standardError = string.Empty;
        try
        {
            using (runPython.pythonProcess)
            {
                UnityEngine.Debug.Log(filePythonExePath);
                UnityEngine.Debug.Log(fileNameParameter);
                pythonProcess.StartInfo = new ProcessStartInfo(filePythonExePath)
                {
                    Arguments = fileNameParameter,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    CreateNoWindow = true
                };
                pythonProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                        // Prepend line numbers to each line of the output.
                        if (!String.IsNullOrEmpty(e.Data))
                    {
                        lineCount++;
                        outputText.Append("\n[" + lineCount + "]: " + e.Data);
                        UnityEngine.Debug.Log(e.Data);
                    }
                });
                pythonProcess.Start();
                pythonProcess.BeginOutputReadLine();
                UnityEngine.Debug.Log(outputText);
                UnityEngine.Debug.Log("Error");
                Thread.Sleep(6000);
                pythonProcess.WaitForExit();
            }
        }
        catch
        {
            UnityEngine.Debug.Log("Could not start python!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Globalization;
using System;
using System.Threading;

public class StartSumo : MonoBehaviour {


    public static Thread childThread; //Runs sumo

    void Awake()
    {
        Process[] localAll = Process.GetProcesses();
        UnityEngine.Debug.Log(localAll);
        ThreadStart childref = new ThreadStart(ChildThread);
        UnityEngine.Debug.Log("In Main: Creating the Child thread");
        childThread = new Thread(childref);
        childThread.Name = "ChildThread";
        //childThread.IsBackground = true;
        childThread.Start();
    }

    public static void ChildThread() // Child thread
    {
        string cmdStartSumo;
        string scenarioPath = @"C:\Users\gielo\OneDrive\Documenten\Arbeit\RealDeal\Unity\DarraghMag97\Real-time-Traffic-Simulation-with-3D-Visualisation\SumoSimulation\map.sumocfg";
        int remotePort = 4001;
        double stepLength = 0.02;
        cmdStartSumo = "sumo-gui -c " + scenarioPath + " --start --remote-port "
            + remotePort.ToString()
            + " --step-length " + stepLength.ToString(CultureInfo.InvariantCulture);
        scenarioPath = @"C:\Users\gielo\OneDrive\Documenten\Arbeit\RealDeal\Unity\DarraghMag97\Real-time-Traffic-Simulation-with-3D-Visualisation\SumoSimulation";
        cmdStartSumo = "cd " + scenarioPath;
        ExecuteCommand(cmdStartSumo);
        UnityEngine.Debug.Log("Child thread done");
    }

    public static void ExecuteCommand(string command)
    {
        var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        processInfo.RedirectStandardError = true;
        processInfo.RedirectStandardOutput = true;

        var process = Process.Start(processInfo);

        process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            UnityEngine.Debug.Log("output>>" + e.Data);
        process.BeginOutputReadLine();

        process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            UnityEngine.Debug.Log("error>>" + e.Data);
        process.BeginErrorReadLine();
        UnityEngine.Debug.Log("Wait exit");
        //childThread = Thread.CurrentThread;
        
        Thread.Sleep(2000);
        
        process.Kill();
        UnityEngine.Debug.Log(process.HasExited);
        process.WaitForExit();
       
        UnityEngine.Debug.Log(string.Format("ExitCode: {0}", process.ExitCode));
        process.Close();
        UnityEngine.Debug.Log("Exited");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour {

    public static SceneSwap sceneSwap;
    bool gameStart;
    public string scenarioPath;
    public int remotePort;
    public double stepLength;

    void Awake()
    {
        if (!gameStart)
        {
            sceneSwap = this;

        // set defaults:
            scenarioPath = @"C:\Users\20210124\Documents\RealDeal\Unity\Real-time-Traffic-Simulation-with-3D-Visualisation\SumoSimulation\map.sumocfg";
            remotePort = 4001;
            stepLength = 0.02;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            gameStart = true;
        }
    }

    public void UnloadScene(int scene)
    {
        StartCoroutine(Unload(scene));
    }
    IEnumerator Unload(int scene)
    {
        yield return null;
        SceneManager.UnloadScene(scene);
    }
}

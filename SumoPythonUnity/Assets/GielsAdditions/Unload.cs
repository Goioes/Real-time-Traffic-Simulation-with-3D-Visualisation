using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unload : MonoBehaviour
{
    public int scene;

    void Awake()
    {

        Debug.Log("Unload scene: ");
        Debug.Log(scene.ToString());
        if (SceneManager.GetSceneByBuildIndex(scene).isLoaded)
        {
            Debug.Log("Unloading scene: ");
            Debug.Log(scene);
            SceneSwap.sceneSwap.UnloadScene(scene);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class optionsMenu : MonoBehaviour {

    public GameObject fileErrorUI;
    public TextMeshProUGUI scenarioPath;
    public Text remotePort;
    public Text stepLength;

    private void Awake()
    {
        scenarioPath.text = SceneSwap.sceneSwap.scenarioPath;
        remotePort.text = SceneSwap.sceneSwap.remotePort.ToString();
        stepLength.text = SceneSwap.sceneSwap.stepLength.ToString();
    }
    public void selectSumoScenario()
    {
        fileErrorUI.SetActive(false);
        scenarioPath.text = "";

        string path = EditorUtility.OpenFilePanel("Load sumo scenario", "", "");
        Debug.Log(path);
        if (path.EndsWith(".sumocfg"))
        {
            fileErrorUI.SetActive(false);
            scenarioPath.text = path;
            SceneSwap.sceneSwap.scenarioPath = path;
        }
        else
        {
            fileErrorUI.SetActive(true);
        }
    }

    public void setRemotePort()
    {
        SceneSwap.sceneSwap.remotePort = int.Parse(remotePort.text);
    }

    public void setStepLength()
    {
        SceneSwap.sceneSwap.stepLength = double.Parse(stepLength.text);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_switch : MonoBehaviour
{

    public void sceneswitch(string scenename)
    {
        SceneManager.LoadScene(sceneName: scenename);
    }
}

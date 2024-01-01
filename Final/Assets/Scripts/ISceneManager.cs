using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ISceneManager : MonoBehaviour
{
    public static ISceneManager _instance;
    private int scene_num = 1;

    void Awake() {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene() {
        scene_num++;
        SceneManager.LoadScene(scene_num);
    }
}

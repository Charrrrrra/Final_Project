using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Scene_01");
    }

    public void QuitGame() {
        Application.Quit();
    }
}

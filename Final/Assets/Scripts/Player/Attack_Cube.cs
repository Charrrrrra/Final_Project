using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cube : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("NPC")) {
            Debug.Log("U caught the Player!");
            ISceneManager._instance.LoadNextScene();
        }

    }
}

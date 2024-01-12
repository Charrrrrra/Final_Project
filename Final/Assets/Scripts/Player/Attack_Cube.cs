using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cube : MonoBehaviour
{
    public SpecialSight cam;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("NPC")) {
            Debug.Log("U caught the Player!");
            ISceneManager._instance.LoadNextScene();
        }

        if (other.CompareTag("Power")) {
            Debug.Log("U got power!");
            cam.sight_time += 3f;
        }
    }
}

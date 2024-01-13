using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleIns : MonoBehaviour
{
    public GameObject obstacle_ins;
    public PlayerMovement my_Player;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine("ShowObstacleInstruction", 5f);
        }
    }

    IEnumerator ShowObstacleInstruction(float delayTime) {
        obstacle_ins.SetActive(true);
        my_Player.can_walk = false;
        yield return new WaitForSeconds(delayTime);
        obstacle_ins.SetActive(false);
        Destroy(gameObject);
        my_Player.can_walk = true;
    }
}

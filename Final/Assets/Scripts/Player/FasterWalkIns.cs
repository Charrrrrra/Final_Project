using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterWalkIns : MonoBehaviour
{  
    public PlayerMovement my_player;
    public PlayerController my_mouse;

    public GameObject faster_ins;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine("ShowFasterInstruction", 5f);
        }
    }

    IEnumerator ShowFasterInstruction(float delayTime) {
        faster_ins.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        faster_ins.SetActive(false);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}

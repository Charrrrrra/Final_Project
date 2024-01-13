using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public NPC_Ai npc;
    public PlayerMovement my_player;
    public float Alert_time;
    private bool Is_runing;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (my_player.currentSpeed > 11.5f) {
                Is_runing = true;
                Debug.Log("runing");
                StartCoroutine("NPC_Scared", 0.8f);
                StartCoroutine("NPC_Running", Alert_time);
            }
        }
    }

    void Update() {
        if (Is_runing) {
            npc.running = true;
            npc.walking = false;
        }
    }

    IEnumerator NPC_Running(float time) {
        yield return new WaitForSeconds(time);
        npc.aiAnim.SetTrigger("walk");
        Is_runing = false;
        Debug.Log("lose alert");
    }

    IEnumerator NPC_Scared(float time) {
        npc.aiAnim.SetTrigger("jumpscare");
        yield return new WaitForSeconds(time);
        npc.aiAnim.SetTrigger("sprint");
    }

}

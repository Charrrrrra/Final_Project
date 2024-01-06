using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIns : MonoBehaviour
{
    public PlayerMovement my_player;
    public PlayerController my_mouse;

    public GameObject attack_ins;

    private bool Is_ins_shown = false;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine("ShowAttackInstruction", 5f);
        }
    }

    IEnumerator ShowAttackInstruction(float delayTime) {
        attack_ins.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        attack_ins.SetActive(false);
        Is_ins_shown = true;
    }

    void Update()
    {
        my_player.Can_attack = false;
        if (Is_ins_shown) {
            my_player.Can_attack = true;
            Destroy(gameObject);
        }
    }
}

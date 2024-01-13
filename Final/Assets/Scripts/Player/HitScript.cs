using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitScript : MonoBehaviour
{
    public PlayerMovement my_player;
    public PlayerController my_mouse;

    void Start() {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wall") && (my_player.currentSpeed >= 6.0f)) {
            Debug.Log("You hit the wall and fall! The V is " + my_player.currentSpeed);
            my_player.can_walk = false;
            my_mouse.can_mouse_move = false;
            // my_player.animator.Play("Fallback");
            my_player.animator.SetTrigger("Fallback");
            my_player.animator.speed = 1.0f;
            StartCoroutine(SetBoolAfterDelay(3.0f));
            my_player.Stop_Moving();
            my_mouse.xRotation = 0f;
            StartCoroutine(SetAttackBool(3.0f));

        }
        
        if (other.CompareTag("Stone") && my_player.currentSpeed >= 5.0f) {
            // my_player.animator.Play("Fallfront");
            my_player.animator.SetTrigger("Fallfront");
            my_player.can_walk = false;
            my_mouse.can_mouse_move = false;
            Debug.Log("You fall!");
            my_player.animator.speed = 1.0f;
            StartCoroutine(SetBoolAfterDelay(2.0f));
            my_player.Stop_Moving();
            my_mouse.xRotation = 0f;
            StartCoroutine(SetAttackBool(3.0f));

        }

    }

    IEnumerator SetBoolAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        my_player.can_walk = true;
        my_mouse.can_mouse_move = true;
    }

    IEnumerator SetAttackBool(float delayTime) {
        my_player.Can_attack = false;
        yield return new WaitForSeconds(delayTime);
        my_player.Can_attack = true;
    }

    IEnumerator SetInstruction(GameObject ins, float delayTime) {
        Debug.Log(1);
        ins.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        ins.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch_ins : MonoBehaviour
{
    public GameObject catch_ins;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine("ShowFasterInstruction", 5f);
        }
    }

    IEnumerator ShowFasterInstruction(float delayTime) {
        catch_ins.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        catch_ins.SetActive(false);
        Destroy(gameObject);
    }
}

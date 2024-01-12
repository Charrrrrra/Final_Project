using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPill : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Death")) {
            Destroy(gameObject);
        }
    }
}

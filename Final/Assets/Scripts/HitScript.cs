using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    public PlayerMovement my_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wall") && (my_player.currentSpeed > 1.0f && my_player.currentSpeed < 4.0f)) {
            my_player.currentSpeed *= -1.0f;
            Debug.Log("You hit the wall and get hurt! The V is " + my_player.currentSpeed);
        }
        else if (other.CompareTag("Wall") && (my_player.currentSpeed >= 4.0f)) {
            Debug.Log("You hit the wall and fall! The V is " + my_player.currentSpeed);
        }
        
        if (other.CompareTag("Stone") && my_player.currentSpeed > 0.1f) {
            Debug.Log("You fall!");
            my_player.currentSpeed = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

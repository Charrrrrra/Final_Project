using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSight : MonoBehaviour
{
    public GameObject targetObject;
    public float sight_time;

    void Update()
    {
        if (sight_time > 0.01f) {
            if (Input.GetKey(KeyCode.W))
            {
                if (targetObject != null)
                {
                    targetObject.SetActive(true);
                    sight_time -= 0.01f;
                }
            }
            else
            {
                if (targetObject != null)
                {
                    targetObject.SetActive(false);
                }
            }
        }

        if (sight_time <= 0.01f) {
            sight_time = 0;
            targetObject.SetActive(false);
        }
    }
}

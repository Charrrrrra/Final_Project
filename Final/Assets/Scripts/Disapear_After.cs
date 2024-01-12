using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear_After : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayedAction", 0.00001f);
    }

    void DelayedAction()
    {
        gameObject.SetActive(false);
    }
}

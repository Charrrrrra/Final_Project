using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_bool : MonoBehaviour
{
    public PlayerMovement my_player;
    public PlayerController my_mouse;
    public static Instruction_bool _instance;
    public bool Is_wall = false;
    public bool Is_stone = false;
    private bool Is_D_Shown = false;


    public GameObject D_instruction;

    public GameObject[] instructions;

    private int currentIndex = 0;
    public float displayTime;

    void Awake() {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        HideAll();
        ShowCurrent(); 
    }

    void Update() {
        if (Is_D_Shown == false) {
            if (currentIndex >= instructions.Length) {
                if (Input.GetKeyUp(KeyCode.A)) {
                    StartCoroutine(Show_D(5f));
                    Is_D_Shown = true;
                }
            }
        }
    }

    IEnumerator Show_D(float delayTime) {
        my_player.can_walk = false;
        D_instruction.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        my_player.can_walk = true;
        D_instruction.SetActive(false);
    }

    void HideAll() {
        foreach (GameObject instruction in instructions) {
            instruction.SetActive(false);
        }
    }

    void ShowCurrent() {
        if (currentIndex < instructions.Length) {
            instructions[currentIndex].SetActive(true);
            my_player.can_walk = false;
            Invoke("HideCurrent", displayTime);
        }
    }

    void HideCurrent() {
        instructions[currentIndex].SetActive(false);
        my_player.can_walk = true;
        currentIndex++;
        ShowCurrent();
    }
}

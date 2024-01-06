using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed; 
    public float stopForceMultiplier;
    public float accelerateMultiplier;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    public float currentSpeed = 0.0f;
    public Transform Camera;

    public bool Can_A_Press = true;
    public bool Can_D_Press = true;
    public bool Is_A_Pressing = false;
    public bool Is_D_Pressing = false;
    public Animator animator;
    public GameObject attack_hands;

    public bool can_walk;
    private bool Is_walking;
    public bool Can_attack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Can_attack = true;
        attack_hands.SetActive(false);
    }

    private void GroundMove() {
        moveDirection = Camera.forward; 
        moveDirection.y = 0f;
        moveDirection.Normalize(); 

        if (currentSpeed == 0) {
            currentSpeed = maxSpeed * 0.5f;
            return;
        }
        else if((currentSpeed *= accelerateMultiplier) > maxSpeed) {
            currentSpeed = maxSpeed;
            // currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);
            return;
        }
        else {
            currentSpeed *= accelerateMultiplier;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);
            return;
        }
    }

    public void Stop_Moving() {
        animator.SetBool("RightWalk", false);
        animator.SetBool("LeftWalk", false);
        animator.SetBool("Idle", true);
        animator.speed = 1.0f;
        currentSpeed = 0.0f;
        rb.velocity = Vector3.zero;
        Can_A_Press = true;
        Can_D_Press = true;
    }

    void FixedUpdate() {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && can_walk) {
            if (Input.GetKey(KeyCode.A) && Can_A_Press == true && Is_A_Pressing)
            {
                animator.SetBool("RightWalk", false);
                animator.SetBool("LeftWalk", true);
                animator.SetBool("Idle", false);
                GroundMove(); 
            }
            if (Input.GetKey(KeyCode.D) && Can_D_Press == true && Is_D_Pressing)
            {
                animator.SetBool("RightWalk", true);
                animator.SetBool("LeftWalk", false);
                animator.SetBool("Idle", false);
                GroundMove();
            }
        }

        if (Mathf.Abs(currentSpeed) > 0) {
            if (can_walk) {
                animator.speed = Mathf.Abs(currentSpeed * 0.05f);
            }
            currentSpeed = currentSpeed * stopForceMultiplier; 

            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);

            rb.velocity = moveDirection * currentSpeed;

            if (Mathf.Abs(currentSpeed) < 3f)
            {
                Stop_Moving();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Is_A_Pressing = true;
            StartCoroutine("Stop_Left_Walk");
            // if (Can_A_Press) {
            //     animator.SetBool("RightWalk", false);
            //     animator.SetBool("LeftWalk", true);
            // }
            
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Is_D_Pressing = true;
            StartCoroutine("Stop_Right_Walk");
            // if (Can_D_Press) {
            //     animator.SetBool("RightWalk", true);
            //     animator.SetBool("LeftWalk", false);
            // }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            if (Input.GetKeyUp(KeyCode.A)) {
                Is_A_Pressing = false;
                Can_A_Press = false;
                Can_D_Press = true;
                currentSpeed -= maxSpeed * 0.1f;
                if (Can_A_Press) {
                    animator.SetBool("LeftWalk", false);
                }
                
            }
            if (Input.GetKeyUp(KeyCode.D)) {
                Is_D_Pressing = false;
                Can_D_Press = false;
                Can_A_Press = true;
                currentSpeed -= maxSpeed * 0.1f;
                if (Can_D_Press) {
                    animator.SetBool("RightWalk", false);
                }
            }
        }

        if (Input.GetMouseButton(0) && Can_attack) {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Stop_Right_Walk() {
        yield return new WaitForSeconds(0.4f);
        Can_D_Press = false;
        Is_D_Pressing = false;
        Can_A_Press = true;
    }

    IEnumerator Stop_Left_Walk() {
        yield return new WaitForSeconds(0.4f);
        Can_A_Press = false;
        Is_A_Pressing = false;
        Can_D_Press = true;
    }

    IEnumerator Attack() {
        Can_attack = false;
        animator.speed = 1f;
        animator.SetTrigger("attack");
        
        yield return new WaitForSeconds(0.3f);
        attack_hands.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        attack_hands.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Can_attack = true;
    }

    private void PlayRight() {
        AudioManager._instance.RightFoot();
    }

   private void PlayLeft() {
        AudioManager._instance.LeftFoot();
    }

    private void PlayAttack() {
        AudioManager._instance.Attack_NPC();
    }

    private void PlayFallBack() {
        AudioManager._instance.Fall_Back();
    }

    private void PlayFallFront() {
        AudioManager._instance.Fall_Front();
    }

    private void PlayScream() {
        AudioManager._instance.MonsterScream();
    }
}
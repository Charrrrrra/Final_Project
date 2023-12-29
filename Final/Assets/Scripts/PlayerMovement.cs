using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed; // 最大速度
    public float stopForceMultiplier; // 停止时的减速系数

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    public float currentSpeed = 0.0f;
    public Transform Camera;

    private bool Is_A_Pressed = false;
    private bool Is_D_Pressed = false;
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
        moveDirection = Camera.forward; // 移动方向为相机的朝向
        moveDirection.y = 0f; // 忽略Y轴的朝向
        moveDirection.Normalize(); // 归一化向量

        if (currentSpeed == 0) {
            currentSpeed = maxSpeed * 0.5f;
            return;
        }
        else if((currentSpeed += maxSpeed * 0.2f) > maxSpeed) {
            currentSpeed = maxSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);
            return;
        }
        else {
            currentSpeed += maxSpeed * 0.2f;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);
            return;
        }
    }

    public void Stop_Moving() {
        Debug.Log("Stopped");
        animator.SetBool("Walk", false);
        animator.speed = 1.0f;
        Is_A_Pressed = false;
        Is_D_Pressed = false;
        currentSpeed = 0.0f;
        rb.velocity = Vector3.zero; // 确保速度为零
    }

    void FixedUpdate() {
        if (Mathf.Abs(currentSpeed) > 0)
        {
            animator.SetBool("Walk", true);
            if (can_walk) {
                animator.speed = Mathf.Abs(currentSpeed * 0.1f) + 0.3f;
            }
            // Debug.Log(currentSpeed);
            // 停止按键后，逐渐减速
            currentSpeed = currentSpeed * stopForceMultiplier; 

            // 限制速度在合理范围内
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);

            rb.velocity = moveDirection * currentSpeed;

            if (Mathf.Abs(currentSpeed) < 2.5f)
            {
                Stop_Moving();
            }
        }

    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && can_walk)
        {
            if (Input.GetKeyDown(KeyCode.A) && Is_A_Pressed == false)
            {
                Is_A_Pressed = true;
                Is_D_Pressed = false;
                GroundMove();
            }
            else if (Input.GetKeyDown(KeyCode.D) && Is_D_Pressed == false)
            {
                Is_D_Pressed = true;
                Is_A_Pressed = false;
                GroundMove();
            }
        }

        if (Input.GetMouseButton(0) && Can_attack) {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack() {
        Can_attack = false;
        animator.SetTrigger("attack");
        Debug.Log("attack");
        yield return new WaitForSeconds(0.3f);
        attack_hands.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        attack_hands.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Can_attack = true;
    }

}

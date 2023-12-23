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

    public bool can_walk;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void GroundMove() {
        moveDirection = Camera.forward; // 移动方向为相机的朝向
        moveDirection.y = 0f; // 忽略Y轴的朝向
        moveDirection.Normalize(); // 归一化向量

        if (currentSpeed == 0) {
            currentSpeed = maxSpeed * 0.3f; // 初始速度设为最大速度的10%
            return;
        }
        else if((currentSpeed += maxSpeed * 0.2f) > maxSpeed) {
            currentSpeed = maxSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);
            Debug.Log(currentSpeed);
            return;
        }
        else {
            currentSpeed += maxSpeed * 0.2f;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);
            // Debug.Log(currentSpeed);
            return;
        }
    }

    public void Stop_Moving() {
        animator.SetBool("Walk", false);
        Is_A_Pressed = false;
        Is_D_Pressed = false;
        currentSpeed = 0.0f;
        rb.velocity = Vector3.zero; // 确保速度为零
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
        
        if (Mathf.Abs(currentSpeed) > 0.0f)
        {
            animator.SetBool("Walk", true);
            // animator.speed = Mathf.Abs(currentSpeed * 0.1f) + 0.3f;
            // Debug.Log(currentSpeed);
            // 停止按键后，逐渐减速
            currentSpeed = currentSpeed * stopForceMultiplier; 

            // 限制速度在合理范围内
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, maxSpeed);

            rb.velocity = moveDirection * currentSpeed;

            if (Mathf.Abs(currentSpeed) < 0.5f)
            {
                Stop_Moving();
            }
        }

        

    }
}

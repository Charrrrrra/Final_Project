using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Ai : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, RunSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, runningDistance, runTime, minRunTime, maxRunTime;
    public bool walking, running;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum, randNum2;
    public int destinationAmount;
    public Vector3 rayCasOffset;

    void Start() {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
    }

    void Update() {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCasOffset, direction, out hit, sightDistance)) {
            if (hit.collider.gameObject.tag == "Player") {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("runRoutine");
                StartCoroutine("runRoutine");
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                aiAnim.SetTrigger("sprint");
                running = true;
            }
        }

        if (running == true) {
            
            Vector3 playerPosition = player.position;
            // 计算玩家到NPC的方向向量
            Vector3 directionToPlayer = playerPosition - transform.position;
            // 反转方向向量，让NPC朝着远离玩家的方向移动
            Vector3 directionAwayFromPlayer = -directionToPlayer;
            // 设置NPC的目的地为远离玩家的方向
            Vector3 destination = transform.position + directionAwayFromPlayer;
            ai.destination = destination;
            ai.speed = RunSpeed;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer >= runningDistance) {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                Debug.Log("RunAway!");
                StopCoroutine("runRoutine");
                StartCoroutine("stayIdle");
                running = false;
            }
        }

        if (walking == true) {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            if (ai.remainingDistance <= ai.stoppingDistance) {
                randNum2 = Random.Range(0, 2);
                if (randNum2 == 0) {
                    randNum = Random.Range(0, destinationAmount);
                    currentDest = destinations[randNum];
                }
                if (randNum2 == 1) {
                    aiAnim.ResetTrigger("sprint");
                    aiAnim.ResetTrigger("walk");
                    aiAnim.SetTrigger("idle");
                    ai.speed = 0;
                    StopCoroutine("stayIdle");
                    StartCoroutine("stayIdle");
                    walking = false;
                }
            }
        }
    }

    IEnumerator stayIdle() {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        aiAnim.ResetTrigger("sprint");
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");
    }

    IEnumerator runRoutine() {
        runTime = Random.Range(minRunTime, maxRunTime);
        yield return new WaitForSeconds(runTime);
        Debug.Log("Escape!");
        walking = true;
        running = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        aiAnim.ResetTrigger("idle");
        aiAnim.ResetTrigger("sprint");
        aiAnim.SetTrigger("walk");
    }

}

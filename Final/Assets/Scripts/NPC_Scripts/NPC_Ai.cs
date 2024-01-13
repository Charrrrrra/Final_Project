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
    public bool is_scared;

    public AudioSource NPC_walk_01;
    public AudioSource NPC_walk_02;
    public AudioSource scream;


    void Start() {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        is_scared = false;
    }

    void Update() {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        Vector3 NPCforward = transform.forward;
        // Vector3 startPoint = transform.position + rayCasOffset;
        // Vector3 endPoint = startPoint + direction * sightDistance;
        // Debug.DrawLine(startPoint, endPoint, Color.yellow);
        // Debug.DrawLine(transform.position, transform.forward, Color.blue);
        if (Physics.Raycast(transform.position + rayCasOffset, direction, out hit, sightDistance)) {
            if (hit.collider.gameObject.tag == "Player" && Vector3.Dot(NPCforward, direction) > -0.5) {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("runRoutine");
                StartCoroutine("runRoutine");
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                aiAnim.SetTrigger("sprint");
                running = true;

                if (!is_scared) {
                    aiAnim.SetTrigger("jumpscare");
                    is_scared = true;
                }
            }
        }

        if (running == true) {
            
            Vector3 playerPosition = player.position;
            Vector3 directionToPlayer = playerPosition - transform.position;
            Vector3 directionAwayFromPlayer = -directionToPlayer;
            Vector3 destination = transform.position + directionAwayFromPlayer * 2.5f;
            ai.destination = destination;
            ai.speed = RunSpeed;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer >= runningDistance) {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                is_scared = false;
                StopCoroutine("runRoutine");
                StartCoroutine("stayIdle");
                running = false;
                randNum = Random.Range(0, destinationAmount);
                currentDest = destinations[randNum];
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
        is_scared = false;
        walking = true;
        running = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        aiAnim.ResetTrigger("idle");
        aiAnim.ResetTrigger("sprint");
        aiAnim.SetTrigger("walk");
    }

    private void Walk01() {
        NPC_walk_01.Play();
    }
    private void Walk02() {
        NPC_walk_02.Play();
    }
    private void NPCScream() {
        scream.Play();
    }
}

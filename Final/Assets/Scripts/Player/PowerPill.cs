using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPill : MonoBehaviour
{
    public float rotationSpeed = 30f; // 旋转速度
    public float floatSpeed = 0.5f;   // 悬浮速度
    public float floatHeight = 0.5f;  // 悬浮高度

    private Vector3 initialPosition;
    private void Start()
    {
        // 保存初始位置
        initialPosition = transform.position;
    }

    private void Update()
    {
        // 旋转
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // 上下悬浮
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Death")) {
            Play_Sound();
            Destroy(gameObject);
        }
    }

    private void Play_Sound() {
        AudioManager._instance.Collect_Power();
    }
}

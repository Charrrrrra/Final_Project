using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterScene : MonoBehaviour
{
    public float fadeDuration = 1.0f; // 渐隐的持续时间
    public Image fadePanel; // 用于遮挡屏幕的Image组件

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // 初始化透明度为完全不透明
        Color color = fadePanel.color;
        color.a = 1.0f;
        fadePanel.color = color;

        // 渐隐
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        // 设置透明度为完全透明
        color.a = 0.0f;
        fadePanel.color = color;
    }
}

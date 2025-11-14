using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    [SerializeField] private float speed = 2f;   // 点滅速度
    [SerializeField] private float minAlpha = 0.2f; // 最小透明度
    [SerializeField] private float maxAlpha = 1f;   // 最大透明度

    private Graphic uiGraphic;       // UI用（Image, Textなど）
    private Renderer objectRenderer; // 3Dオブジェクト用
    private Color baseColor;
    private float t = 0f;
    private bool fadingOut = false;

    void Start()
    {
        uiGraphic = GetComponent<Graphic>();
        objectRenderer = GetComponent<Renderer>();

        if (uiGraphic != null)
            baseColor = uiGraphic.color;
        else if (objectRenderer != null)
            baseColor = objectRenderer.material.color;
    }

    void Update()
    {
        if (uiGraphic == null && objectRenderer == null) return;

        // フェードの進行
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
        if (!fadingOut)
            t += Time.deltaTime * speed;
        else
            t -= Time.deltaTime * speed;

        // 端で反転
        if (t >= 1f)
        {
            t = 1f;
            fadingOut = true;
        }
        else if (t <= 0f)
        {
            t = 0f;
            fadingOut = false;
        }

        // アルファ値適用
        if (uiGraphic != null)
        {
            Color c = baseColor;
            c.a = alpha;
            uiGraphic.color = c;
        }
        else if (objectRenderer != null)
        {
            Color c = baseColor;
            c.a = alpha;
            objectRenderer.material.color = c;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Unity_test : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float animationTime = -1f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private float maxRadius = 5f;
    [SerializeField] private float lineWidth = 0.02f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        Shader defaultShader = Shader.Find("Sprites/Default");
        if (defaultShader != null)
        {
            lineRenderer.material = new Material(defaultShader);
        }
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        int segments = 64;
        lineRenderer.positionCount = segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = ((float)i / segments) * 2f * Mathf.PI;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera cam = Camera.main;
            if (cam != null)
            {
                transform.position = cam.transform.position + cam.transform.forward * 5f;
                transform.rotation = cam.transform.rotation;
            }
            animationTime = 0f;
            lineRenderer.enabled = true;
        }

        if (animationTime >= 0f)
        {
            animationTime += Time.deltaTime;
            float progress = animationTime / duration;
            if (progress >= 1f)
            {
                lineRenderer.enabled = false;
                animationTime = -1f;
            }
            else
            {
                float currentRadius = progress * maxRadius;
                transform.localScale = new Vector3(currentRadius, currentRadius, 1f);
                Color col = Color.white;
                col.a = 1f - progress;
                lineRenderer.startColor = col;
                lineRenderer.endColor = col;
            }
        }
    }
}
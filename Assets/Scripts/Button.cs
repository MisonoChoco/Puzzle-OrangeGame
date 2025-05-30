using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class ButtonScaler : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(1080, 1920); // match your Canvas reference resolution
    public Vector2 originalSize = new Vector2(200, 200); // size in reference resolution

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplyScaling();
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (!Application.isPlaying)
            ApplyScaling(); // update in editor
    }

#endif

    private void OnRectTransformDimensionsChange()
    {
        ApplyScaling();
    }

    private void ApplyScaling()
    {
        if (rectTransform == null) return;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float widthScale = screenWidth / referenceResolution.x;
        float heightScale = screenHeight / referenceResolution.y;
        float scale = Mathf.Min(widthScale, heightScale);

        rectTransform.sizeDelta = originalSize * scale;
    }
}
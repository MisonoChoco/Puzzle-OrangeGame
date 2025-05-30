using UnityEngine;

public class Hovering : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleAmount = 1.1f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * scaleAmount;

        AudioClip winClip = Resources.Load<AudioClip>("ClickSound");
        AudioSource.PlayClipAtPoint(winClip, Vector3.zero, 1.5f);
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
    }
}
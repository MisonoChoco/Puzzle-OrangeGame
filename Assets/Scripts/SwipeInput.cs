using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    public FruitController controller;
    private Vector2 startTouch;

    private void Update()
    {
        // Touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 delta = touch.position - startTouch;

                if (delta.magnitude > 50f)
                {
                    Vector2Int dir = Vector2Int.zero;
                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                        dir = delta.x > 0 ? Vector2Int.right : Vector2Int.left;
                    else
                        dir = delta.y > 0 ? Vector2Int.up : Vector2Int.down;

                    Debug.Log($"Swipe detected: {dir}");
                    controller.MoveAll(dir);
                }
            }
        }

        // Mouse input for Editor testing
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - startTouch;

            if (delta.magnitude > 50f)
            {
                Vector2Int dir = Vector2Int.zero;
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    dir = delta.x > 0 ? Vector2Int.right : Vector2Int.left;
                else
                    dir = delta.y > 0 ? Vector2Int.up : Vector2Int.down;

                Debug.Log($"Swipe detected (mouse): {dir}");
                controller.MoveAll(dir);
            }
        }
#endif
    }
}
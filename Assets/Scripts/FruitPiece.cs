using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPiece : MonoBehaviour
{
    public int x, y;
    public GridManager grid;

    public enum FruitPartType
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public FruitPartType partType;
    public Vector2Int GridPosition { get; private set; }

    public void Init(int startX, int startY, GridManager manager)
    {
        grid = manager;
        GridPosition = new Vector2Int(startX, startY);
        grid.GetTile(startX, startY).SetPiece(this);

        float centerX = (grid.width - 1) / 2f;
        float centerY = (grid.height - 1) / 2f;
        transform.position = new Vector3(startX - centerX, startY - centerY, 0);
    }

    public void TryMove(Vector2Int dir, System.Action onComplete, List<Vector2Int> allOccupiedPositions)
    {
        Vector2Int targetPos = GridPosition + dir;
        float centerX = (grid.width - 1) / 2f;
        float centerY = (grid.height - 1) / 2f;
        Vector3 worldTarget = new Vector3(targetPos.x - centerX, targetPos.y - centerY, 0);

        bool isValid = grid.IsValidPosition(targetPos);
        bool isBlocked = grid.IsBlocked(targetPos);
        bool isOccupied = allOccupiedPositions.Contains(targetPos);

        if (isValid && !isBlocked && !isOccupied)
        {
            allOccupiedPositions.Add(targetPos);
            GridPosition = targetPos;
            StartCoroutine(SlideToPosition(worldTarget, onComplete));
            return;
        }

        // Cannot move - stay in place and mark current position
        allOccupiedPositions.Add(GridPosition);
        onComplete?.Invoke();
    }

    private IEnumerator SlideToPosition(Vector3 target, System.Action onComplete)
    {
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
        onComplete?.Invoke();
    }
}
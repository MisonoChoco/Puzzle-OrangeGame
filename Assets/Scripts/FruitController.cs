using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FruitPiece;

public class FruitController : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject[] fruitPrefabs;
    public FruitPiece[] fruitPieces;

    public List<Vector2Int> fruitSpawnPositions = new List<Vector2Int>();

    public enum SpawnMode
    { Corners, Random }

    public SpawnMode spawnMode = SpawnMode.Corners;

    public static class SceneTracker
    {
        public static int lastLevelIndex = -1;
    }

    private void Start()
    {
        SpawnFruitPieces();
    }

    private void SpawnFruitPieces()
    {
        // Use the custom positions if provided; otherwise fallback to corners
        Vector2Int[] startingPositions;

        if (fruitSpawnPositions != null && fruitSpawnPositions.Count >= 4)
        {
            startingPositions = fruitSpawnPositions.GetRange(0, 4).ToArray();
        }
        else
        {
            startingPositions = new Vector2Int[] {
            new Vector2Int(0, 0),
            new Vector2Int(0, gridManager.height - 1),
            new Vector2Int(gridManager.width - 1, 0),
            new Vector2Int(gridManager.width - 1, gridManager.height - 1)
        };
        }

        fruitPieces = new FruitPiece[4];

        float centerX = (gridManager.width - 1) / 2f;
        float centerY = (gridManager.height - 1) / 2f;

        for (int i = 0; i < 4; i++)
        {
            Vector2Int pos = startingPositions[i];
            Vector3 spawnPos = new Vector3(pos.x - centerX, pos.y - centerY, 0);
            GameObject go = Instantiate(fruitPrefabs[i], spawnPos, Quaternion.identity);
            FruitPiece piece = go.GetComponent<FruitPiece>();
            piece.Init(pos.x, pos.y, gridManager);
            fruitPieces[i] = piece;
        }
    }

    public void MoveAll(Vector2Int dir)
    {
        int movedCount = 0;
        int totalPieces = fruitPieces.Length;

        List<Vector2Int> occupiedPositions = new();
        foreach (var piece in fruitPieces)
            occupiedPositions.Add(piece.GridPosition);

        List<FruitPiece> sorted = GetSortedPieces(fruitPieces, dir);

        void OnPieceMoved()
        {
            movedCount++;
            if (movedCount >= totalPieces)
                CheckWinCondition();
        }

        foreach (var piece in sorted)
        {
            occupiedPositions.Remove(piece.GridPosition); // Free current tile
            piece.TryMove(dir, OnPieceMoved, occupiedPositions);
        }
    }

    private List<FruitPiece> GetSortedPieces(FruitPiece[] pieces, Vector2Int dir)
    {
        List<FruitPiece> sorted = new(pieces);

        if (dir == Vector2Int.up)
            sorted.Sort((a, b) => b.GridPosition.y.CompareTo(a.GridPosition.y));
        else if (dir == Vector2Int.down)
            sorted.Sort((a, b) => a.GridPosition.y.CompareTo(b.GridPosition.y));
        else if (dir == Vector2Int.left)
            sorted.Sort((a, b) => a.GridPosition.x.CompareTo(b.GridPosition.x));
        else if (dir == Vector2Int.right)
            sorted.Sort((a, b) => b.GridPosition.x.CompareTo(a.GridPosition.x));

        return sorted;
    }

    private void CheckWinCondition()
    {
        Dictionary<FruitPartType, Vector2Int> positions = new();

        foreach (var piece in fruitPieces)
        {
            positions[piece.partType] = piece.GridPosition;
        }

        if (!positions.ContainsKey(FruitPartType.TopLeft) ||
            !positions.ContainsKey(FruitPartType.TopRight) ||
            !positions.ContainsKey(FruitPartType.BottomLeft) ||
            !positions.ContainsKey(FruitPartType.BottomRight))
            return;

        Vector2Int bottomLeft = positions[FruitPartType.BottomLeft];

        bool isWin =
            positions[FruitPartType.BottomRight] == bottomLeft + Vector2Int.right &&
            positions[FruitPartType.TopLeft] == bottomLeft + Vector2Int.up &&
            positions[FruitPartType.TopRight] == bottomLeft + Vector2Int.right + Vector2Int.up;

        if (isWin)
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

            if (currentLevelIndex >= unlocked)
            {
                PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex + 1);
            }

            Debug.Log("Win!");

            SceneTracker.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Win");
        }
    }
}
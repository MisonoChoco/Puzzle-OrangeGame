using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 5, height = 5;
    public GameObject tilePrefab;
    public Transform gridParent;

    private Tile[,] tiles;
    public GameObject obstaclePrefab;
    public List<Vector2Int> obstaclePositions = new List<Vector2Int>();

    public Tile GetTile(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return null;
        return tiles[x, y];
    }

    private void Awake()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        tiles = new Tile[width, height];

        float centerX = (width - 1) / 2f;
        float centerY = (height - 1) / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x - centerX, y - centerY, 0);
                GameObject go = Instantiate(tilePrefab, pos, Quaternion.identity, gridParent);
                Tile tile = go.GetComponent<Tile>();
                tile.x = x;
                tile.y = y;
                tiles[x, y] = tile;
            }
        }

        foreach (Vector2Int pos in obstaclePositions)
        {
            if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)
            {
                Vector3 obstaclePos = tiles[pos.x, pos.y].transform.position;
                Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity, gridParent);
            }
        }
    }

    public Vector3 GetTileWorldPosition(Vector2Int gridPos)
    {
        if (!IsValidPosition(gridPos)) return Vector3.zero;

        return tiles[gridPos.x, gridPos.y].transform.position;
    }

    public bool IsBlocked(Vector2Int pos)
    {
        return obstaclePositions.Contains(pos);
    }

    public bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }
}
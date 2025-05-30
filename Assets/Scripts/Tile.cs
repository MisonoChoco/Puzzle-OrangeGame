using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y;
    public FruitPiece currentPiece;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public bool IsFree() => currentPiece == null;

    public void SetPiece(FruitPiece piece) => currentPiece = piece;
}
using System;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Vector2Int position = new Vector2Int(0, 0);
    
    public void Initialize(int x, int y)
    {
        position = new Vector2Int(x, y);
    }
}


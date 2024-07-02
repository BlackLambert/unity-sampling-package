using System;
using UnityEngine;

namespace SBaier.Sampling
{
    public class RectTileInfo : TileInfo
    {
        private static Vector2Int[] delta = new Vector2Int[]
        {
            new Vector2Int(0, 1), // Top
            new Vector2Int(1, 1), // TopRight
            new Vector2Int(1, 0), // Right
            new Vector2Int(1, -1), // BottomRight
            new Vector2Int(0, -1), // Bottom
            new Vector2Int(-1, -1), // BottomLeft
            new Vector2Int(-1, 0), // Left
            new Vector2Int(-1, 1) // LeftTop
        };
        
        public int NeighboursAmount => 8;

        public Coordinate2D GetNeighborCoordinate(int tileSideId, Coordinate2D coordinate)
        {
            return coordinate - delta[tileSideId];
        }
        
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Sampling
{
    public class HexTileInfo : TileInfo
    {
        private static Vector2Int[] pointTopDelta = new Vector2Int[]
        {
            new Vector2Int(1, 1), // TopRight
            new Vector2Int(1, 0), // Right
            new Vector2Int(0, -1), // BottomRight
            new Vector2Int(-1, -1), // BottomLeft
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1) // LeftTop
        };
        
        private static Vector2Int[] flatTopDelta = new Vector2Int[]
        {
            new Vector2Int(0, 1), // Top
            new Vector2Int(1, 1), // TopRight
            new Vector2Int(1, 0), // BottomRight
            new Vector2Int(0, -1), // Bottom
            new Vector2Int(-1, -1), // BottomLeft
            new Vector2Int(-1, 0) // LeftTop
        };
        
        public int NeighboursAmount => 6;
        private readonly HexTileRotation _rotation;

        public HexTileInfo(HexTileRotation rotation)
        {
            _rotation = rotation;
        }
        
        public Coordinate2D GetNeighborCoordinate(int tileSideId, Coordinate2D coordinate)
        {
            return coordinate - GetDelta(tileSideId);
        }

        private Vector2Int GetDelta(int tileSideId)
        {
            return _rotation switch
            {
                HexTileRotation.FlatTop => flatTopDelta[tileSideId],
                HexTileRotation.PointyTop => pointTopDelta[tileSideId],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
using System;
using UnityEngine;

namespace SBaier.Sampling
{
    public enum HexTileRotation
    {
        FlatTop = 0,
        PointyTop = 1
    }
    
    public static class HexTileRotationExtensions
    {
        private const float _maxTileSizeToMinFactor = 0.866f;

        public static Coordinate2D ConvertToHexCoordinates(
            this HexTileRotation rotation, 
            HexGridIndentation indentation,
            Coordinate2D gridCoordinate)
        {
            return new Coordinate2D
            {
                X = rotation.GetHexX(indentation, gridCoordinate),
                Y = rotation.GetHexY(indentation, gridCoordinate)
            };
        }
        
        public static Coordinate2D ConvertToGridCoordinates(
            this HexTileRotation rotation, 
            HexGridIndentation indentation,
            Coordinate2D hexCoordinate)
        {
            return new Coordinate2D
            {
                X = rotation.GetGridX(indentation, hexCoordinate),
                Y = rotation.GetGridY(indentation, hexCoordinate)
            };
        }

        public static Vector3 GetScreenPosition(
            this HexTileRotation rotation,
            HexGridIndentation indentation,
            Coordinate2D gridCoordinate,
            float tileMaxSize)
        {
            float minTileSize = tileMaxSize * _maxTileSizeToMinFactor;
            float halfMin = minTileSize / 2;
            float threeForthMax = tileMaxSize * 3 / 4;

            Vector3 addition = rotation switch
            {
                HexTileRotation.FlatTop =>
                    new Vector3(0, -halfMin * (indentation.IsIndented(gridCoordinate.X) ? 1 : 0)),
                HexTileRotation.PointyTop =>
                    new Vector3(-halfMin * (indentation.IsIndented(gridCoordinate.Y) ? 1 : 0), 0),
                _ => throw new ArgumentOutOfRangeException()
            };

            Vector3 position = rotation switch
            {
                HexTileRotation.FlatTop =>
                    new Vector3(threeForthMax * gridCoordinate.X, minTileSize * gridCoordinate.Y),
                HexTileRotation.PointyTop => 
                    new Vector3(minTileSize * gridCoordinate.X, threeForthMax * gridCoordinate.Y),
                _ => throw new ArgumentOutOfRangeException()
            };

            return position + addition;
        }

        private static int GetHexX(
            this HexTileRotation rotation, 
            HexGridIndentation indentation,
            Coordinate2D gridCoordinate)
        {
            return rotation switch
            {
                HexTileRotation.FlatTop => gridCoordinate.X,
                HexTileRotation.PointyTop => gridCoordinate.X + indentation.GetCoordinateAddition(gridCoordinate.Y),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static int GetHexY(this HexTileRotation rotation, 
            HexGridIndentation indentation,
            Coordinate2D gridCoordinate)
        {
            return rotation switch
            {
                HexTileRotation.FlatTop => gridCoordinate.Y + indentation.GetCoordinateAddition(gridCoordinate.X),
                HexTileRotation.PointyTop => gridCoordinate.Y,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static int GetGridX(this HexTileRotation rotation, 
            HexGridIndentation indentation,
            Coordinate2D hexCoordinate)
        {
            return rotation switch
            {
                HexTileRotation.FlatTop => hexCoordinate.X,
                HexTileRotation.PointyTop => hexCoordinate.X - indentation.GetCoordinateAddition(hexCoordinate.Y),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static int GetGridY(this HexTileRotation rotation, 
            HexGridIndentation indentation,
            Coordinate2D hexCoordinate)
        {
            return rotation switch
            {
                HexTileRotation.FlatTop => hexCoordinate.Y - indentation.GetCoordinateAddition(hexCoordinate.X),
                HexTileRotation.PointyTop => hexCoordinate.Y,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
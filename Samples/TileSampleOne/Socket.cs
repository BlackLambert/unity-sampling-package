using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    public enum Socket
    {
        Any = -1,
        None = 0,
        Water = 1,
        Land = 2,
        
        WaterTopLandBottom = 3,
        WaterBottomLandTop = 4,
        WaterRightLandLeft = 5,
        WaterLeftLandRight = 6,
        
        Tower = 7,
        Forest = 8,
    }
}

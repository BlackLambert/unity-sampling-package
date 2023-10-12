using System.Collections.Generic;
using UnityEngine;

namespace PCGToolkit.Sampling.Examples
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "ScriptableObjects/Enemies", order = 1)]
    public class Enemies : ScriptableObject
    {
        [field: SerializeField] public List<Enemy> List { get; private set; } = new List<Enemy>();
    }
}

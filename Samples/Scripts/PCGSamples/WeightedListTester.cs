
using UnityEngine;
using Random = System.Random;

namespace PCGToolkit.Sampling.Examples
{
    public class WeightedListTester : MonoBehaviour
    {
        [SerializeField] private Enemies enemies;
        [SerializeField] private int amount = 10;

        private WeightedList<Enemy> weightedEnemies;

        void Start()
        {
            weightedEnemies = new WeightedList<Enemy>(new Random());

            foreach (Enemy enemy in enemies.List)
            {
                weightedEnemies.Add(enemy, enemy.Weight);
            }

            for (int i = 0; i < amount; i++)
            {
                Debug.Log($"ChosenEnemy {weightedEnemies.GetRandomItem()}");
            }
        }
    }
}

using Game.Marks;
using UnityEngine;

namespace Game.Runtime
{
    public class LevelPrefab : MonoBehaviour
    {
        [field: SerializeField] public EnemySpawnPoint[] EnemySpawnPoints { get; private set; }
        [field: SerializeField] public PlayerCharacterSpawnPoint PlayerCharacterSpawnPoint { get; private set; }

        private int _currentSpawnIndex;

        public EnemySpawnPoint EnemySpawnPoint()
        {
            _currentSpawnIndex = (_currentSpawnIndex + 1) % EnemySpawnPoints.Length;
            return EnemySpawnPoints[_currentSpawnIndex];
        }
    }
}
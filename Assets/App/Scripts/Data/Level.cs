using Game.Runtime;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public class Level
    {
        [field: SerializeField] public LevelPrefab LevelPrefab { get; private set; }
        [field: SerializeField] public Wave[] Waves { get; private set; }

        public int UnitsOnLevel()
        {
            int count = 0;
            foreach (Wave wave in Waves)
            {
                count += wave.UnitCount();
            }
            return count;
        }
    }
}


using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public class SpawnableUnit
    {
        [field: SerializeField] public int Count { get; private set; }
        [field: SerializeField] public UnitType EnemyType { get; private set; }
    }
}


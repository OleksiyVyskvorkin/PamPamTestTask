using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public class Wave
    {
        [field: SerializeField] public float StartTime { get; private set; } = 1f;
        [field: SerializeField] public float EndTime { get; private set; } = 1f;
        [field: SerializeField] public SpawnableUnit[] SpawnableUnits { get; private set; }

        public int UnitCount()
        {
            int count = 0;
            foreach (var unit in SpawnableUnits)
            {
                count += unit.Count;
            }
            return count;
        }
    }

}

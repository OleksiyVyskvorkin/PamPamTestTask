using Game.Data;
using Game.Systems;
using System;
using UnityEngine;
using Game.Creators;

namespace Game.Runtime.Characters
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public Action OnSpawn { get; set; }
        public PoolContainer Pool { get; set; }

        [field: SerializeField] public HealthSystem HealthSystem { get; private set; }
        [field: SerializeField] public AttackSystem AttackSystem { get; private set; }
        [field: SerializeField] public ColorSwitcher ColorSwitcher { get; private set; }
        [field: SerializeField] public UnitType UnitType { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; set; } = 15f;

        [SerializeField] protected UnitConfig UnitConfig;   
    }
}

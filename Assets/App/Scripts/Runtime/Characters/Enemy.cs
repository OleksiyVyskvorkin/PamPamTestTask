using Game.Creators;
using Game.Data;
using Game.Interfaces;
using Game.Providers;
using Game.Systems;
using System;
using UnityEngine;
using Zenject;

namespace Game.Runtime.Characters
{
    public class Enemy : BaseUnit, IExecutable
    {
        public Action<Enemy> OnReturnToPool { get; set; }

        [field: SerializeField] public EnemyMovementSystem MovementSystem { get; set; }
               
        [Inject]
        public void Construct(PoolContainer pool)
        {
            Pool = pool;
            MovementSystem.Init(UnitConfig.MoveSpeed);
            AttackSystem.Init(UnitConfig, this, pool);
            HealthSystem.Init(UnitConfig.MaxHealth);
            ColorSwitcher.Init(UnitType);
            HealthSystem.OnDie += Die;
            OnSpawn += HealthSystem.ResetHealth;
            OnSpawn += MovementSystem.Warp;
        }

        private void OnDestroy()
        {
            OnSpawn = null;
            OnReturnToPool = null;
        }

        public void Execute()
        {
            if (HealthSystem.IsDead) return;
            if (AttackSystem.Target == null)
            {
                AttackSystem.SetTarget(Pool.Character);
                return;
            }

            HealthSystem.HealthBar.Execute();
            if (AttackSystem.CanAttack())
            {
                AttackSystem.TryAttack();
                MovementSystem.Stop();
            }
            else
            {
                var target = AttackSystem.Target;
                if (target != null && !target.HealthSystem.IsDead)
                    MovementSystem.Move(AttackSystem.Target.transform.position);
                else MovementSystem.Stop();
            }
        }

        private void Die()
        {
            OnReturnToPool?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}

using Game.UI;
using System;
using UnityEngine;

namespace Game.Systems
{
    [Serializable]
    public class HealthSystem : IDisposable
    {
        [field: SerializeField] public HealthBar HealthBar { get; private set; }

        public Action<int> OnHit { get; set; }
        public Action OnDie { get; set; }
        public float MaxHealth { get; private set; }
        public float Health { get; private set; }
        public bool IsDead => Health <= 0;

        public void Dispose()
        {
            OnHit = null;
            OnDie = null;
        }

        public void Init(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
            HealthBar?.Initialize(this);
        }

        public void ResetHealth()
        {
            Health = MaxHealth;
            HealthBar?.ResetHealth();
        }

        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            Health -= damage;

            OnHit?.Invoke(damage);

            if (Health <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}



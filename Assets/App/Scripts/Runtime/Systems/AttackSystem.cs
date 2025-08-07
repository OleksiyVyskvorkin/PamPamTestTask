using Game.Creators;
using Game.Data;
using Game.Runtime.Characters;
using System;
using UnityEngine;

namespace Game.Systems
{
    [Serializable]
    public class AttackSystem
    {       
        public BaseUnit Target { get; private set; }
        public BaseUnit Unit { get; private set; }
        public int Damage { get; private set; }

        [field: SerializeField] public LayerMask LayerMask { get; private set; }

        [SerializeField] private float _angleToStartShoot = 10;
        [SerializeField] private Transform _startProjectilePosition;

        private float _range;
        private float _delayBetweenAttack;       
        private float _attackTime;
        private PoolContainer _pool;

        public void Init(UnitConfig config, BaseUnit unit, PoolContainer pool)
        {
            _pool = pool;
            Unit = unit;
            _range = config.Range;
            Damage = config.Damage;
            _delayBetweenAttack = config.DelayBetweenAttack;
        }

        public void Attack()
        {
            var projectile = _pool.GetProjectile();
            projectile.transform.position = _startProjectilePosition.position;
            projectile.Init(Unit, RayDirection(), LayerMask);
        }

        public void TryAttack()
        {
            if (Time.time > _attackTime && IsLookingAtTarget())
            {
                _attackTime = Time.time + _delayBetweenAttack;
                Attack();
            }
            else
            {
                Unit.transform.LookAt(Target.transform.position);
            }
        }

        public bool CanAttack()
        {
            if (Target == null) return false;
            if (Target.HealthSystem.IsDead) return false;
            if (Vector3.Distance(Target.transform.position, Unit.transform.position) > _range) return false;
            var ray = new Ray(RayStartPosition(), RayDirection());
            return Physics.Raycast(ray, out var hit, _range, LayerMask) && hit.collider != null
                && hit.collider.TryGetComponent<BaseUnit>(out var unit);
        }

        public void SetTarget(BaseUnit unit)
        {
            Target = unit;
        }

        private bool IsLookingAtTarget()
        {
            float angle = Vector3.Angle(_startProjectilePosition.transform.forward, RayDirection());
            return angle < _angleToStartShoot;
        }

        public Vector3 RayStartPosition() => _startProjectilePosition.position;
        private Vector3 RayDirection() => (Target.transform.position - RayStartPosition()).normalized;
    }
}
using Zenject;
using UnityEngine;
using Game.Systems;
using System.Linq;
using Game.Creators;

namespace Game.Runtime.Characters
{
    public class PlayerCharacter : BaseUnit
    {
        private FloatingJoystick _joystick;

        [SerializeField] private PlayerMovementSystem _movementSystem;
        [SerializeField] private AttackZone _attackZone;

        [Inject]
        public void Construct(FloatingJoystick joystick, PoolContainer pool)
        {
            _joystick = joystick;
            Pool = pool;
            _movementSystem.Init(UnitConfig.MoveSpeed);
            AttackSystem.Init(UnitConfig, this, pool);
            HealthSystem.Init(UnitConfig.MaxHealth);
            ColorSwitcher.Init(UnitType);           
        }

        private void Start()
        {
            _attackZone.Activate(UnitConfig.Range);
        }

        private void Update()
        {
            if (HealthSystem.IsDead) return;

            HealthSystem.HealthBar.Execute();
            _movementSystem.Move(_joystick.Direction);
            _movementSystem.RotateCharacter(AttackSystem.Target, this);
            if (AttackSystem.CanAttack())
            {
                AttackSystem.TryAttack();
            }
            else
            {
                AttackSystem.SetTarget(TrySwitchTarget());
            }
        }

        private BaseUnit TrySwitchTarget()
        {
            var enemies = Pool.ActiveEnemies;
            if (enemies.Count == 0) return null;
            enemies = enemies.OrderBy(e => (e.transform.position - transform.position).sqrMagnitude).ToList();
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < UnitConfig.Range)
                {
                    var ray = new Ray(AttackSystem.RayStartPosition(), (enemy.transform.position - transform.position).normalized);
                    if (Physics.Raycast(ray, out var hit, UnitConfig.Range, AttackSystem.LayerMask) && hit.collider != null
                        && hit.collider.TryGetComponent<Enemy>(out var unit))
                    {
                        return enemy;
                    }
                }
            }
            return null;
        }
    }
}

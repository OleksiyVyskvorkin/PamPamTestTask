using Game.Interfaces;
using Game.Runtime.Characters;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Runtime
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IExecutable
    {
        public Action<Projectile> OnReturnToPool { get; set; }

        [SerializeField] private ColorSwitcher _colorSwitcher;
        [SerializeField] private Rigidbody _rb;

        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _lifeTime = 5f;

        private BaseUnit _attacker;
        private Vector3 _previousPosition;
        private LayerMask _layerMask;

        public void Init(BaseUnit attacker, Vector3 direction, LayerMask layerMask)
        {
            direction.y = 0;
            gameObject.SetActive(true);
            _layerMask = layerMask;            
            _previousPosition = transform.position;
            _attacker = attacker;
            _colorSwitcher.Init(_attacker.AttackSystem.Target.UnitType);
            _rb.velocity = direction * _speed;
            StartCoroutine(CDisable());
        }

        private void OnDisable()
        {            
            OnReturnToPool?.Invoke(this);
        }

        public void Execute()
        {
            if (!gameObject.activeInHierarchy) return;

            var ray = new Ray(transform.position, _rb.velocity.normalized);
            float distance = Vector3.Distance(transform.position, _previousPosition);
            if (Physics.Raycast(ray, out var hit, distance, _layerMask) && hit.collider != null)
            {
                if (hit.collider.TryGetComponent<BaseUnit>(out var unit))
                    unit.HealthSystem.TakeDamage(_attacker.AttackSystem.Damage);
                gameObject.SetActive(false);
            }
            
            _previousPosition = transform.position;
        }

        private IEnumerator CDisable()
        {
            yield return new WaitForSeconds(_lifeTime);
            gameObject.SetActive(false);
        }
    }
}


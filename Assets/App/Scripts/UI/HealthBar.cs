using DG.Tweening;
using Game.Interfaces;
using Game.Services;
using Game.Systems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static UnityEditor.PlayerSettings;

namespace Game.UI
{
    public class HealthBar : MonoBehaviour, IExecutable
    {
        [SerializeField] private Slider _slider;

        private Transform _followTarget;
        private HealthSystem _healthSystem;
        private Camera _camera;
        private RectTransform _rectTransform;

        [Inject]
        public void Construct(ParentsHolder parentsHolder)
        {
            _followTarget = new GameObject("FollowTarget").GetComponent<Transform>();
            _followTarget.transform.SetParent(transform.parent, true);
            _followTarget.transform.localPosition = transform.localPosition;

            _camera = Camera.main;
            transform.SetParent(parentsHolder.HealthBarParent, true);
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Execute()
        {
            _rectTransform.position = _camera.WorldToScreenPoint(_followTarget.position);
        }

        public void ResetHealth()
        {
            _slider.DOKill();
            _slider.maxValue = _healthSystem.MaxHealth;
            _slider.value = _healthSystem.MaxHealth;
            gameObject.SetActive(true);
        }

        public void Initialize(HealthSystem health)
        {
            _healthSystem = health;
            health.OnHit += OnTakeDamage;
            health.OnDie += Disable;
            _slider.maxValue = health.MaxHealth;
            _slider.value = health.MaxHealth;
        }

        public void OnTakeDamage(int value)
        {
            _slider.DOKill();
            _slider.maxValue = _healthSystem.MaxHealth;
            _slider.DOValue(_healthSystem.Health, 0.2f).SetUpdate(true);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}

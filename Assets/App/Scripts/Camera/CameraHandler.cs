using Game.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Data
{
    public class CameraHandler : MonoBehaviour
    {
        private CameraConfig _config;
        private Transform _target;

        [Inject]
        public void Construct(MainConfig config, GameController gameController)
        {
            _config = config.CameraConfig;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            transform.position = _target.position + _config.Offset;
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            transform.position = _target.position + _config.Offset;
        }
    }
}


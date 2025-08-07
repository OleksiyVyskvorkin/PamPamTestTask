using UnityEngine;

namespace Game.Runtime
{
    [System.Serializable]
    public class AttackZone
    {
        [SerializeField] private ParticleSystem _ps;

        public void Activate(float range)
        {
            _ps.transform.localScale = Vector3.one * range * 2;
            _ps.Play();
        }
    }
}
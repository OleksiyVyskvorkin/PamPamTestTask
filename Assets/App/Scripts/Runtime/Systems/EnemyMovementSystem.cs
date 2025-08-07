using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Systems
{
    [Serializable]
    public class EnemyMovementSystem : MoveSystem
    {
        [SerializeField] private NavMeshAgent _agent;

        public void Init(float speed)
        {
            Speed = speed;
            _agent.speed = speed;
        }

        public void Warp()
        {
            _agent.Warp(_agent.transform.position);
        }

        public override void Move(Vector3 direction)
        {
            if (!_agent.isOnNavMesh) return;
            _agent.isStopped = false;
            _agent.SetDestination(direction);
        }

        public void Stop()
        {
            if (!_agent.isOnNavMesh) return;
            _agent.isStopped = true;
        }
    }
}



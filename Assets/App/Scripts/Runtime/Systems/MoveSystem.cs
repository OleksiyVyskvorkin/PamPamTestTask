using System;
using UnityEngine;

namespace Game.Systems
{
    [Serializable]
    public abstract class MoveSystem
    {
        public float Speed { get; set; }

        public abstract void Move(Vector3 direction);
    }
}



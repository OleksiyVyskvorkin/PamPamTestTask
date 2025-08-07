using Game.Runtime.Characters;
using System;
using UnityEngine;

namespace Game.Systems
{
    [Serializable]
    public class PlayerMovementSystem : MoveSystem
    {
        [SerializeField] private CharacterController _controller;

        private Vector3 _moveDirection;

        public void Init(float speed)
        {
            Speed = speed;
        }

        public override void Move(Vector3 direction)
        {
            _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
            _controller.Move(_moveDirection * Speed * Time.deltaTime);
        }

        public void RotateCharacter(BaseUnit target, BaseUnit character)
        {
            if (target == null)
            {
                Quaternion moveRotation = Quaternion.LookRotation(_moveDirection);
                _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation,
                    moveRotation, character.RotationSpeed * Time.deltaTime);
            }
            else
            {
                _controller.transform.LookAt(target.transform.position);
            }
        }
    }
}
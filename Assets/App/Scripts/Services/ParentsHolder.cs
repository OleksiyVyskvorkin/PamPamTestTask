using UnityEngine;

namespace Game.Services
{
    public class ParentsHolder : MonoBehaviour
    {
        [field: SerializeField] public Transform EntityContainer { get; private set; }
        [field: SerializeField] public Transform HealthBarParent { get; private set; }
    }
}


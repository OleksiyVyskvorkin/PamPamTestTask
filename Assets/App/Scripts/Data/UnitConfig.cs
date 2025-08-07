using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Data/UnitConfig")]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; } = 10;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public int Damage { get; private set; } = 1;
        [field: SerializeField] public float DelayBetweenAttack { get; private set; } = 1f;
        [field: SerializeField] public float Range { get; private set; } = 1f;
    }
}

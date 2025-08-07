using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public Level[] Levels { get; private set; }
    }
}


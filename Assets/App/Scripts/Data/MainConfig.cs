using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Data/MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        [field: SerializeField] public CreatableConfig CreatableConfig { get; private set; }
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
    }
}

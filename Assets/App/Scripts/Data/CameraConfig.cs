using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Data/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 Offset { get; private set; } = Vector3.up * 10;
    }
}


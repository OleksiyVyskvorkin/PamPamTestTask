using System;
using UnityEngine;

namespace Game.Runtime
{
    [Serializable]
    public class ColorSwitcher 
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public void Init(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Red:
                    _meshRenderer.material.color = Color.red;
                    break;
                case UnitType.Green:
                    _meshRenderer.material.color = Color.green;
                    break;
                case UnitType.Yellow:
                    _meshRenderer.material.color = Color.yellow;
                    break;
                case UnitType.Blue:
                    _meshRenderer.material.color = Color.blue;
                    break;
            }
        }
    }
}

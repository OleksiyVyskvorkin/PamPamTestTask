using Game.Runtime;
using Game.Runtime.Characters;
using Game.UI;
using System.Linq;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "CreatableConfig", menuName = "Data/CreatableConfig")]
    public class CreatableConfig : ScriptableObject
    {
        [field: SerializeField] public ChangeLevelButton ChangeLevelButton { get; private set; }
        [field: SerializeField] public Enemy[] Enemies { get; private set; }
        [field: SerializeField] public PlayerCharacter Character { get; private set; }
        [field: SerializeField] public Projectile Projectile { get; private set; }

        public Enemy GetEnemy(UnitType unitType)
        {
            return Enemies.Where(e => e.UnitType == unitType).First();
        }
    }
}


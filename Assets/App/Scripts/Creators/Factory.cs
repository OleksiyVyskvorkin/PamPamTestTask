using UnityEngine;
using Zenject;

namespace Game.Creators
{
    public class Factory
    {
        private DiContainer _diContainer;

        public Factory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Create<T>(MonoBehaviour monoBehaviour) where T : MonoBehaviour
        {
            var newObject = _diContainer.InstantiatePrefabForComponent<T>(monoBehaviour);
            _diContainer.Rebind<T>().FromInstance(newObject);
            return newObject;
        }
    }
}

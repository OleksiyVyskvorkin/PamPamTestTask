using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
    public class AppStartup : MonoBehaviour
    {
        private LevelLoader _levelLoader;

        [Inject]
        public void Initialize(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        private void Start()
        {
            //TO DO some SDK initialize
            LoadLevel();
        }

        private void LoadLevel() => _levelLoader.LoadLevel(1);
    }
}
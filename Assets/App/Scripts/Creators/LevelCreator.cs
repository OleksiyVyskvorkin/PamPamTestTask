using Game.Data;
using Cysharp.Threading.Tasks;
using Game.Runtime;
using System;
using Game.Providers;

namespace Game.Creators
{
    public class LevelCreator : IDisposable
    {
        public Action<int> OnLevelChanged { get; set; }
        public static LevelPrefab CurrentLevel { get; private set; }

        private Factory _factory;
        private LevelConfig _levelConfig;

        public LevelCreator(Factory factory, MainConfig config, GameDataProvider gameDataProvider)
        {
            _levelConfig = config.LevelConfig;
            _factory = factory;
            OnLevelChanged += gameDataProvider.ChangeLevelId;
            CreateLevel(0);
        }

        public async void CreateLevel(int index)
        {
            if (CurrentLevel != null)
            {
                UnityEngine.Object.Destroy(CurrentLevel.gameObject);
                await UniTask.Yield();
            }
            CurrentLevel = _factory.Create<LevelPrefab>(_levelConfig.Levels[index].LevelPrefab);
            CurrentLevel.transform.SetParent(null);
            OnLevelChanged?.Invoke(index);
        }

        public void Dispose()
        {
            OnLevelChanged = null;
        }
    }
}

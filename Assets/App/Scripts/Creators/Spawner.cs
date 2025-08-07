using Game.Data;
using Game.Infrastructure;
using Game.Providers;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Creators
{
    public class Spawner : MonoBehaviour
    {
        private PoolContainer _poolContainer;
        private LevelConfig _levelConfig;
        private GameDataProvider _gameDataProvider;
        private CameraHandler _cameraHandler;

        [Inject]
        public void Initialize(PoolContainer poolContainer,
            GameController gameController,
            MainConfig config,
            GameDataProvider gameDataProvider,
            CameraHandler cameraHandler)
        {
            _gameDataProvider = gameDataProvider;
            _levelConfig = config.LevelConfig;
            _poolContainer = poolContainer;
            _cameraHandler = cameraHandler;
            gameController.OnLoseGame += StopSpawn;
            gameController.OnStartPlay += StartSpawn;
        }

        public void StopSpawn()
        {
            StopAllCoroutines();
        }

        public void StartSpawn()
        {
            _poolContainer.Character.transform.position = LevelCreator.CurrentLevel.PlayerCharacterSpawnPoint.transform.position;
            _cameraHandler.SetTarget(_poolContainer.Character.transform);
            foreach (var wave in _levelConfig.Levels[_gameDataProvider.CurrentLevelId].Waves)
            {
                StartCoroutine(CStartWave(wave));
            }
        }

        private IEnumerator CStartWave(Wave wave)
        {
            yield return new WaitForSeconds(wave.StartTime);         
            foreach (var unit in wave.SpawnableUnits)
            {
                StartCoroutine(CSpawnUnit(wave, unit));
            }
        }

        private IEnumerator CSpawnUnit(Wave wave, SpawnableUnit spawnableUnit)
        {
            float spawnTime = (wave.EndTime - wave.StartTime) / spawnableUnit.Count;
            int count = 0;
            while (count < spawnableUnit.Count)
            {
                var spawnPosition = LevelCreator.CurrentLevel.EnemySpawnPoint().transform.position;
                var unit = _poolContainer.GetEnemy(spawnableUnit.EnemyType);
                unit.transform.position = spawnPosition;
                unit.gameObject.SetActive(true);
                unit.OnSpawn?.Invoke();
                count++;
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}


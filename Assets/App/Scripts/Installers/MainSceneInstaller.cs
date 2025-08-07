using Game.Creators;
using Game.Data;
using Game.Infrastructure;
using Game.Providers;
using Game.Runtime;
using Game.Services;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private PoolContainer _poolContainer;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private CameraHandler _camera;
        [SerializeField] private ParentsHolder _parentsHolder;
        [SerializeField] private ScoreHandler _scoreHandler;
        [SerializeField] private ExecuteHandler _executeHandler;

        public override void InstallBindings()
        {
            Container.Bind<Spawner>().FromInstance(_spawner).AsSingle().NonLazy();
            Container.Bind<PoolContainer>().FromInstance(_poolContainer).AsSingle().NonLazy();
            Container.Bind<FloatingJoystick>().FromInstance(_joystick).AsSingle().NonLazy();
            Container.Bind<CameraHandler>().FromInstance(_camera).AsSingle().NonLazy();
            Container.Bind<ParentsHolder>().FromInstance(_parentsHolder).AsSingle().NonLazy();
            Container.Bind<ScoreHandler>().FromInstance(_scoreHandler).AsSingle().NonLazy();
            Container.Bind<ExecuteHandler>().FromInstance(_executeHandler).AsSingle().NonLazy();

            Container.Bind<Factory>().AsSingle().NonLazy();
            Container.Bind<LevelCreator>().AsSingle().NonLazy();
            Container.Bind<GameController>().AsSingle().NonLazy();
            Container.Bind<GameDataProvider>().AsSingle().NonLazy();
        }
    }
}


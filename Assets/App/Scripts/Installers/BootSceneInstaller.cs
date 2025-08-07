using Game.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class BootSceneInstaller : MonoInstaller
    {
        [SerializeField] private AppStartup _appStartup;

        public override void InstallBindings()
        {
            Container.Bind<AppStartup>().FromInstance(_appStartup).AsSingle().NonLazy();
        }
    }
}

using Game.Creators;
using Game.Data;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class StartPlayWindow : BaseWindow
    {
        [SerializeField] private RectTransform _levelButtonsContainer;

        [Inject]
        public void Construct(MainConfig config, Factory factory)
        {
            for (int i = 0; i < config.LevelConfig.Levels.Length; i++)
            {
                var button = factory.Create<ChangeLevelButton>(config.CreatableConfig.ChangeLevelButton);
                button.transform.SetParent(_levelButtonsContainer, true);
                button.Initialize(i);
            }
        }
    }
}


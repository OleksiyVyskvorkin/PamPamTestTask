using Game.Creators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class ChangeLevelButton : AnimatedButton
    {
        [SerializeField] private TMP_Text _buttonText;

        private LevelCreator _levelCreator;
        private int _levelIndex;

        [Inject]
        public void Construct(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
            Button.onClick.AddListener(ChangeLevel);
        }

        public void Initialize(int i)
        {
            _levelIndex = i;
            _buttonText.text = $"LEVEL{i + 1}";
        }

        private void ChangeLevel()
        {
            _levelCreator.CreateLevel(_levelIndex);
        }       
    }
}


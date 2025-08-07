using Game.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private BaseWindow _gameplayWindow;
        [SerializeField] private BaseWindow _loseWindow;
        [SerializeField] private BaseWindow _winWindow;
        [SerializeField] private StartPlayWindow _startPlayWindow;

        [Inject]
        public void Construct(GameController gameController)
        {
            gameController.OnWinGame += _winWindow.Show;
            gameController.OnLoseGame += _loseWindow.Show;
            gameController.OnWinGame += _gameplayWindow.Hide;
            gameController.OnLoseGame += _gameplayWindow.Hide;

            _loseWindow.CloseButton.Button.onClick.AddListener(() => 
            {
                _gameplayWindow.Hide();
                _startPlayWindow.Show();
            });
            _winWindow.CloseButton.Button.onClick.AddListener(() => 
            {
                _gameplayWindow.Hide();
                _startPlayWindow.Show();
            });
            _startPlayWindow.CloseButton.Button.onClick.AddListener(() =>
            {
                _gameplayWindow.Show();
                gameController.OnStartPlay?.Invoke();
            });
        }
    }
}


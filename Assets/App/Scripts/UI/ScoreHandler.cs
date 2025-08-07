using Game.Data;
using Game.Infrastructure;
using Game.Providers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ScoreHandler : MonoBehaviour
    {
        public int Score { get; private set; }

        [field: SerializeField] private TMP_Text[] _scoreTexts { get; set; }

        private LevelConfig _levelConfig;
        private GameDataProvider _gameDataProvider;
        private GameController _gameController;

        [Inject]
        public void Construct(GameController gameController, MainConfig config, GameDataProvider gameDataProvider)
        {
            _gameController = gameController;
            gameController.OnLoseGame += ChangeScoreText;
            gameController.OnWinGame += ChangeScoreText;
            gameController.OnStartPlay += OnStartGame;
            _levelConfig = config.LevelConfig;
            _gameDataProvider = gameDataProvider;
        }

        private void ChangeScoreText()
        {
            foreach (var score in _scoreTexts) score.text = $"SCORE:{Score}";
        }
        public void AddScore()
        {
            Score++;
            if (Score >= _levelConfig.Levels[_gameDataProvider.CurrentLevelId].UnitsOnLevel())
                _gameController.WinGame();
        }
        private void OnStartGame() => Score = 0;
    }
}


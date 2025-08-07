using System;

namespace Game.Infrastructure
{
    public class GameController : IDisposable
    {
        public GameState State { get; private set; }

        public Action OnStartPlay;
        public Action OnWinGame;
        public Action OnLoseGame;

        public GameController()
        {
            State = GameState.Playing;
            OnStartPlay += StartPlay;
        }

        public void Dispose()
        {
            OnStartPlay = null;
            OnWinGame = null;
            OnLoseGame = null;
        }

        public void WinGame()
        {
            if (State == GameState.Playing)
            {
                State = GameState.Win;
                OnWinGame?.Invoke();
            }
        }

        public void LoseGame()
        {
            if (State == GameState.Playing)
            {
                State = GameState.Lose;
                OnLoseGame?.Invoke();
            }          
        }

        private void StartPlay()
        {
            State = GameState.Playing;
        }
    }
}


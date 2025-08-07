using Game.Infrastructure;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class SurviveTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text[] _resultTimers;

        [Inject]
        public void Construct(GameController gameController)
        {
            gameController.OnStartPlay += StartTimer;
            gameController.OnWinGame += StopTimer;
            gameController.OnLoseGame += StopTimer;
        }

        private void StartTimer()
        {
            StartCoroutine(CChangeTimer());
        }

        private IEnumerator CChangeTimer()
        {
            int time = 0;
            _timerText.text = "0";
            while (true)
            {
                yield return new WaitForSeconds(1f);
                time += 1;
                _timerText.text = $"{time}";
            }
        }

        private void StopTimer()
        {
            StopAllCoroutines();
            foreach (var timer in _resultTimers)
            {
                timer.text = "TIME:" + _timerText.text;
            }
        }
    }
}


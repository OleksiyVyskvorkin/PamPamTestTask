using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private CanvasGroup _canvasGroup;

        private async void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            await UniTask.Delay(1000);
            if (_canvasGroup.alpha < 1f) gameObject.SetActive(false);
        }

        public void LoadLevel(int index)
        {
            gameObject.SetActive(true);
            StartCoroutine(LoadSceneAsync(index));
        }

        private IEnumerator LoadSceneAsync(int index)
        {
            _canvasGroup.alpha = 1f;
            Time.timeScale = 1f;

            _progressBar.value = 0;

            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            operation.allowSceneActivation = false;

            float progress = 0;

            while (!operation.isDone)
            {
                progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);

                _progressBar.value = progress;
                if (progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                    _progressBar.value = 1;
                }

                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
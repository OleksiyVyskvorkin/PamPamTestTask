using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class AnimatedButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }

        [Inject]
        public void Construct()
        {
            Button.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public void OnClickButton()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
            transform.DOScale(1.07f, 0.1f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo);
        }
    }
}


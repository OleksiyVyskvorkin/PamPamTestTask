using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class BaseWindow : MonoBehaviour
    {
        [field:SerializeField] public AnimatedButton CloseButton { get; private set; }

        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _animationTime = 0.25f;

        private void Awake()
        {
            if (CloseButton) CloseButton.Button?.onClick.AddListener(Hide);
        }

        public virtual void Show()
        {
            _group.gameObject.SetActive(true);
            _group.alpha = 0f;
            _group.DOFade(1f, _animationTime);
        }

        public virtual void Hide() 
        {
            _group.DOKill();
            _group.DOFade(0, _animationTime).OnComplete(() => _group.gameObject.SetActive(false));
        }
    }
}


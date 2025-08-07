using UnityEngine;

namespace Game.UI
{
    public class SafeAreaTopResizer : MonoBehaviour
    {
        [SerializeField] private Transform _rootCanvas;

        public float TopOffset { get; private set; }

        void Start()
        {
            var safeArea = Screen.safeArea;
            var topPixels = Screen.height - (safeArea.height + safeArea.position.y);
            float scale = 1f;
            if (_rootCanvas != null)
            {
                scale = _rootCanvas.localScale.y;
                if (scale < float.Epsilon) scale = 1f;
            }
            TopOffset = -topPixels / scale;
            var rect = GetComponent<RectTransform>();
            rect.offsetMax = new Vector2(rect.offsetMax.x, TopOffset);
        }
    }
}


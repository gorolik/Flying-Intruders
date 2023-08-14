using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            Init();
            SubscribeUpdates();
        }

        private void OnEnable()
        {
            SubscribeUpdates();
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            Cleanup();
            _closeButton.onClick.RemoveListener(Close);
        }

        private void Close() => 
            Destroy(gameObject);

        protected virtual void Init() {}
        protected virtual void SubscribeUpdates() {}
        protected virtual void Cleanup() {}
    }
}
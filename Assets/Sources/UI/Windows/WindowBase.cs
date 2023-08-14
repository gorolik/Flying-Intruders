using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable() => 
            _closeButton.onClick.AddListener(Close);

        private void OnDisable() => 
            _closeButton.onClick.RemoveListener(Close);

        private void Close() => 
            Destroy(gameObject);
    }
}
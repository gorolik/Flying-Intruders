using Sources.Services.Input;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Behaviour.Player
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private IInputSurvice _inputSurvice;
        private IUIFactory _uiFactory;
        private Camera _camera;

        public void Construct(IInputSurvice inputSurvice, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _inputSurvice = inputSurvice;
            
            uiFactory.ActiveWindowsCountChanged += OnActiveWindowsCountChanged;
        }

        private void Start()
        {
            _camera = Camera.main;
            
            Show();
        }

        private void Update()
        {
            Vector2 newPosition = _camera.ScreenToWorldPoint(_inputSurvice.CursorPosition);
            transform.position = newPosition;
        }

        private void OnDestroy()
        {
            _uiFactory.ActiveWindowsCountChanged -= OnActiveWindowsCountChanged;
            Hide();
        }

        private void OnActiveWindowsCountChanged(int count)
        {
            print(count);
            
            if (count > 0)
                Hide();
            else
                Show();
        }

        private void Show()
        {
            _spriteRenderer.enabled = true;
            Cursor.visible = false;
        }

        private void Hide()
        {
            _spriteRenderer.enabled = false;
            Cursor.visible = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                Show();
        }
    }
}
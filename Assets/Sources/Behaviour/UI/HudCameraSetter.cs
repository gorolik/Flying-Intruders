using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class HudCameraSetter : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Start() => 
            _canvas.worldCamera = Camera.main;
    }
}

using UnityEngine;

namespace Sources.Services.Input
{
    public class StandaloneInputService : IInputSurvice
    {
        public Vector2 CursorPosition 
            => UnityEngine.Input.mousePosition;

        public bool IsClicked =>
            UnityEngine.Input.GetMouseButton(0);
    }
}
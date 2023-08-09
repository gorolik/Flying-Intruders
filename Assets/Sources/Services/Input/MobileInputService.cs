using UnityEngine;

namespace Sources.Services.Input
{
    public class MobileInputService : IInputSurvice
    {
        public Vector2 CursorPosition => 
            UnityEngine.Input.GetTouch(0).position;

        public bool IsClicked =>
            UnityEngine.Input.touchCount > 0;
    }
}
using UnityEngine;

namespace Sources.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 CursorPosition => 
            UnityEngine.Input.GetTouch(0).position;

        public override bool IsClicked =>
            UnityEngine.Input.touchCount > 0;
    }
}
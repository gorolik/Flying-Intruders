using UnityEngine;

namespace Assets.Sources.Services.Input
{
    class MobileInputService : InputService
    {
        public override Vector2 CursorPosition
            => UnityEngine.Input.GetTouch(0).position;
    }
}
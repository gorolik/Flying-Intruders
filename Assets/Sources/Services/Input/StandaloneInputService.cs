using UnityEngine;

namespace Assets.Sources.Services.Input
{
    class StandaloneInputService : InputService
    {
        public override Vector2 CursorPosition 
            => UnityEngine.Input.mousePosition;
    }
}
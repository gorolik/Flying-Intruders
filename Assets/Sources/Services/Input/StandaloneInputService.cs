using UnityEngine;

namespace Sources.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 CursorPosition 
            => UnityEngine.Input.mousePosition;
    }
}
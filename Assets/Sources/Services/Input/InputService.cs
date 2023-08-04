using UnityEngine;

namespace Sources.Services.Input
{
    public abstract class InputService : IInputSurvice
    {
        public abstract Vector2 CursorPosition { get; }
    }
}
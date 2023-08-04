using UnityEngine;

namespace Assets.Sources.Services.Input
{
    abstract class InputService : IInputSurvice
    {
        public abstract Vector2 CursorPosition { get; }
    }
}
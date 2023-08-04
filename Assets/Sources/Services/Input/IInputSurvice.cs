using UnityEngine;

namespace Sources.Services.Input
{
    public interface IInputSurvice
    {
        Vector2 CursorPosition { get; }
    }
}
using Sources.Infrastructure.DI;
using UnityEngine;

namespace Sources.Services.Input
{
    public interface IInputSurvice : IService
    {
        Vector2 CursorPosition { get; }
    }
}
using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Services.Input
{
    public interface IInputSurvice : IService
    {
        Vector2 CursorPosition { get; }
    }
}
using Sources.Infrastructure.DI;

namespace Sources.Services.Difficult
{
    public interface IDifficultService : IService
    {
        float DifficultPerSecond { get; }
        float GetDifficult(float playTime);
    }
}
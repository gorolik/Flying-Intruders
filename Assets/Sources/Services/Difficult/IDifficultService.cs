using Sources.Infrastructure.DI;

namespace Sources.Services.Difficult
{
    public interface IDifficultService : IService
    {
        const int MinDifficultValue = 1;
        
        float DifficultPerSecond { get; }
        float MaxDifficultValue { get; set; }
        float GetDifficult(float playTime);
    }
}
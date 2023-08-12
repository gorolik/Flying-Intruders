namespace Sources.Services.Difficult
{
    public class DifficultService : IDifficultService
    {
        public float DifficultPerSecond { get; }

        public DifficultService(float difficultPerSecond) => 
            DifficultPerSecond = difficultPerSecond;

        public float GetDifficult(float playTime)
        {
            return playTime * DifficultPerSecond;
        }
    }
}
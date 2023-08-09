using Sources.Infrastructure.DI;

namespace Sources.Infrastructure.PersistentProgress
{
    public class PersistentProgressService : IService, IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
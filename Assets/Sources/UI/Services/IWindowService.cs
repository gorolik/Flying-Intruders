using Sources.Infrastructure.DI;

namespace Sources.UI.Services
{
    public interface IWindowService : IService
    {
        void Open(WindowId id);
    }
}
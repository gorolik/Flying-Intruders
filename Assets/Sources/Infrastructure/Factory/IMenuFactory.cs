using Sources.Infrastructure.DI;

namespace Sources.Infrastructure.Factory
{
    public interface IMenuFactory : IService
    {
        void CreateMenuHud();
    }
}
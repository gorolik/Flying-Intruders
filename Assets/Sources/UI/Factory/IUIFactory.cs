using Sources.Infrastructure.DI;

namespace Sources.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        void CreatePause();
        void CreateGameOver();
    }
}
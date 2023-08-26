using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.PersistentProgress;
using Sources.UI.Factory;

namespace Sources.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string _mainMenuSceneName = "MainMenu";
        
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly Curtain _curtain;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IMenuFactory _menuFactory;

        public MainMenuState(SceneLoader sceneLoader, Curtain curtain, IUIFactory uiFactory, IMenuFactory menuFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _curtain = curtain;
            _menuFactory = menuFactory;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(_mainMenuSceneName, OnMenuLoaded);
        }

        public void Exit() {}

        private void OnMenuLoaded()
        {
            InitUIRoot();
            InitHud();
            
            _curtain.Hide();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitHud() => 
            _menuFactory.CreateMenuHud();
    }
}
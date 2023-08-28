using Sources.Infrastructure;
using Sources.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private Button _menuButton;
        
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        protected override void Init() => 
            Time.timeScale = 0;

        protected override void SubscribeUpdates() => 
            _menuButton.onClick.AddListener(OnToMainMenuButtonClicked);

        protected override void Cleanup()
        {
            _menuButton.onClick.RemoveListener(OnToMainMenuButtonClicked);
            
            Time.timeScale = 1;
        }

        private void OnToMainMenuButtonClicked() => 
            _gameStateMachine.Enter<MainMenuState>();
    }
}
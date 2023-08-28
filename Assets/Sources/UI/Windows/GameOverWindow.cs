using Sources.Infrastructure;
using Sources.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public class GameOverWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        protected override void Init() => 
            Time.timeScale = 0;

        protected override void SubscribeUpdates()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _menuButton.onClick.AddListener(OnToMenuButtonClicked);
        }

        protected override void Cleanup()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _menuButton.onClick.RemoveListener(OnToMenuButtonClicked);
            
            Time.timeScale = 1;
        }

        private void OnRestartButtonClicked() => 
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);

        private void OnToMenuButtonClicked() => 
            _gameStateMachine.Enter<MainMenuState>();
    }
}
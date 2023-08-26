using Sources.Infrastructure;
using Sources.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour.UI.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        private const string _gameSceneName = "Game";
        
        [SerializeField] private Button _button;
        
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void OnEnable() => 
            _button.onClick.AddListener(OnPlayButtonClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnPlayButtonClicked);
        
        private void OnPlayButtonClicked() => 
            _gameStateMachine.Enter<LoadLevelState, string>(_gameSceneName);
    }
}

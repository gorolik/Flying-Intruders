using Sources.Infrastructure.PersistentProgress;

namespace Sources.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string _gameSceneName = "Game";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentProgressService _progressService;

        public LoadProgressState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public void Enter()
        {
            LoadOrInitProgress();
            
            _gameStateMachine.Enter<LoadLevelState, string>(_gameSceneName);
        }

        public void Exit()
        {
            
        }

        private void LoadOrInitProgress()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? InitProgress();
        }

        private PlayerProgress InitProgress()
        {
            return new PlayerProgress();
        }
    }
}
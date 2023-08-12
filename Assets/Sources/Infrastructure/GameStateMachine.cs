using Sources.Infrastructure.Factory;
using Sources.Infrastructure.States;
using System;
using System.Collections.Generic;
using Sources.Behaviour;
using Sources.Behaviour.UI;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.Difficult;

namespace Sources.Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, Curtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
                    services.Single<IGameFactory>(), 
                    services.Single<IPersistentProgressService>(), 
                    services.Single<IDifficultService>()),
                
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.Single<ISaveLoadService>(), 
                    services.Single<IPersistentProgressService>()),
                
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            IPayloadState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
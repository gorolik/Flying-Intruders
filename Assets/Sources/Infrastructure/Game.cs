using Sources.Behaviour;
using Sources.Behaviour.UI;
using Sources.Infrastructure.DI;

namespace Sources.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, Curtain curtain)
        {
            StateMachine= new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}
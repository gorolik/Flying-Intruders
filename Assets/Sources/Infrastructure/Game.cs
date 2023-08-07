using Assets.Sources.Infrastructure;
using Sources.Services.Input;

namespace Sources.Infrastructure
{
    public class Game
    {
        public static IInputSurvice InputSurvice;
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine= new GameStateMachine();
        }
    }
}
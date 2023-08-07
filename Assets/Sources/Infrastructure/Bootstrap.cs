using Sources.Infrastructure;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        public static Bootstrap Instance;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
using Assets.Sources.Infrastructure;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
using Sources.Behaviour;
using Sources.Infrastructure.States;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain _curtainPrefab;

        public static Bootstrap Instance;
            
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
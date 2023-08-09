using Sources.Behaviour.Extensions;
using Sources.Infrastructure.DI;
using Sources.Infrastructure.Factory;
using UnityEngine;

namespace Sources.Behaviour.Enemy
{
    public class MovingToHole : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _hole;
        private IGameFactory _gameFactory;

        private void Start()
        {
            GetGameFactory();
            InitHolePoint();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_hole == null)
                return;

            Vector3 direction = _hole.position - transform.position.normalized;
            transform.Translate(direction * (_speed * Time.deltaTime), Space.World);
            transform.LookAt2D(transform.position + direction);
        }

        private void InitHolePoint()
        {
            if (_gameFactory.Hole != null)
                GetHole();
            else
                _gameFactory.HoleCreated += GetHole;
        }

        private void GetGameFactory() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();

        private void GetHole() => 
            _hole = _gameFactory.Hole;
    }
}

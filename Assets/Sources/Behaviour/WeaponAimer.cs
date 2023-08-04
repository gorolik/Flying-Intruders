using Sources.Infrastructure;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Behaviour
{
    public class WeaponAimer : MonoBehaviour
    {
        [SerializeField] private Transform _weapon;

        private IInputSurvice _inputSurvice;
        private Camera _camera;
        private Vector2 _previousCursorPosition;

        private void Awake()
        {
            _inputSurvice = Game.InputSurvice;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector2 worldCursorPosition = _camera.ScreenToWorldPoint(_inputSurvice.CursorPosition);

            if (worldCursorPosition == Vector2.zero)
                worldCursorPosition = _previousCursorPosition;

            _previousCursorPosition = worldCursorPosition;

            _weapon.transform.LookAt2D(worldCursorPosition);
        }
    }
}
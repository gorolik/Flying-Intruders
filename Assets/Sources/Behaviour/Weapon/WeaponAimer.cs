using Sources.Behaviour.Extensions;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponAimer : MonoBehaviour
    {
        [SerializeField] private Transform _weapon;

        private IInputSurvice _inputSurvice;
        private Camera _camera;
        private Vector2 _previousCursorPosition;

        public void Construct(IInputSurvice inputSurvice) => 
            _inputSurvice = inputSurvice;

        private void Start() => 
            _camera = Camera.main;

        private void Update() => 
            Aim();

        private void Aim() => 
            _weapon.transform.LookAt2D(GetWorldCursorPosition());

        private Vector2 GetWorldCursorPosition()
        {
            Vector2 worldCursorPosition = _camera.ScreenToWorldPoint(_inputSurvice.CursorPosition);

            if (worldCursorPosition == Vector2.zero)
                worldCursorPosition = _previousCursorPosition;

            _previousCursorPosition = worldCursorPosition;

            return worldCursorPosition;
        }
    }
}
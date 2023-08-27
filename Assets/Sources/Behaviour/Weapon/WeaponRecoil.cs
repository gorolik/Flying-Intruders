using System.Collections;
using UnityEngine;

namespace Sources.Behaviour.Weapon
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] private WeaponShooter _shooter;
        [SerializeField] private Transform _viewTransform;
        [SerializeField] private float _pushBackSpeed = 1;
        [SerializeField] private float _returnSpeed = 1;
        [SerializeField] private float _depth = 1;

        private bool _isPlaying;
        private Coroutine _animation;
        private Vector3 _defaultLocalPosition;
        
        private Vector3 PushedBackPosition => _defaultLocalPosition - Vector3.up * _depth;

        private void Awake() => 
            _defaultLocalPosition = _viewTransform.localPosition;

        private void OnEnable() => 
            _shooter.Shot += OnShot;

        private void OnDisable() => 
            _shooter.Shot -= OnShot;

        
        private void OnShot()
        {
            if (_isPlaying)
                StartCoroutine(ContinueAnimation());
            else
                _animation = StartCoroutine(VisualizeRecoil());
        }
        
        private IEnumerator ContinueAnimation()
        {
            yield return new WaitForEndOfFrame();

            StopCoroutine(_animation);
            _isPlaying = false;

            yield return new WaitForEndOfFrame();

            _animation = StartCoroutine(VisualizeRecoil());
        }

        private IEnumerator VisualizeRecoil()
        {
            bool isPushedBack = false;
            bool isReturned = false;

            _isPlaying = true;

            while (isPushedBack == false)
            {
                if (_viewTransform.localPosition.y > PushedBackPosition.y)
                {
                    float y = Mathf.MoveTowards(_viewTransform.localPosition.y, PushedBackPosition.y, _pushBackSpeed * Time.deltaTime);
                    Vector3 newPosition = new Vector3(_viewTransform.localPosition.x, y, _viewTransform.localPosition.z);
                    
                    _viewTransform.localPosition = newPosition;
                }
                else
                {
                    _viewTransform.localPosition = PushedBackPosition;
                    isPushedBack = true;
                }

                yield return new WaitForEndOfFrame();
            }

            while (isReturned == false)
            {
                if (_viewTransform.localPosition.y < _defaultLocalPosition.y)
                {
                    float y = Mathf.MoveTowards(_viewTransform.localPosition.y, _defaultLocalPosition.y, _returnSpeed * Time.deltaTime);
                    Vector3 newPosition = new Vector3(_viewTransform.localPosition.x, y, _viewTransform.localPosition.z);
                    
                    _viewTransform.localPosition = newPosition;
                }
                else
                    isReturned = true;

                yield return new WaitForEndOfFrame();
            }

            _viewTransform.localPosition = _defaultLocalPosition;
            _isPlaying = false;
        }
    }
}
using UnityEngine;

namespace Sources.Behaviour.Projectile
{
    public class DestroyByDistance : MonoBehaviour
    {
        [SerializeField] private float _maxTravelDistance = 15;
        
        private Vector2 _previousPosition;
        private float _travel;

        private void Start() => 
            _previousPosition = transform.position;

        private void Update()
        {
            _travel += Vector2.Distance(_previousPosition, transform.position);
            
            if(_travel >= _maxTravelDistance)
                Destroy(gameObject);
            
            _previousPosition = transform.position;
        }
    }
}
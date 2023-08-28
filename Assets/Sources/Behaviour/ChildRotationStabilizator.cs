using Sources.Behaviour.Extensions;
using UnityEngine;

namespace Sources.Behaviour
{
    public class ChildRotationStabilizator : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction = Vector3.up;

        private void LateUpdate() =>
            transform.LookAt2D(transform.position + _direction);
    }
}
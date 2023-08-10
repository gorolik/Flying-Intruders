using System;
using UnityEngine;

namespace Sources.Behaviour
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        
        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
            TriggerEnter?.Invoke(other);
        }
    }
}
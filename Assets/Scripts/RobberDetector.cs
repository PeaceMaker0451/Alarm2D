
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class RobberDetector : MonoBehaviour
    {
        private int _intrudersCount;

        public event Action RobberIntered;
        public event Action RobberLeaved;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Robber>(out var robber))
            {
                _intrudersCount++;
                RobberIntered?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Robber>(out var robber))
            {
                _intrudersCount--;

                if (_intrudersCount <= 0)
                    RobberLeaved?.Invoke();
            }
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterSpriteRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 2;

        private Coroutine _currentRotationUpdater;

        public void SetXDirection(float xDirection)
        {
            if( _currentRotationUpdater != null)
                StopCoroutine( _currentRotationUpdater );

            StartCoroutine(UpdateSprite(xDirection));
        }
        
        private IEnumerator UpdateSprite(float xDirection)
        {
            Vector3 turnBackAngle = new Vector3(0, 180, 0);
            Vector3 targetRotation = xDirection > 0 ? Vector3.zero : turnBackAngle;

            while(transform.eulerAngles != targetRotation)
            {
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, targetRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }

            _currentRotationUpdater = null;
        } 
    }
}
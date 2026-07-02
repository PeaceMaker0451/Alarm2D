using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterSpriteRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 2;

        private float _xDirection;
        
        private void Update()
        {
            UpdateSprite();
        }

        public void SetXDirection(float xDirection)
        {
            _xDirection = xDirection;
        }
        
        private void UpdateSprite()
        {
            Vector3 turnBackAngle = new Vector3(0, 180, 0);

            if (_xDirection > 0)
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, Vector3.zero, _rotationSpeed * Time.deltaTime);
            else if (_xDirection < 0)
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, turnBackAngle, _rotationSpeed * Time.deltaTime);
        }
    }
}
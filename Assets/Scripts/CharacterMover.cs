using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterSpriteRotator))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;

        private CharacterSpriteRotator _spriteRotator;

        private void Awake()
        {
            _spriteRotator = GetComponent<CharacterSpriteRotator>();
        }

        public void Move(float xDirection)
        {
            xDirection = Mathf.Clamp(xDirection, -1, 1);
            Vector3 direction = new Vector3(xDirection, 0, 0);

            transform.position += direction * _speed * Time.deltaTime;

            _spriteRotator.SetXDirection(xDirection);
        }
    }
}
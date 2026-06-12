using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Robber : MonoBehaviour
    {
        [SerializeField] private Transform _itemHandler;
        [SerializeField] private Transform _stash;
        [SerializeField] private Item[] _itemsToSteal;

        [SerializeField] private float _throwToStashDistance;
        [SerializeField] private float _grabItemDistance;
        [SerializeField] private float _speed = 2;
        [SerializeField] private float _rotationSpeed = 2;

        private Rigidbody2D _rigidBody;

        private Queue<Item> _targetItems;
        private Item _currentItem; 
        private Transform _currentTarget;

        private bool _targetIsStash;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();

            _targetItems = new();

            foreach (var item in _itemsToSteal)
                _targetItems.Enqueue(item);
        }

        private void Start()
        {
            _rigidBody.freezeRotation = true;
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;

            UpdateTarget();
        }

        private void Update()
        {
            if (_targetIsStash == false && _targetItems.Count == 0 )
                return;
            
            Move();
            
            if(_targetIsStash)
            {
                if(transform.position.x - _currentTarget.position.x <= _throwToStashDistance)
                {
                    DropItem();
                    UpdateTarget();
                }
            }
            else
            {
                if(CanGrabTargetItem())
                {
                    PickItem();
                    UpdateTarget();
                }
            }
        }

        private void UpdateTarget()
        {
            if (_targetIsStash)
                _currentTarget = _stash;
            else if (_targetItems.Count != 0)
                _currentTarget = _targetItems.Peek().transform;
        }

        private void PickItem()
        {
            _currentItem = _targetItems.Dequeue();

            _currentItem.SetGrabbed();
            _currentItem.transform.SetParent(_itemHandler);
            _currentItem.transform.localPosition = Vector3.zero;
            _currentItem.transform.localRotation = Quaternion.identity;

            _targetIsStash = true;
        }

        private void DropItem()
        {
            _currentItem.transform.SetParent(_stash);
            _currentItem.transform.localPosition = Vector3.zero;
            _currentItem.transform.localRotation = Quaternion.identity;

            _currentItem.SetReleased();

            _currentItem = null;
            _targetIsStash = false;
        }

        private void Move()
        {
            if (_currentTarget == null)
                return;
            
            float xDirection = Mathf.Clamp((_currentTarget.position - transform.position).x, -1, 1);
            Vector3 direction = new Vector3(xDirection, 0, 0);

            transform.position += direction * _speed * Time.deltaTime;

            UpdateSprite(direction);
        }

        private void UpdateSprite(Vector3 direction)
        {
            Vector3 turnBackAngle = new Vector3(0, 180, 0);
            
            if (direction.x > 0)
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, Vector3.zero, _rotationSpeed * Time.deltaTime);
            else if (direction.x < 0)
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, turnBackAngle, _rotationSpeed * Time.deltaTime);
        }

        private bool CanGrabTargetItem()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _grabItemDistance);

            foreach (var collider in colliders)
            {
                if (collider.gameObject == _currentTarget.gameObject)
                    return true;
            }

            return false;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ItemHandler), typeof(CharacterMover))]
    public class Robber : MonoBehaviour
    {
        [SerializeField] private Transform _stash;
        [SerializeField] private Item[] _itemsToSteal;

        [SerializeField] private float _throwToStashDistance;
        [SerializeField] private float _grabItemDistance;

        private ItemHandler _itemHandler;
        private CharacterMover _characterMover;

        private Queue<Item> _targetItems;

        private Transform _currentTarget;

        private bool _targetIsStash;

        private void Awake()
        {
            _itemHandler = GetComponent<ItemHandler>();
            _characterMover = GetComponent<CharacterMover>();

            _targetItems = new();

            foreach (var item in _itemsToSteal)
                _targetItems.Enqueue(item);
        }

        private void Start()
        {
            UpdateTarget();
        }

        private void Update()
        {
            if (_targetIsStash == false && _targetItems.Count == 0 )
                return;
            
            _characterMover.Move((_currentTarget.position - transform.position).x);
            
            if(_targetIsStash)
            {
                if(transform.position.x - _currentTarget.position.x <= _throwToStashDistance)
                {
                    _itemHandler.DropItem(_stash);
                    _targetIsStash = false;
                    UpdateTarget();
                }
            }
            else
            {
                if(CanGrabTargetItem())
                {
                    _itemHandler.PickItem(_targetItems.Dequeue());
                    _targetIsStash = true;
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
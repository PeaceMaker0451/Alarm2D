using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ItemHandler), typeof(CharacterMover))]
    public class Robber : MonoBehaviour
    {
        [SerializeField] private Transform _stash;
        [SerializeField] private TargetItems _items;

        [SerializeField] private float _throwToStashDistance;
        [SerializeField] private float _grabItemDistance;

        private ItemHandler _itemHandler;
        private CharacterMover _characterMover;

        private Transform _currentTarget;

        private bool _targetIsStash;

        private void Awake()
        {
            _itemHandler = GetComponent<ItemHandler>();
            _characterMover = GetComponent<CharacterMover>();
        }

        private void Start()
        {
            _items.Initialize();
        }

        //private void Update()
        //{
        //    if (_targetIsStash == false && _targetItems.Count == 0 )
        //        return;
            
        //    _characterMover.Move((_currentTarget.position - transform.position).x);
            
        //    if(_targetIsStash)
        //    {
        //        if(transform.position.x - _currentTarget.position.x <= _throwToStashDistance)
        //        {
        //            _itemHandler.DropItem(_stash);
        //            _targetIsStash = false;
        //            UpdateTarget();
        //        }
        //    }
        //    else
        //    {
        //        if(CanGrabTargetItem())
        //        {
        //            _itemHandler.PickItem(_targetItems.Dequeue());
        //            _targetIsStash = true;
        //            UpdateTarget();
        //        }
        //    }
        //}

        //private void UpdateTarget()
        //{
        //    if (_targetIsStash)
        //        _currentTarget = _stash;
        //    else if (_targetItems.Count != 0)
        //        _currentTarget = _targetItems.Peek().transform;
        //}

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

        [System.Serializable]
        private class TargetItems
        {
            [SerializeField] private Item[] _itemsToSteal;

            private Queue<Item> _targetItems;

            public void Initialize()
            {
                _targetItems = new();

                foreach (var item in _itemsToSteal)
                    _targetItems.Enqueue(item);
            }

            public Item GetNextItem()
            {
                return _targetItems.Dequeue();
            }
        }
    }
}
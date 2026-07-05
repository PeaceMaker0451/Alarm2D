using System.Collections;
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

        private void Awake()
        {
            _itemHandler = GetComponent<ItemHandler>();
            _characterMover = GetComponent<CharacterMover>();
        }

        private void Start()
        {
            _items.Initialize();
            TargetNewItem();
        }

        private IEnumerator MoveToItem(Item item)
        {
            Transform itemTransform = item.transform;
            
            while(CanGrabItem(item) == false)
            {
                _characterMover.Move((itemTransform.position - transform.position).x);
                yield return null;
            }

            TakeItem(item);
        }

        private IEnumerator MoveToStash()
        {
            while(transform.position.x - _stash.position.x > _throwToStashDistance)
            {
                _characterMover.Move((_stash.position - transform.position).x);
                yield return null;
            }

            ThrowItem();
        }

        private void TakeItem(Item item)
        {
            _itemHandler.PickItem(item);
            StartCoroutine(MoveToStash());
        }
        
        private void ThrowItem()
        {
            _itemHandler.DropItem(_stash);
            TargetNewItem();
        }

        private void TargetNewItem()
        {
            if (_items.TryGetNextItem(out Item item) == false)
                return;

            StartCoroutine(MoveToItem(item));
        }

        private bool CanGrabItem(Item item)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _grabItemDistance);

            foreach (var collider in colliders)
            {
                if (collider.gameObject == item.gameObject)
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

            public bool TryGetNextItem(out Item item)
            {
                return _targetItems.TryDequeue(out item);
            }
        }
    }
}
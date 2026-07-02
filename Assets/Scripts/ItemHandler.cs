using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private Transform _handlePoint;

        private Item _currentItem;

        public void PickItem(Item item)
        {
            if (_currentItem != null)
                throw new Exception("Предмет уже взят");
            
            _currentItem = item;

            _currentItem.SetGrabbed();
            _currentItem.transform.SetParent(_handlePoint);
            _currentItem.transform.localPosition = Vector3.zero;
            _currentItem.transform.localRotation = Quaternion.identity;
        }

        public void DropItem(Transform newParent)
        {
            _currentItem.transform.SetParent(newParent);
            _currentItem.transform.localPosition = Vector3.zero;
            _currentItem.transform.localRotation = Quaternion.identity;

            _currentItem.SetReleased();

            _currentItem = null;
        }
    }
}
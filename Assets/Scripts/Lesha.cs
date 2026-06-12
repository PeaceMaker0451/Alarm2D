using UnityEngine;

namespace Assets.Scripts
{
    public class Lesha : MonoBehaviour
    {
        [SerializeField] private GameObject _sleepingSprite;
        [SerializeField] private GameObject _awakeSprite;

        private void Start()
        {
            _sleepingSprite.SetActive(true);
            _awakeSprite.SetActive(false);
        }

        public void WakeUp()
        {
            _sleepingSprite.SetActive(false);
            _awakeSprite.SetActive(true);
        }
    }
}
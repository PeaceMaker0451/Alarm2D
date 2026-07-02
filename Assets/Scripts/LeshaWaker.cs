using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Lesha), typeof(Item))]
    public class LeshaWaker : MonoBehaviour
    {
        [SerializeField] private AlarmSignal _alarm;
        
        private Lesha _lesha;
        private Item _item;

        private void Awake()
        {
            _lesha = GetComponent<Lesha>();
            _item = GetComponent<Item>();
        }

        private void Start()
        {
            _item.Grabbed += WakeLesha;
            _alarm.AlarmVolumeReachedThreshold += WakeLesha;
        }

        private void OnDestroy()
        {
            Unsubscribe();

        }

        private void WakeLesha()
        {
            _lesha.WakeUp();
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _item.Grabbed -= WakeLesha;
            _alarm.AlarmVolumeReachedThreshold -= WakeLesha;
        }
    }
}
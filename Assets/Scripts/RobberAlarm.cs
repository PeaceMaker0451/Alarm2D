using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AlarmSignal), typeof(RobberDetector))]
    public class RobberAlarm : MonoBehaviour
    {
        private RobberDetector _detector;
        private AlarmSignal _signal;

        private void Awake()
        {
            _detector = GetComponent<RobberDetector>();
            _signal = GetComponent<AlarmSignal>();
        }

        private void Start()
        {
            _detector.RobberIntered += _signal.Enable;
            _detector.RobberLeaved += _signal.Disable;
        }

        private void OnDestroy()
        {
            _detector.RobberIntered -= _signal.Enable;
            _detector.RobberLeaved -= _signal.Disable;
        }
    }
}
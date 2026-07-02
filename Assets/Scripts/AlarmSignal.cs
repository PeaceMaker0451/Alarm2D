using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource), typeof(Collider2D))]
    public class AlarmSignal : MonoBehaviour
    {
        [SerializeField] private float _volumeChangeSpeed = 2f;
        [SerializeField] private float _alarmEventVolumeThreshold = 0.7f;

        public event Action AlarmVolumeReachedThreshold;

        private AudioSource _audio;
        private bool _isEnabled;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audio.volume = 0;
            _audio.Play();
        }

        private void Update()
        {
            UpdateVolume();
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        private void UpdateVolume()
        {
            bool volumeWasBelowThreshold = _audio.volume <= _alarmEventVolumeThreshold;

            if (_isEnabled)
                _audio.volume = Mathf.MoveTowards(_audio.volume, 1, Time.deltaTime * _volumeChangeSpeed);
            else
                _audio.volume = Mathf.MoveTowards(_audio.volume, 0, Time.deltaTime * _volumeChangeSpeed);

            if (volumeWasBelowThreshold && _audio.volume > _alarmEventVolumeThreshold)
                AlarmVolumeReachedThreshold?.Invoke();
        }
    }
}
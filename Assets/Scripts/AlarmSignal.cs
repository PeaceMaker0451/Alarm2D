using System;
using System.Collections;
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
        private Coroutine _currentSignalUpdater;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audio.volume = 0;
            _audio.Play();
        }

        public void Enable()
        {
            ClearSignalUpdater();
            _currentSignalUpdater = StartCoroutine(EnableSignal());
        }

        public void Disable()
        {
            ClearSignalUpdater();
            _currentSignalUpdater = StartCoroutine(DisableSignal());
        }

        private void ClearSignalUpdater()
        {
            if (_currentSignalUpdater == null)
                return;

            StopCoroutine(_currentSignalUpdater);
            _currentSignalUpdater = null;
        }

        private IEnumerator EnableSignal()
        {
            float targetVolume = 1;
            
            while(_audio.volume < targetVolume)
            {
                bool volumeWasBelowThreshold = _audio.volume <= _alarmEventVolumeThreshold;

                _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, Time.deltaTime * _volumeChangeSpeed);

                if (volumeWasBelowThreshold && _audio.volume > _alarmEventVolumeThreshold)
                    AlarmVolumeReachedThreshold?.Invoke();

                yield return null;
            }

            _currentSignalUpdater = null;
        }

        private IEnumerator DisableSignal()
        {
            float targetVolume = 0;

            while (_audio.volume > targetVolume)
            {
                _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, Time.deltaTime * _volumeChangeSpeed);
                yield return null;
            }

            _currentSignalUpdater = null;
        }
    }
}
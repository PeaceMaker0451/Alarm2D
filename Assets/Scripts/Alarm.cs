using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource), typeof(Collider2D))]
    public class Alarm : MonoBehaviour
    {
        [SerializeField] private float _volumeChangeSpeed = 2f;
        [SerializeField] private float _alarmEventVolumeThreshold = 0.7f;
        [SerializeField] private string _targetTag = "Robber";

        public event Action AlarmVolumeReachedThreshold;

        private AudioSource _audio;
        private int _intrudersCount;
        private bool _isIntruderDetected;

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

        private void UpdateVolume()
        {
            bool volumeWasBelowThreshold = _audio.volume <= _alarmEventVolumeThreshold;

            if (_isIntruderDetected)
                _audio.volume = Mathf.MoveTowards(_audio.volume, 1, Time.deltaTime * _volumeChangeSpeed);
            else
                _audio.volume = Mathf.MoveTowards(_audio.volume, 0, Time.deltaTime * _volumeChangeSpeed);

            if (volumeWasBelowThreshold && _audio.volume > _alarmEventVolumeThreshold)
                AlarmVolumeReachedThreshold?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == _targetTag)
            {
                _intrudersCount++;
                _isIntruderDetected = true;
            }  
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(_targetTag))
            {
                _intrudersCount--;
                _isIntruderDetected = _intrudersCount > 0;
            }
                
        }
    }
}
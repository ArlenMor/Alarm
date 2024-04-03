using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmDetector[] _detectors;
    [SerializeField] private float _fadeSpeed = 0.05f;

    private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    
    private float _currentVolume;

    private Coroutine _volumeCorutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        _currentVolume = _audioSource.volume;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _detectors.Length; i++)
        {
            _detectors[i].OnTriggerEntered += TurnOn;
            _detectors[i].OnTriggerExited += TurnOff;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _detectors.Length; i++)
        {
            _detectors[i].OnTriggerEntered -= TurnOn;
            _detectors[i].OnTriggerExited -= TurnOff;
        }
    }

    private void TurnOn() => SetVolume(_maxVolume);

    private void TurnOff() => SetVolume(_minVolume);

    private void SetVolume(float volume)
    {
        if (_volumeCorutine != null)
            StopCoroutine(_volumeCorutine);

        _volumeCorutine = StartCoroutine(ChangeVolume(volume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        _audioSource.Play();

        while (_currentVolume != targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _fadeSpeed * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return null;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}

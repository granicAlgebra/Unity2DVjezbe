using System.Collections.Generic;
using UnityEngine;

public class SfxManagar : MonoBehaviour
{
    public static SfxManagar Instance { get; private set; }

    [SerializeField] private AudioSource _audioSourcePrefab;

    private List<AudioSource> _audioSources;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _audioSources = new List<AudioSource>();
        
        for (int i = 0; i < 5; i++)
        {
            var newAudioSource = Instantiate(_audioSourcePrefab, transform);
            _audioSources.Add(newAudioSource);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        int audioSourceIndex = -1;
        for (int i = 0; i < _audioSources.Count; i++)
        {
            if (_audioSources[i].isPlaying == false)
            {
                audioSourceIndex = i;
                break;
            }
        }

        if (audioSourceIndex == -1)
        {
            var newAudioSource = Instantiate(_audioSourcePrefab, transform);
            _audioSources.Add(newAudioSource);
            audioSourceIndex = _audioSources.Count - 1;
        }

        _audioSources[audioSourceIndex].clip = clip;
        _audioSources[audioSourceIndex].Play();
    }
}

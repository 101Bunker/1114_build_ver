using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)SoundType.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // MP3 Player   -> AudioSource
    // MP3 À½¿ø     -> AudioClip
    // °ü°´(±Í)     -> AudioListener

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(SoundType));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)SoundType.Effect].loop = false;
        }
    }

    public enum SoundType
    {
        Effect,
        MaxCount,
    }

    public void Play(string path, SoundType type = SoundType.Effect)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type);
    }

    public void Play(AudioClip audioClip, SoundType type = SoundType.Effect)
    {
        if (audioClip == null)
            return;

        if (type == SoundType.Effect)
        {
            AudioSource audioSource = _audioSources[(int)SoundType.Effect];
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (_audioClips.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }
        return audioClip;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}

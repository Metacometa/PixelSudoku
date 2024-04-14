using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public enum Themes{Zero, Easy, Medium, Hard};

    private AudioSource[] audioSources;
    [SerializeField] private AudioClip[] themes;
    private int themesSize;
    private float volume = 80f;

    Themes soling;

    void Start()
    {
        themesSize = themes.Length;
        audioSources = new AudioSource[themesSize];

        for (int i = 0; i < themesSize; ++i)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].loop = true;
            audioSources[i].clip = themes[i];

            audioSources[i].Play();
        }

        PlayEasyTheme();
    }

    public void PlayZeroTheme()
    {
        SoloClip(Themes.Zero);
    }

    public void PlayEasyTheme()
    {
        SoloClip(Themes.Easy);
    }

    public void PlayMediumTheme()
    {
        SoloClip(Themes.Medium);
    }

    public void PlayHardTheme()
    {
        SoloClip(Themes.Hard);
    }

    private void SoloClip(Themes theme)
    {
        for (int i = 0; i < themesSize; ++i)
        {
            audioSources[i].volume = 0f;
        }

        audioSources[(int) theme].volume = volume;

        soling = theme;
    }

    public void SetVolume(float source)
    {
        volume = source;
        audioSources[(int) soling].volume = volume;
    }
}

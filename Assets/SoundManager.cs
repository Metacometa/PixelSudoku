using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sounds{BinClosing, BinOpening, BinRemoving, DeleteCell, WriteCell, Slider, Win};
    [SerializeField] private AudioClip[] sounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(Sounds sound)
    {
        audioSource.clip = sounds[(int)sound];
        audioSource.Play();
    }
}

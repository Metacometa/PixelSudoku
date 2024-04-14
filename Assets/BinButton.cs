using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinButton : MonoBehaviour
{
    private Animator animator;
    private SoundManager sound;

    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();    
    }

    public void PointerEnter()
    {   
        animator.SetTrigger("Opening");
        sound.Play(SoundManager.Sounds.BinOpening);
    }

    public void PointerExit()
    {
        animator.SetTrigger("Ending");
        sound.Play(SoundManager.Sounds.BinClosing);
    }
}

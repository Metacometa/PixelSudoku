using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinButton : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PointerEnter()
    {   
        animator.SetTrigger("Opening");
    }

    public void PointerExit()
    {
        animator.SetTrigger("Ending");
    }
}

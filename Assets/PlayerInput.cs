using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int numberInput = 0;
    private int asciiShift = 48;

    void Update()
    {
        bool numberOnInput = false;

        for (int i = (int) KeyCode.Alpha1; i <= (int) KeyCode.Alpha9; ++i)
        {
            if (Input.GetKey((KeyCode) i))
            {
                numberInput = i - asciiShift;
                numberOnInput = true;
                break;
            }
        }

        if (!numberOnInput)
        {
            numberInput = 0;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{

    [SerializeField] private int i = 0;
    [SerializeField] private int j = 0;

    private TextMeshProUGUI text;   
    private RectTransform rect;
    [SerializeField] private RectTransform field;

    private PlayerInput playerInput;
    private SudokuLogic sudokuLogic;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponentInChildren<RectTransform>();  

        playerInput = GameObject.Find("PlayerInput").GetComponent<PlayerInput>();     
        sudokuLogic = GameObject.Find("SudokuLogic").GetComponent<SudokuLogic>(); 
    }

    void Update()
    {
        int valueInGrid = sudokuLogic.grid[i][j];
        
        if (valueInGrid != 0)
        {
            text.text = valueInGrid.ToString();
        }
        else 
        {
            text.text = "";  
        }
    }

    public void SetNumberOfCell(int i, int j)
    {
        this.i = i;
        this.j = j;
        SetPosition();
    }

    public void SetPosition()
    {   
        float fieldX = field.rect.width / 2;
        float fieldY = -field.rect.height / 2;

        rect.localPosition = new Vector3(rect.rect.width * j - fieldX + rect.rect.width / 2, -rect.rect.height * i - fieldY - rect.rect.height / 2);
    }

    public void InputNumber()
    {
        if (sudokuLogic.gridFlagged[i][j])
        {
            return;
        }

        if (playerInput.numberInput > 0 && playerInput.numberInput < 10)
        {
            sudokuLogic.grid[i][j] = playerInput.numberInput;
        }
        else
        {
            sudokuLogic.grid[i][j] = 0;
        }
    }
}

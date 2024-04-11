using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private GameObject cellGameObject;
    private GameObject canvas;
    private SudokuLogic sudokuLogic;
    void Start()
    {
        sudokuLogic = GameObject.Find("SudokuLogic").GetComponent<SudokuLogic>(); 
        for (int i = 0; i < sudokuLogic.height; ++i)
        {
            for (int j = 0; j < sudokuLogic.width; ++j)
            {
                GameObject cell = Instantiate(cellGameObject, transform.position, Quaternion.identity);
                cell.transform.SetParent(transform);
                cell.transform.localScale = new Vector3(1, 1, 1);

                Cell cellCur = cell.GetComponent<Cell>();
                cellCur.SetNumberOfCell(i, j);
            }

        }
    }
}

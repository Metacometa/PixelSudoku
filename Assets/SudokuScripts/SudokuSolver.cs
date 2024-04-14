using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudokuSolver : MonoBehaviour
{
    [SerializeField] private float moveDelay = 5f;

    private SudokuLogic logic;

    void Start()
    {
        logic = GetComponent<SudokuLogic>();
    }

    public bool IsSolvable(List<List<int>> source, List<List<bool>> flaggedSource, int height, int width)
    {
        return AlgorithmY(source, flaggedSource, height, width) == 1;
    }

    public int AlgorithmY(List<List<int>> source, List<List<bool>> flaggedSource, int height, int width)
    {
        (int i, int j) pos = FindFreePos(source, flaggedSource, height, width);

        if (pos == (-1, -1))
        {
            if (IsGridValid(source, height, width))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        int temp = source[pos.i][pos.j];

        bool[] used = new bool[10];
        FillUsedRowDigits(used, pos.i, pos.j, source, height, width);
        FillUsedColumnDigits(used, pos.i, pos.j, source, height, width);
        FillUsedBoxDigits(used, pos.i, pos.j, source, height, width);

        int solutionsNum = 0;
        for (int num = source[pos.i][pos.j]; num < height; ++num)
        {
            source[pos.i][pos.j]++;

            if (used[source[pos.i][pos.j]] == false)
            {
                solutionsNum += AlgorithmY(source, flaggedSource, height, width);
            }
        }

        source[pos.i][pos.j] = temp;
        return solutionsNum;
    }

    private void FillUsedRowDigits(bool[] used, int row, int column, List<List<int>> source, int height, int width)
    {
        for (int i = 0; i < height; ++i)
        {
            used[source[row][i]] = true;
        }
    }

    private void FillUsedColumnDigits(bool[] used, int row, int column, List<List<int>> source, int height, int width)
    {
        for (int i = 0; i < width; ++i)
        {
            used[source[i][column]] = true;
        }
    }

    private void FillUsedBoxDigits(bool[] used, int row, int column, List<List<int>> source, int height, int width)
    {
        int boxRow = (row / 3) * 3;
        int boxColumn = (column / 3) * 3;

        for (int i = boxRow; i < (boxRow + 3); ++i)
        {
            for (int j = boxColumn; j < (boxColumn + 3); ++j)
            {
                used[source[i][j]] = true;
            }
        }
    }

    public (int, int) FindFreePos(List<List<int>> source, List<List<bool>> flaggedSource, int height, int width)
    {
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                if (!flaggedSource[i][j] && source[i][j] == 0)
                {
                    return (i, j);
                }
            }
        }   

        return (-1, -1);
    }

    public bool IsGridValid(List<List<int>> source, int height, int width)
    {
        return ValidateRows(source, height, width) && ValidateColumns(source, height, width) && ValidateBoxes(source, height, width);
    }
    private bool IsInsertionValid(int value, int row, int column, List<List<int>> source, int height, int width)
    {
        return IsRowValid(value, row, column, source, height, width) && IsColumnValid(value, row, column, source, height, width) &&
            IsBoxValid(value, row, column, source, height, width);
    }
    private bool IsRowValid(int value, int row, int column, List<List<int>> source, int height, int width)
    {
        for (int i = 0; i < height; ++i)
        {
            if (source[row][i] == value && i != column)
            {
                return false;
            }
        }

        return true;
    }
    private bool IsColumnValid(int value, int row, int column, List<List<int>> source, int height, int width)
    {
        for (int i = 0; i < height; ++i)
        {
            if (source[i][column] == value && i != row)
            {
                return false;
            }
        }

        return true;
    }
    private bool IsBoxValid(int value, int row, int column, List<List<int>> source, int height, int width)
    {
        int boxRow = (row / 3) * 3;
        int boxColumn = (column / 3) * 3;

        for (int i = boxRow; i < (boxRow + 3); ++i)
        {
            for (int j = boxColumn; j < (boxColumn + 3); ++j)
            {
                if (value == source[i][j] && i != row && j != column)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool ValidateRows(List<List<int>> source, int height, int width)
    {
        for (int i = 0; i < height; ++i)
        {
            HashSet<int> rowCombo = new HashSet<int>();

            for (int j = 0; j < width; ++j)
            {
                if (source[i][j] != 0 && rowCombo.Contains(source[i][j]))
                {
                    return false;
                }
                else 
                {
                    rowCombo.Add(source[i][j]);
                }
            }
        }   

        return true;
    }
    private bool ValidateColumns(List<List<int>> source, int height, int width)
    {
        for (int j = 0; j < height; ++j)
        {
            HashSet<int> rowCombo = new HashSet<int>();

            for (int i = 0; i < width; ++i)
            {
                if (source[i][j] != 0 && rowCombo.Contains(source[i][j]))
                {
                    return false;
                }
                else 
                {
                    rowCombo.Add(source[i][j]);
                }
            }
        }   

        return true;
    }
    private bool ValidateBoxes(List<List<int>> source, int height, int width)
    {
        int step = 3;

        for (int i = 0; i < height; i += step)
        {
            for (int j = 0; j < width; j += step)
            {
                HashSet<int> rowCombo = new HashSet<int>();
                for (int i_box = i; i_box < (i + step); ++i_box)
                {
                    for (int j_box = j; j_box < (j + step); ++j_box)
                    {
                        if (source[i_box][j_box] != 0 && rowCombo.Contains(source[i_box][j_box]))
                        {
                            return false;
                        }
                        else 
                        {
                            rowCombo.Add(source[i_box][j_box]);
                        }
                    }
                }
                

            }
        }  

        return true;
    }    
}


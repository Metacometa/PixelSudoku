using System.Collections;
using System.Collections.Generic;
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

        if (IsGridValid(source, height, width) && pos == (-1, -1))
        {
            return 1;
        }

        if (pos == (-1, -1))
        {
            return 0;
        }

        int temp = source[pos.i][pos.j];

        int solutionsNum = 0;
        for (int num = source[pos.i][pos.j]; num < height; ++num)
        {
            source[pos.i][pos.j]++;

            if (IsGridValid(source, height, width))
            {
                solutionsNum += AlgorithmY(source, flaggedSource, height, width);
            }
        }

        source[pos.i][pos.j] = temp;
        return solutionsNum;
    }

    public bool AlgorithmX(List<List<int>> source, List<List<bool>> flaggedSource, int height, int width)
    {
        (int i, int j) pos = FindFreePos(source, flaggedSource, height, width);

        if (IsGridValid(source, height, width) && pos == (-1, -1))
        {
            return true;
        }

        if (pos == (-1, -1))
        {
            return false;
        }

        int temp = source[pos.i][pos.j];

        for (int num = source[pos.i][pos.j]; num < height; ++num)
        {
            source[pos.i][pos.j]++;

            if (IsGridValid(source, height, width))
            {
                if (AlgorithmX(source, flaggedSource, height, width) == true)
                {
                    return true;
                }
            }
        }

        source[pos.i][pos.j] = temp;
        return false;
    }
    /*
    public IEnumerator Solve(List<List<int>> source, List<List<bool>> flaggedSource, int gridHeight, int gridWidth)
    {
        logic.isSolving = true;
        logic.isSudokuExist = true;

        height = gridHeight;
        width = gridWidth;

        (int i, int j) startPos = FindFreePos(source, flaggedSource);

        yield return StartCoroutine(AlgorithmX(startPos, source, flaggedSource, (isAdded) => {
            logic.isSolvable = isAdded;
        })); 

        logic.isSolving = false; 
    }

    public IEnumerator AlgorithmX((int i, int j) pos, List<List<int>> source, List<List<bool>> flaggedSource, System.Action<bool> callback)
    {
        int temp = source[pos.i][pos.j];
        bool shouldReturnTrue = false;

        for (int num = source[pos.i][pos.j]; num < height; ++num)
        {
            yield return new WaitForSeconds(moveDelay);

            source[pos.i][pos.j]++;

            if (IsGridValid(source))
            {
                (int i, int j) nextPos = FindFreePos(source, flaggedSource);

                if (nextPos == (-1, -1))
                {
                    shouldReturnTrue = true;
                    break;
                }
 
                yield return StartCoroutine(InsertNumber(nextPos, source, flaggedSource, (isAdded) => {
                    shouldReturnTrue = isAdded;
                })); 

                if (shouldReturnTrue)
                {
                    break;
                }
            }
        }

        if (callback != null) 
        {
            callback(shouldReturnTrue);

            if (shouldReturnTrue == false)
            {
                source[pos.i][pos.j] = temp;
            }
        } 
    }
    */
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


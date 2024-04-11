using System.Collections.Generic;
using UnityEngine;

public class SudokuGenerator : MonoBehaviour
{
    private SudokuSolver solver;
    void Start()
    {
        solver = GetComponent<SudokuSolver>();
    }

    /*
    private bool IsDifficultyRight(List<List<int>> grid, int row, int column, (int min, int max) rule,  int height, int width)
    {
        int count = 0;
        for (int i = 0; i < height; ++i)
        {
            if (grid[row][i] != 0)
            {
                count++;
            }
        }

        if (!InRange(rule.min, count, rule.max))
        {
            return false;
        }

        count = 0;
        for (int i = 0; i < width; ++i)
        {
            if (grid[i][column] != 0)
            {
                count++;
            }
        }

        if (!InRange(rule.min, count, rule.max))
        {
            return false;
        }

        return true;
    }
    */


    public void CreateGrid(ref List<List<int>> grid, ref List<List<bool>> gridFlagged, int height, int width)
    {
        grid = new();
        gridFlagged = new();

        for (int i = 0; i < height; ++i)
        {
            grid.Add(new List<int>());
            gridFlagged.Add(new List<bool>());
               
            for (int j = 0; j < width; ++j)
            {
                grid[i].Add(0);
                gridFlagged[i].Add(false);
            }
        }  
    }
    public List<List<int>> CopyGrid(List<List<int>> source, int height, int width)
    {
        List<List<int>> grid = new();

        for (int i = 0; i < height; ++i)
        {
            grid.Add(new List<int>());
               
            for (int j = 0; j < width; ++j)
            {
                grid[i].Add(source[i][j]);
            }
        }  

        return grid;
    }
    public List<List<bool>> FillFlags(List<List<int>> grid, int height, int width)
    {
        List<List<bool>> flagged = new();

        for (int i = 0; i < height; ++i)
        {
            flagged.Add(new List<bool>());
            for (int j = 0; j < width; ++j)
            {
                if (grid[i][j] == 0)
                {

                    flagged[i].Add(false);
                }
                else 
                {
                    flagged[i].Add(true);
                }
            }
        }  

        return flagged;
    }
    public void FillGrid(List<List<int>> grid, int start, int end, int step, int height, int width)
    {
        int curr = start; 

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                curr %= end + 1;  
                
                if ((i == 3 || i == 6) && j == 0)
                {
                    curr += 2;
                }

                grid[i][j] = curr;
                curr++;

                if (curr != end)
                {
                    curr %= end;
                }


            }
            curr %= end + 1;   
            curr += step;
        }  
    }
}


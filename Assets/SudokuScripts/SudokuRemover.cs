using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SudokuRemover : MonoBehaviour
{
    private SudokuGenerator gen;
    private SudokuSolver solver;
    private delegate void Removing(List<List<int>> grid, int height, int width);

    [SerializeField] private int easyDeletingMin = 7;
    [SerializeField] private int mediumDeletingMin = 5;
    [SerializeField] private int hardDeletingMin = 2;

    void Awake()
    {
        gen = GetComponent<SudokuGenerator>();
        solver = GetComponent<SudokuSolver>();
    }

    public void DeleteCells(List<List<int>> grid, int height, int width, SudokuLogic.Difficulty difficulty = SudokuLogic.Difficulty.Medium)
    {
        Removing removing = EasyRemoving;

        System.Random rnd = new System.Random();
        switch(difficulty)
        {
            case SudokuLogic.Difficulty.Easy:
            {
                removing = EasyRemoving;
                break;
            }
            case SudokuLogic.Difficulty.Medium:
            {    
                removing = MediumRemoving;
                break;
            }
            case SudokuLogic.Difficulty.Hard:
            {    
                removing = HardRemoving;
                break;
            }
            default:
            {
                return;
                break;
            }
        }

        removing(grid, height, width);
    }

    void HardRemoving(List<List<int>> grid, int height, int width)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < rnd.Next(19, 23); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Easy);
        }
        for (int i = 0; i < rnd.Next(9,14); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Medium);
        }
        for (int i = 0; i < rnd.Next(28, 34); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Hard);
        }     
    }

    void MediumRemoving(List<List<int>> grid, int height, int width)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < rnd.Next(17,21); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Easy);
        }
        for (int i = 0; i < rnd.Next(20,24); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Medium);
        }
        for (int i = 0; i < rnd.Next(9,13); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Hard);
        }
                
    }

    void EasyRemoving(List<List<int>> grid, int height, int width)
    {
        System.Random rnd = new System.Random();

        for (int i = 0; i < rnd.Next(18,22); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Easy);
        }
        for (int i = 0; i < rnd.Next(8,12); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Medium);
        }
        for (int i = 0; i < rnd.Next(9,13); ++i)
        {
            DeleteCell(grid, height, width, SudokuLogic.Difficulty.Hard);
        }
    }


    void DeleteCell(List<List<int>> grid, int height, int width, SudokuLogic.Difficulty difficulty = SudokuLogic.Difficulty.Medium)
    {
        /*
        System.Random rnd = new System.Random();

        int tries = 0;
        int maxTries = 100;
        while(true)
        {
            int row = rnd.Next(0, height);
            int column = rnd.Next(0, width);

            int temp = grid[row][column];
            grid[row][column] = 0;  

            if (solver.IsSolvable(grid, gen.FillFlags(grid, height, width), height, width))
            {
                //Debug.Log("Pos: " + new Vector2(row, column));
                return;
            }

            grid[row][column] = temp;

            tries++;

            if (tries > maxTries)
            {
                return;
            }
        }*/


        
        (int row, int column) pos = ChoosePos(grid, height, width, difficulty);

        if (pos == (-1, -1))
        {
            return;
        }

        int temp = grid[pos.row][pos.column];
        grid[pos.row][pos.column] = 0;

        List<List<int>> copied = gen.CopyGrid(grid, height, width);
        List<List<bool>> flagged = gen.FillFlags(copied, height, width);

        if (solver.IsSolvable(copied, flagged, height, width))
        {
            return;
        }

        grid[pos.row][pos.column] = temp;
        
    
}
    private (int row, int column) ChoosePos(List<List<int>> grid, int height, int width, SudokuLogic.Difficulty difficulty = SudokuLogic.Difficulty.Medium)
    {
        int minRemovable;
        switch(difficulty)
        {
            case SudokuLogic.Difficulty.Easy:
            {
                minRemovable = easyDeletingMin;
                break;
            }
            case SudokuLogic.Difficulty.Medium:
            {
                minRemovable = mediumDeletingMin;      
                break;
            }
            case SudokuLogic.Difficulty.Hard:
            {
                minRemovable = hardDeletingMin;     
                break;
            }
            default:
            {
                minRemovable = 0;
                break;
            }
        }

        System.Random rnd = new System.Random();

        int row = rnd.Next(0, height);
        int column = rnd.Next(0, width);

        int max_tries = 100;
        int tries = 0;
        while((!IsRemovable(grid, (row, column), height, width, minRemovable) || grid[row][column] == 0) && (tries < max_tries))
        {
            row = rnd.Next(0, height);
            column = rnd.Next(0, width);  

            tries++;
        }

        if (tries >= max_tries)
        {
            return (-1, -1);
        }

        return (row, column);
    }

    private bool IsRemovable(List<List<int>> grid, (int row, int column) pos, int height, int width, int min = 0, int max = 10000)
    {
        int counter = 0;
        for (int i = 0; i < height; ++i)
        {
            if (grid[pos.row][i] != 0)
            {
                counter++;
            }
        }

        if (!InRange(min, counter, max))
        {
            return false;
        }

        counter = 0;
        for (int i = 0; i < width; ++i)
        {
            if (grid[i][pos.column]!= 0)
            {
                counter++;
            }
        }

        if (!InRange(min, counter, max))
        {
            return false;
        }

        return true;

    }

    private bool InRange(int left, int value, int right)
    {
        return left <= value && value <= right;
    }
}

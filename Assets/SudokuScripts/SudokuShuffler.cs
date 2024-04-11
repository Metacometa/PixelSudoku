using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SudokuShuffler : MonoBehaviour
{
    private delegate void ShuffleOperation(List<List<int>> grid, int height, int width);
    private SudokuGenerator gen;

    void Start()
    {
        gen = GetComponent<SudokuGenerator>();
    }

    public void Shuffle(List<List<int>> grid, int height, int width, int reps = 100)
    {
        System.Random rnd = new System.Random();
        List<ShuffleOperation> operations = new List<ShuffleOperation>{Transpose, SwapRowsSmall, SwapColumnsSmall, SwapRowsBox, SwapColumnsBox};

        for (int i = 0; i < reps; ++i)
        {
            int operation = rnd.Next(0, operations.Count);

            operations[operation](grid, height, width);
        }
    }
    private void Transpose(List<List<int>> grid, int height, int width)
    {
        List<List<int>> grid_ = gen.CopyGrid(grid, height, width);

        for (int i = 0; i < height; ++i)
        {
               
            for (int j = 0; j < width; ++j)
            {
                grid[i][j] = grid_[j][i];
            }
        }          

    }
    private void SwapRowsSmall(List<List<int>> grid, int height, int width)
    {
        (int firstRow, int secondRow, int box) = RandomForSwap();
        for (int i = 0; i < height; ++i)
        {
            int shift = 3 * box;
            int temp = grid[firstRow + shift][i];

            grid[firstRow + shift][i] = grid[secondRow + shift][i];
            grid[secondRow + shift][i] = temp;
        }

    }
    private void SwapColumnsSmall(List<List<int>> grid, int height, int width)
    {
        (int firstRow, int secondRow, int box) = RandomForSwap();
        for (int i = 0; i < height; ++i)
        {
            int shift = 3 * box;
            int temp = grid[i][firstRow + shift];
            
            grid[i][firstRow + shift] = grid[i][secondRow + shift];
            grid[i][secondRow + shift] = temp;
        }

    }
    private void SwapRowsBox(List<List<int>> grid, int height, int width)
    {
        (int firstRow, int secondRow, int box) = RandomForSwap();

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                int firstShift = 3 * firstRow;
                int secondShift = 3 * secondRow;

                int temp = grid[i + firstShift][j];

                grid[i + firstShift][j] = grid[i + secondShift][j];

                grid[i + secondShift][j] = temp;
            }
        }
    }
    private void SwapColumnsBox(List<List<int>> grid, int height, int width)
    {
        (int firstRow, int secondRow, int box) = RandomForSwap();

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                int firstShift = 3 * firstRow;
                int secondShift = 3 * secondRow;

                int temp = grid[j][i + firstShift];

                grid[j][i + firstShift] = grid[j][i + secondShift];

                grid[j][i + secondShift] = temp;
            }
        }
    }    
    private (int first, int second, int box) RandomForSwap()
    {
        System.Random rnd = new System.Random();

        SortedSet<int> rows = new SortedSet<int>();

        for (int i = 0; i < 3; ++i)
        {
            rows.Add(i);
        }


        int firstRow = rnd.Next(0, rows.Count);
        rows.Remove(firstRow);

        int secondRow = rnd.Next(0, rows.Count);
        secondRow = rows.ElementAt(secondRow);

        int boxLine = rnd.Next(0, 3);

        return (firstRow, secondRow, boxLine);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SudokuLogic : MonoBehaviour
{
    public enum Difficulty{Easy, Medium, Hard};

    public List<List<int>> grid = new();
    public List<List<bool>> gridFlagged = new();

    private int height = 9;
    private int width = 9;
 
    private SudokuGenerator gen;
    private SudokuSolver solver;
    private SudokuShuffler shuffler;
    private SudokuRemover remover;

    private Difficulty difficulty = Difficulty.Medium;

    [SerializeField] private GameObject newspaper;
    [SerializeField] private Sprite basicNewspaperSprite;
    [SerializeField] private Sprite winNewspaperSprite;

    void Awake()
    {
        gen = GetComponent<SudokuGenerator>();
        solver = GetComponent<SudokuSolver>();
        shuffler = GetComponent<SudokuShuffler>();
        remover = GetComponent<SudokuRemover>();
        gen.CreateGrid(ref grid, ref gridFlagged, height, width);

    }

    public void Play()
    {
        gen.FillGrid(grid, 1, width, 3, height, width);
        shuffler.Shuffle(grid, height, width, 1000);
        remover.DeleteCells(ref grid, height, width, difficulty);
        
        gridFlagged = gen.FillFlags(grid, height, width);

        Debug.Log("Counter: " + countDeleted(gridFlagged));

        //StartCoroutine(solver.Solve(grid, gridFlagged, height, width));
    }

    private int countDeleted(List<List<bool>> flagged)
    {
        int counter = 0;
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                if (flagged[i][j])
                {
                    counter++;
                }
            }
        }

        return counter;
    }


    public void Test()
    {
        Debug.Log("Solutions: " + solver.AlgorithmY(grid, gridFlagged, height, width));
    }

    void Update()
    {
        if (solver.FindFreePos(grid, gridFlagged, height, width) == (-1, -1) && solver.IsGridValid(grid, height, width))
        {
            setSprite(winNewspaperSprite);
        }
        else
        {
            setSprite(basicNewspaperSprite);
        }

    }

    void setSprite(Sprite source)
    {
        newspaper.GetComponent<Image>().sprite = source;
    }

    public void SetDifficulty(float value)
    {
        difficulty = (Difficulty) value;
    }
}

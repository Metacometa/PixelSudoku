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

    [SerializeField] public int height = 9;
    [SerializeField] public int width = 9;
 
    private SudokuGenerator gen;
    private SudokuSolver solver;
    private SudokuShuffler shuffler;
    private SudokuRemover remover;

    private Difficulty difficulty = Difficulty.Medium;

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
        remover.DeleteCells(grid, height, width, difficulty);
        
        gridFlagged = gen.FillFlags(grid, height, width);



        //StartCoroutine(solver.Solve(grid, gridFlagged, height, width));
    }

    void Update()
    {
        Debug.Log("Difficulty: " + difficulty);
    }

    public void SetDifficulty(float value)
    {
        difficulty = (Difficulty) value;
    }
}

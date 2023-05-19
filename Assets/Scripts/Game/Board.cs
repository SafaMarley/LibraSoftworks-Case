using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    private int boardSize;
    [SerializeField] private GameObject boardCellPrefab;

    private int minimumMatch = 3;
    
    private BoardCell[,] boardCells;

    private void Awake()
    {
        Instance = this;

        boardSize = PlayerPrefs.GetInt("boardSize");
        
        boardCells = new BoardCell[boardSize, boardSize];
        float boardSizeOffset = (boardSize - 1) / 2.0f;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject boardCell = Instantiate(boardCellPrefab, transform);
                boardCell.name = "(" + i + ", " + j + ")";
                boardCells[i, j] = boardCell.GetComponent<BoardCell>();
                boardCells[i, j].Initialize(i - boardSizeOffset, j - boardSizeOffset, Mathf.Max(1.0f, boardSize / 10.0f));
            }
        }

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                BoardCell boardCellRef = boardCells[i, j];
                if (i > 0)
                {
                    boardCellRef.AddToNeighbors(boardCells[i - 1, j]);
                }
                if (j > 0)
                {
                    boardCellRef.AddToNeighbors(boardCells[i, j - 1]);
                }
                if (i < boardSize - 1)
                {
                    boardCellRef.AddToNeighbors(boardCells[i + 1, j]);
                }
                if (j < boardSize - 1)
                {
                    boardCellRef.AddToNeighbors(boardCells[i, j + 1]);
                }
            }
        }
    }

    public bool FindMatches(BoardCell activatedCell)
    {
        List<BoardCell> matchingCells = new List<BoardCell>();
        FindMatches(activatedCell, matchingCells);
        matchingCells.Add(activatedCell);

        if (matchingCells.Count >= minimumMatch)
        {
            foreach (BoardCell boardCell in matchingCells)
            {
                boardCell.Deactivate();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FindMatches(BoardCell boardCell, List<BoardCell> matchingCells)
    {

        foreach (BoardCell cell in boardCell.Neighbors)
        {
            if (matchingCells.Contains(cell))
            {
                continue;
            }

            if (cell.IsActive)
            {
                matchingCells.Add(cell);
                FindMatches(cell, matchingCells);
            }
        }
    }
}

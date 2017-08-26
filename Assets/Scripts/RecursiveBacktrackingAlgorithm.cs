using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktrackingAlgorithm : MazeAlgorithm {

	public RecursiveBacktrackingAlgorithm(Cell[,] cells) : base(cells)
	{
		CurrentCell = Stack.GetRandomCell();
		VisitedCells = 1;
	}

	public override void CreateMaze()
	{
		Debug.Log("The selected Cell is: Cell " + CurrentCell.Index.ToString());
	}
}

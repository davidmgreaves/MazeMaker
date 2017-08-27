using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktrackingAlgorithm : MazeAlgorithm {

	public List<Cell> CookieTrail { get; set; }

	public RecursiveBacktrackingAlgorithm(Cell[,] cells) : base(cells)
	{
		CurrentCell = Stack.GetRandomCell();
		CurrentCell.Visited = true;
		VisitedCells = 1;
	}

	public override void CreateMaze()
	{
		Cell neighbour = Stack.GetRandomNeighbour(CurrentCell.Index); // check for case where no unvisited neighbour is found
		Debug.Log("Current Cell's Index: " + CurrentCell.Index);
		Debug.Log("Neighbours Index: " + neighbour.Index);

		// while(VisitedCells < TotalCells)
		//		if current cell has unvisited neighbour
		//			DestroyWall(Direction)
		//			CookieTrail.Add(currentCell)
		//			CurrentCell = neighbour
		//			VisitedCells++
		//			CurrentCell.Visited = true
		//		else
		//			CurrentCell = CookieTrail.Pop()
	}

	public void BreakWall()
	{
		// destroy wall between two adjoining cells
	}
}

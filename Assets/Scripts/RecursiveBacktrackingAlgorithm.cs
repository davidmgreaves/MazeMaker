using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecursiveBacktrackingAlgorithm : MazeAlgorithm {

	public List<Cell> CookieTrail { get; set; }
	//protected enum direction { North = 1, South = 2, East = 3, West = 4 }

	public RecursiveBacktrackingAlgorithm(Cell[,] cells) : base(cells)
	{
		CookieTrail = new List<Cell>();
		CurrentCell = Stack.GetRandomCell();
		CurrentCell.Visited = true;
		VisitedCells = 1;
	}

	public override void CreateMaze()
	{

		List<Stack.direction> availableDirections = null;
		Cell neighbour = null;

		while (VisitedCells < Stack.TotalCells)
		{

			try
			{
				availableDirections = Stack.GetListOfUnvisitedNeighbours(CurrentCell);
				neighbour = Stack.GetRandomNeighbour(CurrentCell, availableDirections);
			}
			catch (Exception e)
			{
				CurrentCell = CookieTrail.ElementAt(CookieTrail.Count - 1);
				CookieTrail.RemoveAt(CookieTrail.Count - 1);
			}

			if (Stack.CellHasNotBeenVisited(neighbour)){
				DestroyAdjoiningWall(CurrentCell, neighbour);
				CookieTrail.Add(CurrentCell); // is this reference or value?
				CurrentCell = neighbour;
				CurrentCell.Visited = true;
				VisitedCells++;
			}
			else
			{
				CurrentCell = CookieTrail.ElementAt(CookieTrail.Count - 1);
				CookieTrail.RemoveAt(CookieTrail.Count - 1);
			}
		}
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

	public void DestroyAdjoiningWall(Cell currentCell, Cell adjoiningCell)
	{
		GameObject.Destroy(Stack.GetAdjoiningWall(currentCell, adjoiningCell));
	}
}

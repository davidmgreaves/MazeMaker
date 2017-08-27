using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecursiveBacktrackingAlgorithm : MazeAlgorithm {

	public List<Cell> CookieTrail { get; set; }

	public RecursiveBacktrackingAlgorithm(Cell[,] cells) : base(cells)
	{
		CookieTrail = new List<Cell>();
		CurrentCell = Stack.GetRandomCell();
		CurrentCell.Visited = true;
		VisitedCells = 1;
	}

	public override void CreateMaze()
	{
		List<Stack.Direction> availableDirections = null;
		Cell neighbour = null;

		while (VisitedCells < Stack.TotalCells)		// create Coroutine variant for visualising the maze creation as an option in the web player
		{
			availableDirections = Stack.GetListOfUnvisitedNeighbours(CurrentCell);

			if(availableDirections.Count > 0)
			{
				neighbour = Stack.GetRandomNeighbour(CurrentCell, availableDirections);

				if (Stack.CellHasNotBeenVisited(neighbour))
				{
					DestroyAdjoiningWall(CurrentCell, neighbour);
					CookieTrail.Add(CurrentCell);
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

			else
			{
				CurrentCell = CookieTrail.ElementAt(CookieTrail.Count - 1);
				CookieTrail.RemoveAt(CookieTrail.Count - 1);
			}
		}
	}

	public void DestroyAdjoiningWall(Cell currentCell, Cell adjoiningCell)
	{
		GameObject.Destroy(Stack.GetAdjoiningWall(currentCell, adjoiningCell));
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static Stack;

public class HuntAndKillAlgorithm : MazeAlgorithm {

	public List<Cell> CookieTrail { get; set; }
	public List<Stack.Direction> Directions { get; private set; }

	public HuntAndKillAlgorithm(Cell[,] cells) : base(cells)
	{
		CookieTrail = new List<Cell>();
		CurrentCell = Stack.GetRandomCell();
		CurrentCell.Visited = true;
		VisitedCells = 1;
		IsDestructive = true;

		
		Directions.Add(Stack.Direction.North); // better way to do this? look into iterating enum types, also consider making Direction enum static?
		Directions.Add(Stack.Direction.South);
		Directions.Add(Stack.Direction.East);
		Directions.Add(Stack.Direction.West);
	}

	public override void CreateMaze()
	{
		List<Stack.Direction> availableDirections = null;
		Cell neighbour = null;

		while (VisitedCells < Stack.TotalCells)     // create Coroutine variant for visualising the maze creation as an option in the web player
		{
			availableDirections = Stack.GetListOfUnvisitedNeighbours(CurrentCell);

			if (availableDirections.Count > 0)
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
					Hunt();
				
				}
			}

			else
			{
				CurrentCell = CookieTrail.ElementAt(CookieTrail.Count - 1);
				CookieTrail.RemoveAt(CookieTrail.Count - 1);
			}
		}
	}

	public void Hunt()
	{
		foreach(Cell c in Stack.Cells)
		{
			if(c.Visited == false)
			{
				foreach(Stack.Direction d in Directions)
				{
					if(!Stack.NeighbourHasNotBeenVisited(c, d)) // double negative- add opposing function, or consider changing to a custom return type?
					{
						CurrentCell = c;
						break;
					}
				}
			}
		}
	}

	public void DestroyAdjoiningWall(Cell currentCell, Cell adjoiningCell)
	{
		GameObject.Destroy(Stack.GetAdjoiningWall(currentCell, adjoiningCell));
	}
}

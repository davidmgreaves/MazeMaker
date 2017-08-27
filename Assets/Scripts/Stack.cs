using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stack {

	public Cell[,] Cells { get; set; }
	public int Rows { get; set; }
	public int Columns { get; set; }
	public int TotalCells { get; set; }
	public enum Direction { North = 1, South = 2, East = 3, West = 4, None = 0 }

	public Stack(Cell[,] cells)
	{
		Cells = cells;
		Rows = cells.GetLength(0);
		Columns = cells.GetLength(1);
		TotalCells = cells.Length;
	}

	public Cell GetCell(int index)
	{
		Cell desired = null;

		foreach(Cell c in Cells)
		{
			if (c.Index == index)
				desired = c;
		}

		return desired;
	}

	public Cell GetRandomCell()
	{
		int index = UnityEngine.Random.Range(1, TotalCells);

		return GetCell(index);
	}

	public bool CellHasNotBeenVisited(Cell currentCell)
	{
		bool cellIsUnvisited = false;

		if (currentCell.Visited == false)
			cellIsUnvisited = true;

		return cellIsUnvisited;
	}

	// performs a bounds check to see if the cell has an adjacent cell in the direction passed as a parameter
	public bool CurrentCellHasNeighbour(Cell currentCell, Direction dir) 
	{
		// if the current cell is not on one of the mazes edges, the resulting value will always be true
		bool hasNeighbour = true;

		switch (dir)
		{
			case Direction.North:

				// checks if the cell is in the last row, there is no northern neighbour if this statement evaluates to true
				if (currentCell.Row == (Rows - 1))
					hasNeighbour = false;

				break;

			case Direction.South:

				if (currentCell.Row == 0)
					hasNeighbour = false;

				break;

			case Direction.East:

				if (currentCell.Column == Columns - 1)
					hasNeighbour = false;

				break;

			case Direction.West:

				if (currentCell.Column == 0)
					hasNeighbour = false;

				break;
		}

		return hasNeighbour;
	}

	// Returns a boolean indicating weather the adjacent cell in the direction passed as a parameter has not been visited
	public bool NeighbourHasNotBeenVisited(Cell currentCell, Direction dir)
	{
		bool neighbourIsUnvisited = false;

		if (GetNeighbour(currentCell, dir).Visited == false)
			neighbourIsUnvisited = true;

		return neighbourIsUnvisited;
	}

	// Returns a collection of directions where the current cell has an adjacent unvisited cell
	public List<Direction> GetListOfUnvisitedNeighbours(Cell currentCell)
	{
		List<Direction> validDirections = new List<Direction>();

		if (CurrentCellHasNeighbour(currentCell, Direction.North) && NeighbourHasNotBeenVisited(currentCell, Direction.North))
		{
			validDirections.Add(Direction.North);
		}

		if (CurrentCellHasNeighbour(currentCell, Direction.South) && NeighbourHasNotBeenVisited(currentCell, Direction.South))
		{
			validDirections.Add(Direction.South);
		}

		if (CurrentCellHasNeighbour(currentCell, Direction.East) && NeighbourHasNotBeenVisited(currentCell, Direction.East))
		{
			validDirections.Add(Direction.East);
		}

		if (CurrentCellHasNeighbour(currentCell, Direction.West) && NeighbourHasNotBeenVisited(currentCell, Direction.West))
		{
			validDirections.Add(Direction.West);
		}

		return validDirections;
	}

	// returns the adjacent cell in the direction passed as a parameter- this method assumes that there is a valid neighbouring cell in the direction
	// supplied, bounds testing needs to be performed by the calling class with the HasNeighbour() method, prior to calling this method
	public Cell GetNeighbour(Cell currentCell, Direction dir) 
	{
		Cell neighbour = null;
		int index = 0;

		switch (dir)
		{
			case Direction.North:
				index = currentCell.Index + Columns;
				neighbour = GetCell(index);
				break;

			case Direction.South:
				index = currentCell.Index - Columns;
				neighbour = GetCell(index);
				break;

			case Direction.East:
				index = currentCell.Index + 1;
				neighbour = GetCell(index);
				break;

			case Direction.West:
				index = currentCell.Index - 1;
				neighbour = GetCell(index);
				break;
		}

		return neighbour;
	}

	// Returns an adjacent cell by first choosing a random direction, if that cell has already been visited, the method will instead return a nulled cell
	public Cell GetRandomNeighbour(Cell currentCell, List<Direction> validDirections)
	{																  
		Cell unvisited = null;
		List<Cell> availableCells = new List<Cell>();

		foreach(Direction dir in validDirections)
		{
			if (CurrentCellHasNeighbour(currentCell, dir) && NeighbourHasNotBeenVisited(currentCell, dir))
			{
				unvisited = GetNeighbour(currentCell, dir);
				availableCells.Add(unvisited);
			}
		}

		unvisited = availableCells[UnityEngine.Random.Range(0, availableCells.Count)];

		return unvisited;

	}

	public GameObject GetAdjoiningWall(Cell currentCell, Cell neighbour)
	{
		if (currentCell.Index == neighbour.Index - Columns)
			return currentCell.NorthWall;
			

		if (currentCell.Index == neighbour.Index + Columns)
			return currentCell.SouthWall;

		if (currentCell.Index == neighbour.Index - 1)
			return currentCell.EastWall;

		if (currentCell.Index == neighbour.Index + 1)
			return currentCell.WestWall;

		Debug.Log("Something went horribly wrong in Stack.GetAdjoiningWall");
		return null;
	}
}

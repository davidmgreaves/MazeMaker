using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stack {

	public Cell[,] Cells { get; set; }
	public int Rows { get; set; }
	public int Columns { get; set; }
	public int TotalCells { get; set; }
	public enum direction { North = 1, South = 2, East = 3, West = 4, None = 0 }

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
	public bool CurrentCellHasNeighbour(Cell currentCell, direction dir) 
	{
		// if the current cell is not on one of the mazes edges, the resulting value will always be true
		bool hasNeighbour = true;

		switch (dir)
		{
			case direction.North:

				// checks if the cell is in the last row, there is no northern neighbour if this statement evaluates to true
				if (currentCell.Row == (Rows - 1))
					hasNeighbour = false;

				break;

			case direction.South:

				if (currentCell.Row == 0)
					hasNeighbour = false;

				break;

			case direction.East:

				if (currentCell.Column == Columns - 1)
					hasNeighbour = false;

				break;

			case direction.West:

				if (currentCell.Column == 0)
					hasNeighbour = false;

				break;
		}

		return hasNeighbour;
	}

	// Returns a boolean indicating weather the adjacent cell in the direction passed as a parameter has not been visited
	public bool NeighbourHasNotBeenVisited(Cell currentCell, direction dir)
	{
		bool neighbourIsUnvisited = false;

		if (GetNeighbour(currentCell, dir).Visited == false)
			neighbourIsUnvisited = true;

		return neighbourIsUnvisited;
	}

	// Returns a collection of directions where the current cell has an adjacent unvisited cell
	public List<direction> GetListOfUnvisitedNeighbours(Cell currentCell)
	{
		List<direction> validDirections = new List<direction>();

		if (CurrentCellHasNeighbour(currentCell, direction.North) && NeighbourHasNotBeenVisited(currentCell, direction.North))
		{
			validDirections.Add(direction.North);
		}

		if (CurrentCellHasNeighbour(currentCell, direction.South) && NeighbourHasNotBeenVisited(currentCell, direction.South))
		{
			validDirections.Add(direction.South);
		}

		if (CurrentCellHasNeighbour(currentCell, direction.East) && NeighbourHasNotBeenVisited(currentCell, direction.East))
		{
			validDirections.Add(direction.East);
		}

		if (CurrentCellHasNeighbour(currentCell, direction.West) && NeighbourHasNotBeenVisited(currentCell, direction.West))
		{
			validDirections.Add(direction.West);
		}

		return validDirections;
	}

	// returns the adjacent cell in the direction passed as a parameter- this method assumes that there is a valid neighbouring cell in the direction
	// supplied, bounds testing needs to be performed by the calling class with the HasNeighbour() method, prior to calling this method
	public Cell GetNeighbour(Cell currentCell, direction dir) 
	{
		Cell neighbour = null;
		int index = 0;

		switch (dir)
		{
			case direction.North:
				index = currentCell.Index + Columns;
				neighbour = GetCell(index);
				break;

			case direction.South:
				index = currentCell.Index - Columns;
				neighbour = GetCell(index);
				break;

			case direction.East:
				index = currentCell.Index + 1;
				neighbour = GetCell(index);
				break;

			case direction.West:
				index = currentCell.Index - 1;
				neighbour = GetCell(index);
				break;
		}

		return neighbour;
	}

	// Returns an adjacent cell by first choosing a random direction, if that cell has already been visited, the method will instead return a nulled cell
	public Cell GetRandomNeighbour(Cell currentCell, List<direction> validDirections)
	{																  
		Cell unvisited = null;
		List<Cell> availableCells = new List<Cell>();

		foreach(direction dir in validDirections)
		{
			Debug.Log("Direction: " + dir); 
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

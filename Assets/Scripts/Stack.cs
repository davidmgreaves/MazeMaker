using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stack : MonoBehaviour {

	public Cell[,] Cells { get; set; }
	public int Rows { get; set; }
	public int Columns { get; set; }
	public int TotalCells { get; set; }

	protected enum direction { North = 1, South = 2, East = 3, West = 4 }

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

	protected Cell GetNeighbour(int currentIndex, direction dir)
	{
		Cell neighbour = null;
		int index = 0;

		switch (dir)
		{
			case direction.North:
				index = currentIndex + Columns;
				neighbour = GetCell(index);
				break;

			case direction.South:
				index = currentIndex - Columns;
				neighbour = GetCell(index);
				break;

			case direction.East:
				index = currentIndex + 1;
				neighbour = GetCell(index);
				break;

			case direction.West:
				index = currentIndex - 1;
				neighbour = GetCell(index);
				break;
		}

		return neighbour;
	}

	public Cell GetRandomNeighbour(int currentIndex)
	{
		int index = UnityEngine.Random.Range(1, 4);

		return GetNeighbour(currentIndex, (direction)index);

	}
}

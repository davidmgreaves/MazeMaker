using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {

	public GameObject Floor { get; set; }
	public GameObject NorthWall { get; set; }
	public GameObject SouthWall { get; set; }
	public GameObject EastWall { get; set; }
	public GameObject WestWall { get; set; }
	public bool Visited { get; set; }
	public int Index { get; set; }
	public int Column { get; set; }
	public int Row { get; set; }


	public Cell(int x, int z, int cellCount)
	{
		Row = x;
		Column = z;
		Index = cellCount;
		Visited = false;
	}
}

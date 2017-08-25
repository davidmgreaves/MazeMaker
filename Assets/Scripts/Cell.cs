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
	// add property to identify cells within a collection- either a cell number or a column and index, both could be useful in different scenarios

	public Cell ()
	{
		Visited = false;
	}

}

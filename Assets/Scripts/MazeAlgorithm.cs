using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAlgorithm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// add maze carving behaviour- create seperate class for maze algorithms?? maybe an interface...
	public void CreateTheMaze()
	{

		// pick a random cell to start the maze from - create a seed based random class to handle this
		// set the cells visited boolean to true
		//VisitedCells = 1;

		//while (VisitedCells < TotalCells)
		//{ 
		//	// do choose a random direction to move in (north, south, east or west)
		//	// if the cell directly adjacent to the current cell in the above direction is unvisited, destroy the adjoining wall and move to that cell,
		//	//		else pick a new direction. If all adjacent cells have been visited, move back to the first available cell in the cookietrail with at
		//	//		least one neighbouring cell that is unvisited.
		//	//			if there is no such cells remaining randomly select a new cell from the list of unvisited cells and repeat the previous step
		//	// Add the previous cell to the cookie trail for backtracking purposes, and repeat the previous step for the current cell
		//	// Once the number of visited cells is no longer less than the number of total cells the maze has been completed
		//}
	}
}

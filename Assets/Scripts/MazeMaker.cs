using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour {

	public GameObject walls;
	public GameObject floorPrefab;
	public GameObject wallPrefab;

	public int Columns { get; private set; }
	public int Rows { get; private set; }
	public Cell[,] Cells { get; private set; }
	public int WallLength { get; private set; }
	public Vector3 BoardCenter { get; private set; }
	public Vector3 InitialPosition { get; private set; }
	public Vector3 CurrentPosition { get; private set; }
	public List<int> CookieTrail { get; private set; }
	public int VisitedCells { get; private set; }
	public int TotalCells { get; private set; }

	public IEnumerator BuildTheEggCarton()
	{
		WaitForSeconds delay = new WaitForSeconds(0.2f);

		// creates an object for storing the walls of the maze
		walls = new GameObject();
		walls.name = "Walls";
		Columns = 5;
		Rows = 5;
		WallLength = 2;
		TotalCells = Rows * Columns;

		// the egg carton extends from the initialPosition to the right of the initalPositions x and z location, the initialPosition is effectively
		// determined by moving in the negative of the X and Z directions, by half of the columns and rows respectively. This ensures the maze is always
		// centered on the screen, irrespective of the number of rows and columns specified
		InitialPosition = new Vector3((-Columns / 2) + WallLength / 2, 0.0f, (-Rows / 2) + WallLength / 2);

		CurrentPosition = InitialPosition;
		Cells =  new Cell[(int)Rows, (int)Columns];

		for(int x = 0; x < Rows; x++)
		{
			for(int z = 0; z < Columns; z++)
			{
				yield return delay;

				// creates a cell at the current position and instantiates a floor and four walls for the cell
				CurrentPosition = new Vector3(InitialPosition.x + z * WallLength - WallLength / 2, 0.0f,InitialPosition.z + x * WallLength - WallLength / 2);
				Cell currentCell = new Cell();
				currentCell.Floor = Instantiate(floorPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				
				// all cells get a south and a west wall
				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength / 2, 0.0f, InitialPosition.z + (x * WallLength) - WallLength);
				currentCell.SouthWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				currentCell.SouthWall.transform.parent = walls.transform;

				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength, 0.0f, InitialPosition.z + (x * WallLength) - WallLength / 2);
				currentCell.WestWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				currentCell.WestWall.transform.Rotate(0.0f, 90.0f, 0.0f);
				currentCell.WestWall.transform.parent = walls.transform;

				// but only cells in the last row and column get a north and an east well too
				if (x == (Rows - 1))
				{
					CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength / 2, 0.0f, InitialPosition.z + (x * WallLength));
					currentCell.NorthWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
					currentCell.NorthWall.transform.parent = walls.transform;
				}

				if (z == (Columns - 1))
				{
					CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength), 0.0f, InitialPosition.z + (x * WallLength) - WallLength / 2);
					currentCell.EastWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
					currentCell.EastWall.transform.Rotate(0.0f, 90.0f, 0.0f);
					currentCell.EastWall.transform.parent = walls.transform;
				}

				// consider giving each cell's north and east wall a reference to the south and west wall of the cells directly 
				// above and directly to the right...

				Cells[x, z] = currentCell;

			}
		}
	}

	// add maze carving behaviour- create seperate class for maze algorithms?? maybe an interface...
	public IEnumerator CarveTheMaze ()
	{
		WaitForSeconds delay = new WaitForSeconds(0.1f);

		// pick a random cell to start the maze from - create a seed based random class to handle this
		// set the cells visited boolean to true
		VisitedCells = 1;

		while ( VisitedCells < TotalCells)
		{
			yield return delay;

			// do choose a random direction to move in (north, south, east or west)
			// if the cell directly adjacent to the current cell in the above direction is unvisited, destroy the adjoining wall and move to that cell,
			//		else pick a new direction. If all adjacent cells have been visited, move back to the first available cell in the cookietrail with at
			//		least one neighbouring cell that is unvisited.
			//			if there is no such cells remaining randomly select a new cell from the list of unvisited cells and repeat the previous step
			// Add the previous cell to the cookie trail for backtracking purposes, and repeat the previous step for the current cell
			// Once the number of visited cells is no longer less than the number of total cells the maze has been completed
		}
	}

}

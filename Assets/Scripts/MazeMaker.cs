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

	// method used to create a grid of cells without any walls to be used with additive maze algorithms
	public IEnumerator GenerateCells()
	{
		WaitForSeconds delay = new WaitForSeconds(0.2f);

		Columns = 5;
		Rows = 5;
		WallLength = 2;
		TotalCells = Rows * Columns;

		// the grid extends from the initialPosition to the right of the initalPositions x and z location, the initialPosition is effectively
		// determined by starting at the worlds origin, and moving in the negative of the X and Z directions, by half of the columns and rows 
		// respectively. This ensures the maze is always centered on the screen, irrespective of the number of rows and columns specified
		InitialPosition = new Vector3((-Columns / 2) + WallLength / 2, 0.0f, (-Rows / 2) + WallLength / 2);

		CurrentPosition = InitialPosition;
		Cells = new Cell[(int)Rows, (int)Columns];

		for (int x = 0; x < Rows; x++)
		{
			for (int z = 0; z < Columns; z++)
			{
				yield return delay;

				// creates a cell at the current position, instantiates a floor for the cell and adds the cell to the 
				// collection of existing cells
				CurrentPosition = new Vector3(InitialPosition.x + z * WallLength - WallLength / 2, 0.0f, InitialPosition.z + x * WallLength - WallLength / 2);
				Cell currentCell = new Cell();
				currentCell.Floor = Instantiate(floorPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				Cells[x, z] = currentCell;

			}
		}
	}

	// method used to create a grid of cells with four walls to be used with destructive maze algorithms
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

		// the grid extends from the initialPosition to the right of the initalPositions x and z location, the initialPosition is effectively
		// determined by starting at the worlds origin, and moving in the negative of the X and Z directions, by half of the columns and rows 
		// respectively. This ensures the maze is always centered on the screen, irrespective of the number of rows and columns specified
		InitialPosition = new Vector3((-Columns / 2) + WallLength / 2, 0.0f, (-Rows / 2) + WallLength / 2);

		CurrentPosition = InitialPosition;
		Cells =  new Cell[(int)Rows, (int)Columns];

		for(int x = 0; x < Rows; x++)
		{
			for(int z = 0; z < Columns; z++)
			{
				yield return delay;

				// creates a cell at the current position, instantiates a floor and four walls for the cell and adds the cell to 
				// the collection of existing cells
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

}

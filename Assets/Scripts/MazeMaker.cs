using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour {

	public GameObject allWalls;
	public GameObject northWalls;
	public GameObject southWalls;
	public GameObject eastWalls;
	public GameObject westWalls;
	public GameObject allFloorTiles;

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
	public int cellCount { get; set; }
	public RecursiveBacktrackingAlgorithm recursiveBacktrackingMaze { get; set; }

	// method used to create a grid of cells without any walls to be used with additive maze algorithms
	public IEnumerator GenerateCells( int rows, int columns)
	{
		WaitForSeconds delay = new WaitForSeconds(0.2f);

		// creates an object for storing the floor tiles of the maze
		allFloorTiles = new GameObject();
		allFloorTiles.name = "Floor Tiles";

		Rows = rows;
		Columns = columns;
		WallLength = 2;
		TotalCells = Rows * Columns;
		cellCount = 1;

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
				Cell currentCell = new Cell( x, z, cellCount++);
				currentCell.Floor = Instantiate(floorPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				Cells[x, z] = currentCell;
				currentCell.Floor.transform.parent = allFloorTiles.transform;
				currentCell.Floor.name = "Floor " + currentCell.Index.ToString();

			}
		}
	}

	// method used to create a grid of cells with four walls to be used with destructive maze algorithms
	public IEnumerator BuildTheEggCarton(int rows, int columns)
	{
		WaitForSeconds delay = new WaitForSeconds(0.2f);

		// creates an object for storing the walls and floor tiles of the maze
		allWalls = new GameObject();
		allWalls.name = "Walls";
		northWalls = new GameObject();
		northWalls.name = "North Walls";
		northWalls.transform.parent = allWalls.transform.parent;
		southWalls = new GameObject();
		southWalls.name = "South Walls";
		southWalls.transform.parent = allWalls.transform.parent;
		eastWalls = new GameObject();
		eastWalls.name = "East Walls";
		eastWalls.transform.parent = allWalls.transform.parent;
		westWalls = new GameObject();
		westWalls.name = "West Walls";
		westWalls.transform.parent = allWalls.transform.parent;

		allFloorTiles = new GameObject();
		allFloorTiles.name = "Floor Tiles";

		Rows = rows;
		Columns = columns;
		WallLength = 2;
		TotalCells = Rows * Columns;
		cellCount = 1;

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
				Cell currentCell = new Cell(x, z, cellCount++);
				currentCell.Floor = Instantiate(floorPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				currentCell.Floor.transform.parent = allFloorTiles.transform;
				currentCell.Floor.name = "Floor " + currentCell.Index.ToString();

				// all cells get their own north and an east wall instantiated
				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength / 2, 0.0f, InitialPosition.z + (x * WallLength));
				currentCell.NorthWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				currentCell.NorthWall.name = "North Wall " + currentCell.Index.ToString();
				currentCell.NorthWall.transform.parent = northWalls.transform;

				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength), 0.0f, InitialPosition.z + (x * WallLength) - WallLength / 2);
				currentCell.EastWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				currentCell.EastWall.transform.Rotate(0.0f, 90.0f, 0.0f);
				currentCell.EastWall.name = "East Wall " + currentCell.Index.ToString();
				currentCell.EastWall.transform.parent = eastWalls.transform;

				// but only cells in the first row and column get a south and a west well instantiate too
				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength / 2, 0.0f, InitialPosition.z + (x * WallLength) - WallLength);

				if (x == (0))
				{
					currentCell.SouthWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
					currentCell.SouthWall.name = "South Wall " + currentCell.Index.ToString();
					currentCell.SouthWall.transform.parent = southWalls.transform;
				}

				else
				{
					// all other cells simply get a reference to the Northern or Eastern wall of the cell adjacently below or to the left instead
					currentCell.SouthWall = Cells[x - 1, z].NorthWall;
					currentCell.SouthWall.name = "South Wall " + currentCell.Index.ToString();
					//currentCell.SouthWall.transform.parent = southWalls.transform;
				}

				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength, 0.0f, InitialPosition.z + (x * WallLength) - WallLength / 2);

				if (z == (0))
				{
					currentCell.WestWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
					currentCell.WestWall.transform.Rotate(0.0f, 90.0f, 0.0f);
					currentCell.WestWall.name = "West Wall " + currentCell.Index.ToString();
					currentCell.WestWall.transform.parent = westWalls.transform;
				}

				else
				{
					currentCell.WestWall = Cells[x, z - 1].EastWall;
					currentCell.WestWall.name = "West Wall " + currentCell.Index.ToString();
					//currentCell.WestWall.transform.parent = westWalls.transform;
				}

				Cells[x, z] = currentCell;
			}
		}

		recursiveBacktrackingMaze = new RecursiveBacktrackingAlgorithm(Cells);
		recursiveBacktrackingMaze.CreateMaze();
	}

}

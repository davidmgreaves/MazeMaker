using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour {

	public GameObject floorPrefab;
	public GameObject wallPrefab;

	public int Columns { get; private set; }
	public int Rows { get; private set; }

	public Cell[,] Cells { get; private set; }
	public int WallLength { get; private set; }
	public Vector3 InitialPosition { get; private set; }
	public Vector3 CurrentPosition { get; private set; }
	public List<int> CookieTrail { get; private set; }
	public int VisitedCells { get; private set; }
	public int TotalCells { get; private set; }
	public int CellCount { get; set; }
	public RecursiveBacktrackingAlgorithm RecursiveBacktrackingMaze { get; private set; }
	public HuntAndKillAlgorithm HuntAndKillMaze { get; private set; }
	public string SelectedAlgorithm { get; set; }

	// method used to create a grid of cells without any walls to be used with additive maze algorithms
	public void GenerateCells( int rows, int columns, WaitForSeconds delay) 
	{
		WaitForSeconds waitForSeconds = delay;

		Rows = rows;
		Columns = columns;
		WallLength = 2;
		TotalCells = Rows * Columns;
		CellCount = 1;

		// sets the position for the first cell relative to the origin in world space, such that the maze will always be centered regardless of size
		InitialPosition = new Vector3(((float) -Columns / 2f) + (float) WallLength / 2f, 0.0f, ((float) -Rows / 2f) + (float) WallLength / 2f);

		CurrentPosition = InitialPosition;
		Cells = new Cell[(int)Rows, (int)Columns];

		for (int x = 0; x < Rows; x++)
		{
			for (int z = 0; z < Columns; z++)
			{
				// creates a cell at the current position, instantiates a floor for the cell and adds the cell to the 
				// collection of existing cells
				CurrentPosition = new Vector3(InitialPosition.x + z * WallLength - WallLength / 2, 0.0f, InitialPosition.z + x * WallLength - WallLength / 2);
				Cell currentCell = new Cell( x, z, CellCount++);
				currentCell.Floor = Instantiate(floorPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				Cells[x, z] = currentCell;
			}
		}

		try
		{
			LaunchMazeAlgorithm(SelectedAlgorithm, delay); // additive maze algorithm not instantiated yet, this method call will fail
		}
		catch (System.Exception e)
		{
			Debug.Log("An additive maze algorithm has not been instantiated yet! LaunchMazeAlgorithm should not be called from GenerateCells");
			throw e;
		} 
	}

	// method used to create a grid of cells with four walls to be used with destructive maze algorithms
	public void BuildTheEggCarton(int rows, int columns, WaitForSeconds delay)
	{
		WaitForSeconds waitForSeconds = delay;

		Rows = rows;
		Columns = columns;
		WallLength = 2;
		TotalCells = Rows * Columns;
		CellCount = 1;

		// sets the position for the first cell relative to the origin in world space, such that the maze will always be centered regardless of size
		InitialPosition = new Vector3(((float) -Columns / 2f) * WallLength + WallLength, 0.0f, ((float) -Rows / 2f) * WallLength + WallLength);

		CurrentPosition = InitialPosition;
		Cells =  new Cell[(int)Rows, (int)Columns];

		for(int x = 0; x < Rows; x++)
		{
			for(int z = 0; z < Columns; z++)
			{
				// creates a cell at the current position, instantiates a floor and four walls for the cell and adds the cell to 
				// the collection of existing cells
				CurrentPosition = new Vector3(InitialPosition.x + z * WallLength - WallLength / 2, 0.0f,InitialPosition.z + x * WallLength - WallLength / 2);
				Cell currentCell = new Cell(x, z, CellCount++);
				currentCell.Floor = Instantiate(floorPrefab, CurrentPosition, Quaternion.identity) as GameObject;

				// all cells get their own north and an east wall instantiated
				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength / 2, 0.0f, InitialPosition.z + (x * WallLength));
				currentCell.NorthWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;

				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength), 0.0f, InitialPosition.z + (x * WallLength) - WallLength / 2);
				currentCell.EastWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				currentCell.EastWall.transform.Rotate(0.0f, 90.0f, 0.0f);

				// but only cells in the first row and column get their own south and a west well instantiated too
				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength / 2, 0.0f, InitialPosition.z + (x * WallLength) - WallLength);

				if (x == (0))
				{
					currentCell.SouthWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
				}

				else
				{
					// all other cells simply get a reference to the Northern or Eastern wall of the cell adjacently below or to the left instead
					currentCell.SouthWall = Cells[x - 1, z].NorthWall;
				}

				CurrentPosition = new Vector3(InitialPosition.x + (z * WallLength) - WallLength, 0.0f, InitialPosition.z + (x * WallLength) - WallLength / 2);

				if (z == (0))
				{
					currentCell.WestWall = Instantiate(wallPrefab, CurrentPosition, Quaternion.identity) as GameObject;
					currentCell.WestWall.transform.Rotate(0.0f, 90.0f, 0.0f);
				}

				else
				{
					currentCell.WestWall = Cells[x, z - 1].EastWall;
				}

				Cells[x, z] = currentCell;
			}
		}

		LaunchMazeAlgorithm(SelectedAlgorithm, delay);
	}

	// Selects the appropriate algorithm based on the options selected in the webplayer (unimplemented) and creates the maze
	public void LaunchMazeAlgorithm(string selectedAlgorithm, WaitForSeconds delay)						 // this should be part of the maze manager class!
	{
		switch (selectedAlgorithm)
		{
			case "RecursvieBacktracking":
				break;

			case "HuntAndKill":
				break;

			case "RecursiveDivision":
				break;
		}

		RecursiveBacktrackingMaze = new RecursiveBacktrackingAlgorithm(Cells);   // add code to deal with selecting the appropriate algorithm for when multiple
		HuntAndKillMaze = new HuntAndKillAlgorithm(Cells);

		RecursiveBacktrackingMaze.CreateMaze();                                  // maze classes are available
	}

}

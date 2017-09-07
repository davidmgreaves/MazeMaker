using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

	public MazeMaker mazeMaker;
	public int Columns = 5; // consider making these properties again when you build a UI for the web player
	public int Rows = 4;    // and no longer need to access the values directly in the inspector
	public GameObject playerPrefab;
	public GameObject playerInstance;
	public EndPoint endPoint;
	//public Player player;
	public Vector3 playerStartingPosition; 
	
	public WaitForSeconds Delay { get; private set; }

	void Start () {

		mazeMaker.SelectedAlgorithm = "RecursiveBacktracking";
		Delay = new WaitForSeconds(0f);
		
		LoadMaze();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			RestartMaze();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnPlayer();
		}
	}
	
	void LoadMaze () {

		switch (mazeMaker.SelectedAlgorithm)
		{
			case "RecursiveDivision":
				mazeMaker.GenerateCells(Rows, Columns, Delay);
				break;

			default:
				mazeMaker.BuildTheEggCarton(Rows, Columns, Delay);
				break;
		}
		
	}

	void RestartMaze ()
	{
		StopAllCoroutines();
		Destroy(mazeMaker.gameObject);
		Destroy(playerInstance);
		LoadMaze();
	}

	void SpawnPlayer()
	{
		//player = new Player(Rows, Columns);
		Instantiate(playerInstance, mazeMaker.InitialPosition, playerPrefab.transform.rotation);
		Instantiate(endPoint, new Vector3(Rows + 1, 1, Columns - 3), Quaternion.identity);

	}
}

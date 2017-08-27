using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

	public MazeMaker mazeMaker;

	public bool UsesDestructiveAlgorithm { get; set; }
	public int Columns; // consider making these properties again when you build a UI for the web player
	public int Rows;	// and no longer need to access the values directly in the inspector

	void Start () {

		UsesDestructiveAlgorithm = true;

		LoadMaze();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			RestartMaze();
		}
	}
	
	void LoadMaze () {

		if (UsesDestructiveAlgorithm)
		{
			StartCoroutine(mazeMaker.BuildTheEggCarton(Rows, Columns));
		}
		else
		{
			StartCoroutine(mazeMaker.GenerateCells(Rows, Columns));
		}
		
	}

	void RestartMaze ()
	{
		Destroy(mazeMaker.gameObject);
		LoadMaze();
	}
}

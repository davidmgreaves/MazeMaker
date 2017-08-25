using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

	public MazeMaker mazeMaker;

	public bool UsesDestructiveAlgorithm { get; set; }

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
			StartCoroutine(mazeMaker.BuildTheEggCarton());
		}
		else
		{
			StartCoroutine(mazeMaker.GenerateCells());
		}
		
	}

	void RestartMaze ()
	{
		Destroy(mazeMaker.gameObject);
		LoadMaze();
	}
}

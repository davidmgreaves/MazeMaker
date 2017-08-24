using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

	public MazeMaker mazeMakerPrefab;

	private MazeMaker mazeMakerInstance;

	void Start () {
		LoadMaze();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Enter))
		{
			RestartMaze();
		}
	}
	
	void LoadMaze () {
		mazeMakerInstance = Instantiate(mazeMakerPrefab) as MazeMaker;
	}

	void RestartMaze ()
	{
		Destoy(mazeMakerInstance.gameObject);
		LoadMaze;
	}
}

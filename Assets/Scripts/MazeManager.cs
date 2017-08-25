using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

	public MazeMaker mazeMaker;

	void Start () {
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
		//mazeMakerInstance = Instantiate(mazeMakerPrefab) as MazeMaker;
		//mazeMakerInstance.BuildTheEggCarton();
		StartCoroutine(mazeMaker.BuildTheEggCarton());
	}

	void RestartMaze ()
	{
		Destroy(mazeMaker.gameObject);
		LoadMaze();
	}
}

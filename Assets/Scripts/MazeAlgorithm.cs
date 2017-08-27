using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeAlgorithm : MonoBehaviour {

	public Cell CurrentCell { get; set; }
	public int VisitedCells { get; set; }
	public Stack Stack { get; set; }

	public MazeAlgorithm( Cell[,] cells )
	{

		Stack = new Stack(cells);
	}

	public abstract void CreateMaze();
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Cell currentCell;
	private Vector3 currentPosition;

	public Player(int x, int z)
	{
		transform.position = new Vector3(x, 0, z);
		currentPosition = transform.position;
	}

	public void SetLocation(Vector3 pos)
	{
		transform.position = currentPosition + pos;
		currentPosition = transform.position;
	}

	private void Move(Stack.Direction direction)
	{
		switch (direction)
		{
			case Stack.Direction.North:
				SetLocation(new Vector3(0, 0, 1));
				break;

			case Stack.Direction.South:
				SetLocation(new Vector3(0, 0, -1));
				break;

			case Stack.Direction.East:
				SetLocation(new Vector3(1, 0, 0));
				break;

			case Stack.Direction.West:
				SetLocation(new Vector3(-1, 0, 0));
				break;

		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Move(Stack.Direction.North);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Move(Stack.Direction.East);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Move(Stack.Direction.South);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Move(Stack.Direction.West);
		}
	}
}

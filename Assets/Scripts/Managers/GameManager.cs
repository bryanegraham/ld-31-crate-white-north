using UnityEngine;
using System.Collections.Generic;

class GameManager : MonoBehaviour
{
	public static GameManager Instance {get; private set;}

	public enum Side
	{
		LEFT
		, RIGHT
		, MAX
	}

	public int[] Score = new int[(int)Side.MAX];
	public int GameDuration = 300;

	public float TimeRemaining;
	public bool GameRunning = true;

	public GameObject[] IndestructableCrates;
	public GameObject[] Players;

	void Start()
	{
		if (null == Instance)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}

		SetupGame();
	}

	void Update()
	{
		if (GameRunning)
		{
			TimeRemaining -= Time.deltaTime;

			if (TimeRemaining <= 0f)
			{
				EndGame();
			}
		}
	}

	public void ModifyPoints(Side side, int value)
	{
		Score[(int)side] += value;
	}

	public void SetupGame()
	{
		TimeRemaining = (float)GameDuration;
		RepositionPlayers();
		RepositionCrates();
	}

	public void StartGame()
	{
		GameRunning = true;
	}

	public void EndGame()
	{
		GameRunning = false;
	}

	private void RepositionPlayers()
	{
		foreach (GameObject player in Players)
		{
			player.rigidbody.velocity = Vector3.zero;
			player.rigidbody.angularVelocity = Vector3.zero;
		}

		Players[0].transform.position = new Vector3(-2f, 0f, 0f);
		Players[1].transform.position = new Vector3(2f, 0f, 0f);
		Players[1].transform.eulerAngles = new Vector3(0f, 180f, 0f);
	}

	private void RepositionCrates()
	{
		int numCrates = IndestructableCrates.Length;

		float[] zValues = {15f, 5f, -5f, -15f};

		for (int i = 0 ; i < numCrates; ++i)
		{
			float x = i >= numCrates/2 ? -15f : 15f;
			float y = 0.5f;

			int idx = i % (numCrates/2);

			float z = zValues[idx];

			IndestructableCrates[i].transform.position = new Vector3(x, y, z);
			IndestructableCrates[i].transform.eulerAngles = new Vector3(0f, 0f, 0f);

		}
	}
}
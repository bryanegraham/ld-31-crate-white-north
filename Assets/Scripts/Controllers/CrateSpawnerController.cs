using UnityEngine;
using System;
using System.Collections.Generic;

class CrateSpawnerController : MonoBehaviour
{
	public List<GameObject> spawnedCrates;
	public Queue<GameObject> cratePool;

	public int MaxCratesSpawned = 10;
	public int SpawnPenguineCount = 5;
	public int SpawnBreakableCount = 10;

	public float SpawnIntervalInSeconds = 15;
	public float SpawnInitialDelay = 30;

	public float spawnCounter = 0f;

	public Vector3 SpawnPosition = Vector3.zero;
	public Vector3 SpawnVelocity = Vector3.zero; 

	public Transform CrateRoot = null;
	public GameObject PenguinCratePrefab = null;
	public GameObject NormalCratePrefab = null;
	public GameObject IndestructableCratePrefab = null;

	void Start()
	{
		GameManager.Instance.GameSetupListeners += OnGameSetup;
		GameManager.Instance.GameEndListeners += OnGameEnd;

		spawnedCrates = new List<GameObject>();
		cratePool = new Queue<GameObject>();

		int numSpawns = (GameManager.Instance.GameDuration - (int)SpawnInitialDelay) / (int)SpawnIntervalInSeconds;
		SpawnBreakableCount = numSpawns - SpawnPenguineCount - 1;
	}

	void Update()
	{
		if (GameManager.Instance.GameRunning)
		{
			spawnCounter -= Time.deltaTime;

			if (spawnCounter <= 0)
			{
				Spawn();
				spawnCounter = SpawnIntervalInSeconds;
			}
		}
	}

	void Spawn()
	{
		if (cratePool.Count > 0)
		{
			GameObject go = cratePool.Dequeue();
			spawnedCrates.Add(go);

			go.rigidbody.velocity = SpawnVelocity;
			go.SetActive(true);
		}
	}

	void InitSpawnList()
	{
		List<GameObject> temp = new List<GameObject>();
		for (int i = 0 ; i < SpawnPenguineCount; ++i)
		{
			temp.Add(SpawnA(PenguinCratePrefab));
		}
		for (int i = 0 ; i < SpawnBreakableCount; ++i)
		{
			temp.Add(SpawnA(NormalCratePrefab));
		}

		// Fisher-Yates shuffle here, son
		System.Random rnd = new System.Random();
		int n = temp.Count;
		while (n > 1)
		{
			n--;
			int t = rnd.Next(n+1);
			Swap(temp, n, t);
		}
		temp.Add(SpawnA(IndestructableCratePrefab));
		Swap(temp, temp.Count - 1, temp.Count/2);

		cratePool = new	Queue<GameObject>(temp);
	}

	private void Swap(List<GameObject> list, int a, int b)
	{
		GameObject go = list[a];
		list[a] = list[b];
		list[b] = go;
	}

	GameObject SpawnA(GameObject prefab)
	{
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		go.SetActive(false);

		go.transform.position = new Vector3(SpawnPosition.x, go.transform.position.y, SpawnPosition.z);
		go.transform.parent = CrateRoot;
		return go;
	}

	public void OnGameSetup()
	{
		spawnCounter = SpawnInitialDelay;

		foreach (GameObject go in cratePool)
		{
			Destroy(go);
		}
		cratePool.Clear();

		foreach (GameObject go in spawnedCrates)
		{
			Destroy(go);
		}
		spawnedCrates.Clear();

		InitSpawnList();
	}

	public void OnGameEnd(int[] score)
	{

	}
}
using UnityEngine;
using System.Collections.Generic;

class GameEndOverlay : MonoBehaviour
{
	public GameObject[] MainMenuButtons;
	public GameObject[] ReplayButtons;
	public GameObject Player1WinsImage;
	public GameObject Player2WinsImage;
	public GameObject TieGameImage;
	public GameObject Panel;

	public Vector3 OffScreenPos = Vector3.zero;
	public Vector3 OnScreenPos = Vector3.zero;
	public Vector3 targetPosition;
	public float interpRate = 3.0f;

	void Start()
	{
		GameManager.Instance.GameEndListeners += OnEndGame;
		GameManager.Instance.GameSetupListeners += OnSetupGame;

		OnScreenPos = Panel.transform.position;
		OffScreenPos = Panel.transform.position;
		OffScreenPos.y -= 600;

		targetPosition = OnScreenPos;
	}

	void Update()
	{
		Panel.transform.position = Vector3.Lerp(Panel.transform.position, targetPosition, interpRate * Time.deltaTime);
	}

	public void OnEndGame(int[] score)
	{
		targetPosition = OnScreenPos;

		foreach (GameObject go in MainMenuButtons)
		{
			go.SetActive(false);
		}

		foreach (GameObject go in ReplayButtons)
		{
			go.SetActive(true);
		}

		if (score[0] > score[1])
		{
			Player1WinsImage.SetActive(true);
		}
		else if (score[1] > score[0])
		{
			Player2WinsImage.SetActive(true);
		}
		else
		{
			TieGameImage.SetActive(true);
		}
	}

	public void OnSetupGame()
	{
		targetPosition = OffScreenPos;
	}
}
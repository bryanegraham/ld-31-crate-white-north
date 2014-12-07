using UnityEngine;

class DisableUIEventsOnGameStart : MonoBehaviour
{
	void Start()
	{
		GameManager.Instance.GameSetupListeners += OnGameStart;
		GameManager.Instance.GameEndListeners += OnGameEnd;
	}

	void OnGameStart()
	{
		gameObject.SetActive(false);
	}

	void OnGameEnd(int[] scores)
	{
		gameObject.SetActive(true);
	}
}
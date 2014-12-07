using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

class ScoreOverlay : MonoBehaviour
{
	public GameObject Panel;
	public Text LeftScore;
	public Text RightScore;
	public Text ScoreClock;

	private TimeSpan span;

	void Start()
	{
		GameManager.Instance.GameEndListeners += OnEndGame;
		GameManager.Instance.GameSetupListeners += OnSetupGame;
	}

	void Update()
	{
		if (GameManager.Instance.GameRunning)
		{
			LeftScore.text = GameManager.Instance.Score[(int)GameManager.Side.LEFT].ToString();
			RightScore.text = GameManager.Instance.Score[(int)GameManager.Side.RIGHT].ToString();

			span = TimeSpan.FromSeconds((double)GameManager.Instance.TimeRemaining);
			ScoreClock.text = span.Minutes + ":" + span.Seconds.ToString("00");
		}
	}

	public void OnSetupGame()
	{
		Panel.SetActive(true);
	}

	public void OnEndGame(int[] scores)
	{
		ScoreClock.text = "0:00";
	}
}
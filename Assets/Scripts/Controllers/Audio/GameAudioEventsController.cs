using UnityEngine;

class GameAudioEventsController : MonoBehaviour
{
	public AudioClip CrowdFail;
	public AudioClip CrowdWin;
	public AudioClip BandFail;
	public AudioClip BandWin;

	public AudioClip BeginGame;

	public AudioSource Crowd;
	public AudioSource Band;

	void Start()
	{
		GameManager.Instance.GameSetupListeners += OnGameSetup;
		GameManager.Instance.GameEndListeners += OnGameEnd;
	}

	void OnGameSetup()
	{
		//Band.clip = BeginGame;
		Crowd.Stop();
	}

	void OnGameEnd(int[] scores)
	{
		if (scores[0] == scores[1])
		{
			Band.clip = BandFail;
			Crowd.clip = CrowdFail;
		}
		else
		{
			Band.clip = BandWin;
			Crowd.clip = CrowdWin;
		}

		//Band.Play();
		Crowd.Play();
	}
}
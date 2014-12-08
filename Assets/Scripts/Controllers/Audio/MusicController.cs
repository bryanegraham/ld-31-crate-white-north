using UnityEngine;

class MusicController : MonoBehaviour
{
	public AudioClip[] InGameMusic = null;
	public AudioClip[] MenuMusic = null;

	public AudioClip SabTrombone = null;

	public AudioSource Source;

	void Start()
	{
		GameManager.Instance.GameSetupListeners += OnGameSetup;
		GameManager.Instance.GameEndListeners += OnGameEnd;

		OnGameEnd(null);
			// Start Menu Music
	}

	void OnGameSetup()
	{
		Source.Stop();
		Source.clip = RandomTrack(InGameMusic);
		Source.Play();
	}

	void OnGameEnd(int[] scores)
	{
		Source.Stop();
		
		if (null == scores || scores[0] != scores[1])
		{
			Source.clip = RandomTrack(MenuMusic);
			Source.loop = true;
		}
		else
		{
			Source.clip = SabTrombone;
			Source.loop = false;
		}
		Source.Play();
	}

	AudioClip RandomTrack(AudioClip[] list)
	{
		int i = Random.Range(0, list.Length);
		return list[i];
	}
}
using UnityEngine;

class ParticleLifecycleController : MonoBehaviour
{
	public ParticleSystem[] Systems;

	void Start()
	{
		Systems = gameObject.GetComponentsInChildren<ParticleSystem>(true);
	}

	void Update()
	{
		bool needsDestroy = true;
		foreach (ParticleSystem ps in Systems)
		{
			if (ps.isPlaying)
			{
				needsDestroy = false;
				break;
			}
		}

		if (needsDestroy)
		{
			Destroy(gameObject);
		}
	}
}
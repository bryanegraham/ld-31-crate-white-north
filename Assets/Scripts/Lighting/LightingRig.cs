using UnityEngine;

class LightingRig : MonoBehaviour
{
	public GameObject[] Lights;

	public void ActivateRig()
	{
		foreach (GameObject go in Lights)
		{
			go.SetActive(true);
		}
	}

	public void DeActivateRig()
	{
		foreach (GameObject go in Lights)
		{
			go.SetActive(false);
		}
	}
}
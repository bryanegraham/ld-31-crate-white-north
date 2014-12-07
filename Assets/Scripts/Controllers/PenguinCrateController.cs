using UnityEngine;
using System.Collections;

public class PenguinCrateController : DestructingCrateController 
{
	public GameObject PenguinPrefab;

	protected override void OnDestruct()
	{
		if (null != DestructFXPrefab)
		{
			GameObject.Instantiate(DestructFXPrefab, transform.position, transform.rotation);
		}

		if (null != PenguinPrefab)
		{
			GameObject.Instantiate(PenguinPrefab, transform.position, transform.rotation);	
		}
		gameObject.SetActive(false);
	}
}

using UnityEngine;
using System.Collections;

public class DestructingCrateController : BasicCrateController 
{
	public GameObject DestructFXPrefab;

	protected override void OnCollideWhileUnOwned(Collision collision, PlayerController player)
	{
		Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag == "Crate")
		{
			OnDestruct();
		}
	}

	protected override void OnDestruct()
	{
		if (null != DestructFXPrefab)
		{
			GameObject.Instantiate(DestructFXPrefab, transform.position, transform.rotation);
		}
		gameObject.SetActive(false);
	}
}

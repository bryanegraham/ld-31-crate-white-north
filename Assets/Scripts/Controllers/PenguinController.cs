using UnityEngine;
using System.Collections;

public class PenguinController : MonoBehaviour
{
	public float maxSpeed = 30f;
	public float dampRate = 3f;

	void Start()
	{
		GameManager.Instance.GameSetupListeners += OnGameSetup;
	}

	void OnGameSetup()
	{
		GameManager.Instance.GameSetupListeners -= OnGameSetup;
		Destroy(gameObject);
	}

	void Update()
	{
		if (rigidbody.velocity.sqrMagnitude < (maxSpeed * maxSpeed))
		{
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, transform.forward * maxSpeed, dampRate * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		float x = Random.Range(-1f,1f);
		float z = Random.Range(-1f,1f);
		
		transform.forward = new Vector3(x, 0f, z).normalized;
	}
}

using UnityEngine;
using System.Collections;

public class DampingController : MonoBehaviour 
{
	public float dampRate = 1f;	
	public Rigidbody RigidBody;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (null != RigidBody)
		{
			RigidBody.velocity = Vector3.Lerp(RigidBody.velocity, Vector3.zero, dampRate * Time.deltaTime);
		}
	}
}

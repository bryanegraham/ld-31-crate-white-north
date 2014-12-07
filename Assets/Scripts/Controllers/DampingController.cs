using UnityEngine;
using System.Collections;

public class DampingController : MonoBehaviour 
{
	public float dampRate = 1f;	
	public Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (null != rb)
		{
			rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, dampRate * Time.deltaTime);
		}
	}
}

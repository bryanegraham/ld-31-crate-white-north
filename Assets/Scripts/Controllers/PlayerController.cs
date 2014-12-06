using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float impulseMagnitude = 1f;
	public float maxVelocity = 100f;

	public Rigidbody RigidBody;

	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Left;
	public KeyCode Right;
	public KeyCode Release;

	private float maxVelocitySquared;
	private BasicCrateController crate;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		maxVelocitySquared = maxVelocity * maxVelocity;

		Vector3 impulse = Vector3.zero;

		// Locomotion
		if (Input.GetKey(Up))
			impulse.z = 1;
		else if (Input.GetKey(Down))
			impulse.z = -1;

		if (Input.GetKey(Left))
			impulse.x = -1;
		else if (Input.GetKey(Right))
			impulse.x = 1;

		impulse.Normalize();

		if (null != crate)
		{
			impulse *= impulseMagnitude - crate.GetDrag();
		}
		else
		{
			impulse *= impulseMagnitude;
		}

		RigidBody.velocity += impulse;

		if (RigidBody.velocity.sqrMagnitude > maxVelocitySquared)
		{
			Vector3 clampedVelocity = RigidBody.velocity.normalized * maxVelocity;
			RigidBody.velocity = clampedVelocity;
		}

		// Crate control
		if (Input.GetKey(Release) && null != crate)
		{
			crate.Owner = null;
			crate = null;
		}

		if (crate != null && crate.Owner != this)
		{
			crate = null;
		}

		// Align self
		Vector3 facing = RigidBody.velocity.normalized;

		if (facing.sqrMagnitude > 0.1f)
		{
			transform.forward = facing;
		}
	}

	public bool BeginTow(BasicCrateController crate_)
	{
		if (null == crate)
		{
			crate = crate_;
			return true;
		}
		else
		{
			return false;
		}
	}
}

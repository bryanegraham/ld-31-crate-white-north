using UnityEngine;
using System.Collections;

public class BasicCrateController : MonoBehaviour 
{
	public static float HeaviestCrate = 100f;
	
	public Rigidbody RigidBody;
	public PlayerController Owner;
	public LineRenderer line;

	public float followDampRate = 2f;
	public float followEslasticity = 100f;
	public float maxDistance = 1.5f;

	// Use this for initialization
	void Start () 
	{
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (null != Owner)
		{
			if (Vector3.Distance(transform.position, Owner.transform.position) <= maxDistance)
			{
				RigidBody.velocity = Vector3.Lerp(RigidBody.velocity, Owner.RigidBody.velocity, followDampRate * Time.deltaTime);
			}
			else
			{
				Vector3 delta = (Owner.transform.position + (Owner.transform.forward * -1) - transform.position).normalized;
				RigidBody.velocity = Vector3.Lerp(RigidBody.velocity, delta * followEslasticity, followDampRate);
			}

			line.enabled = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, Owner.transform.position);
		}
		else
		{
			line.enabled = false;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		GameObject subject = collision.gameObject;
		PlayerController other = null;

		if (subject.tag == "Player")
		{
			other = subject.GetComponent<PlayerController>();
		}

		Debug.Log("[!] Collide with " + subject.name);
	
		if (null == Owner)
		{
			OnCollideWhileUnOwned(collision, other);
		}
		else if (other != Owner)
		{
			OnCollideWhileOwned(collision, other);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (null != other.transform.parent)
		{
			GameObject subject = other.transform.parent.gameObject;
			if (subject.tag == "Player")
			{
				PlayerController player = subject.GetComponent<PlayerController>();
				if (null != player && null == Owner)
				{
					OnClaimedByPlayer(player);
				}
			}
		}
	}

	public float GetDrag()
	{
		return RigidBody.mass / HeaviestCrate;
	}

	protected virtual void OnCollideWhileOwned(Collision collision, PlayerController otherPlayer)
	{
		Owner = null;
	}

	protected virtual void OnCollideWhileUnOwned(Collision collision, PlayerController player)
	{
		// empty
	}

	protected virtual void OnClaimedByPlayer(PlayerController controller)
	{
		if (controller.BeginTow(this))
		{
			Owner = controller;
			RigidBody.angularVelocity = Vector3.zero;
		}
	}

	protected virtual void OnDestruct()
	{

	}
}

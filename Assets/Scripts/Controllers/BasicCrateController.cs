using UnityEngine;
using System.Collections;

public class BasicCrateController : MonoBehaviour 
{
	public static float HeaviestCrate = 100f;
	
	public Rigidbody rb;
	public PlayerController Owner;
	public LineRenderer line;

	public AudioEventController AudioEvents;

	public int pointsValue = 1;

	public float followDampRate = 2f;
	public float followEslasticity = 100f;
	public float maxDistance = 4.0f;
	public float followDistance = 1.5f;
	public float maxAttachVelocity = 10f;

	// Use this for initialization
	void Start () 
	{
		line = GetComponent<LineRenderer>();
		line.enabled = false;

		if (null == AudioEvents)
		{
			AudioEvents = gameObject.GetComponent<AudioEventController>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (null != Owner)
		{
			float distance = Vector3.Distance(transform.position, Owner.transform.position);
			if (distance >= followDistance)
			{
				Vector3 delta = (Owner.transform.position + (Owner.transform.forward * (-followDistance)) - transform.position).normalized;
				rb.velocity = Vector3.Lerp(rb.velocity, delta * followEslasticity, followDampRate);
			}
			//else if (distance >= followDistance)
			//{
				//rb.velocity = Vector3.Lerp(rb.velocity, Owner.rb.velocity, followDampRate * Time.deltaTime);
			//}

			line.SetPosition(0, transform.position);
			line.SetPosition(1, Owner.transform.position);
			line.enabled = true;
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
		else if (subject.tag == "Terrain")
		{
			return;
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

		AudioEvents.Event("Crash");
	}

	void OnTriggerEnter(Collider other)
	{
		SledInVolume(other);
	}

	void OnTriggerStay(Collider other)
	{
		SledInVolume(other);
	}

	private void SledInVolume(Collider other)
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
		return rb.mass / HeaviestCrate;
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
		if (rb.velocity.magnitude <= maxAttachVelocity)
		{
			if (controller.BeginTow(this))
			{
				Owner = controller;
				rb.angularVelocity = Vector3.zero;
			}
		}
	}

	protected virtual void OnDestruct()
	{

	}
}

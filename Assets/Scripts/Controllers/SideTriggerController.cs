using UnityEngine;

class SideTriggerController : MonoBehaviour
{
	public GameManager.Side side = GameManager.Side.LEFT;
	
	void OnTriggerEnter(Collider coll)
	{
		TriggerAction(coll, true);
	}

	void OnTriggerExit(Collider coll)
	{
		TriggerAction(coll, false);
	}

	private void TriggerAction(Collider coll, bool enter)
	{
		if (coll.isTrigger && null != coll.rigidbody)
		{
			GameObject go = coll.rigidbody.gameObject;
			BasicCrateController crate = go.GetComponent<BasicCrateController>();

			if (null != go)
			{
				GameManager.Instance.ModifyPoints(side, enter ? crate.pointsValue : -crate.pointsValue);
			}
		}
	}
}
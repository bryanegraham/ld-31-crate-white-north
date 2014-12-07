using UnityEngine;
using System.Collections.Generic;

class SideTriggerController : MonoBehaviour
{
	public GameManager.Side side = GameManager.Side.LEFT;
	
	public List<BasicCrateController> Crates = new List<BasicCrateController>();

	void Update()
	{
		int score = 0;
		int idx = 0;
		while (idx < Crates.Count)
		{
			if (!Crates[idx].gameObject.activeSelf)
			{
				Crates.RemoveAt(idx);
			}
			else
			{
				score += Crates[idx].pointsValue;
				idx++;
			}
		}

		GameManager.Instance.Score[(int)side] = score;
	}

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

			if (null != crate)
			{
				if (enter)
				{
					Crates.Add(crate);
				}
				else
				{
					Crates.Remove(crate);
				}
			}
		}
	}
}
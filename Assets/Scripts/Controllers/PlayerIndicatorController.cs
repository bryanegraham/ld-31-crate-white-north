using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerIndicatorController : MonoBehaviour
{
	public GameObject Target;
	public float HoverHeight = 6f;
	public Image image;

	void Start()
	{
		image = gameObject.GetComponent<Image>();
	}

	void Update()
	{
		transform.position = Target.transform.position + (Vector3.up * HoverHeight);

		image.enabled = GameManager.Instance.GameRunning;
	}
}

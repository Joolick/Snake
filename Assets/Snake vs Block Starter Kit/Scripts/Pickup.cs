using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{

	public Text amountText;
	public GameObject bodyPrefab;

	public AudioClip pickupSound;

	private int amount;

	private void OnEnable()
	{
		amount = Random.Range(1, 6);
		amountText.text = amount.ToString();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{

			AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);

			for (int i = 0; i < amount; i++)
			{
				int index = other.transform.childCount;
				GameObject newBody = Instantiate(bodyPrefab, other.transform);
				newBody.transform.localPosition = new Vector3(0, -index, 0);

				FollowTarget followTarget = newBody.GetComponent<FollowTarget>();
				if (followTarget != null)
				{
					followTarget.target = other.transform.GetChild(index - 1);
				}
			}

			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				player.SetText(player.transform.childCount);
			}


		}

		gameObject.SetActive(false);
	}
}
using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour 
{
	private Vector3 _movementVector;
	private int sign = 1;
	void Start()
	{
		if(transform.position.y > 0.5f)
			sign = -1;
	}

	void OnTriggerEnter2D(Collider2D targetObject)
	{
		if(targetObject.CompareTag("Bird"))
		{
			GameController.Instance.RaiseOnRocketHitBird(targetObject.gameObject);
			Invoke("KillMe", 0.1f);
		}
	}

	void Update()
	{
		_movementVector = this.transform.position;
		_movementVector.y += 2 * Time.deltaTime * sign;
		this.transform.position = _movementVector;
	}

	void KillMe()
	{
		Destroy(gameObject);
	}

}

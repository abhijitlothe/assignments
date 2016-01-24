using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour 
{
	private Vector3 _movementVector;
	private int _sign = 1;
	private Vector2 _size;
	private float _offScreenY;


	public float Speed { get; set; }
	public Vector2 Size 
	{
		get 
		{
			return _size;
		}

		set 
		{
			_size = value;
		}
	}

	void Start()
	{
		_size = GetComponent<SpriteRenderer>().bounds.size;
		_offScreenY = GameController.Instance.ViewportHeight/2 + Size.y/2;
		if(transform.position.y > 0.5f)
		{
			_sign = -1;
			transform.Rotate(0, 0, -180);
		}
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
		_movementVector.y += Speed * Time.deltaTime * _sign;
		this.transform.position = _movementVector;

		if(Mathf.Abs(transform.position.y) > _offScreenY)
		{
			KillMe();
		}
	}

	void KillMe()
	{
		Destroy(gameObject);
	}

}

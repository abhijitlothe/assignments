using UnityEngine;
using System.Collections;

public class TurretMover : MonoBehaviour 
{
	private Vector3 _turretPosition;
	private float _speed;

	// Use this for initialization
	void Start () 
	{
		_speed = 2.0f;
		GameController.Instance.OnBirdDied += StopMoving;
	}

	void StopMoving()
	{
		enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_turretPosition = this.transform.position;
		_turretPosition.x -= _speed  * Time.deltaTime;
		transform.position = _turretPosition;
	}

	void OnDestroy()
	{
		if(GameController.Instance != null)
		{
			GameController.Instance.OnBirdDied -= StopMoving;
		}
	}
}

using UnityEngine;
using System.Collections;

public class ObstacleMover : MonoBehaviour 
{
	Vector3 obstaclePos;

	public float speed = 2.0f;

	// Use this for initialization
	void Start () 
	{
		GameController.Instance.OnBirdDied += StopObstacleMove;
	}

	void StopObstacleMove()
	{
		enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		obstaclePos = this.transform.position;
		obstaclePos.x -= speed * Time.deltaTime;
		transform.position = obstaclePos;
	}

	void OnDestroy()
	{
		if(GameController.Instance != null)
		{
			GameController.Instance.OnBirdDied -= StopObstacleMove;
		}
	}
}

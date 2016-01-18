using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour 
{
	public GameObject ObstaclePrefab;

	enum State
	{
		Idle,
		Spawn,
		Wait
	}

	State _currentState;
	List<GameObject> _obstacles;
	private float _camWidth;
	private float _camHeight;
	private float _spawnTime;

	// Use this for initialization
	void Start () 
	{
		_currentState = State.Idle;
		GameController.Instance.OnBirdDied += StopSpawn;
		_camHeight = 2 * Camera.main.orthographicSize;
		_camWidth = Camera.main.aspect * _camHeight;
	}

	public void StartGame()
	{
		DestroyAllObstacles();
		_obstacles = new List<GameObject>();
		_currentState = State.Spawn;
	}

	void DestroyAllObstacles()
	{
		if(_obstacles != null)
		{
			foreach(GameObject obstacle in _obstacles)
			{
				if(obstacle.gameObject != null)
					Destroy(obstacle);
			}
		}
	}

	void OnDestroy()
	{
		StopSpawn();

		if(GameController.Instance != null)
			GameController.Instance.OnBirdDied -= StopSpawn;		
	}

	void StopSpawn ()
	{
		_currentState = State.Idle;
		DestroyAllObstacles();
	}

	// Update is called once per frame
	void Update () 
	{
		switch(_currentState)
		{
		case State.Spawn:
			TrySpawnNewObstacle();
			_currentState = State.Wait;
			_spawnTime = Time.time;
			break;
		case State.Wait:
			if(Time.time - _spawnTime > 2.0f)
			{
				_currentState = State.Spawn;
			}
			break;
		}
	}


	void TrySpawnNewObstacle()
	{
		int variationIndex = UnityEngine.Random.Range(0, 2);
		Obstacle newObstacle = Instantiate<GameObject>(ObstaclePrefab).GetComponent<Obstacle>();
		newObstacle.BirdController = GameController.Instance.BirdControllerInstance;

		_obstacles.Add(newObstacle.gameObject);

		switch(variationIndex)
		{
		case 0:
			newObstacle.gameObject.transform.position = new Vector3(_camWidth, 0, newObstacle.gameObject.transform.position.z);
			break;
		
		case 1:
			newObstacle.gameObject.transform.Rotate(0, 0, 180);
			newObstacle.gameObject.transform.position = new Vector3(_camWidth/2, _camHeight/2 - newObstacle.Size.y, newObstacle.gameObject.transform.position.z);

			break;
		}
	}
}

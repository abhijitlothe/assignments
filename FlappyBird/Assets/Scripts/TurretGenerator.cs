using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretGenerator : MonoBehaviour 
{
	public GameObject TurretPrefab;

	enum State
	{
		Idle,
		Spawn,
		Wait
	}

	State _currentState;
	List<GameObject> _turrets;
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
		DestroyAllTurrets();
		_turrets = new List<GameObject>();
		_currentState = State.Spawn;
	}

	void DestroyAllTurrets()
	{
		if(_turrets != null)
		{
			foreach(GameObject turret in _turrets)
			{
				if(turret.gameObject != null)
					Destroy(turret);
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
		DestroyAllTurrets();
	}

	// Update is called once per frame
	void Update () 
	{
		switch(_currentState)
		{
		case State.Spawn:
			StartCoroutine(TrySpawnNewTurret());
			_currentState = State.Wait;
			_spawnTime = Time.time;
			break;
		case State.Wait:
			if(Time.time - _spawnTime > Random.Range(2.0f, 3.5f))
			{
				_currentState = State.Spawn;
			}
			break;
		}
	}


	IEnumerator TrySpawnNewTurret()
	{
		int variationIndex = UnityEngine.Random.Range(0, 2);
		Turret newTurret = null;
		switch(variationIndex)
		{
		case 0: //standing up
			{
				newTurret =  CreateTurret(true);

				//yield return null;

 				newTurret.gameObject.SetActive(true);
				float y = Random.Range(-_camHeight/2,  -_camHeight/3 );
				newTurret.gameObject.transform.position = new Vector3(_camWidth, Mathf.Max(-_camHeight/2, y - newTurret.TotalSize.y/2), newTurret.gameObject.transform.position.z);
			}
			break;
		
		case 1: //upside down
			{
				newTurret = CreateTurret(false);
				//yield return null;
				newTurret.gameObject.SetActive(true);
				float y = Random.Range(_camHeight/3 , _camHeight/2);
				newTurret.gameObject.transform.position = new Vector3(_camWidth, Mathf.Min(_camHeight/2, y + newTurret.TotalSize.y/2) , newTurret.gameObject.transform.position.z);
			}
			break;

		
		}
		yield break;
	}

	private Turret CreateTurret(bool upFacing)
	{
		Turret newTurret = Instantiate<GameObject>(TurretPrefab).GetComponent<Turret>();
		newTurret.BirdController = GameController.Instance.BirdControllerInstance;
		_turrets.Add(newTurret.gameObject);

		if(!upFacing)
		{
			newTurret.gameObject.transform.Rotate(0, 0, 180);
		}

		newTurret.gameObject.SetActive(false);
		return newTurret;
	}
}

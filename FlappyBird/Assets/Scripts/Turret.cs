using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour 
{
	#region inspector variables
	public GameObject Pillar;
	public GameObject Cannon;
	public DangerZone DangerZone;
	public GameObject Projectile;
	public int NumProjectiles = 1; //TODO: hook these up
	public int ProjectileLaunchIntervalInSeconds = 0; //TODO: hook these up

	#endregion

	private float _camWidth;
	private bool _birdCrossed;

	private GameObject _projectileInstance;
	private Vector2 _totalSize;

	public Vector2 PillarSize
	{
		get
		{
			Vector2 size =  Pillar.GetComponent<SpriteRenderer>().bounds.size;

			return size;
		}
	}

	public Vector2 TotalSize
	{
		get
		{
			_totalSize =  Pillar.GetComponent<SpriteRenderer>().bounds.size;
			_totalSize.y += Cannon.GetComponent<SpriteRenderer>().bounds.size.y;
			return _totalSize;
		}
	}


	public BirdController BirdController
	{
		get; set;
	}

	void Start()
	{
		_birdCrossed = false;
		_camWidth = GameController.Instance.ViewportWidth;
		_totalSize =  Pillar.GetComponent<SpriteRenderer>().bounds.size;
		_totalSize.y += Cannon.GetComponent<SpriteRenderer>().bounds.size.y;
		DangerZone.GetComponent<DangerZone>().OnTriggerEnter += OnBirdEnterDangerZone;
	}

	void Update()
	{
		if(AmIOffScreen())
		{
			Destroy(gameObject);
		}
		else if (!_birdCrossed)
		{
			if(BirdController != null)
			{
				if(BirdController.transform.position.x - Pillar.transform.position.x >= PillarSize.x/2)
				{
					_birdCrossed = true;
 					GameController.Instance.RaiseBirdSurvivedObstacle();
				}
			}
		}
	}


	bool AmIOffScreen()
	{
		if((Pillar.transform.position.x + PillarSize.x/2) < -_camWidth/2)
		{
			return true;
		}

		return false;
	}

	void OnBirdEnterDangerZone(Collider2D bird)
	{
		if(bird.CompareTag("Bird"))
		{
			StartCoroutine(ShootProjectiles());
		}
	}

	private IEnumerator ShootProjectiles()
	{
		if(_projectileInstance != null)
			yield break;
		int sign = transform.position.y > 0.5f ? -1 : 1;
		float cannonHeight = Cannon.GetComponent<SpriteRenderer>().bounds.size.y;
		_projectileInstance = Instantiate(Projectile) as GameObject;
		_projectileInstance.GetComponent<RocketController>().Speed = Random.Range(2.0f, 3.4f);
		_projectileInstance.transform.parent = this.transform;
		_projectileInstance.transform.position = new Vector3(Cannon.transform.position.x, Cannon.transform.position.y + sign * cannonHeight/2.0f, Cannon.transform.position.z);

		yield break;
	}
}

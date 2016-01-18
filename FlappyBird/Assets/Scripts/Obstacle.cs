using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour 
{

	public GameObject Pillar;
	public GameObject Cannon;
	private float _camWidth;
	private bool _birdCrossed;

	public Vector2 Size
	{
		get
		{
			Vector2 size =  Pillar.GetComponent<SpriteRenderer>().bounds.size;

			return size;
		}
	}

	public BirdController BirdController
	{
		get; set;
	}

	void Start()
	{
		_camWidth = Camera.main.aspect * 2 * Camera.main.orthographicSize;
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
				if(BirdController.transform.position.x - Pillar.transform.position.x >= Size.x/2)
				{
					_birdCrossed = true;
 					GameController.Instance.RaiseBirdSurvivedObstacle();
				}
			}
		}
	}



	bool AmIOffScreen()
	{
		if((Pillar.transform.position.x + Size.x/2) < -_camWidth/2)
		{
			return true;
		}

		return false;
	}
}

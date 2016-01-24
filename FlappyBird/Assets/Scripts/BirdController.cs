using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour 
{
	public Rigidbody2D Bird;

	public Vector2 Size
	{
		get
		{
			Vector2 size =  Bird.gameObject.GetComponent<SpriteRenderer>().bounds.size;

			return size;
		}
	}

	void Start()
	{
		GameController.Instance.OnRocketHitBird += (bird)=> bird.gameObject.SetActive(false);
	}

	public void Init()
	{
		this.GetComponent<Rigidbody2D>().isKinematic = true;	
	}

	public void StartGame()
	{
		this.GetComponent<Rigidbody2D>().isKinematic = false;
		gameObject.SetActive(true);
		Vector3 center = Camera.main.transform.position;
		center.z = this.transform.position.z;
		this.transform.position = center;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Bird.velocity = Vector2.zero;
			Bird.AddForce(new Vector2(0, 300));
		}

		if(AmIBelowTheScreen())
		{
			GameController.Instance.RaiseOnBirdFellOff(this.gameObject);
			gameObject.SetActive(false);

		}
			
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Pillar") || collider.CompareTag("Cannon"))
		{
			gameObject.SetActive(false);
			GameController.Instance.RaiseBirdDied();
		}
	}


	bool AmIBelowTheScreen()
	{
		if((Bird.gameObject.transform.position.y + Size.y/2) < -GameController.Instance.ViewportHeight/2)
		{
			return true;
		}

		return false;
	}
}

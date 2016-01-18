using UnityEngine;
using System.Collections;

public class EffectsController : MonoBehaviour 
{
	public ExplodeBirdEffect ExplodeBirdEffect;

	// Use this for initialization
	void Start () 
	{
		GameController.Instance.OnRocketHitBird += HandleRocketHitBird;
	}

	void HandleRocketHitBird (GameObject bird)
	{
		ExplodeBirdEffect.Apply(bird);
	}
	
	// Update is called once per frame
	void OnDestroy () 
	{
		if(GameController.Instance != null)
		{
			GameController.Instance.OnRocketHitBird -= HandleRocketHitBird;
		}
	}
}

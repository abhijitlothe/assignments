using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	private static GameController _instance;

	public static GameController Instance
	{
		get 
		{
			return _instance;
		}
		private set 
		{
			if(_instance == null)
				_instance = value;
			else
				Debug.LogError("Instance already instantiated");
		}
	}


	public System.Action OnBirdDied;
	public System.Action OnBirdSurvivedObstacle;
	public System.Action<GameObject> OnRocketHitBird;
	public System.Action<GameObject> OnBirdFellOff;	

	public BirdController BirdControllerInstance;
	public ObstacleGenerator ObstacleGenerator;
	public GameUI GameHUD;

	void Awake()
	{
		Instance = this;
	}

	public void RaiseBirdDied() 
	{ 
		if(OnBirdDied != null) 
			OnBirdDied();
	}

	public void RaiseBirdSurvivedObstacle()
	{
		if(OnBirdSurvivedObstacle != null)
			OnBirdSurvivedObstacle();
	}

	public void RaiseOnRocketHitBird(GameObject bird)
	{
		if(OnRocketHitBird != null)
		{
			OnRocketHitBird(bird);
		}
		RaiseBirdDied();
	}

	public void RaiseOnBirdFellOff(GameObject bird)
	{
		if(OnBirdFellOff != null)
			OnBirdFellOff(bird);

		RaiseBirdDied();
	}

	public void OnStartGame()
	{
		if(BirdControllerInstance != null)
		{
			BirdControllerInstance.StartGame();
		}

		if(ObstacleGenerator != null)
		{
			ObstacleGenerator.StartGame();
		}

		if(GameHUD != null)
		{
			GameHUD.StartGame();
		}

	}
}

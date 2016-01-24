using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	private static GameController _instance;
	private int _currentScore = 0;
	private int _bestScore = 0;

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

	public float ViewportWidth {get; set;}
	public float ViewportHeight {get; set;}

	public System.Action OnBirdDied;
	public System.Action OnBirdSurvivedObstacle;
	public System.Action<GameObject> OnRocketHitBird;
	public System.Action<GameObject> OnBirdFellOff;	

	public BirdController BirdControllerInstance;
	public TurretGenerator TurretGenerator;
	public GameUI GameHUD;

	void Awake()
	{
		Instance = this;
		_bestScore = PlayerPrefs.GetInt("bestScore", -1);
	}

	void Start()
	{
		ViewportHeight = 2 * Camera.main.orthographicSize;
		ViewportWidth = Camera.main.aspect * ViewportHeight;
	}

	public void RaiseBirdDied() 
	{ 
		if(OnBirdDied != null) 
			OnBirdDied();
		EndGame();
	}

	public void RaiseBirdSurvivedObstacle()
	{
		if(OnBirdSurvivedObstacle != null)
			OnBirdSurvivedObstacle();
		++_currentScore;
		if(GameHUD != null)
			GameHUD.UpdateScoreText(_currentScore);
	}

	public void RaiseOnRocketHitBird(GameObject bird)
	{
		if(OnRocketHitBird != null)
		{
			OnRocketHitBird(bird);
		}
		RaiseBirdDied();
		EndGame();
	}

	public void RaiseOnBirdFellOff(GameObject bird)
	{
		if(OnBirdFellOff != null)
			OnBirdFellOff(bird);

		RaiseBirdDied();
		EndGame();
	}

	public void OnStartGame()
	{
		_currentScore = 0;

		if(BirdControllerInstance != null)
			BirdControllerInstance.StartGame();

		if(TurretGenerator != null)
			TurretGenerator.StartGame();

		if(GameHUD != null)
			GameHUD.StartGame(_currentScore, _bestScore);
	}

	private void EndGame()
	{
		SetBestScore(_currentScore);
	}	

	private void SetBestScore(int score)
	{
		if(_bestScore < score)
		{
			_bestScore = score;
			PlayerPrefs.SetInt("bestScore", _bestScore);
			if(GameHUD != null)
				GameHUD.SetBestScore(_bestScore);
		}
	}
}

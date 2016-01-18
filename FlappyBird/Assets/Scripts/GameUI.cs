using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
	public Button StartButton;
	public Text   ScoreText;

	private int _score = 0;

	// Use this for initialization
	void Start () 
	{
		GameController.Instance.OnBirdDied += OnBirdDied;
		GameController.Instance.OnBirdSurvivedObstacle += OnBirdSurvivedObstacle;
		Debug.Assert(StartButton != null);
		Debug.Assert(ScoreText != null);
		StartButton.gameObject.SetActive(true);
		ScoreText.transform.parent.gameObject.SetActive(false);
	}

	void OnDestroy()
	{
		if(GameController.Instance != null)
		{
			GameController.Instance.OnBirdDied -= OnBirdDied;
			GameController.Instance.OnBirdSurvivedObstacle -= OnBirdSurvivedObstacle;
		}
	}

	public void OnBirdDied()
	{
		if(StartButton != null)
		{
			StartButton.GetComponentInChildren<Text>().text = "Start Again";
			StartButton.gameObject.SetActive(true);
		}
	}

	public void OnBirdSurvivedObstacle()
	{
		++_score;
		UpdateScoreText();
	}

	public void StartGame()
	{
		_score = 0;
		ScoreText.transform.parent.gameObject.SetActive(true);
		UpdateScoreText();
		StartButton.gameObject.SetActive(false);
	}

	public void OnStartButtonClicked()
	{
		GameController.Instance.OnStartGame();
	}

	private void UpdateScoreText()
	{
		ScoreText.text = _score.ToString("N0");
	}

}

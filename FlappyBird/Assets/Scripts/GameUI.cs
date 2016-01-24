using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
	#region inspector variables
	public Button StartButton;
	public Text   ScoreText;
	public Text	  BestScoreText;
	#endregion

	// Use this for initialization
	void Start () 
	{
		GameController.Instance.OnBirdDied += OnBirdDied;
		Debug.Assert(StartButton != null);
		Debug.Assert(ScoreText != null);
		StartButton.gameObject.SetActive(true);
		ScoreText.transform.parent.gameObject.SetActive(false);
		BestScoreText.transform.parent.gameObject.SetActive(false);
	}

	void OnDestroy()
	{
		if(GameController.Instance != null)
		{
			GameController.Instance.OnBirdDied -= OnBirdDied;
		}
	}

	public void OnBirdDied()
	{
		StartButton.GetComponentInChildren<Text>().text = "Start Again";
		StartButton.gameObject.SetActive(true);
	}


	public void StartGame(int score, int bestScore)
	{
		ScoreText.transform.parent.gameObject.SetActive(true);
		BestScoreText.transform.parent.gameObject.SetActive(true);
		SetBestScore(bestScore);
		UpdateScoreText(score);
		StartButton.gameObject.SetActive(false);
	}

	public void OnStartButtonClicked()
	{
		GameController.Instance.OnStartGame();
	}

	public void UpdateScoreText(int score)
	{
		ScoreText.text = score.ToString("N0");
	}

	public void SetBestScore(int score)
	{
		BestScoreText.text = score.ToString("N0");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	// quit game
	public void quitGame()
	{
		Application.Quit();
	}

	// play game again
	public void playAgain()
	{
		SceneManager.LoadScene("GameScene");
	}

	// start playing
	public void startPlay()
	{
		SceneManager.LoadScene("GameScene");
	}

	// back to start
	public void back()
	{
		SceneManager.LoadScene("PlayScene");
	}
}

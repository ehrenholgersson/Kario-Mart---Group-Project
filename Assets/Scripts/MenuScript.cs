using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	public GameObject pausePanel;
	public bool IsPaused;

	void Awake()
	{
		if (pausePanel != null)
		{
			pausePanel.SetActive(false);
		}
		Time.timeScale = 1f;
		IsPaused = false;
	}
	public void ChangeSceneByIndex(int sceneId)
	{
	SceneManager.LoadScene(sceneId);
	}

   public void Egress()
   {
	 Application.Quit();
		#if UNITY_EDITOR
	 UnityEditor.EditorApplication.isPlaying = false;
	    #endif
   }
   
   private void Update()
   {
		if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P))
		{
			if (IsPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
   }
   private void PauseGame()
   {
		pausePanel.SetActive (true);
		Time.timeScale = 0f;
		IsPaused = true;
   }
   public void ResumeGame()
   {
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
		IsPaused = false;
   }
}

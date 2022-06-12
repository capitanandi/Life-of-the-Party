using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = true;

    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        DebugLevel(); //DEBUG. REMOVE BEFORE COMPLETION.
        QuitGame();
        PauseGame();
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("GameManager::QuitGame() Called");
            Application.Quit();
        }
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                
                GameObject[] ObjectsToPause = GameObject.FindGameObjectsWithTag("Moving");

                foreach(GameObject obj in ObjectsToPause)
                {
                    obj.GetComponent<SpotlightMovement>().enabled = false;
                }
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;

                GameObject[] ObjectsToPause = GameObject.FindGameObjectsWithTag("Moving");

                foreach (GameObject obj in ObjectsToPause)
                {
                    obj.GetComponent<SpotlightMovement>().enabled = true;
                }
            }
        }
    }

    private void DebugLevel() //DEBUG
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }
  
}

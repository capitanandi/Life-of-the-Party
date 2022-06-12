using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject talkPrompt;

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DialogueManager dialogueManager;

    public bool playerIsDead;

    void Start()
    {
        playerIsDead = false;
        talkPrompt.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPaused == true)
        {
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            pauseScreen.gameObject.SetActive(false);
        }

        if(dialogueManager.talkPromptActive == true)
        {
            talkPrompt.gameObject.SetActive(true);
        }
        else
        {
            talkPrompt.gameObject.SetActive(false);
        }
    }
}

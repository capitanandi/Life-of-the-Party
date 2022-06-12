using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public float textSpeed = 0.5f;
 
    public bool canTalk;
    public bool talkPromptActive;

    public GameObject dialogueBox;

    public PlayerMovement playerMovement;
    public PlayerLook playerLook;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        dialogueBox.gameObject.SetActive(false);

        canTalk = true;
        talkPromptActive = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.gameObject.SetActive(true);
        playerMovement.canMove = false;
        playerLook.whileTalking = true;
        talkPromptActive = false;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (canTalk == true)
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        canTalk = false;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if(ClickedToSkip())
            {
                dialogueText.text = sentence;
                if (dialogueText.text.Length == sentence.Length)
                {
                    canTalk = true;
                }
                yield break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        if(dialogueText.text.Length == sentence.Length)
        {
            canTalk = true;
        }
        else
        {
            canTalk = false;
        }
    }

    public bool ClickedToSkip()
    {
        if(dialogueBox.gameObject.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        playerMovement.canMove = true;
        playerLook.whileTalking = false;
        Debug.Log("Conversation ended");
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class DialogueControl : MonoBehaviour
{

    public enum idiom
    {
        pt_br,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueWindow;
    public Image profileSprite;
    public Text actorNameText;
    public Text speechText;

    [Header("Settings")]
    public float typingSpeed;

    //Control variables
    private bool isShowing;
    public int index = 0;
    private string[] sentences;
    private string[] actorNames;

    public static DialogueControl instance;

    // Chamado antes de todos os Starts()
    private void Awake()
    {
        instance = this;
    }
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            if (speechText.text.ToCharArray().Length < sentences[index].ToCharArray().Length)
            {
                StopAllCoroutines();
                speechText.text = sentences[index];
            }
            else
            {
                index++;
                Speech(sentences.ToArray(), actorNames.ToArray());
            }
        }
        else 
        {
            if (speechText.text.ToCharArray().Length < sentences[index].ToCharArray().Length)
            {
                StopAllCoroutines();
                speechText.text = sentences[index];
            }
            else if (speechText.text.ToCharArray().Length == sentences[index].ToCharArray().Length)
            {
                ExitSpeech();
            }

        }
    }

    // Call speech
    public void Speech(string[] txt, string[] names)
    {
        PlayerController.instance.canControl = false;
        speechText.text = null;
        actorNameText.text = null;

        if (!isShowing)
        {
            dialogueWindow.SetActive(true);
            sentences = txt;
            actorNames = names;
            actorNameText.text = actorNames[index];
            StartCoroutine(TypeSentence());
            isShowing = true;

            Debug.Log(actorNames[index]);

        }
        else
        {
            actorNameText.text = actorNames[index];
            StartCoroutine(TypeSentence());
        }
    }

    public void ExitSpeech()
    {
        index = 0;
        sentences = null;
        dialogueWindow.SetActive(false);
        isShowing = false;
        NPCDialogue.instance.isTalking = false;
        PlayerController.instance.canControl = true;
    }


}

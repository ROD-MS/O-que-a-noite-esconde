using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask layerPlayer;

    public DialogueSettings dialogue;

    public bool isTalking = false;

    public bool canTalk = false;

    private List<string> sentences = new List<string>();
    private List<string> names = new List<string>();

    public static NPCDialogue instance;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        GetTexts();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canTalk && !isTalking)
        {
            DialogueControl.instance.Speech(sentences.ToArray(), names.ToArray());
            isTalking = true;
        }
        else if (Input.GetMouseButtonDown(0) && canTalk && isTalking)
        {
            DialogueControl.instance.NextSentence();
        }
    }

    void GetTexts()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueControl.instance.language)
            {
                case DialogueControl.idiom.pt_br:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;

                case DialogueControl.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;

                case DialogueControl.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }

            names.Add(dialogue.dialogues[i].actorName);
        }
    }
 
}

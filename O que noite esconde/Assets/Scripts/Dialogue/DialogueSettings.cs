using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]

public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string nameActor;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>();
}


[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;

}


#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target;

        Languages lang = new Languages();
        lang.portuguese = ds.sentence;

        Sentences sent = new Sentences();
        sent.profile = ds.speakerSprite;
        sent.actorName = ds.nameActor;
        sent.sentence = lang;


        if (GUILayout.Button("Create Dialogue"))
        {
            if (ds.sentence != "")
            {
                ds.dialogues.Add(sent);

                ds.speakerSprite = null;
                ds.nameActor = null;
                ds.sentence = null;
            }
        }
    }
}


#endif
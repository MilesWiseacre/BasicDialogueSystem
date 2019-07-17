using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Put on a gameobject to build a scene.
public class Scene : MonoBehaviour
{
    // Allows me to create an array of dialogue for scenes
    [System.Serializable]
    public class Dialogue
    {
        //Allows me to create a dropdown menu to specify the current speaking character
        public enum Actors
        {
            Null, Player_One, Second_Player, P3
        };
        public Actors speaker;
        // Dialogue
        [TextArea(2, 10)]
        public string lines;
    }
    // Array of Dialogue
    public Dialogue[] scene;
    // Reference to the Quest Manager
    QuestManager qManager;
    public int[] preReq;
    // Reference to the Dialogue Window
    DWindow talk;
    // Sets up some of the references so they can be used
    private void Start()
    {
        talk = FindObjectOfType<DWindow>().GetComponent<DWindow>();
        qManager = FindObjectOfType<QuestManager>().GetComponent<QuestManager>();
    }
    // Checks if the player has the necessary quests to be used
    bool CheckPreReq()
    {
        foreach(int i in preReq)
        {
            if (qManager.ReturnQuest(i).comp == true)
            {
                return true;
            }
        }
        return false;
    }
    // Starts the dialogue
    public void _StartDialogue()
    {
        // Moves forward only if there are no PreReq or if all PreReq are filled
        if (preReq.Length == 0 || CheckPreReq())
        {
            talk.currScene = this;
            talk._OpenWindow();
        }
    }
}

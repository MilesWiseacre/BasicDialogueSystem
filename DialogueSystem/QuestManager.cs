using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Put on the same object as DWindow so it can be used
public class QuestManager : MonoBehaviour
{
    // Allows me to prevent the player from doing certain things until they progress through the game
    [System.Serializable]
    public class Quest
    {
        // Allows me to know which Quest I am referring to
        public int id;
        public string desc;
        // Determines whether or not the Quest is complete
        public bool comp;
        // Allows me to declare a quest
        public Quest(int id, string desc, bool comp)
        {
            this.id = id;
            this.desc = desc;
            this.comp = comp;
        }
        // Allows me to call a quest
        public Quest(Quest quest)
        {
            this.id = quest.id;
            this.desc = quest.desc;
            this.comp = quest.comp;
        }
    }
    // Creates a list of quests
    public List<Quest> quests = new List<Quest>();
    // Function commented out builds a list of Quests in the script
    private void Start()
    {
        //BuildQuestDatabase();
    }
    // Specify the game Quests in here if you don't want to use the Inspector
    void BuildQuestDatabase()
    {
        quests = new List<Quest>()
        {
            new Quest (0, "Test quest one", false),
            new Quest (1, "Test quest two", false)
        };
    }
    // Reads a specific quest from the array
    public Quest ReturnQuest(int id)
    {
        return quests.Find(quest => quest.id == id);
    }
    // Completes a quest
    public void GetQuest(int id)
    {
        ReturnQuest(id).comp = true;
    }
    // Removes a quest
    public void TakeQuest(int id)
    {
        ReturnQuest(id).comp = false;
    }
}

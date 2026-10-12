using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private Npc[] npcs;
    //SaveData saveData;

    string saveLocation;

    //SaveData saveData = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcs = FindObjectsOfType<Npc>();
        saveLocation = Path.Combine(Application.persistentDataPath + Constants.SaveFile);

        LoadGame();
    }

    // Update is called once per frame
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            npcSaveData = GetNpcState()
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    private List<NpcSaveData> GetNpcState()
    {
        List<NpcSaveData> npcStates = new List<NpcSaveData>();

        foreach(Npc npc in npcs)
        {
            NpcSaveData npcSaveData = new NpcSaveData
            {
                NpcID = npc.ID,
                //hasInteracted = npc.HasInteracted
            };
            npcStates.Add(npcSaveData);
        }

        return npcStates;
    }

    private void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
        }
        else
        {
            SaveGame();
        }
        //LoadNpcStates();
    }

    private void LoadNpcStates(List<NpcSaveData> npcStates)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData
{
    public Vector3 playerPosition;
    //public string mapBoundry; // the boundary name for the map
    //public List<InventorySaveData> inventorySaveData

    public List<NpcSaveData> npcSaveData;

}

[System.Serializable]
public class NpcSaveData
{
    public string NpcID;
    public bool hasInteracted;
}
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Savedata 
{
  public Vector3 playerPosition;
  public List <IventorySaveData> iventorySaveData ;
  public List <ChestSaveData> chestSaveData;
  public List<IventorySaveData> hotbarSaveData;

}

[System.Serializable]
public class ChestSaveData
{
    public string chestID;
    public bool isOpened;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    public List<LevelItem> listLevelItem;
}
[Serializable]
public class LevelItem
{
    public string levelName;
    public Sprite levelImage;
}
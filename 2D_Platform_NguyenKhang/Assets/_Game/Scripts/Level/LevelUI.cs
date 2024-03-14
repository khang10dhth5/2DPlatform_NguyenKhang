using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Text txtLevelName;
    [SerializeField] private Image imgLevel;
    [SerializeField] private Button btnLevelUI;
    //[SerializeField] private LevelController levelController;
    public void SetLevelUI(LevelItem levelItem, Action<string> actionClick)
    {
        txtLevelName.text ="Level "+ levelItem.levelName;
        imgLevel.sprite = levelItem.levelImage;
        btnLevelUI.onClick.AddListener(() =>
        {
            actionClick.Invoke(levelItem.levelName);
        });
    }
}

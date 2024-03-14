using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject pnlControl;
    public GameObject pnlMenu;

    [SerializeField] private GameObject pnlPause;
    [SerializeField] private GameObject pnlMainMenu;
    

    [SerializeField] private LevelSO levelSO;
    [SerializeField] private Transform parentTranform;
    [SerializeField] private LevelUI levelPrefabs;

    [SerializeField] private Text txtCoin;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnJump;
    [SerializeField] private Button btnAttack;
    [SerializeField] private Button btnThrow;
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnLevel;
    [SerializeField] private Button btnBack;

    private LevelController currrentMap;

    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if(instance==null)
    //        {
    //            instance = FindObjectOfType<UIManager>();
    //        }
    //        return instance;
    //    }
    //}



    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OnInit();
    }
    public void SetCoin(float coin)
    {
        txtCoin.text ="Coin:" + coin.ToString();
    }

    public void SetButton(Player player)
    {
        AddEventByScripts(btnLeft.gameObject, EventTriggerType.PointerDown, player, -1);
        AddEventByScripts(btnLeft.gameObject, EventTriggerType.PointerUp, player, 0);
        AddEventByScripts(btnRight.gameObject, EventTriggerType.PointerDown, player, 1);
        AddEventByScripts(btnRight.gameObject, EventTriggerType.PointerUp, player, 0);
        btnJump.onClick.AddListener(() =>
        {
            player.Jump();
        });
        btnAttack.onClick.AddListener(() =>
        {
            player.Attack();
        });
        btnThrow.onClick.AddListener(() =>
        {
            player.Throw();
        });
    }

    private void AddEventByScripts(GameObject go, EventTriggerType eventType, Player player, float horizontal)
    {
        if(go.GetComponent<EventTrigger>()==null)
        {
            go.AddComponent<EventTrigger>();
        }
        EventTrigger trigger = go.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((f)=>
        {
            player.horizontal =horizontal;
        });
        trigger.triggers.Add(entry);
    }

    private void OnInit()
    {
        LoadLevel();
        pnlControl.SetActive(false);
        pnlPause.SetActive(false);
        pnlMenu.SetActive(false);
        pnlMainMenu.SetActive(true);

        btnPause.onClick.AddListener(() =>
        {
            PauseGame();
        });

        btnResume.onClick.AddListener(() =>
        {
            ResumeGame();
        });
        btnQuit.onClick.AddListener(() =>
        {
            QuitGame();
        });
        btnLevel.onClick.AddListener(() =>
        {
            PlayGame();
        });
        btnBack.onClick.AddListener(() =>
        {
            BackMainMenu();
        });

        int coin = PlayerPrefs.GetInt(KeyConstant.KEY_COIN);
        txtCoin.text = "Coin: " + coin;
    }

    private void BackMainMenu()
    {
        pnlMenu.SetActive(false);
        pnlMainMenu.SetActive(true);
    }
    private void QuitGame()
    {
        Time.timeScale = 1;
        Destroy(CameraFollow.instance.target.gameObject,0.2f);
        DestroyCurrentMap();
        OnInit();
    }
    private void PlayGame()
    {
        pnlMenu.SetActive(true);
        pnlMainMenu.SetActive(false);
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
        pnlPause.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pnlPause.SetActive(true);
    }

    private void ClearHorizontalGroup()
    {
        foreach (Transform child in parentTranform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private void LoadLevel()
    {
        ClearHorizontalGroup();
        for(int i=0;i<levelSO.listLevelItem.Count;i++)
        {
            LevelUI levelUI = Instantiate(levelPrefabs, parentTranform);
            levelUI.SetLevelUI(levelSO.listLevelItem[i], OnLevelItemUIClickHandle);
        }
    }

    private void OnLevelItemUIClickHandle(string index)
    {
        LevelController levelController = Resources.Load<LevelController>($"{PathConstant.MAP_PATH}{index}");
        currrentMap = Instantiate(levelController);
    }

    private void DestroyCurrentMap()
    {
        Destroy(currrentMap.gameObject);
    }
}

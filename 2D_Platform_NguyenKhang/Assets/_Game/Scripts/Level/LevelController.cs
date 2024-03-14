using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Player playerPrefabs;
    [SerializeField] private Transform startPoint;

    private void Awake()
    {
        Player player = Instantiate(playerPrefabs, startPoint.position, Quaternion.identity);
        CameraFollow.instance.target = player.transform;
        UIManager.instance.pnlControl.SetActive(true);
        UIManager.instance.pnlMenu.SetActive(false);
        UIManager.instance.SetButton(player);
    }

}

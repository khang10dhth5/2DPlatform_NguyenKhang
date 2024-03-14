using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCombat : MonoBehaviour
{
    [SerializeField] private Text txtHP;

    public void OnInit(float hp)
    {
        txtHP.text = hp + "";
        Invoke(nameof(OnDespawn), 1f);
    }
    private void OnDespawn()
    {
        Destroy(gameObject, 0.5f);
    }
}

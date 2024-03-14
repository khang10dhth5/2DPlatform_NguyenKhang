using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Image imgFill;
    float hp;
    float maxHp;

    // Update is called once per frame
    void Update()
    {
        imgFill.fillAmount = Mathf.Lerp(imgFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
    }
    public void OnInit(float maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        imgFill.fillAmount = 1;
    }
    public void SetHp(float hp)
    {
        this.hp = hp;
    }
}

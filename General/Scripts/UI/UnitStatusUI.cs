using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike;

public class UnitStatusUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpTxt;
    [SerializeField] private Image shieldImg;
    [SerializeField] private Text shieldTxt;
    float maxHp, hp, shield;

    public virtual void InitUI(Status maxStatus)
    {
        maxHp = maxStatus.hp;
        hpSlider.maxValue = maxHp;
    }

    public virtual void SetUI(Status status)
    {
        hp = status.hp;
        hpSlider.value = hp;
        hpTxt.text = hp + "/" + maxHp;

        shield = status.shield;
        shieldTxt.text = shield.ToString();
        shieldImg.gameObject.SetActive(shield > 0);
    }
}

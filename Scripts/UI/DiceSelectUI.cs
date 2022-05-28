using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike;
using TMPro;

public class DiceSelectUI : MonoBehaviour
{
    private static DiceSelectUI _inst;
    public static DiceSelectUI inst => _inst;
    
    [SerializeField] private DiceInfoUI prefab;
    [SerializeField] Button acceptBtn, weaponBtn;
    [SerializeField] TextMeshProUGUI costTxt;
    [SerializeField] Color selected;

    float width = Screen.width;
    DiceInfoUI[] diceUIs;
    bool[] diceUse;
    bool weaponUse;
    int maxCost;
    int cost;

    System.Action<bool[], bool> accept;

    private void Awake()
    {
        _inst = this;
        InitWeaponBtn();
        weaponBtn.gameObject.SetActive(false);
        acceptBtn.gameObject.SetActive(false);
        costTxt.gameObject.SetActive(false);
    }

    public void SetDiceUI(List<Dice> dices, Weapon weapon, int maxCost, System.Action<bool[], bool> accept)
    {
        this.accept = accept;
        this.maxCost = maxCost;

        float xInterval = width / (dices.Count + 3);
        diceUIs = new DiceInfoUI[dices.Count];
        diceUse = new bool[dices.Count];

        weaponBtn.gameObject.SetActive(true);
        acceptBtn.gameObject.SetActive(true);
        costTxt.gameObject.SetActive(true);
        SetCostText();

        var pos = weaponBtn.transform.localPosition;
        pos.x = xInterval;
        weaponBtn.transform.localPosition = pos;

        for (int i = 0; i < dices.Count; i++)
        {
            diceUIs[i] = Instantiate(prefab, transform);
            pos.x += xInterval;
            diceUIs[i].transform.localPosition = pos;
            var j = i;
            diceUIs[i].SetInfo(dices[i], selected, () =>
            {
                diceUse[j] = !diceUse[j];
                if (diceUse[j]) cost++;
                else cost--;
                SetCostText();
            });
        }
        pos.x = (dices.Count + 2) * xInterval;
        acceptBtn.transform.localPosition = pos;
    }

    public void InitWeaponBtn()
    {
        var image = weaponBtn.GetComponent<Image>();
        var color = image.color;
        weaponBtn.onClick.AddListener(() =>
        {
            weaponUse = !weaponUse;
            if (weaponUse)
            {
                image.color = selected;
                cost++;
            }
            else
            {
                image.color = color;
                cost--;
            }
            SetCostText();
        });

    }

    public void SetWeaponInfo()
    {

    }

    void SetCostText()
    {
        costTxt.text = $"{cost}/{maxCost}";
        if (cost > maxCost)
            costTxt.color = Color.red;
        else
            costTxt.color = Color.black;
    }

    public void Accept()
    {
        accept(diceUse, weaponUse);
    }

    public void Permit()
    {
        foreach (var uis in diceUIs)
        {
            Destroy(uis.gameObject);
        }
        if(weaponUse)
            weaponBtn.onClick.Invoke();
        weaponBtn.gameObject.SetActive(false);
        acceptBtn.gameObject.SetActive(false);
        costTxt.gameObject.SetActive(false);
    }
}

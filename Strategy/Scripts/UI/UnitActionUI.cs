using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using LSemiRoguelike.Strategy;

public class UnitActionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private Button button;
    private TextMeshProUGUI text;
    private StrategyAction info;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetInfo(StrategyAction info, System.Action action)
    {
        this.info = info;
        //image.sprite = info.sprite;
        text.text = info.ToString();
        button.onClick.AddListener(() => action());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RangeViewManager.Inst.ViewRange(info.routes);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RangeViewManager.Inst.HideRange();
    }
}

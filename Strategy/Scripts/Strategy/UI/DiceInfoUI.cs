using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;
using LSemiRoguelike;

public class DiceInfoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private Button button;
    private TextMeshProUGUI text;
    private Dice info;

    private bool use;
    private Color defColor, selectedColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        defColor = image.color;
    }

    public void SetInfo(Dice info, Color selected, System.Action action)
    {
        this.info = info;
        //image.sprite = info.sprite;
        text.text = info.ToString();
        button.onClick.AddListener(() => action());
        selectedColor = selected;
        button.onClick.AddListener(() => 
        {
            use = !use;
            image.color = use ? selectedColor : defColor;
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}

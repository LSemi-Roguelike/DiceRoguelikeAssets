using UnityEngine;
using UnityEngine.UI;

using TMPro;
using LSemiRoguelike;

public class WeaponInfoUI : MonoBehaviour
{
    private Image image;
    private Button button;
    private TextMeshProUGUI text;
    private Weapon info;

    private bool _use;
    public bool use => _use;
    private Color unuseColor;
    [SerializeField] private Color useColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        unuseColor = image.color;
    }

    public void SetInfo(Weapon info)
    {
        this.info = info;
    }
    public void ChangeColor(bool use)
    {
        _use = use;
        image.color = use ? useColor : unuseColor;
    }

    public void OnSelected()
    {
        ChangeColor(!use);
    }
}

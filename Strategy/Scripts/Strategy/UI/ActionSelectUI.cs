using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike.Strategy;

public class ActionSelectUI : MonoBehaviour
{
    private static ActionSelectUI _inst;
    public static ActionSelectUI inst => _inst;

    [SerializeField] private UnitActionUI prefab;
    //[SerializeField] private GameObject pannel;
    float width = Screen.width;
    UnitActionUI[] infoUIs;

    private void Awake()
    {
        _inst = this;
    }

    public void SetActionUI(List<StrategeAction> actions, System.Action<int> returnAction)
    {
        float xInterval = width / (actions.Count + 1);
        infoUIs = new UnitActionUI[actions.Count];
        for (int i = 0; i < actions.Count; i++)
        {
            infoUIs[i] = Instantiate(prefab, transform);
            var pos = infoUIs[i].transform.localPosition;
            pos.x = (i + 1) * xInterval;
            infoUIs[i].transform.localPosition = pos;
            var j = i;
            infoUIs[i].SetInfo(actions[i], () =>
            {
                returnAction(j);
                SelectEnd();
            });
        }
        //pannel.SetActive(true);
    }

    private void SelectEnd()
    {
        foreach (var uis in infoUIs)
        {
            Destroy(uis.gameObject);
        }
        infoUIs = new UnitActionUI[] { };
        //pannel.SetActive(false);
    }
}

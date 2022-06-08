using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike.Strategy;

public class ActionSelectUI : MonoBehaviour
{
    private static ActionSelectUI _inst;
    public static ActionSelectUI Inst => _inst;

    [SerializeField] private UnitActionUI prefab;
    private readonly float width = Screen.width;
    private UnitActionUI[] infoUIs;

    private void Awake()
    {
        _inst = this;
    }

    public void SetActionUI(List<StrategyAction> actions, System.Action<int> returnAction)
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
    }

    public void SelectEnd()
    {
        foreach (var uis in infoUIs)
        {
            Destroy(uis.gameObject);
        }
        infoUIs = new UnitActionUI[] { };
    }
}

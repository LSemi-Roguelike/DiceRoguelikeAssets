using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Sample_Map : MonoBehaviour {
    public static Sample_Map Inst { get; private set; }

    private void Awake() => Inst = this;

    [SerializeField] GameObject mapParent;
    [SerializeField] GameObject point_prefab;
    [SerializeField] GameObject line_prefab;
    [SerializeField] Text debug_out;
    

    Map map = new Map();
    // Start is called before the first frame update
    void Start() {

        map.SetParent(mapParent);
        map.Make();

        map.Draw(point_prefab, line_prefab);

        debug_out.text = map.Debug_print();

        
    }

    // Update is called once per frame
    void Update()
    {
        Keyprocess();
    }

    void Keyprocess() {
        if (Input.GetKeyDown(KeyCode.R)) {
            map.Make();
            map.Draw(point_prefab, line_prefab);
            debug_out.text = map.Debug_print();
        }
        if (Input.GetKeyDown(KeyCode.C)) {

        }
        if (Input.GetKeyDown(KeyCode.M)) {
            map.Make();
            map.Draw(point_prefab, line_prefab);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            map.Draw(point_prefab, line_prefab);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            debug_out.text = map.Debug_print();
        }


    }

    void testcase() {
        Map test_map = new Map();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yubunen.Mapmaker;

public class DebugMapmaker : MonoBehaviour
{
    [SerializeField] GameObject point_prefab;
    [SerializeField] GameObject line_prefab;

    [SerializeField] GameObject newRoute;

    Map map = new Map();
    // Start is called before the first frame update
    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        Keyprocess();
    }

    void Keyprocess()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            map.Make();
            map.DrawMap(this.gameObject);
        }else if(Input.GetKeyDown(KeyCode.F)){
        }
    }

}

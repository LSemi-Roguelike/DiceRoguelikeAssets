using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Text;

public class Sample_01 : MonoBehaviour {
    public static Sample_01 Inst { get; private set; }

    private void Awake() => Inst = this;

    [SerializeField] Text Output_opt;
    // Start is called before the first frame update
    void Start() {
        Floor floor = new Floor();
        Output_opt.text = floor.Debug_print();
    }

    // Update is called once per frame
    void Update() {
        
    }
}

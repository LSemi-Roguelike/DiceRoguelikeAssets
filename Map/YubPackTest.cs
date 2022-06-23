using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YubPack.EzDebug;
using YubPack.Roguelike;

public class YubPackTest : MonoBehaviour
{
    Mapmaker maker;
    ObjectSetter setter;

    void Start()
    {
        maker = new Mapmaker(10);
        setter = new ObjectSetter();

        foreach(Vector3 vector3 in maker.GetVector3s())
        {
            setter.setSphere(vector3);
        }

        foreach ((Vector3, Vector3) line in maker.GetLines())
        {
            setter.setLine(line.Item1, line.Item2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

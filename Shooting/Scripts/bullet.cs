using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
    {
        //攻撃した相手がEnemyの場合
        if(other.CompareTag("wall"))
		{
            Destroy(this.gameObject);
        }
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

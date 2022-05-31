using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
    {
        //攻撃した相手がEnemyの場合
        if(other.CompareTag("ballet"))
		{
            Destroy(this.gameObject);
			Destroy(other.gameObject);
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

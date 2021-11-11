using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : Player
{
    [SerializeField] float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = -Input.GetAxisRaw("Horizontal");
        float ver = -Input.GetAxisRaw("Vertical");

        transform.position -= new Vector3(hor, 0, ver) * Time.deltaTime * moveSpeed;

    }
}

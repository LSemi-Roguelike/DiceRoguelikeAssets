using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickDirection : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 target;

    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;
 
    /*void Update ()
    {
        mouse = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 5));
        transform.LookAt(target);
    }*/

    void Start()
    {
        mainCamera = Camera.main;
    }
 
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var raycastHitList = Physics.RaycastAll(ray).ToList();
            if (raycastHitList.Any()) {
                var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList.First().point);
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
 
                currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                currentPosition.y = 0;
                transform.LookAt(currentPosition);
            }
        }
    }


}

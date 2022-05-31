using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffect : MonoBehaviour
{
    [SerializeField] float remainTime;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(remainTime);
        Destroy(gameObject);
    }
}

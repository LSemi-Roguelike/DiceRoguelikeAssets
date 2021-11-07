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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ItemLog();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack.Cast(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Item item = AddItem(collision.gameObject.GetComponent<ItemObject>().GetItem());
            Destroy(collision.gameObject);
            item.SetOwner(this);
            Debug.Log(item);
            Debug.Log(items[ItemType.PASSIVE].Count);
            Debug.Log(items[ItemType.ACTIVE].Count);
        }
    }
    public void ItemLog()
    {
        int count = 0;
        foreach (ItemType itemType in System.Enum.GetValues(typeof(ItemType)))
        {
            foreach (Item item in items[itemType])
            {
                Debug.Log(itemType.ToString() + ":" + item.GetItemName());
                count++;
            }
        }
        Debug.Log(count + " items");
        Debug.Log("Status" + totalStatus);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] TileMapManager tileMapManagerPrefab;
        [SerializeField] ListManager listManagerPrefab;
        [SerializeField] InputManager inputManagerPrefab;
        // Start is called before the first frame update
        void Awake()
        {
            Instantiate(listManagerPrefab);
            Instantiate(inputManagerPrefab);
            Instantiate(tileMapManagerPrefab);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
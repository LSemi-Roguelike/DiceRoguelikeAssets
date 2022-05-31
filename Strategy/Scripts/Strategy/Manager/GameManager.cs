using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] TileMapManager tileMapManagerPrefab;
        [SerializeField] ResourceManager resourceManagerPrefab;
        [SerializeField] InputManager inputManagerPrefab;
        // Start is called before the first frame update
        void Awake()
        {
            Instantiate(resourceManagerPrefab);
            Instantiate(inputManagerPrefab);
            Instantiate(tileMapManagerPrefab);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
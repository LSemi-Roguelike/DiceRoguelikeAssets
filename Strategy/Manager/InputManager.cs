using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager manager;

        [SerializeField] GameObject onCursorPrefab;

        private GameObject _onCursor;
        public GameObject onCursor { get { return _onCursor; } }

        private void Awake()
        {
            if (manager)
            {
                Destroy(this);
            }
            manager = this;
            _onCursor = Instantiate(onCursorPrefab);
            _onCursor.transform.parent = transform;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Tilemap")))
            {
                _onCursor.SetActive(true);
                _onCursor.transform.position = hit.transform.position + new Vector3(0, hit.transform.localScale.y / 2, 0);
            }
            else
            {
                _onCursor.SetActive(false);
            }
        }

        public Vector3Int GetMouseCellPos()
        {
            return TileMapManager.manager.WorldToCell(_onCursor.transform.position);
        }
    }
}
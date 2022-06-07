using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class ViewPointMove : MonoBehaviour
    {
        public static ViewPointMove Inst;

        [SerializeField] private float moveTime;
        [SerializeField] private float moveSpeed;

        bool canMove = false;

        void Awake() => Inst = this;

        // Start is called before the first frame update
        void Start()
        {
            canMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(MoveToParentCo());
                return;
            }

            if (!canMove)
                return;

            var mousePos = new Vector2(Input.mousePosition.x / Screen.width
                , Input.mousePosition.y / Screen.height);

            if (mousePos.x > 0.9f)
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                if (mousePos.x > 0.95f)
                    transform.Translate(moveSpeed * 3 * Time.deltaTime, 0, 0);
            }
            else if (mousePos.x < 0.1f)
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                if (mousePos.x < 0.05f)
                    transform.Translate(-moveSpeed * 3 * Time.deltaTime, 0, 0);
            }

            if (mousePos.y > 0.9f)
            {
                transform.Translate(0, 0, moveSpeed * Time.deltaTime);
                if (mousePos.y > 0.95f)
                    transform.Translate(0, 0, moveSpeed * 3 * Time.deltaTime);
            }
            else if (mousePos.y < 0.1f)
            {
                transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
                if (mousePos.y < 0.05f)
                    transform.Translate(0, 0, -moveSpeed * 3 * Time.deltaTime);
            }

        }

        public void ResetParent()
        {
            transform.SetParent(null);
        }

        public void MoveTo(Transform target)
        {
            transform.SetParent(target);
            StartCoroutine(MoveToParentCo());
        }

        IEnumerator MoveToParentCo()
        {
            canMove = false;
            float cameraSpeed = (transform.position - transform.parent.position).magnitude * (1 / moveTime);
            while (transform.parent.position != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, cameraSpeed * Time.deltaTime);
                yield return null;
            }
            canMove = true;
        }
    }
}
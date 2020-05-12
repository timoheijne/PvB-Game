using UnityEngine;

public class CameraMovement : MonoBehaviour
{
        [SerializeField]
        private float _dragSpeed = 2;

        private Vector3 dragOrigin;
        
        void Update()
        {
                if (Input.GetMouseButtonDown(0))
                {
                        dragOrigin = Input.mousePosition;
                        return;
                }
 
                if (!Input.GetMouseButton(0)) return;

                Vector3 pos = Input.mousePosition - dragOrigin;
                Debug.Log(pos);
                
                transform.position += new Vector3(pos.y * _dragSpeed * Time.deltaTime, 0, -pos.x * _dragSpeed * Time.deltaTime);
                dragOrigin = Input.mousePosition;
        }
}
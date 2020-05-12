using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
        [SerializeField]
        private float _dragSpeed = 2;

        [SerializeField]
        private Vector3 _minCamp;
        
        [SerializeField]
        private Vector3 _maxClamp;
        
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
                
                transform.position += new Vector3(pos.y * _dragSpeed * Time.deltaTime, 0, -pos.x * _dragSpeed * Time.deltaTime);
                dragOrigin = Input.mousePosition;

                ClampMovement();
        }

        private void ClampMovement()
        {
                Vector3 _newPos = transform.position;
                _newPos.x = Mathf.Clamp(_newPos.x, _minCamp.x, _maxClamp.x);
                _newPos.z = Mathf.Clamp(_newPos.z, _minCamp.z, _maxClamp.z);

                transform.position = _newPos;
        }

        private void OnDrawGizmos()
        {
                Gizmos.color = Color.red;
                Vector3 midPoint = new Vector3((_minCamp.x+_maxClamp.x)/2.0f,(_minCamp.y+_maxClamp.y)/2.0f,(_minCamp.z+_maxClamp.z)/2.0f);
                Gizmos.DrawWireCube(midPoint, new Vector3(_maxClamp.x - _minCamp.x, 1, _maxClamp.z - _minCamp.z));
        }
}
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        [SerializeField] private float _zoomSpeed = 1;
        [SerializeField] private float _yOffsetSpeed = 1;
        
        [SerializeField, Range(2, 4.99f)]
        private float _minDistance = 2;

        [SerializeField, Range(5, 100)]
        private float _maxDistance = 5;

        private Vector3 _offset;
        
        private void Start()
        {
                transform.LookAt(transform.parent);
        }

        private void Update()
        {
                float _distance = Vector3.Distance(transform.position, transform.parent.position);
                if (_distance < _minDistance && Input.mouseScrollDelta.y > 0 || _distance > _maxDistance && Input.mouseScrollDelta.y < 0)
                {
                        return;
                }
                
                transform.Translate(Vector3.forward * (Input.mouseScrollDelta.y * _zoomSpeed) * Time.deltaTime);
                transform.position += new Vector3(0, -Input.mouseScrollDelta.y * _yOffsetSpeed, 0);
                transform.LookAt(transform.parent);
        }
}
using System;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
        [SerializeField]
        private float _maxDistance = .5f;

        public bool InteractWithObject()
        {
                if(PatientSystem.Instance == null)
                {
                        throw new ArgumentNullException("PatientSystem");
                }
                
                GameObject _gameObject = FindObject();
                
                if (_gameObject == null)
                {
                        return false;
                }
                
                InteractableObject _interactableObject = _gameObject.GetComponent<InteractableObject>();
                return _interactableObject != null && PatientSystem.Instance.MarkObjectiveDone(_interactableObject.ObjectiveName);
        }

        private GameObject FindObject()
        {
                bool _hitDetect = Physics.BoxCast(transform.position, transform.localScale, transform.forward, out RaycastHit _hitInfo, Quaternion.identity, _maxDistance);
                return (_hitDetect) ?  _hitInfo.transform.gameObject : null;
        }
        
        void OnDrawGizmos()
        {
                Gizmos.color = Color.red;

                Gizmos.DrawRay(transform.position, transform.forward * _maxDistance * 2);
                //Draw a cube at the maximum distance
                Gizmos.DrawWireCube(transform.position + transform.forward * _maxDistance * 2, transform.localScale);
        }
}

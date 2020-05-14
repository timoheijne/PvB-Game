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
                
                InteractableObject _interactableObject = FindInteractableObject();
                return _interactableObject != null &&  _interactableObject.HasPatient() && PatientSystem.Instance.MarkObjectiveDone(_interactableObject.ObjectiveName);
        }

        public InteractableObject FindInteractableObject()
        {
                GameObject _gameObject = FindObject();
                if (_gameObject == null)
                {
                        return null;
                }
                
                return _gameObject.GetComponent<InteractableObject>();
        }

        private GameObject FindObject()
        {
                bool _hitDetect = Physics.BoxCast(transform.position - transform.forward*_maxDistance, Vector3.one * 0.45f, transform.forward, out RaycastHit _hitInfo, transform.rotation, 1.9f);
                
                return (_hitDetect) ?  _hitInfo.transform.gameObject : null;
        }
        
        void OnDrawGizmos()
        {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, transform.forward);
                //Draw a cube in front of this gameobject
                Gizmos.DrawWireCube(transform.position + transform.forward, Vector3.one * _maxDistance);
        }
}

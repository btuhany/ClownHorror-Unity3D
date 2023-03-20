
using UnityEngine;
using Abstracts;
namespace Controllers
{
    public class RaycasterController : MonoBehaviour
    {
        [SerializeField] Transform _playerCamera;
        [SerializeField] LayerMask _layer;
        [SerializeField] float _interactDistance;
        

        public bool IsInactive;
        Targetable _currentTargetable;
        GrabbedObjController _grabbedObj;
        private void Update()
        {
            if(IsInactive) { return; }
            HandleRaycastActions();
        }
        private void HandleRaycastActions()
        {

            if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _interactDistance, _layer))  // ?Can check it with from layer
            {
                _currentTargetable = hit.collider.GetComponent<Targetable>();
                _currentTargetable.ToggleHighlight(true);

            }
            else if (_currentTargetable)
            {
                _currentTargetable.ToggleHighlight(false);
                _currentTargetable = null;
            }
        }
        public void Interact()
        {
            if (_currentTargetable == null) return;
            if (_currentTargetable.TryGetComponent(out Interactable interactableObj))
            {
                interactableObj.Interact();
            }

        }
    }
}



using UnityEngine;
using Abstracts;


namespace Controllers
{
    public class RaycasterController : MonoBehaviour
    {
        [SerializeField] Transform _playerCamera;
        [SerializeField] LayerMask _layer;
        [SerializeField] float _interactDistance;
        

        bool _deactivateRaycasting;
        Targetable _currentTargetable;
        PickedUpObjectController _pickedUpObjController;
        GunController _gunController;

        public bool DeactivateRaycasting { get => _deactivateRaycasting; set => _deactivateRaycasting = value; }

        private void Awake()
        {
            _pickedUpObjController = GetComponent<PickedUpObjectController>();
            _gunController = GetComponentInChildren<GunController>();
        }
        private void Update()
        {
            if (_gunController.IsAimed) 
            { 
                ResetCurrentTargetable();
                
                return; 
            }
            if(_deactivateRaycasting) { return; }
            HandleRaycastActions();
        }
        private void HandleRaycastActions()
        {
            if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _interactDistance, _layer))  // ?Can check it with from layer
            {
               if(hit.collider.TryGetComponent(out Targetable currentTargetable))
                {
                    if(_currentTargetable != currentTargetable)
                        ResetCurrentTargetable();
                    _currentTargetable = currentTargetable;
                    _currentTargetable.ToggleHighlight(true);
                }
               else
               {
                    ResetCurrentTargetable();
               }


            }
            else
            {

                    ResetCurrentTargetable();

            }


        }
        public void InteractOrPickUp()
        {
            if (_currentTargetable == null) return;
            if (_currentTargetable.TryGetComponent(out Interactable interactableObj))
            {
                interactableObj.Interact();         
            }
            else if(_currentTargetable.TryGetComponent(out PickUpAble pickUpAbleObj))
            {
                if (_pickedUpObjController.PickUpOrDrop(pickUpAbleObj))
                    _deactivateRaycasting = true;
                else
                    _deactivateRaycasting = false;
            }
        }

        private void ResetCurrentTargetable()
        {
            if (_currentTargetable)
            {
                _currentTargetable.ToggleHighlight(false);
                _currentTargetable = null;
            }
        }

        
    }
}


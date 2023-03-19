using System.Collections;
using UnityEngine;
public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField][Range(1, 7)] float _gravityScale;
    [SerializeField][Range(0.1f, 5)] float _forceToMoveObjects;

    [Header("Crouch&StandUp")]
    [SerializeField] Transform _cameraHolderTransform;
    [SerializeField][Range(1f, 3f)] float _crouchHeight;   //should set before starting game
    [SerializeField] float _cameraLerpSpeed = 10f;
    [SerializeField][Range(1f, 2f)] float _crouchCameraLerpUntilYpos = 1.15f;
    [SerializeField][Range(0.1f, 1f)] float _standUpCameraLerpUntilYpos = 0.9f;

    const float _gravity = -9.81f;
    bool _isCrouched;
    float _colliderHeightAtStart;
    Vector3 velocity;
    Vector3 _cameraHolderLocalPosAtStart;

    //Caching at awake for better perf/optimizing
    Vector3 _colliderCenterAtCrouch;
    Vector3 _colliderHeightAtCrouch;         //for camera holder pos at crouch

    GroundCheck _groundCheck;
    CharacterController _characterController;
    Transform _transform;

    public Vector3 Velocity { get => velocity; }
    public Vector3 ColliderHeightVectorAtCrouch { get => _colliderHeightAtCrouch;}

    public bool IsCrouched { get => _isCrouched;}

    private void Awake()
    {
        _cameraHolderLocalPosAtStart = _cameraHolderTransform.localPosition;
        _groundCheck = GetComponent<GroundCheck>();
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _colliderHeightAtStart = _characterController.height;
        _colliderCenterAtCrouch = new Vector3(0, -(_characterController.center.y + (_characterController.height - _crouchHeight) / 2), 0);
        _colliderHeightAtCrouch = new Vector3(0, _crouchHeight, 0);
    }
    private void Update()
    {
        HandleGravity();
    }
    private void HandleGravity()
    {
        if (_groundCheck.IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //else if(velocity.y < 0)
        //{
        //    _gravityScale *= 1.2f;
        //}
        velocity.y += _gravity * Time.deltaTime * _gravityScale;
        _characterController.Move(velocity * Time.deltaTime);
    }
    public void Crouch()
    {
        _isCrouched = true;
        _characterController.center = _colliderCenterAtCrouch;
        _characterController.height = _crouchHeight;
        StopAllCoroutines();
        StartCoroutine(CrouchCamera());
    }
    public void StandUp()
    {
        _isCrouched = false;  
        _characterController.height =  _colliderHeightAtStart;
        _characterController.center = Vector3.zero;
        StopAllCoroutines();
        StartCoroutine(StandUpCamera());

    }

    public void GroundMovement(Vector3 direction, float speed)
    {                                           
        //diagonal speed limit
        if (direction.magnitude > 1.7f)
        {
            direction.Normalize();
        }
        _characterController.Move(direction * speed * Time.deltaTime);
    }

    public void Jump(float jumpHeight)
    {
        // if (!_groundCheck.IsGrounded) return;  //double check condition
        velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * _gravity);

    }

    //Physic interaction with PickUpDrop layer objects
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.layer != 9)  //if gameobject's layer is PickUpDrop
            return;

        GameObject gameObj = hit.gameObject;
        Vector3 forceDir = gameObj.transform.position - _transform.position;
        forceDir.y = 0;
        forceDir.Normalize();

        Rigidbody gameObjRb = gameObj.GetComponent<Rigidbody>();
        gameObjRb.AddForceAtPosition(forceDir * _forceToMoveObjects , _transform.position, ForceMode.Impulse);

    }
    
    IEnumerator CrouchCamera()
    {
        while(_cameraHolderTransform.localPosition.y > (_groundCheck.GroundCheckLocalPos + _colliderHeightAtCrouch).y * _crouchCameraLerpUntilYpos)
        {
            _cameraHolderTransform.localPosition = Vector3.Lerp(_cameraHolderTransform.localPosition, _groundCheck.GroundCheckLocalPos + _colliderHeightAtCrouch, Time.deltaTime* _cameraLerpSpeed);
         
            yield return null;
        }
        _cameraHolderTransform.localPosition = _groundCheck.GroundCheckLocalPos + _colliderHeightAtCrouch;
        Debug.Log("finito");
    }
    IEnumerator StandUpCamera()
    {
        while (_cameraHolderTransform.localPosition.y < _cameraHolderLocalPosAtStart.y * _standUpCameraLerpUntilYpos)
        {
            _cameraHolderTransform.localPosition = Vector3.Lerp(_cameraHolderTransform.localPosition, _cameraHolderLocalPosAtStart, Time.deltaTime * _cameraLerpSpeed);

            yield return null;
        }
        _cameraHolderTransform.localPosition = _cameraHolderLocalPosAtStart;
        Debug.Log("finito");
    }
}

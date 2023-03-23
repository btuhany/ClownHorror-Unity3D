using System.Collections;
using UnityEngine;
using Managers;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] RecoilEffectHandler _recoil;
        [Header("FX")]
        [SerializeField] Transform _barrelTransform;
        [SerializeField] Transform _casinExitTransform;
        [SerializeField] float _shotPower;
        [SerializeField] private float ejectPower = 150f;
        [Header("Shooting")]
        [SerializeField] private float _range = 50f;
        [SerializeField] private Camera _fpsCam;
        [SerializeField] LayerMask _layerMask;

        [Header("Camera Movement At Aim")]
        [SerializeField] Transform _defaultGunPos;
        [SerializeField] Transform _aimingGunPos;
        [SerializeField] float _camFovAtAim;
        [SerializeField] float _gunAimPosLerpSpeed;
        [SerializeField] float _gunDefaultPosLerpSpeed;
        [SerializeField] float _aimCamFOVLerpSpeed;
        [SerializeField] float _defaultFOVCamLerpSpeed;


        private bool _onTransitionToAimCam;
        private bool _isOnDefaultCam;
        float _defaultFov;
        Animator _anim;
        SwayHandler _swayHandler;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _swayHandler = GetComponent<SwayHandler>();
            _defaultFov = _fpsCam.fieldOfView;
            transform.position = _defaultGunPos.position;
        }
        private void Update()
        {
            if (_onTransitionToAimCam)
            {
                _swayHandler.AimSway();
            }
            else if (_isOnDefaultCam)  //dont check onTransitionToDefaultCam because _swayHandler.WeaponSway also manipulates postion therefore prevents the transition.
            {
                _swayHandler.WeaponSway();
            }
        }
        private void InstantiateMuzzleFX()
        {
            GameObject newObjMuzzleFx = ObjectPoolManager.Instance.GetObjectFromPool(_barrelTransform, PoolObjectId.MuzzleFlashFx);
            StartCoroutine(SetToPool(newObjMuzzleFx, 0.5f, PoolObjectId.MuzzleFlashFx));
        }
        private void InstantiateBullet()
        {
            GameObject newObjBullet = ObjectPoolManager.Instance.GetObjectFromPool(_barrelTransform, PoolObjectId.Bullet);
            newObjBullet.GetComponent<Rigidbody>().AddForce(_barrelTransform.forward * _shotPower);
            StartCoroutine(SetToPool(newObjBullet, 0.7f, PoolObjectId.Bullet));
        }
        public void Shoot()
        {
            _anim.SetTrigger("Fire");
            _recoil.RecoilEffect();
            InstantiateMuzzleFX();
            InstantiateBullet();
            if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out RaycastHit hit, _range, _layerMask))
            {



            }
        }
        public void AimCam()
        {
            _isOnDefaultCam = false;
            if (Vector3.Distance(transform.position, _aimingGunPos.position) < 0.2f && Mathf.Abs(_camFovAtAim - _fpsCam.fieldOfView) < 0.02f) return;
            _onTransitionToAimCam = true;
            transform.position = Vector3.Lerp(transform.position, _aimingGunPos.position, Time.deltaTime * _gunAimPosLerpSpeed);
            _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _camFovAtAim, Time.deltaTime * _aimCamFOVLerpSpeed);


        }
        public void DefaultCam()
        {
            if (Vector3.Distance(transform.position, _defaultGunPos.position) < 0.03f && Mathf.Abs(_defaultFov - _fpsCam.fieldOfView) < 0.02f)
            {
                _isOnDefaultCam = true;

                return;
            }

            _onTransitionToAimCam = false;
            _swayHandler.WeaponSwayTime = 5.5f;

            transform.position = Vector3.Lerp(transform.position, _defaultGunPos.position, Time.deltaTime * _gunDefaultPosLerpSpeed);
            _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _defaultFov, Time.deltaTime * _defaultFOVCamLerpSpeed);

        }

        public void BulletCasingFx() //trigger on the animation event
        {
            Debug.Log("deneme");
            
            GameObject tempCasing = ObjectPoolManager.Instance.GetObjectFromPool(_casinExitTransform, PoolObjectId.BulletCasin);
          
         
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (_casinExitTransform.position - _casinExitTransform.right * 0.3f - _casinExitTransform.up * 0.6f), 1f);
           
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);


            StartCoroutine(SetToPool(tempCasing, 1f, PoolObjectId.BulletCasin));
        }
        IEnumerator SetToPool(GameObject gameObj, float delay, PoolObjectId objectID)
        {
            yield return new WaitForSeconds(delay);
            ObjectPoolManager.Instance.SetPool(gameObj, objectID);
            yield return null;
        }
    }

}

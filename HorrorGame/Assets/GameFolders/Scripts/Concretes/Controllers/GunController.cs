using System.Collections;
using UnityEngine;
using Managers;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        [Header("Shooting")]
        [SerializeField] private float _range = 50f;
        [SerializeField] private Camera _fpsCam;
        [SerializeField] LayerMask _layerMask;
        [SerializeField] private float _shootCoolDown;
        [Header("RecoilFX")]
        [SerializeField] CamRecoilEffectHandler _recoil;
        [Header("FX")]
        [SerializeField] Transform _barrelTransform;
        [SerializeField] Transform _casinExitTransform;
        [SerializeField] float _shotPower;
        [SerializeField] private float ejectPower = 150f;
        [SerializeField][Range(0.1f, 5f)] float _bulletHeadSetPoolDelay = 0.7f;
        [SerializeField][Range(0.1f, 5f)] float _muzzleFxSetPoolDelay = 0.5f;
        [SerializeField][Range(0.1f, 5f)] float _bulletHoleSetPoolDelay = 1.2f;
        [SerializeField][Range(0.1f, 5f)] float _bulletCasinSetPoolDelay = 1f;
        [Header("Camera Movement At Aim")]
        [SerializeField] Transform _defaultGunPos;
        [SerializeField] Transform _aimingGunPos;
        [SerializeField] float _camFovAtAim;
        [SerializeField] float _gunAimPosLerpSpeed;
        [SerializeField] float _gunDefaultPosLerpSpeed;
        [SerializeField] float _aimCamFOVLerpSpeed;
        [SerializeField] float _defaultFOVCamLerpSpeed;

        private float _shootCoolDownTimer;
        private bool _isShooted;
        private bool _onTransitionToAimCam;
        private bool _isOnDefaultCam;
        float _defaultFov;
        Animator _anim;
        [SerializeField] CamSwayHandler swayHandler;
        

        public bool OnTransitionToAimCam { get => _onTransitionToAimCam; }

        private void Awake()
        {
            
            _anim = GetComponent<Animator>();
            
            _defaultFov = _fpsCam.fieldOfView;
            transform.position = _defaultGunPos.position;
            _shootCoolDownTimer = _shootCoolDown;
        }

        private void Update()
        {
            if (_onTransitionToAimCam)
            {         
                swayHandler.AimSway();
            }
            else if (_isOnDefaultCam)  //dont check onTransitionToDefaultCam because _swayHandler.WeaponSway also manipulates postion therefore prevents the transition.
            {
                
                swayHandler.WeaponSway();
            }
            if(_isShooted)
            {
                _shootCoolDownTimer -= Time.deltaTime;
                if(_shootCoolDownTimer < 0 )
                {
                    _shootCoolDownTimer = _shootCoolDown;
                    _isShooted = false;
                }
            }
        }
        public void AimCam()
        {
           
            _isOnDefaultCam = false;
            if (Mathf.Abs(_camFovAtAim - _fpsCam.fieldOfView) < 0.02f)
            {

                return;
            }
            swayHandler.OnAimCamTransition();

            _onTransitionToAimCam = true;

            _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _camFovAtAim, Time.deltaTime * _aimCamFOVLerpSpeed);


        }
        public void DefaultCam()
        {
           
            if (Mathf.Abs(_defaultFov - _fpsCam.fieldOfView) < 0.02f)
            {
                _isOnDefaultCam = true;
                return;
            }
            _onTransitionToAimCam = false;
            swayHandler.OnDefaultCamTransition();
            _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _defaultFov, Time.deltaTime * _defaultFOVCamLerpSpeed);

        }
        public void Shoot()
        {
            if (_isShooted) return;

            _isShooted = true;
            _anim.SetTrigger("Fire");
            _recoil.RecoilEffect();
            InstantiateMuzzleFX();
            InstantiateBullet();
            if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out RaycastHit hit, _range, _layerMask))
            {
                InstantiateBulletHoleFX(hit).transform.SetParent(hit.transform);
                
            }
        }

        public void BulletCasingFx() //trigger on the animation event
        {
            
            GameObject tempCasing = ObjectPoolManager.Instance.GetObjectFromPool(_casinExitTransform, PoolObjectId.BulletCasin);
          
         
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (_casinExitTransform.position - _casinExitTransform.right * 0.3f - _casinExitTransform.up * 0.6f), 1f);
           
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);


            StartCoroutine(SetToPool(tempCasing, _bulletCasinSetPoolDelay, PoolObjectId.BulletCasin));
        }
        private void InstantiateMuzzleFX()
        {
            GameObject newObjMuzzleFx = ObjectPoolManager.Instance.GetObjectFromPool(PoolObjectId.MuzzleFlashFx);
            newObjMuzzleFx.transform.position = _barrelTransform.position;
            newObjMuzzleFx.transform.rotation = _barrelTransform.rotation;
            newObjMuzzleFx.SetActive(true);
            newObjMuzzleFx.transform.SetParent(_barrelTransform);        //move pos after insantiating
            StartCoroutine(SetToPool(newObjMuzzleFx, _muzzleFxSetPoolDelay, PoolObjectId.MuzzleFlashFx));
        }
        private void InstantiateBullet()
        {
            GameObject newObjBullet = ObjectPoolManager.Instance.GetObjectFromPool(_barrelTransform, PoolObjectId.Bullet);
            newObjBullet.GetComponent<Rigidbody>().AddForce(_barrelTransform.forward * _shotPower);
            StartCoroutine(SetToPool(newObjBullet, _bulletHeadSetPoolDelay, PoolObjectId.Bullet));
        }
        private GameObject InstantiateBulletHoleFX(RaycastHit hit)
        {
            GameObject newBulletHole = ObjectPoolManager.Instance.GetObjectFromPool(PoolObjectId.WoodBulletHoleFx);
            newBulletHole.transform.position = hit.point;
            newBulletHole.transform.rotation = Quaternion.LookRotation(hit.normal);
            newBulletHole.SetActive(true);
            StartCoroutine(SetToPool(newBulletHole, _bulletHoleSetPoolDelay, PoolObjectId.WoodBulletHoleFx));

            return newBulletHole;
        }
        IEnumerator SetToPool(GameObject gameObj, float delay, PoolObjectId objectID)
        {
            yield return new WaitForSeconds(delay);
            ObjectPoolManager.Instance.SetPool(gameObj, objectID);
            yield return null;
        }
    }

}

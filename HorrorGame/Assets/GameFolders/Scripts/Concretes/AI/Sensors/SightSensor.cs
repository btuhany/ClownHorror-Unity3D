
using AI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sensors
{
    public class SightSensor : MonoBehaviour
    {
        [SerializeField] AiEnemyConfig _config;

        [Header("Sight")]
        [SerializeField] Transform _eyeTransform;
        [SerializeField] float _distance = 10f;
        [SerializeField] float _angle = 30f;
        [SerializeField] float _height = 1.0f;

        [Header("Scanner")]
        [SerializeField] int _count; //how many colliders are triggered during the scan
        [SerializeField] int _scanFreq = 30;
        [SerializeField] float _scanInterval;
        [SerializeField] float _scanTimer;
        [SerializeField] LayerMask _layers;
        [SerializeField] LayerMask _obstacleLayers;


        Transform _transform;
        public Collider[] _colliders = new Collider[100];
        public List<GameObject> _objectsInSightList = new List<GameObject>();

        public List<GameObject> ObjectsInSightList { get => _objectsInSightList; }
        public float Distance { get => _distance; }
        public float Angle { get => _angle; }
        public float Height { get => _height; }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            GameManager.Instance.OnHardDiff += HandleOnHardDiff;
            GameManager.Instance.OnNormalDiff += HandleOnNormalDiff;
            _scanInterval = 1.0f / _scanFreq;
        }

        private void HandleOnNormalDiff()
        {
            _angle = _config.NormalSightAngle;
        }

        private void HandleOnHardDiff()
        {
            _angle = _config.HardSightAngle;
        }

        private void Update()
        {
            _scanTimer -= Time.deltaTime;
            if (_scanTimer < 0)
            {
                ScanSphere();
                _scanTimer = _scanInterval;
            }
        }

        private void ScanSphere()
        {

            _count = Physics.OverlapSphereNonAlloc(_transform.position, _distance, _colliders, _layers, QueryTriggerInteraction.Collide);

            //add triggered collider gameobjects to the list

            _objectsInSightList.Clear();
            for (int i = 0; i < _count; i++)
            {
                GameObject obj = _colliders[i].gameObject;
                
                if (IsInSight(obj))
                {
                    _objectsInSightList.Add(obj);
                }

            }

        }
        public bool IsInSight(GameObject obj)
        {
            
            Vector3 objPosInAiLocal = obj.gameObject.transform.position - _transform.position;

            if (objPosInAiLocal.y < 0 || objPosInAiLocal.y > _height)
                return false;

            //Calculating angle and check 
            objPosInAiLocal.y = 0;
            float deltaAngle = Vector3.Angle(objPosInAiLocal, transform.forward);

            if (deltaAngle > _angle)
            {
                return false;
            }

            if (obj.layer == 10)
            {
                return true;
            }
            if (Physics.Linecast(_eyeTransform.position, obj.gameObject.transform.position, _obstacleLayers) )
            {


                return false;  // returns false if it finds something




            }
            
            return true;

        }



    }

}

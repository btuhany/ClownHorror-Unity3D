
using System.Collections.Generic;
using UnityEngine;


public class AiSightSensor : MonoBehaviour
{

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

    private List<GameObject> _objectsInSightList = new List<GameObject>();

    Transform _transform;
    Collider[] _colliders = new Collider[2];

    public List<GameObject> ObjectsInSightList { get => _objectsInSightList; }
    public float Distance { get => _distance;}
    public float Angle { get => _angle;}
    public float Height { get => _height;}

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _scanInterval = 1.0f / _scanFreq;
    }
    private void Update()
    {
        _scanTimer -= Time.deltaTime;
        if(_scanTimer<0)
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
            _objectsInSightList.Add(_colliders[i].gameObject);   
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

        if(deltaAngle>_angle)
        {
            return false;
        }

        if(Physics.Linecast(_eyeTransform.position, obj.gameObject.transform.position, _obstacleLayers))
        {
            return false;  // returns false if it finds something
        }
        return true;
        
    }



}

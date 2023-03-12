
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AiSightSensor : MonoBehaviour
{

    [Header("Sight")]
    [SerializeField] Transform _eyeTransform;
    [SerializeField] float _distance = 10f;
    [SerializeField] float _angle = 30f;
    [SerializeField] float _height = 1.0f;
    [SerializeField] Color _meshColor = Color.red;

    [Header("Scanner")]
    [SerializeField] int _count; //how many colliders are triggered during the scan
    [SerializeField] int _scanFreq = 30;
    [SerializeField] float _scanInterval;
    [SerializeField] float _scanTimer;
    [SerializeField] LayerMask _layers;
    [SerializeField] LayerMask _obstacleLayers;

    private List<GameObject> _objectsInSightList = new List<GameObject>();

    Transform _transform;
    Collider[] _colliders = new Collider[50];
    Mesh _mesh;  //Represents the sensor
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
    private bool IsInSight(GameObject obj)
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

    Mesh CreateWedgeMesh()
    {
        //Vector caching
        Vector3 vectorUp = Vector3.up;
        Vector3 vectorForw = Vector3.forward;

        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;    // 2 triangle on left,right and far side. 1 triangle on top and bottom.
        int numVertices = numTriangles * 3;
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];  //ignoring the index
        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        // Main vertice locations
        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * vectorForw * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * vectorForw * _distance;

        Vector3 topCenter = bottomCenter + vectorUp * _height;
        Vector3 topLeft = bottomLeft + vectorUp * _height;
        Vector3 topRight = bottomRight + vectorUp * _height;

        int vertIndex = 0;
        //left side
        vertices[vertIndex++] = bottomCenter;
        vertices[vertIndex++] = bottomLeft;
        vertices[vertIndex++] = topLeft;

        vertices[vertIndex++] = topLeft;
        vertices[vertIndex++] = topCenter;
        vertices[vertIndex++] = bottomCenter;

        //right side
        vertices[vertIndex++] = bottomCenter;
        vertices[vertIndex++] = topCenter;
        vertices[vertIndex++] = topRight;

        vertices[vertIndex++] = topRight;
        vertices[vertIndex++] = bottomRight;
        vertices[vertIndex++] = bottomCenter;

        float currentAngle = -_angle;
        float deltaAngle = (_angle * 2) / segments; //main angle is _angle*2
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * vectorForw * _distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * vectorForw * _distance;

            topRight = bottomRight + vectorUp * _height;
            topLeft = bottomLeft + vectorUp * _height;

            //far side
            vertices[vertIndex++] = bottomLeft;
            vertices[vertIndex++] = bottomRight;
            vertices[vertIndex++] = topRight;

            vertices[vertIndex++] = topRight;
            vertices[vertIndex++] = topLeft;
            vertices[vertIndex++] = bottomLeft;

            //top
            vertices[vertIndex++] = topCenter;
            vertices[vertIndex++] = topLeft;
            vertices[vertIndex++] = topRight;

            //bottom
            vertices[vertIndex++] = bottomCenter;
            vertices[vertIndex++] = bottomRight;
            vertices[vertIndex++] = bottomLeft;
            currentAngle += deltaAngle;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnValidate()
    {
        _mesh = CreateWedgeMesh();
    }
    private void OnDrawGizmos()
    {
        if(_mesh)
        {
            Gizmos.color = _meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }
        Gizmos.DrawWireSphere(_transform.position, _distance);
        for (int i = 0; i < _count; i++)
        {
            Gizmos.DrawWireSphere(_colliders[i].transform.position, 1f);
        }
        Gizmos.color = Color.green;
        foreach(var obj in _objectsInSightList)
        {
            if(IsInSight(obj))
                Gizmos.DrawSphere(obj.transform.position, 0.5f);
        }

    }
}

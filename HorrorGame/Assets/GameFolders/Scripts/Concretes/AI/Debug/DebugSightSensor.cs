using UnityEngine;
using Unity;
using Sensors;

namespace AI.DebugGizmos
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SightSensor))]
    public class DebugSightSensor : MonoBehaviour
    {
        [SerializeField] Color _meshColor = Color.red;
        [SerializeField] SightSensor _sensor;
        [SerializeField] Transform _transform;

        float _angle;
        float _height;
        float _distance;
        Mesh _mesh;  //Represents the sensor

        private void Update()
        {
            _angle = _sensor.Angle;
            _height = _sensor.Height;
            _distance = _sensor.Distance;
            _mesh = CreateWedgeMesh();
        }
        private void OnDrawGizmos()
        {
            if (_mesh)
            {
                Gizmos.color = _meshColor;
                Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
            }

            Gizmos.DrawWireSphere(_transform.position, _sensor.Distance);
            Gizmos.color = Color.green;
            foreach (var obj in _sensor.ObjectsInSightList)
            {
                if (_sensor.IsInSight(obj))
                    Gizmos.DrawSphere(obj.transform.position, 1f);
            }

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
    }

}

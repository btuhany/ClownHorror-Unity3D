using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetPointForNavmesh : SingletonMonoObject<GetPointForNavmesh>
{
    [SerializeField] float _range;
    private void Awake()
    {
        SingletonThisObject(this);
    }

    //Creates a random location vector3 from the center with a distance(range).
    //In this random location it uses SamplePosition(Finds the nearest point based on the NavMesh within a specified range)
    //if there is a point in the navmesh it returns true (tries 30 times), false otherwise

    private Vector3 RandomPoint(Vector3 center, float range)
    {
        for (int i = 0; i < 30; i++)   // 30?
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }

    public Vector3 GetRandomPointFromTransform(Transform gameObject, float radius)
    {
        Vector3 point = RandomPoint(gameObject.position, radius);
        Debug.DrawRay(point, Vector3.up, Color.red, 1);
        return point;
    }
    public Vector3 GetRandomPointFromInstance()
    {
        Vector3 point = RandomPoint(transform.position, _range);
        Debug.DrawRay(point, Vector3.up, Color.red, 1);
        return RandomPoint(transform.position, _range);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
#endif
}

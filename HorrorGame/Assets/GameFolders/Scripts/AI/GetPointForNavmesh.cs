using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetPointForNavmesh : SingletonMonoObject<GetPointForNavmesh>
{
    [SerializeField] float _range;
    [SerializeField] bool anotherArea;
    [SerializeField] int exp;
    [SerializeField] NavMeshAgent newAI;
    [SerializeField] float _sampleRange = 1f;
    int areaMask;

    Vector3 _gizmoTransform;
  
    private void Awake()
    {
        SingletonThisObject(this);
        areaMask = newAI.areaMask;
        areaMask += 1 << NavMesh.GetAreaFromName("Everything");//turn on all
        areaMask -= 1 << NavMesh.GetAreaFromName("Walkable");
        
    }

    //Creates a random location vector3 from the center with a distance(range).
    //In this random location it uses SamplePosition(Finds the nearest point based on the NavMesh within a specified range)
    //if there is a point in the navmesh it returns true (tries 30 times), false otherwise

    private Vector3 RandomPoint(Vector3 center, float range)
    {
        Vector3 randomVector = Random.insideUnitSphere;
        randomVector.y = 0f;
        Vector3 randomPoint = center + randomVector * range;
        NavMeshHit hit;
        _gizmoTransform = randomPoint;
        
        if (!anotherArea)
        {
            if (NavMesh.SamplePosition(randomPoint, out hit, _sampleRange, NavMesh.AllAreas))
            { 
                return hit.position;
            }
        }
        else
        {
            if (NavMesh.SamplePosition(randomPoint, out hit, _sampleRange, areaMask))
            {
                return hit.position;
            }
        }
        return center;  //do something
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
        return point;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
        Gizmos.DrawSphere(_gizmoTransform, _sampleRange);
       
    }
#endif
}

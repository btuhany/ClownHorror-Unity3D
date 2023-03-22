
using UnityEngine;
using UnityEngine.AI;
namespace AI.DebugGizmos
{
    public class DebugNavMeshAgent : MonoBehaviour
    {
        public bool Velocity;
        public bool DesiredVelocity;
        public bool Path;
        public bool WanderRadius;

        [SerializeField] AiEnemy _ai;
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] Transform _transform;

        private void OnDrawGizmos()
        {
           

            if (Velocity)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_transform.position, _transform.position + _agent.velocity);
            }
            if (DesiredVelocity)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_transform.position, _transform.position + _agent.desiredVelocity);
            }
            if (Path)
            {
                Gizmos.color = Color.black;
                Vector3 prevCorner = _transform.position;
                var agentPath = _agent.path;
                foreach (var corner in agentPath.corners)
                {
                    Gizmos.DrawLine(prevCorner, corner);
                    Gizmos.DrawSphere(corner, 0.1f);
                    prevCorner = corner;
                }

            }
            if (WanderRadius)
            {
                Gizmos.DrawWireSphere(transform.position, _ai.Config.SeekRandomPointRadius);
            }
        }

    }

}

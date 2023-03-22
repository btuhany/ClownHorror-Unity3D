using UnityEngine;


namespace AI
{
    [CreateAssetMenu()]
    public class AiEnemyConfig : ScriptableObject
    {
        [Header("Movement Speeds")]  //Walk,FastWalk,Run,FastRun,Sprint
        public float[] EasyMovementSpeeds = new float[5];
        public float[] NormalMovementSpeeds = new float[5];
        public float[] HardMovementSpeeds = new float[5];
        [Header("Chase Player State")]
        [Range(0, 5f)] public float MaxSetDestTime;
        [Range(0, 5f)] public float MaxChangeStateTime;
        [Header("Wander/Roam State")]
        public float RandomPointRadius;
        [Header("Idle State")]
        public float IdleWaitTime;
    }

}

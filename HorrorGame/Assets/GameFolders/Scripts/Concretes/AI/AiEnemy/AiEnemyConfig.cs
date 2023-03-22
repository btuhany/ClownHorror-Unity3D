using UnityEngine;


namespace AI
{
    [CreateAssetMenu()]
    public class AiEnemyConfig : ScriptableObject
    {
        [Header("Movement Speeds")]  //Walk,FastWalk,Run,FastRun,Sprint
        public float[] EasyMovementSpeeds;
        public float[] NormalMovementSpeeds;
        public float[] HardMovementSpeeds;
        [Header("Chase Player State")]
        [Range(0, 5f)] public float MaxSetDestTime;
        [Range(0, 5f)] public float MaxChangeStateTime;

    }

}

using UnityEngine;


namespace AI
{
    [CreateAssetMenu()]
    public class AiEnemyConfig : ScriptableObject
    {
        
        public float[] MovementSpeeds;
        [Header("Chase Player State")]
        [Range(0, 5f)] public float MaxSetDestTime;

    }

}

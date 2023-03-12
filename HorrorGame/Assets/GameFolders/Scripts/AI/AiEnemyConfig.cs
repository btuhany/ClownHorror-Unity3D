using UnityEngine;

[CreateAssetMenu()]
public class AiEnemyConfig : ScriptableObject
{
    [SerializeField] private float _maxTime;
    public float MaxDistance;
}

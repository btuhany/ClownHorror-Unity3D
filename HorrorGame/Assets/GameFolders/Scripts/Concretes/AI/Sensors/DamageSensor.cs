
using UnityEngine;
using Sensors;
using Controllers;
//public enum BodyPart
//{
//    Head,
//    Body,
//}
namespace Sensors
{
    public class DamageSensor : MonoBehaviour
    {
        //  [SerializeField] private BodyPart _bodyPart;
        [SerializeField] private int _maxDamage;
        [SerializeField] EnemyHealthController _enemy;


        public void TakeHit()
        {
            _enemy.DecreaseHealth(_maxDamage);

        }

    }

}

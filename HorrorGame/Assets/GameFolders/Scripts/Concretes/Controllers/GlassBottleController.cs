using Abstracts;

using UnityEngine;

namespace Controllers
{
    public class GlassBottleController : PickUpAble
    {
        void OnCollisionEnter(Collision collision)
        {
            if (_isThrowed)
            {
                CreateTheSoundWave();
                Destroy(this.gameObject);
            }
                
        }
    }
}


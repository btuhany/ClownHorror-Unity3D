using UnityEngine;

namespace Abstracts
{
    public abstract class Targetable : MonoBehaviour
    {
        [SerializeField] OutlineHighlight _objectHighlight;

        public void ToggleHighlight(bool activeState)
        {
            _objectHighlight.enabled = activeState;
        }
    }
}


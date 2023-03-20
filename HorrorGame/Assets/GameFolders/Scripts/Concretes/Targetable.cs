using UnityEngine;

namespace Abstracts
{
    public abstract class Targetable : MonoBehaviour
    {
        [SerializeField] Light _objectHighlight;

        public void ToggleHighlight(bool activeState)
        {
            _objectHighlight.gameObject.SetActive(activeState);
        }
    }
}


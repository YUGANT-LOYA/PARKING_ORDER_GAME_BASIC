using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YugantLibrary.Generic
{
    public class ToggleButton : MonoBehaviour
    {
        public bool isToggleOn;
        [SerializeField] GameObject onOffButtonGm;
        public LayerMask buttonToggleLayerMask;
        public UnityEvent<GameObject> ToggleOnEvent,ToggleOffEvent;

        private void OnTriggerEnter(Collider other)
        {
            if ((buttonToggleLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                isToggleOn = !isToggleOn;
                Debug.Log("Toggle Status : " + isToggleOn);
                Renderer renderer = onOffButtonGm.GetComponent<Renderer>();
                if (isToggleOn)
                {
                    renderer.material.color = Color.red;
                    ToggleOnEvent?.Invoke(this.gameObject);
                }
                else
                {
                    renderer.material.color = Color.green;
                    ToggleOffEvent?.Invoke(this.gameObject);
                }
            }
        }

    }
}

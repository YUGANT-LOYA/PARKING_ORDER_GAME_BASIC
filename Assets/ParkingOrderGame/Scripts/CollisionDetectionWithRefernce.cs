using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YugantLibrary.Generic
{
    public class CollisionDetectionWithRefernce : MonoBehaviour
    {
        public LayerMask layerMaskForCollisionDetection; 

        public UnityEvent<Collision> CollisionEnterWithReferenceEvent, CollisionStayWithReferenceEvent, CollisionExitWithReferenceEvent;

        private void Start()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((layerMaskForCollisionDetection.value & (1 << collision.gameObject.layer)) != 0)
            {
                CollisionEnterWithReferenceEvent?.Invoke(collision);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if ((layerMaskForCollisionDetection.value & (1 << collision.gameObject.layer)) != 0)
            {
                CollisionStayWithReferenceEvent?.Invoke(collision);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if ((layerMaskForCollisionDetection.value & (1 << collision.gameObject.layer)) != 0)
            {
                CollisionExitWithReferenceEvent?.Invoke(collision);
            }
        }
    }
}

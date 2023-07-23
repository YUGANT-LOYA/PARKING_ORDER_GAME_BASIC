using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace YugantLibrary.ParkingOrderGame
{
    public class CameraShake : MonoBehaviour
    {
        Vector3 defaultPos;
        [Range(0f, 2f)]
        [SerializeField] float shakeDuration = 0.75f;
        [Range(0f, 1f)]
        [SerializeField] float shakeStrength = 0.6f;
        [Range(0, 90)]
        [SerializeField] int shakeRandomness = 40;

        private void Awake()
        {
            defaultPos = transform.position;
        }

        [Button]
        public void ShakeCamera()
        {
            this.transform.DOShakePosition(shakeDuration, shakeStrength, shakeRandomness).OnComplete(() =>
            {
                this.transform.position = defaultPos;
            });
        }

    }
}
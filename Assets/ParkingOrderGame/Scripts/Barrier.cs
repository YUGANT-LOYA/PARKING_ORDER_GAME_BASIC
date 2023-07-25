using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YugantLibrary.ParkingOrderGame
{
    public class Barrier : MonoBehaviour
    {
        [SerializeField] float timeToMoveBarrier = 0.5f, barrierYVal = -0.75f;
        [SerializeField] Vector3 startPos, endPos;

        private void Start()
        {
            startPos = this.transform.localPosition;
            endPos = new Vector3(startPos.x,barrierYVal,startPos.z);
        }

        public void MoveBarrierUp()
        {
            this.transform.DOMove(startPos,timeToMoveBarrier);
        }

        public void MoveBarrierDown()
        {
            this.transform.DOMove(endPos,timeToMoveBarrier);
        }
    }
}

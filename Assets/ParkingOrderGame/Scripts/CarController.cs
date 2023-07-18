using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YugantLibrary.ParkingOrderGame
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] GameObject car;
        [SerializeField] LineRenderer lineRenderer;
        public bool IsCarMoving { get; private set; }
        public bool IsCarAtTarget { get; private set; }

        public Action OnCarStartMovingEvent, OnDesinationReachedEvent, OnStartReachedEvent;

        private void OnEnable()
        {
            OnCarStartMovingEvent += CarStartMoving;
            OnStartReachedEvent += CarAtStartLocation;
            OnDesinationReachedEvent += CarAtTargetLocation;
        }

        private void OnDisable()
        {
            OnCarStartMovingEvent -= CarStartMoving;
            OnStartReachedEvent -= CarAtStartLocation;
            OnDesinationReachedEvent -= CarAtTargetLocation;
        }

        void CarStartMoving()
        {

        }

        void CarAtTargetLocation()
        {

        }

        void CarAtStartLocation()
        {

        }

    }
}

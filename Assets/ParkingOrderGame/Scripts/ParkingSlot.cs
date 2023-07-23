using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YugantLibrary.ParkingOrderGame
{
    public class ParkingSlot : MonoBehaviour
    {
        [SerializeField] CarController parkingCarController;
        public bool isCarParked;

        private void OnEnable()
        {
            parkingCarController.OnCarStartMovingEvent += CarIsNotParked;
            parkingCarController.OnDesinationReachedEvent += CarReached;
        }

        private void OnDisable()
        {
            parkingCarController.OnCarStartMovingEvent -= CarIsNotParked;
            parkingCarController.OnDesinationReachedEvent -= CarReached;
        }

        public CarController GetParkingCar() 
        {
            return parkingCarController; 
        }

        void CarReached()
        {
            isCarParked = true;
        }

        void CarIsNotParked()
        {
            isCarParked = false;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YugantLibrary.ParkingOrderGame
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] Transform vehicleContainer, parkingSlotContainer, followPathContainer;
        [SerializeField] int totalMoves = 5;
        bool canAnswerBeChecked;
        int totalCars;
        public Action CheckAnswerEvent;


        private void OnEnable()
        {
            CheckAnswerEvent += CheckAnswer;
        }

        private void OnDisable()
        {
            CheckAnswerEvent -= CheckAnswer;
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            InputHandler.Instance.canClick = true;
            InputHandler.Instance.movesRemaining = totalMoves;
            UI_Handler.Instance.SetMovesRemainingText(totalMoves);
            for (int i = 0; i < vehicleContainer.childCount; i++)
            {
                CarController carController = vehicleContainer.GetChild(i).GetComponent<CarController>();
                carController.SetLevelHandler(this);
                totalCars++;
            }
        }

        public void SetIsAnyCarInMovingState(bool isMovingState)
        {
            canAnswerBeChecked = isMovingState;
        }

        void CheckAnswer()
        {
            int carParkedCount = 0;
            bool canCheckAnswer = true;

            for (int i = 0; i < vehicleContainer.childCount; i++)
            {
                CarController carController = vehicleContainer.GetChild(i).GetComponent<CarController>();
                if (carController.IsCarMoving)
                {
                    canCheckAnswer = false;
                    break;
                }
            }

            for (int i = 0; i < parkingSlotContainer.childCount; i++)
            {
                ParkingSlot parkingSlotScript = parkingSlotContainer.GetChild(i).GetComponent<ParkingSlot>();

                if (parkingSlotScript.isCarParked)
                {
                    carParkedCount++;
                }
                else
                {
                    break;
                }
            }

            //Debug.Log("Can Answer Check : "+ canCheckAnswer);

            if (canCheckAnswer)
            {
                if (carParkedCount == totalCars)
                {
                    Debug.Log("Level Completed !!");
                    LevelCompleted();
                }

                if (InputHandler.Instance.movesRemaining <= 0)
                {
                    Debug.Log("Level Failed !!");
                    LevelFailed();
                }
            }
        }

        void LevelCompleted()
        {
            UI_Handler.Instance.GameWinPopUp();
        }

        public void LevelFailed()
        {
            UI_Handler.Instance.GameOverPopUp();
        }
    }
}

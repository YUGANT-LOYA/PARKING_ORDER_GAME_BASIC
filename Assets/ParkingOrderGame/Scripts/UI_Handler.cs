using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YugantLibrary.ParkingOrderGame
{
    public class UI_Handler : MonoBehaviour
    {
        public static UI_Handler Instance;

        [SerializeField] Button restartButton, nextButton;
        [SerializeField] TextMeshProUGUI movesRemainingText,Level_Num_Text;
        [SerializeField] GameObject gameOverPopUp, gameWinPopUp;
        Camera cam;
        private void Awake()
        {
            CreateSingleton();
            cam = Camera.main;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(() =>
            {
                OnClickRestart();
            });

            nextButton.onClick.AddListener(() =>
            {
                OnClickNext();
            });
        }

        private void CreateSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        public void SetMovesRemainingText(int val)
        {
            movesRemainingText.text = $"Moves : {val}";
        }

        public void SetLevelNumber(int num)
        {
            Level_Num_Text.text = $"Level : {num}";
        }

        public void OnClickNext()
        {
            GameController.Instance.OnNext();
        }

        public void OnClickRestart()
        {
            GameController.Instance.CreateLevel();
        }

        public void GameOverPopUp()
        {
            gameOverPopUp.gameObject.SetActive(true);
        }

        public void GameWinPopUp()
        {
            gameWinPopUp.gameObject.SetActive(true);
        }

        public void ShakeCamera()
        {
            cam.GetComponent<CameraShake>().ShakeCamera();
        }
    }
}

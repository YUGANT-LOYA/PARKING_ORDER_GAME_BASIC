using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YugantLibrary.ParkingOrderGame
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        [SerializeField] Transform levelContainer, popUpContainer, ParticleEffectContainer;
        [SerializeField] ParticleSystem carCrashParticle;
        [SerializeField] int totalLevels = 2;
        public bool isLevelDesigningInProgress;
        [SerializeField] LevelHandler currLevelHandler;
        public int CurrLevelNum { get; private set; } = 1;

        private void Awake()
        {
            CreateSingleton();
        }

        private void Start()
        {
            CreateLevel();
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

        public Transform GetParticleEffectContainer()
        {
            return ParticleEffectContainer;
        }

        public void CreateLevel()
        {
            ClearParticleEffectContainer();
            ClearLevelContainer();
            ResetPopUp();
            GameObject level = Resources.Load<GameObject>($"Levels/Level_{CurrLevelNum}");
            Debug.Log("Resource Level " + level.name);
            GameObject createdLevel = Instantiate(level, levelContainer);
            UI_Handler.Instance.SetLevelNumber(CurrLevelNum);
            currLevelHandler = createdLevel.GetComponent<LevelHandler>();
        }

        void ClearLevelContainer()
        {
            if (levelContainer.childCount > 0)
            {
                for (int i = levelContainer.childCount - 1; i >= 0; i--)
                {
                    Destroy(levelContainer.GetChild(i).gameObject);
                }
            }
        }

        public void OnNext()
        {
            if (CurrLevelNum >= totalLevels)
            {
                CurrLevelNum = 1;
            }
            else
            {
                CurrLevelNum++;
            }

            Debug.Log("CurrLevelNum " + CurrLevelNum);
            CreateLevel();
        }

        private void ResetPopUp()
        {
            for (int i = popUpContainer.childCount - 1; i >= 0; i--)
            {
                popUpContainer.GetChild(i).gameObject.SetActive(false);
            }
        }

        void ClearParticleEffectContainer()
        {
            for (int i = ParticleEffectContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(ParticleEffectContainer.GetChild(i).gameObject);
            }
        }

        public ParticleSystem GetCarCrashParticleEffect()
        {
            return carCrashParticle;
        }
    }

}

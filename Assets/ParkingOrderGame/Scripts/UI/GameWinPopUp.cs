using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace YugantLibrary.ParkingOrderGame
{
    public class GameWinPopUp : MonoBehaviour
    {
        [SerializeField] Transform mainPopUpTrans;
        [SerializeField] Button nextButton, restartButton;
        [SerializeField] float timeToAnimatePopUp = 0.75f;

        private void Awake()
        {
            restartButton.onClick.AddListener(() =>
            {
                UI_Handler.Instance.OnClickRestart();
            });

            nextButton.onClick.AddListener(() =>
            {
                UI_Handler.Instance.OnClickNext();
            });
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(Init));
        }

        private IEnumerator Init()
        {
            mainPopUpTrans.localScale = Vector3.zero;
            yield return new WaitForSeconds(0.1f);
            mainPopUpTrans.DOScale(Vector3.one, timeToAnimatePopUp);
        }
    }
}

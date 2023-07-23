using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YugantLibrary.ParkingOrderGame
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance;
        public LayerMask carLayerMask;
        [SerializeField] CarController currCarSelected;
        public bool canClick { get; set; } = true;
        public int movesRemaining { get; set; }

        private void Awake()
        {
            CreateSingleton();
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

        private void Update()
        {
            if (!canClick)
                return;

            if (Input.GetMouseButtonDown(0) && movesRemaining > 0)
            {
                CarController carController = GetCarOnTouch(Input.mousePosition);

                if (carController != null)
                {
                    carController.OnCarStartMovingEvent?.Invoke();
                    UI_Handler.Instance.SetMovesRemainingText(movesRemaining);
                }
            }
        }

        CarController GetCarOnTouch(Vector2 mousePos)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            CarController carController = null;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, carLayerMask))
            {
                // Check if the clicked object is a car
                carController = hit.collider.GetComponentInParent<CarController>();

                if (carController != null)
                {
                    Debug.Log("Car Clicked : " + carController.gameObject.name);

                }
            }

            return carController;
        }


    }
}

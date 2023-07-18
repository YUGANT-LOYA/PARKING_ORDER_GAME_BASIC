using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YugantLibrary.ParkingOrderGame
{
    public class GameController : MonoBehaviour
    {
       public static GameController instance;

        private void Awake()
        {
            CreateSingleton();
        }

        private void CreateSingleton()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

       
    }
}

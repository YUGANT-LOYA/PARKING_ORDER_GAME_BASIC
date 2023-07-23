using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace YugantLibrary.ParkingOrderGame
{
    public class CarController : MonoBehaviour
    {
        Rigidbody carRbBody;
        LevelHandler levelHandler;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] float lineRendererWidth = 0.5f, carSpeed = 10f, carRotationSpeed = 10f, brakeDeviationAngle = 10f, timeToAppyBrake = 0.2f;
        int currLineIndex = 1;
        public bool IsCarMoving { get; private set; }
        public bool IsCarAtTarget { get; private set; }

        public Action OnCarStartMovingEvent, OnDesinationReachedEvent, OnStartReachedEvent;
        public Action<CarController, Vector3> OnCarCrashEvent;

        public bool isCarCrashed { get; private set; }



        private void OnEnable()
        {
            OnCarStartMovingEvent += CarStartMoving;
            OnStartReachedEvent += CarAtStartLocation;
            OnDesinationReachedEvent += CarAtTargetLocation;
            OnCarCrashEvent += OnCarCrash;
        }

        private void OnDisable()
        {
            OnCarStartMovingEvent -= CarStartMoving;
            OnStartReachedEvent -= CarAtStartLocation;
            OnDesinationReachedEvent -= CarAtTargetLocation;
            OnCarCrashEvent -= OnCarCrash;
        }

        private void Awake()
        {
            carRbBody = GetComponent<Rigidbody>();
            SetUpLinerenderer();
            OnStartReachedEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            if (IsCarMoving && !isCarCrashed)
            {
                FollowPath();
            }
        }

        public void SetLevelHandler(LevelHandler level)
        {
            levelHandler = level;
        }

        void SetUpLinerenderer()
        {
            lineRenderer.startWidth = lineRendererWidth;
            lineRenderer.endWidth = lineRendererWidth;

            SetCarFaceAtStart();
        }

        private void SetCarFaceAtStart()
        {
            transform.position = lineRenderer.GetPosition(0);
        }

        private void FollowPath()
        {
            Vector3 targetPos = lineRenderer.GetPosition(currLineIndex);

            RotateCarTowardsPoint(targetPos);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, carSpeed * Time.deltaTime);


            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                if (!IsCarAtTarget)
                {
                    if (currLineIndex < lineRenderer.positionCount - 1)
                    {
                        currLineIndex++;
                    }
                    else
                    {
                        Debug.Log("Car Reached Destination");
                        OnDesinationReachedEvent?.Invoke();
                    }
                }
                else
                {
                    if (currLineIndex > 0)
                    {
                        currLineIndex--;
                    }
                    else
                    {
                        Debug.Log("Car Reached Start");
                        OnStartReachedEvent?.Invoke();
                    }
                }
            }

        }

        void RotateCarTowardsPoint(Vector3 targetPos)
        {
            Quaternion target;

            if (!IsCarAtTarget)
            {
                target = Quaternion.LookRotation(targetPos - transform.position);
            }
            else
            {
                target = Quaternion.LookRotation(transform.position - targetPos);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, target, carRotationSpeed * Time.deltaTime);
        }

        void CarStartMoving()
        {
            if (IsCarMoving || InputHandler.Instance.movesRemaining <= 0)
                return;


            InputHandler.Instance.movesRemaining--;

            if (InputHandler.Instance.movesRemaining == 0)
            {

            }

            levelHandler.SetIsAnyCarInMovingState(true);
            IsCarMoving = true;
        }

        void CarAtTargetLocation()
        {
            IsCarAtTarget = true;
            currLineIndex = lineRenderer.positionCount - 2;
            carRbBody.velocity = Vector3.zero;
            ParkingAnimation();
        }

        void CarAtStartLocation()
        {
            IsCarAtTarget = false;
            currLineIndex = 1;
            carRbBody.velocity = Vector3.zero;
            ParkingAnimation();
        }

        void ParkingAnimation()
        {
            if (!IsCarAtTarget)
            {
                //Reverse DoTween
                transform.DORotate(new Vector3(transform.rotation.eulerAngles.x - brakeDeviationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), timeToAppyBrake).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(transform.rotation.eulerAngles.x + brakeDeviationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), timeToAppyBrake).OnComplete(() =>
                    {
                        levelHandler.CheckAnswerEvent?.Invoke();
                    });
                });
            }
            else
            {
                //Front DoTween
                transform.DORotate(new Vector3(transform.rotation.eulerAngles.x + brakeDeviationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), timeToAppyBrake).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(transform.rotation.eulerAngles.x - brakeDeviationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), timeToAppyBrake).OnComplete(() =>
                    {
                        levelHandler.CheckAnswerEvent?.Invoke();
                    });
                });
            }

            IsCarMoving = false;
        }

        void OnCarCrash(CarController collidedCarController, Vector3 forceDirection)
        {
            Debug.Log("Car Crashed !");

            IsCarMoving = false;
            InputHandler.Instance.canClick = false;
            Transform particleContainer = GameController.Instance.GetParticleEffectContainer();
            ParticleSystem particleSystem = GameController.Instance.GetCarCrashParticleEffect();
            ParticleSystem carCrashEffect = Instantiate(particleSystem, particleContainer);
            Vector3 particlePos = this.transform.position + (collidedCarController.transform.position - this.transform.position) / 2;
            carCrashEffect.transform.position = particlePos;

            UI_Handler.Instance.ShakeCamera();

            StartCoroutine(nameof(LevelFail));
        }

        //Used in Unity Events in Inspector
        public void CollisionDetected(Collision collision)
        {
            GameObject collidedGameObj = collision.gameObject;
            Debug.Log("Collsion Detected -- " + collidedGameObj);
            CarController carController = collidedGameObj.GetComponentInParent<CarController>();
            Debug.Log("carController : " + carController);
            Debug.Log("Collision Relative Force : " + collision.relativeVelocity.magnitude);
            Vector3 directionOfCollision = collision.GetContact(0).normal;
            Debug.Log("directionOfCollision : " + directionOfCollision);
            OnCarCrashEvent?.Invoke(carController, directionOfCollision);
        }

        IEnumerator LevelFail()
        {
            yield return new WaitForSeconds(1f);
            levelHandler.LevelFailed();
        }
    }
}

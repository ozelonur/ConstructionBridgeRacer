#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.ScriptableObjects;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Managers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerMovementBear : Bear
    {
        #region Serialized Fields

        [Header("Movement Settings")] [SerializeField]
        private PlayerMovementData playerMovementData;

        [Header("Components")] [SerializeField]
        private Transform rotateTransform;

        #endregion

        #region Private Variables

        private Joystick _joystick;
        private NavMeshAgent _navMeshAgent;
        private Transform _stair;
        private bool _canMove;
        private bool _canCheckAngle;
        private bool _canStepComplete;
        private float _speed;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.enabled = false;
        }

        private void Start()
        {
            _joystick = FindObjectOfType<Joystick>();
            _speed = playerMovementData.movementSpeed;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }

            if (GameManager.Instance.IsGamePaused)
            {
                return;
            }

            if (_canCheckAngle)
            {
                if (_stair != null)
                {
                    if (Quaternion.Angle(_stair.rotation, GetRotateTransform().rotation) < 90)
                    {
                        _speed = 0;
                    }

                    else
                    {
                        _speed = playerMovementData.movementSpeed;
                        _canCheckAngle = false;
                    }
                }
            }

            if (_canStepComplete)
            {
                if (_stair != null)
                {
                    if (Quaternion.Angle(_stair.rotation, GetRotateTransform().rotation) > 90)
                    {
                        _speed = 0;
                    }

                    else
                    {
                        _speed = playerMovementData.movementSpeed;
                        _canStepComplete = false;
                    }
                }
            }

            float inputX = _joystick.Direction.x;
            float inputZ = _joystick.Direction.y;

            if (_joystick.Direction.magnitude <= .15f)
            {
                return;
            }


            float joystickMagnitude = _joystick.Direction.magnitude;

            if (joystickMagnitude <= .2f)
            {
                joystickMagnitude = .2f;
            }

            Vector3 destination = new Vector3(inputX, 0, inputZ).normalized *
                                  (_speed * Time.deltaTime * joystickMagnitude);

            transform.Translate(destination, Space.World);

            Vector3 lookDirection = new Vector3(inputX, 0, inputZ);

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            rotateTransform.rotation = Quaternion.Slerp(rotateTransform.rotation, lookRotation,
                playerMovementData.rotationSpeed * Time.deltaTime);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
                Register(CustomEvents.PlayerCanMove, PlayerCanMove);
                Register(CustomEvents.OnStepCompleted, OnStepCompleted);
                Register(CustomEvents.CheckAngleStatus, CheckAngleStatus);
                Register(CustomEvents.OnFinishLine, OnFinishLine);
                Register(CustomEvents.IsStairCompleted, IsStairCompleted);
            }

            else
            {
                UnRegister(CustomEvents.PlayerCanMove, PlayerCanMove);
                UnRegister(GameEvents.OnGameStart, OnGameStart);
                UnRegister(CustomEvents.OnStepCompleted, OnStepCompleted);
                UnRegister(CustomEvents.CheckAngleStatus, CheckAngleStatus);
                UnRegister(CustomEvents.OnFinishLine, OnFinishLine);
                UnRegister(CustomEvents.IsStairCompleted, IsStairCompleted);
            }
        }

        private void IsStairCompleted(object[] args)
        {
            _stair = (Transform)args[0];
            _canStepComplete = true;
        }

        private void OnFinishLine(object[] args)
        {
            rotateTransform.DOLocalRotate(Vector3.zero, .3f).SetEase(Ease.Linear).SetLink(gameObject);
        }

        private void CheckAngleStatus(object[] args)
        {
            _stair = (Transform)args[0];
            _canCheckAngle = true;
        }

        private void OnStepCompleted(object[] args)
        {
            bool status = (bool)args[0];
            _canMove = !status;
        }

        private void OnGameStart(object[] args)
        {
            _canMove = true;
            _navMeshAgent.enabled = true;
        }

        private void PlayerCanMove(object[] args)
        {
            bool status = (bool)args[0];

            _canMove = status;
            _navMeshAgent.enabled = status;
        }

        #endregion

        #region Public Variables

        public Transform GetRotateTransform()
        {
            return rotateTransform;
        }

        #endregion
    }
}
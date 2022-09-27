#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.ScriptableObjects;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerMovementBear : Bear
    {
        #region Serialized Fields

        [Header("Movement Settings")]
        [SerializeField] private PlayerMovementData playerMovementData;

        [Header("Components")] [SerializeField]
        private Transform rotateTransform;

        #endregion

        #region Private Variables

        private Joystick _joystick;
        private NavMeshAgent _navMeshAgent;
        private bool _canMove;


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
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
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
                                  (playerMovementData.movementSpeed * Time.deltaTime * joystickMagnitude);

            transform.Translate(destination, Space.World);

            Vector3 lookDirection = new Vector3(inputX, 0, inputZ);

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection,Vector3.up);

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
            }

            else
            {
                UnRegister(CustomEvents.PlayerCanMove, PlayerCanMove);
                UnRegister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] args)
        {
            _canMove = true;
            _navMeshAgent.enabled = true;
        }

        private void PlayerCanMove(object[] args)
        {
            bool status = (bool) args[0];
            
            _canMove = status;
            _navMeshAgent.enabled = status;
        }

        #endregion
    }
}
#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Ai
{
    public class AiMovementActor : Bear
    {
        #region Serialized Fields
        
        [SerializeField] private BrickType allowedBrickType;

        #endregion

        #region Private Variables

        private BrickManager _brickManager;
        private AiCollectBear _aiCollectBear;
        private NavMeshAgent _navMeshAgent;

        private Transform _targetTransform;
        private Transform _centerTransform;
        private BrickBear _currentBrickBear;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _aiCollectBear = GetComponent<AiCollectBear>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _brickManager = BrickManager.Instance;
            _navMeshAgent.enabled = false;
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
                Register(CustomEvents.SendCentrePoint, GetCentrePoint);
            }

            else
            {
                UnRegister(GameEvents.OnGameStart, OnGameStart);
                UnRegister(CustomEvents.SendCentrePoint, GetCentrePoint);
            }
        }

        private void GetCentrePoint(object[] args)
        {
            _centerTransform = (Transform) args[0];
        }

        private void OnGameStart(object[] args)
        {
            _navMeshAgent.enabled = true;
            ScanCollectable();
        }

        #endregion

        #region Public Methods

        public void ScanCollectable()
        {
            BrickBear brickBear = _brickManager.GetClosestAvailableBrickBear(allowedBrickType, transform.position);

            _navMeshAgent.SetDestination(brickBear != null ? brickBear.transform.position : _centerTransform.position);
        }

        #endregion
    }
}
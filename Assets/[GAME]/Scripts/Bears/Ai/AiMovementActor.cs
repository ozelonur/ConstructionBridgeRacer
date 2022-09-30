#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Bears.Stair;
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
        private Transform _finishLineTransform;
        private BrickBear _currentBrickBear;

        private int _areaCount;
        private bool _canMove;

        #endregion

        #region Public Variables

        public int AreaId;

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
                Register(CustomEvents.GetFinishLine, GetFinishLine);
                Register(CustomEvents.GetAreaCount, GetAreaCount);
                Register(CustomEvents.BotCanMove, BotCanMove);
            }

            else
            {
                UnRegister(GameEvents.OnGameStart, OnGameStart);
                UnRegister(CustomEvents.SendCentrePoint, GetCentrePoint);
                UnRegister(CustomEvents.GetFinishLine, GetFinishLine);
                UnRegister(CustomEvents.GetAreaCount, GetAreaCount);
                UnRegister(CustomEvents.BotCanMove, BotCanMove);
            }
        }

        private void BotCanMove(object[] args)
        {
            bool status = (bool)args[0];
            _navMeshAgent.enabled = status;
            _canMove = status;
        }

        private void GetAreaCount(object[] args)
        {
            _areaCount = (int)args[0];
        }

        private void GetFinishLine(object[] args)
        {
            _finishLineTransform = (Transform)args[0];
        }

        private void GetCentrePoint(object[] args)
        {
            _centerTransform = (Transform)args[0];
        }

        private void OnGameStart(object[] args)
        {
            _navMeshAgent.enabled = true;
            _canMove = true;
            ScanCollectable();
        }

        #endregion

        #region Public Methods

        public void ScanCollectable()
        {
            if (!_canMove)
            {
                return;
            }

            if (_aiCollectBear.count >= 3)
            {
                if (AreaId >= _areaCount - 1)
                {
                    _navMeshAgent.SetDestination(_finishLineTransform.position);
                    return;
                }

                StairBuilderBear stairBuilderBear = StairManager.Instance.GetStair(AreaId);
                _navMeshAgent.SetDestination(stairBuilderBear.GetTargetStairPosition());
                return;
            }

            BrickBear brickBear =
                _brickManager.GetClosestAvailableBrickBear(allowedBrickType, transform.position, AreaId);

            if (brickBear == null)
            {
                Vector3 randomPosition = _centerTransform.position + Random.insideUnitSphere * 10f;
                randomPosition.y = 0f;
                _navMeshAgent.SetDestination(randomPosition);
            }

            else
            {
                _navMeshAgent.SetDestination(brickBear.transform.position);
            }
        }

        #endregion
    }
}
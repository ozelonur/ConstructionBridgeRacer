#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Bears.Stair;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Ai
{
    public class AiMovementActor : Bear
    {
        #region Serialized Fields

        [SerializeField] private BrickType allowedBrickType;
        [Range(0, 10)] [SerializeField] private float minimumSpeed;
        [Range(0, 10)] [SerializeField] private float maximumSpeed;

        #endregion

        #region Private Variables

        private BrickManager _brickManager;
        private AiCollectBear _aiCollectBear;
        private NavMeshAgent _navMeshAgent;

        private Transform _finishLineTransform;
        private StairBuilderBear _currentStair;

        private int _areaCount;
        private bool _canMove;
        private bool _isBrickNull;

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
        
        

        private void Update()
        {
            if (GameManager.Instance.IsGamePaused || !GameManager.Instance.IsGameStarted ||
                GameManager.Instance.IsGameEnded)
            {
                return;
            }

            if (!_isBrickNull)
            {
                return;
            }


            if (_navMeshAgent.stoppingDistance <= 1)
            {
                ScanCollectable();
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
                Register(CustomEvents.GetFinishLine, GetFinishLine);
                Register(CustomEvents.GetAreaCount, GetAreaCount);
                Register(CustomEvents.BotCanMove, BotCanMove);
                Register(CustomEvents.OnStepCompleted, StepCompleted);
            }

            else
            {
                UnRegister(GameEvents.OnGameStart, OnGameStart);
                UnRegister(CustomEvents.GetFinishLine, GetFinishLine);
                UnRegister(CustomEvents.GetAreaCount, GetAreaCount);
                UnRegister(CustomEvents.BotCanMove, BotCanMove);
                UnRegister(CustomEvents.OnStepCompleted, StepCompleted);
            }
        }

        private void StepCompleted(object[] args)
        {
            if ((bool)args[0])
            {
                _navMeshAgent.speed = 0;
            }

            else
            {
                _navMeshAgent.speed = 3;
            }
        }

        private void BotCanMove(object[] args)
        {
            bool status = (bool)args[0];
            _navMeshAgent.enabled = status;
            _canMove = status;

            if (status)
            {
                _navMeshAgent.speed = Random.Range(minimumSpeed, maximumSpeed);
            }
        }

        private void GetAreaCount(object[] args)
        {
            _areaCount = (int)args[0];
        }

        private void GetFinishLine(object[] args)
        {
            _finishLineTransform = (Transform)args[0];
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

                if (_currentStair == null)
                {
                    _currentStair = StairManager.Instance.GetStair(AreaId);
                    _currentStair.SetStairUsing(true);
                }

                _navMeshAgent.SetDestination(_currentStair.GetTargetStairPosition());
                return;
            }

            BrickBear brickBear =
                _brickManager.GetClosestAvailableBrickBear(allowedBrickType, transform.position, AreaId);

            if (brickBear == null)
            {
                _isBrickNull = true;
                Vector3 position = transform.position;

                Vector3 randomPos = position + Random.insideUnitSphere * 3;
                randomPos.y = position.y;

                _navMeshAgent.SetDestination(randomPos);
            }

            else
            {
                _isBrickNull = false;
                _navMeshAgent.SetDestination(brickBear.transform.position);
            }
        }

        #endregion
    }
}
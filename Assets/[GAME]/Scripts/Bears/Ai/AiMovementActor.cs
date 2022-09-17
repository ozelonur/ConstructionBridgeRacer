#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Ai
{
    public class AiMovementActor : Bear
    {
        #region Serialized Fields

        [Range(1, 200)] [SerializeField] private float radius = 1f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private BrickType allowedBrickType;

        #endregion

        #region Private Variables

        private AiCollectBear _aiCollectBear;
        private NavMeshAgent _navMeshAgent;
        private bool _canMove;

        private Transform _targetTransform;
        private Transform _centerTransform;
        private BrickBear _currentBrickBear;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _aiCollectBear = GetComponent<AiCollectBear>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.enabled = false;
            _canMove = false;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }


            if (Mathf.Abs(_navMeshAgent.remainingDistance - _navMeshAgent.stoppingDistance) <= .2f)
            {
                ScanCollectable();
            }

            MoveToTarget();
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
            _canMove = true;
            _navMeshAgent.enabled = true;
        }

        #endregion

        #region Private Methods

        private void ScanCollectable()
        {
            _currentBrickBear = null;

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].TryGetComponent(out BrickBear brickBear);
                if (brickBear == null)
                {
                    continue;
                }

                if (brickBear.isCollected)
                {
                    continue;
                }

                if (brickBear.brickType != allowedBrickType)
                {
                    continue;
                }

                _currentBrickBear = brickBear;

                _targetTransform = _currentBrickBear.transform;

                _canMove = true;
                break;
            }

            if (_currentBrickBear == null)
            {
                if (_aiCollectBear.count <= 0)
                {
                    Vector3 pos = Random.insideUnitSphere * 8;
                    pos.y = 0;
                    _centerTransform.position = pos;

                    _canMove = true;

                    _targetTransform = _centerTransform;
                    print("Target : " + _targetTransform);
                }

                else
                {
                    _targetTransform = _centerTransform;
                    print("Target : " + _targetTransform);
                }
            }
        }

        private void MoveToTarget()
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        #endregion
    }
}
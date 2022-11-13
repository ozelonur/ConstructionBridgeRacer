#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Stair
{
    public class StairBuilderBear : Bear
    {
        #region Serialized Fields

        [Header("Blockers")] [SerializeField] private NavMeshObstacle navMeshObstacle;

        [Header("Configuration")] [SerializeField]
        private float step;

        [SerializeField] private StairBear firstStair;
        [SerializeField] private StairBear lastStair;

        #endregion

        #region Private Variables

        private int index;
        private Vector3 _targetStairPosition;
        private bool _isStairUsing;

        #endregion

        #region Public Variables

        public int stairID;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            SetTargetStairPosition(lastStair.transform.position);
        }

        private void Start()
        {
            StairManager.Instance.AddStair(this);
        }

        #endregion

        #region Public Methods

        public Vector3 GetTargetStairPosition()
        {
            return _targetStairPosition;
        }

        public void SetTargetStairPosition(Vector3 targetStairPosition)
        {
            _targetStairPosition = targetStairPosition;
        }

        public void SetStep()
        {
            Vector3 center = navMeshObstacle.center;

            center = new Vector3(center.x, center.y, center.z + step);
            navMeshObstacle.center = center;
        }

        public void ResetCenter(Vector3 centerPos)
        {
            Vector3 center = navMeshObstacle.center;
            center = new Vector3(center.x, center.y, centerPos.z);
            navMeshObstacle.center = center;
        }

        public bool IsStairUsing()
        {
            return _isStairUsing;
        }

        public void SetStairUsing(bool isStairUsing)
        {
            _isStairUsing = isStairUsing;
        }

        public void CheckIsStairCompleted(StairBear stair, ICollector collector)
        {
            if (stair != lastStair)
            {
                return;
            }

            ResetCenter(firstStair.transform.localPosition);
            if (collector.collectorType == CollectorType.Player)
            {
                Roar(CustomEvents.OnStepCompleted, true);
            }
            collector.SetAreaId();
            collector.SetTarget();
        }
        
        public void NavmeshObstacleStatus(bool status)
        {
            navMeshObstacle.enabled = status;
        }

        #endregion
    }
}
#region Header

// Developed by Onur ÖZEL

#endregion

using System.Linq;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Bears.Stair
{
    public class StairBuilderBear : Bear
    {
        #region Serialized Fields

        [Header("Blockers")] [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private BoxCollider checker;

        [Header("Stairs")] [SerializeField] private GameObject[] stairs;

        [Header("Configuration")] [SerializeField]
        private float step;

        [SerializeField] private Vector3 stairSize;

        #endregion

        #region Private Variables

        private int index;

        #endregion

        #region Public Variables

        public int stairID;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            foreach (GameObject stair in stairs)
            {
                stair.transform.localScale = Vector3.zero;
            }
        }

        private void Start()
        {
            StairManager.Instance.AddStair(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICollector collector)) return;

            switch (collector.collectorType)
            {
                case CollectorType.Player:
                    PlayerBuild(collector);
                    break;
                case CollectorType.Bot:
                    BotBuild(collector);
                    break;
                default:
                    Debug.LogError("Collector Type is not defined");
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void CommonBuild()
        {
            Vector3 center = checker.center;

            center = new Vector3(center.x, center.y, center.z + step);
            checker.center = center;

            navMeshObstacle.center = center;

            stairs[index].SetActive(true);
            stairs[index].transform.DOScale(stairSize, 0.3f)
                .SetEase(Ease.OutBack)
                .SetLink(stairs[index]);
            index++;
        }

        private void PlayerBuild(ICollector collector)
        {
            if (collector.GetCount() <= 0) return;
            collector.SubtractCount(stairs[index].transform);

            CommonBuild();

            if (index < stairs.Length) return;
            checker.enabled = false;
            navMeshObstacle.enabled = false;
        }

        private void BotBuild(ICollector collector)
        {
            if (collector.GetCount() <= 0)
            {
                collector.SetTarget();
                return;
            }

            collector.SubtractCount(stairs[index].transform);

            CommonBuild();

            if (index < stairs.Length) return;
            collector.SetAreaId();
            collector.SetTarget();
            checker.enabled = false;
            navMeshObstacle.enabled = false;
        }

        #endregion

        #region Public Methods

        public Vector3 GetTargetStairPosition()
        {
            return stairs.Last().transform.position;
        }

        #endregion
    }
}
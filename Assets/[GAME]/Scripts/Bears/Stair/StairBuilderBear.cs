#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Interfaces;
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

        #region MonoBehaviour Methods

        private void Awake()
        {
            foreach (GameObject stair in stairs)
            {
                stair.transform.localScale = Vector3.zero;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollector collector))
            {
                if (collector.GetCount() <= 0) return;
                if (index >= stairs.Length)
                {
                    checker.enabled = false;
                    navMeshObstacle.enabled = false;
                    return;
                }

                collector.SubtractCount(stairs[index].transform);
                BuildStair();
            }
        }

        #endregion

        #region Private Methods

        private void BuildStair()
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

        #endregion
    }
}
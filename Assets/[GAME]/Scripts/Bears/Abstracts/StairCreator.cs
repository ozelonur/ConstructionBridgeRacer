#region Header
// Developed by Onur ÖZEL
#endregion

using System.Collections;
using _GAME_.Scripts.Bears.Stair;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Abstracts
{
    public class StairCreator : Bear
    {
        #region Serialized Fields

        [SerializeField] private Transform stairParent;
        [SerializeField] private Vector3 offset;

        #endregion

        #region Private Variables

        private int stairCount;

        #endregion
        
        #region MonoBehaviour Methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(CreateStairs(stairParent, BrickType.Blue, 10));
            }
        }

        #endregion
        
        #region Public Methods

        public IEnumerator CreateStairs(Transform startPointTransform, BrickType brickType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                StairBear stairBear = PoolManager.Instance.GetStair();
                
                stairBear.transform.position = startPointTransform.position + offset * stairCount;
                stairCount++;
                stairBear.InitStair(brickType);

                yield return new WaitForSeconds(.05f);
            }
        }

        #endregion
    }
}
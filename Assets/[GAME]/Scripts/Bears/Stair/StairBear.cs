#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Stair
{
    public class StairBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private Renderer stairRenderer;
        [SerializeField] private Material[] brickColors;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        #endregion

        #region Public Methods

        public void InitStair(BrickType brickType)
        {
            stairRenderer.material = brickColors[(int)brickType];
            transform.DOScale(Vector3.one, .3f)
                .SetEase(Ease.OutBack)
                .SetLink(gameObject);
        }

        public void ReturnToPool()
        {
            transform.localScale = Vector3.zero;
            PoolManager.Instance.StairPool.Release(this);
        }

        #endregion
    }
}
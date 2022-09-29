#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Brick
{
    public class BrickBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private Renderer brickRenderer;

        #endregion

        #region Public Variables

        public BrickType brickType;

        #endregion

        #region Public Variables

        public bool isCollected;
        public Collider collider;
        public int SpawnerId;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            collider = GetComponent<Collider>();
            transform.localScale = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<ICollector>()?.Collect(brickType, this);
        }

        #endregion

        #region Public Methods

        public void BrickCollected()
        {
            Roar(CustomEvents.SpawnBrick, transform, this);
        }

        public void InitBrick(BrickType brick, Material material, int spawnerId)
        {
            brickRenderer.material = material;
            brickType = brick;
            collider.enabled = true;
            isCollected = false;
            SpawnerId = spawnerId;
        }

        public void SetPosition(Vector3 position, bool canAnimate = true)
        {
            transform.position = position;

            if (!canAnimate)
            {
                transform.localScale = Vector3.one;
                return;
            }

            transform.DOScale(Vector3.one, .3f)
                .SetEase(Ease.OutBack)
                .SetLink(gameObject);
        }

        public void ResetBrick()
        {
            Transform brickTransform = transform;
            brickTransform.localScale = Vector3.zero;
            brickTransform.localPosition = Vector3.zero;
            brickTransform.localEulerAngles = Vector3.zero;
            PoolManager.Instance.BrickPool.Release(this);
        }

        #endregion
    }
}
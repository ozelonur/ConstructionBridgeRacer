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

        public Collider collider;
        public int spawnerId;

        public BrickType brickType;
        public bool isCollected;

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

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameComplete, OnGameCompleted);
                Register(CustomEvents.DestroyAllBricks, DestroyAllBricks);
            }

            else
            {
                UnRegister(GameEvents.OnGameComplete, OnGameCompleted);
                UnRegister(CustomEvents.DestroyAllBricks, DestroyAllBricks);
            }
        }

        private void DestroyAllBricks(object[] args)
        {
            PoolManager.Instance.BrickPool.Release(this);
        }

        private void OnGameCompleted(object[] args)
        {
            PoolManager.Instance.BrickPool.Release(this);
        }

        #endregion

        #region Public Methods

        public void BrickCollected()
        {
            if (isCollected)
            {
                return;
            }

            isCollected = true;
            Roar(CustomEvents.SpawnBrick, transform.position, this);
        }

        public void InitBrick(Vector3 spawnPoint,BrickType brick, Material material, int spawnerID)
        {
            isCollected = false;
            Roar(CustomEvents.AddBrickToList, this);
            brickRenderer.material = material;
            brickType = brick;
            collider.enabled = true;
            spawnerId = spawnerID;
            SetPosition(spawnPoint);
        }

        public void SetPosition(Vector3 position, bool canAnimate = true)
        {
            Transform brickTransform = transform;
            brickTransform.position = position;
            brickTransform.localEulerAngles = Vector3.zero;
            transform.DOScale(new Vector3(1, 1.5f, 1), .3f)
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
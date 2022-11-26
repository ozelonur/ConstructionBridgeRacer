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
            print("DestroyAllBricks");
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
            Roar(CustomEvents.SpawnBrick, transform, this);
        }

        public void InitBrick(BrickType brick, Material material, int spawnerID)
        {
            Roar(CustomEvents.AddBrickToList, this);
            brickRenderer.material = material;
            brickType = brick;
            collider.enabled = true;
            spawnerId = spawnerID;
        }

        public void SetPosition(Vector3 position, bool canAnimate = true)
        {
            Transform brickTransform = transform;
            brickTransform.position = position;
            brickTransform.localEulerAngles = Vector3.zero;

            if (!canAnimate)
            {
                transform.localScale = Vector3.one;
                return;
            }

            transform.DOScale(new Vector3(1,1.5f,1), .3f)
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
#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Bears.Stair;
using UnityEngine;
using UnityEngine.Pool;

namespace _GAME_.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Singleton

        public static PoolManager Instance;

        #endregion

        #region Serialized Fields

        [Header("Prefabs")] [SerializeField] private BrickBear brickPrefab;
        [SerializeField] private StairBear stairPrefab;

        [Header("Pool Parent")] [SerializeField]
        private Transform poolParent;

        [SerializeField] private Transform stairsParent;

        #endregion

        #region Public Variables

        public ObjectPool<BrickBear> BrickPool;
        public ObjectPool<StairBear> StairPool;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitPool(out BrickPool, brickPrefab, poolParent, 50, 50);
            InitStairPool(out StairPool, stairPrefab, stairsParent, 50, 50);
        }

        #endregion

        #region Private Methods

        private void InitPool(out ObjectPool<BrickBear> pool, BrickBear prefab, Transform parent,
            int defaultCapacity,
            int maxSize) =>
            pool = new ObjectPool<BrickBear>(() =>
                    Instantiate(prefab, parent),
                poolObject => poolObject.gameObject.SetActive(true),
                poolObject =>
                {
                    poolObject.gameObject.SetActive(false);
                    poolObject.transform.SetParent(parent);
                },
                Destroy,
                false, defaultCapacity, maxSize);

        private void InitStairPool(out ObjectPool<StairBear> pool, StairBear prefab, Transform parent,
            int defaultCapacity,
            int maxSize) =>
            pool = new ObjectPool<StairBear>(() =>
                    Instantiate(prefab, parent),
                poolObject => poolObject.gameObject.SetActive(true),
                poolObject =>
                {
                    poolObject.gameObject.SetActive(false);
                    poolObject.transform.SetParent(parent);
                },
                Destroy,
                false, defaultCapacity, maxSize);

        #endregion

        #region Public Methods

        public BrickBear GetBrick()
        {
            BrickBear brick = BrickPool.Get();

            return brick;
        }

        public StairBear GetStair()
        {
            StairBear stair = StairPool.Get();
            return stair;
        }

        #endregion
    }
}
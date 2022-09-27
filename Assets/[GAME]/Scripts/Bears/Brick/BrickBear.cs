#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Brick
{
    public class BrickBear : Bear
    {
        #region Serialized Fields

        public BrickType brickType;
        [SerializeField] private Material[] brickColors;
        [SerializeField] private Renderer brickRenderer;

        #endregion

        #region Private Variables


        #endregion

        #region Public Variables

        public bool isCollected;
        public Collider collider;

        #endregion
        
        #region MonoBehaviour Methods

        private void Awake()
        {
            brickRenderer.material = brickColors[(int) brickType];
            collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<ICollector>()?.Collect(brickType, this);
        }

        #endregion
    }
}
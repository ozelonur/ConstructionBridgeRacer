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

        [SerializeField] private BrickType brickType;
        [SerializeField] private Material[] brickColors;
        [SerializeField] private Renderer brickRenderer;

        #endregion
        
        #region MonoBehaviour Methods

        private void Awake()
        {
            brickRenderer.material = brickColors[(int) brickType];
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<ICollector>()?.Collect(brickType, this);
        }

        #endregion
    }
}
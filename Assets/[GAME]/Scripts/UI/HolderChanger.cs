#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.UI
{
    public class HolderChanger : Bear
    {
        #region Serialized Fields

        [Header("Holders")] [SerializeField] private Sprite[] holders;
        [SerializeField] private Image holderImage;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
            }

            else
            {
                UnRegister(GameEvents.InitLevel, InitLevel);
            }
        }

        private void InitLevel(object[] args)
        {
            holderImage.sprite = holders[Random.Range(0, holders.Length)];
        }

        #endregion
    }
}
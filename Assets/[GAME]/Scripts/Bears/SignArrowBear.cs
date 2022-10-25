#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class SignArrowBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private Transform arrow;

        #endregion

        #region Private Variables

        private bool _isArrowActive;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            arrow.localScale = Vector3.zero;
            _isArrowActive = false;
        }

        private void Update()
        {
            if (!_isArrowActive)
            {
                return;
            }

            arrow.transform.forward = Vector3.Lerp(arrow.transform.forward, Vector3.forward, .01f);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                UnRegister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] args)
        {
            arrow.DOScale(Vector3.one, .3f)
                .OnComplete(() => _isArrowActive = true)
                .SetEase(Ease.OutBack)
                .SetDelay(.3f)
                .SetLink(arrow.gameObject);
        }

        #endregion
    }
}
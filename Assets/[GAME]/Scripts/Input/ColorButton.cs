using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Input
{
    public class ColorButton : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private BrickType brickType;
        [SerializeField] private Button button;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            button.onClick.AddListener(OnClickColorButton);
        }

        #endregion

        #region Private Methods

        private void OnClickColorButton()
        {
            Roar(CustomEvents.GetBrickType, brickType);
        }

        #endregion
    }
}
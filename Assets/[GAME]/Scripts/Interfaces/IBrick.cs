#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Enums;

namespace _GAME_.Scripts.Interfaces
{
    public interface IBrick
    {
        #region Properties

        public BrickType BrickType { get; set; }

        #endregion

        #region Public Method

        public void Collect(params object[] args);

        #endregion
    }
}
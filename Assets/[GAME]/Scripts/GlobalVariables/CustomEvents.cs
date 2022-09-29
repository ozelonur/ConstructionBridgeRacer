#region Header

// Developed by Onur ÖZEL

#endregion

namespace _GAME_.Scripts.GlobalVariables
{
    /// <summary>
    /// This class is used to store Custom Events that you create.
    /// </summary>
    public class CustomEvents
    {
        public const string PlayerCanMove = nameof(PlayerCanMove);
        public const string GetCameraFollowTransform = nameof(GetCameraFollowTransform);
        public const string SwitchCamera = nameof(SwitchCamera);
        public const string SpawnBrick = nameof(SpawnBrick);
        public const string SignAvailableBricks = nameof(SignAvailableBricks);
        
        // FOR AI
        public const string SendCentrePoint = nameof(SendCentrePoint);
        public const string OnFinishLine = nameof(OnFinishLine);
        
    }
}
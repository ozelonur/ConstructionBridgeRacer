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
        public const string BotCanMove = nameof(BotCanMove);
        public const string GetCameraFollowTransform = nameof(GetCameraFollowTransform);
        public const string SwitchCamera = nameof(SwitchCamera);
        public const string SpawnBrick = nameof(SpawnBrick);
        public const string GetFinishLine = nameof(GetFinishLine);
        public const string GetAreaCount = nameof(GetAreaCount);
        
        // FOR AI
        public const string SendCentrePoint = nameof(SendCentrePoint);
        
        // FOR UI
        public const string ShowCurrency = nameof(ShowCurrency);
        public const string ShowEarnedCurrency = nameof(ShowEarnedCurrency);
        
        // Garage
        public const string GetCar = nameof(GetCar);

    }
}
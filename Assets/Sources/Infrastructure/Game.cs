using Sources.Services.Input;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class Game
    {
        public static IInputSurvice InputSurvice;

        public Game()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                InputSurvice = new StandaloneInputService();
            else
                InputSurvice = new MobileInputService();
        }
    }
}
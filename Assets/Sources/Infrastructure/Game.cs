using Assets.Sources.Services.Input;
using UnityEngine;

namespace Assets.Sources.Infrastructure
{
    internal class Game
    {
        public IInputSurvice InputSurvice;

        public Game()
        {
            if(SystemInfo.deviceType == DeviceType.Desktop)
                InputSurvice = new StandaloneInputService();
            else
                InputSurvice = new MobileInputService();    
        }
    }
}
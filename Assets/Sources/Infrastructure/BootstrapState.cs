using Sources.Infrastructure;
using Sources.Services.Input;
using UnityEngine;

namespace Assets.Sources.Infrastructure
{
    public class BootstrapState : IState
    {
        public void Enter()
        {
            Game.InputSurvice = RegisterInputService();
        }

        public void Exit()
        {

        }

        private IInputSurvice RegisterInputService()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}

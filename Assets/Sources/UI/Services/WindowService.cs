using System;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Pause:
                    _uiFactory.CreatePause();
                    break;
                default:
                    Debug.LogError("Window type is unknown");
                    break;
            }
        }
    }
}
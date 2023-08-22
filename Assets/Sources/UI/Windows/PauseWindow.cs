using UnityEngine;

namespace Sources.UI.Windows
{
    public class PauseWindow : WindowBase
    {
        protected override void Init() => 
            Time.timeScale = 0;

        protected override void Cleanup() => 
            Time.timeScale = 1;
    }
}
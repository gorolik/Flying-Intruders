using Sources.Behaviour.HealthSystem;
using Sources.UI.Factory;

namespace Sources.Behaviour
{
    public class PlayerDie : DieObserver
    {
        private IUIFactory _uiFactory;
        
        public void Construct(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        protected override void Die() => 
            _uiFactory.CreateGameOver();
    }
}
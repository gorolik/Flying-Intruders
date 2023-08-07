namespace Assets.Sources.Infrastructure
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}

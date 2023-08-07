namespace Sources.Infrastructure
{
    public interface IExitableState
    {
        public void Exit();
    }

    public interface IState : IExitableState
    {
        public void Enter();
    }

    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}

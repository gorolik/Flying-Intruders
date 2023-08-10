namespace Sources.Behaviour.HealthSystem
{
    public interface IHealth : IDamagable
    {
        float CurrentValue { get; set; }
        float MaxValue { get; }
    }
}
namespace Assets.Scripts
{
    public interface IEnemyStats
    {
        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        float Damage { get; set; }
    }
}
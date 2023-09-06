namespace OneDay.TowerDefense.Base.Entities
{
    public abstract class AMoveComponent : EntityComponent
    {
        public abstract void SetMovingPaused(bool paused);
    }
}
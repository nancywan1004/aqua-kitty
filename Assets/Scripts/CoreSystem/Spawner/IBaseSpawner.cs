namespace CoreSystem.Spawner
{
    public interface IBaseSpawner
    {
        void SpawnRandom(int spawnNum);
        void ConfigureSpawnerSetting(int level);
    }
}
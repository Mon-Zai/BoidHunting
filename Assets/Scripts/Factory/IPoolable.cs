namespace Pool
{
    public interface IPoolable
    {
        void OnGetFromPool();
        void OnReturnToPool();
    }
}

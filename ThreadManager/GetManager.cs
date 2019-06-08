using ThreadManager.Interfaces;

namespace ThreadManager
{
    public static class GetManager
    {
        public static IManager Manager;

        static GetManager()
        {
            Manager = new Manager();
        }
    }
}

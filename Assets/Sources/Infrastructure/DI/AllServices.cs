namespace Sources.Infrastructure.DI
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => 
            _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TService>(TService service) where TService : IService =>
            Implementation<TService>.Instance = service;

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.Instance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService Instance;
        }

    }
}

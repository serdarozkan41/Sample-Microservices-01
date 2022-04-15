using SM01.Infrastructure.Storages.Local;

namespace SM01.Infrastructure.Storages
{
    public class StorageOptions
    {
        public string Provider { get; set; }

        public LocalOption Local { get; set; }

        public bool UsedLocal()
        {
            return Provider == "Local";
        }

        public bool UsedFake()
        {
            return Provider == "Fake";
        }
    }
}

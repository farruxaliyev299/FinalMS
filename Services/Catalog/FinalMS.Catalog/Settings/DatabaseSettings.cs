namespace FinalMS.Catalog.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
    public string CategoryCollectionName { get; set; }
    public string ProductCollectionName { get; set; }
    public string StoreCollectionName { get; set; }
}

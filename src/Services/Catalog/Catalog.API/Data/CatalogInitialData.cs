using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        // Chequemos si algun producto existe, si existe no seguimos
        // pero si existe haremos el seeding
        if (await session.Query<Product>().AnyAsync())
            return;

    }
}

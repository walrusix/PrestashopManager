using Walrus.PrestashopManager.Domain.Common;
using Walrus.PrestashopManager.Utilities;

namespace Walrus.PrestashopManager.UserWebApi.Services.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}

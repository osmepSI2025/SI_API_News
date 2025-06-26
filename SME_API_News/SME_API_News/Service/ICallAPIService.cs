using SME_API_News.Models;

namespace SME_API_News.Services
{
    public interface ICallAPIService
    {
        Task<string> GetDataApiAsync(MapiInformationModels apiModels, object xdata);

    }
}

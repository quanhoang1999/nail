using Cms.Data.Entities.Identity;
using Cms.Service.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service.Abstracts.Identiy
{
    public interface IFunctionService: IWebServiceBase<Function, string, FunctionViewModel>
    {
    
        IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);
        bool CheckExistedId(string id);

        void UpdateParentId(string sourceId, string targetId, Dictionary<string, int> items);

        void ReOrder(string sourceId, string targetId);
    }
}

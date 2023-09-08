using IssueTrackerSynchronizationService.Dto.RedmineModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerSynchronizationService.Client.Interfaces
{
    /// <summary>
    /// Интерфейс для Redmine
    /// </summary>
    public interface IRedmineClient: IClient<RedmineIssueModel>
    {
    }
}

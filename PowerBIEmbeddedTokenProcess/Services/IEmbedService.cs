using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PowerBIEmbeddedTokenAPI.DTO;

namespace PowerBIEmbeddedTokenAPI.Services
{
    public interface IEmbedService
    {
        EmbedConfig EmbedConfig { get; }

        Task<bool> EmbedReport(string userName, string roles, Guid reportId, Guid workspaceId);
    }
}

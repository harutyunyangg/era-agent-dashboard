using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraAgentDashboard.Application.Interfaces;

public interface IVapiClient
{
    Task<IEnumerable<CallLog>> GetCallLogsAsync(CancellationToken cancellationToken = default);
}
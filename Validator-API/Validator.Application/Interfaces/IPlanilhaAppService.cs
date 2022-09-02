using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Core;

namespace Validator.Application.Interfaces
{
    public interface IPlanilhaAppService
    {
        Task<dynamic> Upload(FileStream objPlanilha);
    }
}

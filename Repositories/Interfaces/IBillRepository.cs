using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;

namespace Repositories.Interfaces
{
    public interface IBillRepository
    {
        public Task InsertBill(Bill billCreate);
    }
}

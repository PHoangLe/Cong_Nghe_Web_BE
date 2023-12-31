using BusinessObjects.DTO.StatisticDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StatisticService
    {
        private readonly ILogger<StatisticService> _logger;
        private readonly StatisticRepository _statisticRepository;

        public StatisticService(StatisticRepository _statisticRepository, ILogger<StatisticService> logger)
        {
            this._statisticRepository = _statisticRepository;
            _logger = logger;
        }

        public List<RevenueStatisticDTO> GetRevenueReport(DateTime? fromDate, DateTime? toDate, string reportType)
        {
            DateTime now = DateTime.Now;
            return _statisticRepository.GetRevenueReport(fromDate.GetValueOrDefault(now.AddDays(-30)), toDate.GetValueOrDefault(now), reportType);
        }

        public GeneralStatisticDTO GetGeneralReport(DateTime? fromDate, DateTime? toDate)
        {
            DateTime now = DateTime.Now;
            return _statisticRepository.GetGeneralReport(fromDate.GetValueOrDefault(now.AddDays(-30)), toDate.GetValueOrDefault(now));
        }
    }
}

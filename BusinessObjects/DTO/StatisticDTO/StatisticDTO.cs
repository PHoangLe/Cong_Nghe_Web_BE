using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.StatisticDTO
{
    public record StatisticDTO
    {
        public DateTime ReportDate { get; set; }
        public long TotalRevenue { get; set; }

        public StatisticDTO(DateTime report_date, long total_revenue)
        {
            ReportDate = report_date;
            TotalRevenue = total_revenue;
        }
    }
}

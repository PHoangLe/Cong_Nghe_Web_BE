using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.StatisticDTO
{
    public record RevenueStatisticDTO
    {
        public DateTime ReportDate { get; set; }
        public long TotalRevenue { get; set; }

        public RevenueStatisticDTO(DateTime report_date, long total_revenue)
        {
            ReportDate = report_date;
            TotalRevenue = total_revenue;
        }
    }
    public record GeneralStatisticDTO
    {
        public long servingCount { get; set; }
        public long customerCount { get; set; }
        public decimal restaurantRating { get; set; }
        public long feedbackCount { get; set; }

        public GeneralStatisticDTO(long servingCount, long customerCount, decimal restaurantRating, long feedbackCount)
        {
            this.servingCount = servingCount;
            this.customerCount = customerCount;
            this.restaurantRating = restaurantRating;
            this.feedbackCount = feedbackCount;
        }
    }
}

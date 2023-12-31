using BusinessObjects.DataModels;
using BusinessObjects.DTO.StatisticDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class StatisticRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<StatisticRepository> _logger;
        private readonly string connectionString;

        public StatisticRepository(Menu_minder_dbContext context, ILogger<StatisticRepository> logger)
        {
            _context = context;
            _logger = logger;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        

        public List<RevenueStatisticDTO> GetRevenueReport(DateTime fromDate, DateTime toDate, string reportType)
        {
            string functionName = "generate_report";
            List<RevenueStatisticDTO> result = new List<RevenueStatisticDTO>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(functionName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("fromdate", fromDate);
                    command.Parameters.AddWithValue("todate", toDate);
                    command.Parameters.AddWithValue("granularity", reportType);

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime reportDate = (DateTime) reader["report_date"];
                                long totalRevenue = (long) reader["total_revenue"];

                                result.Add(new RevenueStatisticDTO(reportDate, totalRevenue));
                            }
                        }
                    }
                }
            }
            return result;
        }

        public GeneralStatisticDTO GetGeneralReport(DateTime fromDate, DateTime toDate)
        {
            string functionName = "general_statistic";
            GeneralStatisticDTO result = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(functionName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_from_date", fromDate);
                    command.Parameters.AddWithValue("p_to_date", toDate);

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                long servingCount = (long)reader["serving_count"];
                                long customerCount = (long)reader["customer_count"];
                                decimal restaurantRating = (decimal)reader["restaurant_rating"];
                                long feedbackCount = (long)reader["feedback_count"];

                                result = new GeneralStatisticDTO(servingCount, customerCount, restaurantRating, feedbackCount);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

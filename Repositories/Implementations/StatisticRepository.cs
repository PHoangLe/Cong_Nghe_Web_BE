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

        

        public List<StatisticDTO> GetRevenueReport(DateTime fromDate, DateTime toDate, string reportType)
        {
            string functionName = "generate_report";
            List<StatisticDTO> result = new List<StatisticDTO>();

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

                                result.Add(new StatisticDTO(reportDate, totalRevenue));
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

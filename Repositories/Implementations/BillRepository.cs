using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.BillDTO;
using BusinessObjects.DTO.StatisticDTO;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<IBillRepository> _logger;
        private readonly IMapper _mapper;
        private readonly string connectionString;


        public BillRepository(Menu_minder_dbContext context, ILogger<IBillRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<BillResultDto>> GetAllBill()
        {
            List<BillResultDto> data = new List<BillResultDto>();
            try
            {
                data = await _context.Bills
                    .OrderByDescending(bill => bill.CreatedAt)
                    .Select(bill => _mapper.Map<BillResultDto>(bill))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task InsertBill(Bill billCreate)
        {
            try
            {
                this._context.Bills.Add(billCreate);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<BillDetailDto> GetBillDetails(int servingId)
        {
            string functionName = "get_bill_detail";
            List<BillDetailDto> result = new List<BillDetailDto>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(functionName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("p_serving_id", servingId);

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int foodId = (int) reader["food_id"];
                                string name = (string) reader["name"];
                                string image = (string) reader["image"];
                                int quantity = (int) reader["quantity"];
                                string foodOrderStatus = (string) reader["food_order_status"];
                                int price = (int) reader["price"];
                                DateTime updateAt = (DateTime) reader["update_at"];

                                result.Add(new BillDetailDto(foodId, name, image, quantity, price, foodOrderStatus, updateAt));
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Domain.Infra
{
    public class BudgetRepository
    {
        private readonly string _connectionString = "server=wa-db-01.cwwhxvtrxmqx.us-east-1.rds.amazonaws.com,1433;user id=wassist;password=weddingassistfiap2017;database=db_wedding_assist";

        public int SaveBudget(AuctionBudget budget)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[budget-newbudget]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Budget data
                    cmd.Parameters.AddWithValue("@fk_couple", budget.CoupleId);
                    cmd.Parameters.AddWithValue("@fk_bidPriority", DBNull.Value);
                    cmd.Parameters.AddWithValue("@duration", 0); // CHANGE PROP TYPE
                    cmd.Parameters.AddWithValue("@maxAmount", 0);
                    cmd.Parameters.Add("@bdtId", SqlDbType.Int).Direction = ParameterDirection.Output;



                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Budget");

                    int budgetId = Convert.ToInt32(cmd.Parameters["@bdtId"].Value);

                    foreach(var budgetService in budget.Services)
                    {
                        SaveBudgetService(budgetId, budgetService);
                    }

                    int auctionId = SaveAuction(budgetId, budget.Duration);

                    return auctionId; //check here later
                }
            }
        }

        public List<AuctionBudget> GetBudgetsByFiance(int id)
        {
            List<AuctionBudget> budgets = new List<AuctionBudget>();


            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[Auction-Select-GetAllAuctionsByFiance]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@coupleId", id);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuctionBudget newBudget = new AuctionBudget();



                            budgets.Add(newBudget);
                        }
                    }
                    conn.Close();
                }
            }
            return budgets;
        }

        private void SaveBudgetService(int budgetId, BudgetService budgetService)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[budget-service-new-service]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //BudgetService data
                    cmd.Parameters.AddWithValue("@fk_budgetid", budgetId);
                    cmd.Parameters.AddWithValue("@fk_serviceid", (int)budgetService.ServiceType);
                    cmd.Parameters.AddWithValue("@maxAmount", 0);
                    cmd.Parameters.Add("@budgetserviceid", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Service");

                    int budgetServiceId = Convert.ToInt32(cmd.Parameters["@budgetserviceid"].Value);

                    foreach(BudgetServiceCategory category in budgetService.Categories)
                    {
                        SaveCategory(budgetServiceId, category);
                    }
                }
            }
        }

        private void SaveCategory(int budgetServiceId, BudgetServiceCategory category)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[budget-category-new-category]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //BudgetServiceCategory data
                    cmd.Parameters.AddWithValue("@fk_budgetService", budgetServiceId);
                    cmd.Parameters.AddWithValue("@fk_category", (int)category.Category);
                    cmd.Parameters.AddWithValue("@maxAmount", 12.34);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Category");

                    int categoryId = Convert.ToInt32(cmd.Parameters["@id"].Value);

                    foreach (BudgetCategoryItem item in category.Items)
                    {
                        SaveItem(categoryId, item);
                    }
                }
            }
        }

        private void SaveItem(int categoryId, BudgetCategoryItem item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[budget-item-newitem]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //BudgetServiceCategory data
                    cmd.Parameters.AddWithValue("@fk_categoryid", categoryId);
                    cmd.Parameters.AddWithValue("@fk_itemid", (int)item.Type);
                    cmd.Parameters.AddWithValue("@itmQuantity", item.PeopleQuantity);
                    cmd.Parameters.AddWithValue("@maxAmount", 0);
                    cmd.Parameters.Add("@itemid", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Item");

                    int itemId = Convert.ToInt32(cmd.Parameters["@itemid"].Value);

                }
            }
        }

        private int SaveAuction(int budgetId, DateTime endDate)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[auction-newAuction]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Auction data
                    cmd.Parameters.AddWithValue("@fk_bdtId", budgetId);
                    cmd.Parameters.AddWithValue("@act_startDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@act_endDate", endDate);
                    cmd.Parameters.Add("@id_act", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Auction");

                    return Convert.ToInt32(cmd.Parameters["@id_act"].Value);

                }
            }
        }
    }

}

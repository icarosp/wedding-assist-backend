using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Infra
{
    public class BidRepository
    {
        private readonly string _connectionString = "server=wa-db-02.cwwhxvtrxmqx.us-east-1.rds.amazonaws.com,1433;user id=wassist;password=weddingassistfiap2017;database=db_wedding_assist";

        public Bid GetBidById(int id)
        {
            Bid bid = new Bid();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-selectBid]", conn))//change proc name
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bid_Id", id);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bid.BidId = (int)reader[0];
                            bid.AuctionId = (int)reader[1];
                            bid.ProviderId = (int)reader[2];
                            bid.BidCreationDate = Convert.ToDateTime(reader[4]);
                        }
                    }
                    conn.Close();

                    bid.Services = GetBidServiceByBidId(bid.BidId);

                    return bid; 
                }
            }
        }

        public List<BidService> GetBidServiceByBidId(int id)
        {
            List<BidService> services = new List<BidService>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-selectBidService]", conn))//change proc name
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bidId", id);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BidService service = new BidService();

                            service.ServiceType = (EService) reader[6];
                            service.Amount = Convert.ToDecimal(reader[1]);
                            service.ServiceId = (int)reader[0];

                            service.Categories = GetBidCategoryByServiceId(service.ServiceId);

                            services.Add(service);
                        }
                    }
                    conn.Close();

                    return services;
                }
            }
        }


        public List<BidServiceCategory> GetBidCategoryByServiceId(int id)
        {
            List<BidServiceCategory> categories = new List<BidServiceCategory>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-selectBidCategory]", conn))//change proc name
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bid_srv_id", id);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BidServiceCategory category = new BidServiceCategory();

                            category.Amount = Convert.ToDecimal(reader[3]);
                            category.Category = (EBudgetServiceCategory) reader[2];
                            category.CategoryId = (int)reader[0]; 

                            category.Items = GetBidItemsByCategoryId(category.CategoryId);


                            categories.Add(category);

                        }
                    }
                    conn.Close();

                    return categories;
                }
            }
        }

        public List<BidItem> GetBidItemsByCategoryId(int id)
        {
            List<BidItem> items = new List<BidItem>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-selectBidItem]", conn))//change proc name
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cat_id", id);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BidItem item = new BidItem();

                            item.BidItemId = (int) reader[0];
                            item.BidItemAmount = Convert.ToDecimal(reader[3]);
                            item.BidItemDescription = Convert.ToString(reader[5]);
                            item.ItemId = (int) reader[9];
                            item.Description = Convert.ToString(reader[14]);
                            item.PeopleQuantity = Convert.ToInt32(reader[10]);

                            items.Add(item);
                        }
                    }
                    conn.Close();

                    return items;
                }
            }
        }


        public int SaveWinnerBid(int bidId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-newBidWinner]", conn))//change proc name
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Budget data
                    cmd.Parameters.AddWithValue("@bidId", bidId);
                    cmd.Parameters.AddWithValue("@bidDate ", DateTime.Now.AddHours(-3));
                    cmd.Parameters.Add("@providerId", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    var providerId = Convert.ToInt32(cmd.Parameters["@providerId"].Value);

                    conn.Close();

                    return providerId; //check here later
                }
            }
        }


        public int SaveBid(Bid bid)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[Bid-Insert-CreateBid]", conn))//change proc name
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Budget data
                    cmd.Parameters.AddWithValue("@auctionId", bid.AuctionId);
                    cmd.Parameters.AddWithValue("@providerId ", bid.ProviderId);
                    cmd.Parameters.AddWithValue("@creationDate", DateTime.Now.AddHours(-3));
                    cmd.Parameters.AddWithValue("@total_amount", bid.TotalAmount());
                    cmd.Parameters.Add("@BidId", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Bid");

                    int bidId = Convert.ToInt32(cmd.Parameters["@BidId"].Value);

                    foreach (var bidService in bid.Services)
                    {
                        SaveBidService(bidId, bidService);
                    }

                    //int auctionId = SaveAuction(budgetId, bid.Duration);

                    return bidId; //check here later
                }
            }
        }

        private void SaveBidService(int bidId, BidService bidService)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-newBidService]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int teste = (int)bidService.ServiceType;

                    //BudgetService data
                    cmd.Parameters.AddWithValue("@fk_bidId", bidId);
                    cmd.Parameters.AddWithValue("@srvId", teste);
                    cmd.Parameters.AddWithValue("@bid_amount", bidService.Amount);
                    cmd.Parameters.Add("@id_bid_serv", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Service");

                    int bidServiceId = Convert.ToInt32(cmd.Parameters["@id_bid_serv"].Value);

                    foreach (var category in bidService.Categories)
                    {
                        SaveCategory(bidServiceId, category);
                    }
                }
            }
        }

        private void SaveCategory(int bidServiceId, BidServiceCategory category)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-newBidCategory]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //BudgetServiceCategory data
                    cmd.Parameters.AddWithValue("@fk_bidServId", bidServiceId);
                    cmd.Parameters.AddWithValue("@fk_catId", (int)category.Category);
                    cmd.Parameters.AddWithValue("@bid_amount", category.Amount);
                    cmd.Parameters.Add("@id_bid_cat", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Category");

                    int categoryId = Convert.ToInt32(cmd.Parameters["@id_bid_cat"].Value);

                    foreach (var item in category.Items)
                    {
                        SaveItem(categoryId, item);
                    }
                }
            }
        }

        private void SaveItem(int categoryId, BidItem item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[bid-newBidItem]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //BudgetServiceCategory data
                    cmd.Parameters.AddWithValue("@fk_bidCatId", categoryId);
                    cmd.Parameters.AddWithValue("@fk_bdtItmId", item.ItemId);
                    cmd.Parameters.AddWithValue("@bid_amount", item.BidItemAmount);
                    cmd.Parameters.AddWithValue("@bid_description", item.BidItemDescription);
                    cmd.Parameters.Add("@id_bid_cat", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Item");

                    int itemId = Convert.ToInt32(cmd.Parameters["@id_bid_cat"].Value);

                }
            }
        }

        public List<AuctionBid> GetAllBidsInAuctionById(int auctionId)
        {
            List<AuctionBid> auctionBids = new List<AuctionBid>();

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("[Bid-Select-GetAllBidsByAuctionId]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@auctionId", auctionId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuctionBid bid = new AuctionBid();

                            bid.BidId = (int)reader[5];
                            bid.ProviderName = Convert.ToString(reader[11]);
                            bid.Amount = Convert.ToDecimal(reader[8]);
                            bid.BidTime = Convert.ToDateTime(reader[9]);
                            if (reader[16] != DBNull.Value)
                                bid.IsWinner = true;


                            auctionBids.Add(bid);
                        }
                    }
                    conn.Close();

                }
            }

            auctionBids = auctionBids.OrderBy(x => x.Amount).ToList();

            //SET POSITIONS
            for (int i = 1; i <= auctionBids.Count; i++) {
                auctionBids[i-1].Position = i;
            }

            return auctionBids;
        }
    }
}

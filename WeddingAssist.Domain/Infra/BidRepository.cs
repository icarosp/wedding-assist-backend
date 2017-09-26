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

                    //BudgetService data
                    cmd.Parameters.AddWithValue("@fk_bidId", bidId);
                    cmd.Parameters.AddWithValue("@fk_srvId", (int)bidService.ServiceType);
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
                            bid.ProviderName = Convert.ToString(reader[12]);
                            bid.Amount = Convert.ToDecimal(reader[8]);
                            bid.BidTime = Convert.ToDateTime(reader[9]);

                            auctionBids.Add(bid);
                        }
                    }
                    conn.Close();

                }
            }

            return auctionBids;
        }





        //
    }
}

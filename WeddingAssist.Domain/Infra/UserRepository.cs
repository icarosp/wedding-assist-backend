using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Enums;

namespace WeddingAssist.Domain.Infra
{
    public class UserRepository
    {
        private readonly string ConnectionString = "server=wa-db-01.cwwhxvtrxmqx.us-east-1.rds.amazonaws.com,1433;user id=wassist;password=weddingassistfiap2017;database=db_wedding_assist";

        public User GetUserByEmail(string email)
        {
            dynamic user = null;
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"SELECT * FROM tb_user usr LEFT JOIN tb_fiance fic ON fic.usr_id = usr.usr_id LEFT JOIN tb_provider prv ON prv.usr_id = usr.usr_id WHERE usr_email = '{email}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader[6] != DBNull.Value)
                            {
                                user = new Fiance();
                                user.FianceId = (int)reader[6];
                                user.Name = (string)reader[7];
                                user.Birth = (DateTime)reader[8];
                                user.Gender = (EGender)reader[9];
                                user.Enable = (bool)reader[10];
                                user.HasNewBid = false;
                            }
                            else
                            {
                                user = new Provider();
                                user.ProviderId = (int)reader[12];
                                user.ProviderName = (string)reader[13];
                                user.Logo = (string)reader[14];
                                user.Enable = (bool)reader[16];
                                user.HasNewBudget = false;
                            }

                            user.Id = (int)reader[0];
                            user.Nickname = (string)reader[1];
                            user.Email = (string)reader[2];
                            user.Phone = (string)reader[3];
                            user.AwsUserId = (string)reader[4];
                            user.RegistrationStatus = (ERegistrationStatus)reader[5];
                        }
                    }
                    conn.Close();

                }
            }
            return user;
        }

        public Fiance GetFianceById(int id)
        {
            Fiance fiance = null;
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"SELECT * FROM tb_user usr INNER JOIN tb_fiance fic ON fic.usr_id = usr.usr_id WHERE usr.usr_id = '{id}'", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fiance = new Fiance();

                            fiance.Id = (int)reader[0];
                            fiance.Nickname = (string)reader[1];
                            fiance.Email = (string)reader[2];
                            fiance.Phone = (string)reader[3];
                            fiance.AwsUserId = (string)reader[4];
                            fiance.RegistrationStatus = (ERegistrationStatus)reader[5];
                            fiance.FianceId = (int)reader[6];
                            fiance.Name = (string)reader[7];
                            fiance.Birth = (DateTime)reader[8];
                            fiance.Gender = (EGender)reader[9];
                            fiance.Enable = (bool)reader[10];
                            fiance.HasNewBid = false;
                        }
                    }
                    conn.Close();

                }
            }
            return fiance;
        }

        public Provider GetProviderById(int id)
        {
            Provider provider = null;
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"SELECT * FROM tb_user usr INNER JOIN tb_provider prv ON prv.usr_id = usr.usr_id WHERE usr.usr_id = '{id}'", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            provider = new Provider();

                            provider.Id = (int)reader[0];
                            provider.Nickname = (string)reader[1];
                            provider.Email = (string)reader[2];
                            provider.Phone = (string)reader[3];
                            provider.AwsUserId = (string)reader[4];
                            provider.RegistrationStatus = (ERegistrationStatus)reader[5];
                            provider.ProviderId = (int)reader[6];
                            provider.ProviderName = (string)reader[7];
                            provider.Logo = (string)reader[8];
                            provider.Enable = (bool)reader[10];
                            provider.HasNewBudget = false;
                        }
                    }
                    conn.Close();

                }
            }
            return provider;
        }

        public bool IsEmailAlreadyRegistered(string email)
        {
            int numberOfUsersWithSameEmail = 0;

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"SELECT count(*) FROM tb_user WHERE usr_email = '{email}'", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numberOfUsersWithSameEmail = (int)reader[0];
                        }
                    }
                    conn.Close();
                }
            }

            if (numberOfUsersWithSameEmail == 0)
                return false;
            return true;

        }

        public void SaveFiance(Fiance fiance)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[User-Insert-SaveFiance]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //User data
                    cmd.Parameters.AddWithValue("@usr_email", fiance.Email);
                    cmd.Parameters.AddWithValue("@usr_nickname", fiance.Nickname);
                    cmd.Parameters.AddWithValue("@usr_phone", fiance.Phone);
                    cmd.Parameters.AddWithValue("@usr_aws_user_id", "AWS_ID_HARDCODE");//fiance.AwsUserId);
                    cmd.Parameters.AddWithValue("@rst_id", 2);//(int)fiance.RegistrationStatus);

                    //Fiance data
                    cmd.Parameters.AddWithValue("@fic_name", fiance.Name);
                    cmd.Parameters.AddWithValue("@fic_birth", fiance.Birth);
                    cmd.Parameters.AddWithValue("@gnd_id", (int)fiance.Gender);
                    cmd.Parameters.AddWithValue("@fic_status", true);//fiance.Enable);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Fiance");

                }
            }
        }

        public void SaveProvider(Provider provider)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[User-Insert-SaveProvider]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //User data
                    cmd.Parameters.AddWithValue("@usr_email", provider.Email);
                    cmd.Parameters.AddWithValue("@usr_nickname", provider.ProviderName);
                    cmd.Parameters.AddWithValue("@usr_phone", provider.Phone);
                    cmd.Parameters.AddWithValue("@usr_aws_user_id", "AWS_ID_HARDCODE");// provider.AwsUserId);
                    cmd.Parameters.AddWithValue("@rst_id", 2);//(int)provider.RegistrationStatus);
                    

                    //Provider data
                    cmd.Parameters.AddWithValue("@prv_name", provider.ProviderName);
                    cmd.Parameters.AddWithValue("@prv_logo", provider.Logo);
                    cmd.Parameters.AddWithValue("@prv_status", true);//provider.Enable);
                    cmd.Parameters.Add("@providerId", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    int providerId = Convert.ToInt32(cmd.Parameters["@providerId"].Value);

                    foreach (var service in provider.Services) {
                        SaveProviderService((int)service, providerId);
                    }

                    if (providerId < 0)
                        throw new Exception("Error to save Provider");

                }
            }
        }

        public void SaveProviderService(int serviceId, int providerId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[User-Insert-SaveProviderService]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@serviceId", serviceId);
                    cmd.Parameters.AddWithValue("@providerId", providerId);
                    cmd.Parameters.Add("@success",SqlDbType.Bit).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if(!(bool) cmd.Parameters["@success"].Value)
                        throw new Exception("Error Saving ProviderService");
                }
            }
        }

        public void ConfirmEmail(string email)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"UPDATE tb_user SET rst_id = {(int)ERegistrationStatus.CONFIRMED} WHERE usr_email = '{email}'", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception($"Error to confirm email {email}");
                }
            }
        }

        public List<Fiance> GetAllFiances()
        {
            List<Fiance> fiances = new List<Fiance>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"SELECT * FROM tb_user usr INNER JOIN tb_fiance fic ON fic.usr_id = usr.usr_id", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Fiance fiance = new Fiance();
                            fiance.Id = (int)reader[0];
                            fiance.Nickname = (string)reader[1];
                            fiance.Email = (string)reader[2];
                            fiance.Phone = (string)reader[3];
                            fiance.AwsUserId = (string)reader[4];
                            fiance.RegistrationStatus = (ERegistrationStatus)reader[5];
                            fiance.FianceId = (int)reader[6];
                            fiance.Name = (string)reader[7];
                            fiance.Birth = (DateTime)reader[8];
                            fiance.Gender = (EGender)reader[9];
                            fiance.Enable = (bool)reader[10];

                            fiances.Add(fiance);
                        }
                    }
                    conn.Close();
                }
            }

            return fiances;
        }

        public List<Provider> GetAllProviders()
        {
            List<Provider> providers = new List<Provider>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand($"SELECT * FROM tb_user usr INNER JOIN tb_provider prv ON prv.usr_id = usr.usr_id", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Provider provider = new Provider();
                            provider.Id = (int)reader[0];
                            provider.Nickname = (string)reader[1];
                            provider.Email = (string)reader[2];
                            provider.Phone = (string)reader[3];
                            provider.AwsUserId = (string)reader[4];
                            provider.RegistrationStatus = (ERegistrationStatus)reader[5];
                            provider.ProviderId = (int)reader[6];
                            provider.ProviderName = (string)reader[7];
                            provider.Logo = (string)reader[8];
                            provider.Enable = (bool)reader[10];

                            providers.Add(provider);
                        }
                    }
                    conn.Close();
                }
            }

            return providers;
        }
    }
}

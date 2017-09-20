using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Domain.Infra
{
    public class UserRepository
    {
        private readonly string ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public User GetUserByEmail(string email)
        {
            User user = null;
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
                            //build Fiance
                        }
                    }
                    conn.Close();

                }
            }
            return user;
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
                    cmd.Parameters.AddWithValue("@usr_aws_user_id", fiance.AwsUserId);
                    cmd.Parameters.AddWithValue("@rst_id", (int)fiance.RegistrationStatus);

                    //Fiance data
                    cmd.Parameters.AddWithValue("@fic_name", fiance.Name);
                    cmd.Parameters.AddWithValue("@fic_birth", fiance.Birth);
                    cmd.Parameters.AddWithValue("@gnd_id", (int)fiance.Gender);
                    cmd.Parameters.AddWithValue("@fic_status", fiance.Enable);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected < 1)
                        throw new Exception("Error to save Fiance");

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WeddingAssist.Domain.Entities;

namespace WeddingAssist.Domain.Infra
{
    public class UserRepository
    {
        private readonly SqlConnection sqlConnection = new SqlConnection("server=wa-db-01.cwwhxvtrxmqx.us-east-1.rds.amazonaws.com,1433;user id=wassist;password=weddingassistfiap2017;database=db_wedding_assist");

        public Fiance GetFianceByEmail(string email)
        {
            Fiance fiance = new Fiance();
            var command = new SqlCommand($"SELECT * FROM tb_user WHERE usr_email = '{email}'", sqlConnection);
            sqlConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    //build Fiance
                }
            }
            sqlConnection.Close();
            return fiance;
        }
    }
}

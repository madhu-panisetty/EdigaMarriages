using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace EdigaMarriages.Models
{
    public class MarriagesDB
    {
        private readonly string connectionString;
        public MarriagesDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MarriagesDB"].ConnectionString;
        }

        public List<MProfile> GetProfiles(int page, int pageSize, Search search)
        {
            List<MProfile> profilesList = new List<MProfile>();
            int rowStart = ((page - 1) * pageSize);

            MProfile profile;
            string queryString = "select * from Profiles where deleted=0";

            if (search != null)
            {
                queryString += " and gender=@gender";
                queryString += @" and surname in 
                                (select surname from surnames s
                                where s.namegroup not in (select namegroup from surnames sn where sn.surname like @surname)
                                )";

            }

            //queryString += " order by profileID OFFSET ((@page - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY"; //sqlserver
            queryString += " order by profileID LIMIT @rowStart ,  @pageSize"; //mysql

            using (MySqlConnection connection =
            new MySqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@page", page);
                command.Parameters.AddWithValue("@rowStart", rowStart);
                command.Parameters.AddWithValue("@pageSize", pageSize);
                if (search != null)
                {
                    command.Parameters.AddWithValue("@gender", search.Gender);
                    command.Parameters.AddWithValue("@surname", search.Surname);
                }

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profile = new MProfile();
                    profile.ProfileID = reader["ProfileID"] == DBNull.Value ? "" : reader["ProfileID"].ToString();
                    profile.Surname = reader["SurName"] == DBNull.Value ? "" : (string)reader["SurName"];
                    profile.Name = reader["Name"] == DBNull.Value ? "" : (string)reader["Name"];
                    profile.FatherName = reader["FatherName"] == DBNull.Value ? "" : (string)reader["FatherName"];

                    profile.DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(reader["DateOfBirth"].ToString());

                    profile.Education = reader["Education"] == DBNull.Value ? "" : (string)reader["Education"];
                    profile.Height = reader["Height"] == DBNull.Value ? "" : (string)reader["Height"];
                    profile.Colour = reader["Colour"] == DBNull.Value ? "" : (string)reader["Colour"];
                    profile.PlaceOfBirth = reader["PlaceOfBirth"] == DBNull.Value ? "" : (string)reader["PlaceOfBirth"];
                    profile.Raasi = reader["Raasi"] == DBNull.Value ? "" : (string)reader["Raasi"];
                    profile.Occupation = reader["Occupation"] == DBNull.Value ? "" : (string)reader["Occupation"];
                    profile.AnnualIncome = reader["AnnualIncome"] == DBNull.Value ? "" : (string)reader["AnnualIncome"];
                    profile.Gender = reader["Gender"] == DBNull.Value ? false : reader.GetBoolean("Gender");
                    profile.Address = reader["Address"] == DBNull.Value ? "" : (string)reader["Address"];
                    profile.Phone = reader["Phone"] == DBNull.Value ? "" : (string)reader["Phone"];
                    profile.Mobile = reader["Mobile"] == DBNull.Value ? "" : (string)reader["Mobile"];
                    profile.Email = reader["Email"] == DBNull.Value ? "" : (string)reader["Email"];

                    profile.Requirement = reader["Requirement"] == DBNull.Value ? "" : (string)reader["Requirement"];
                    profile.Brothers = reader["Brothers"] == DBNull.Value ? "" : (string)reader["Brothers"];
                    profile.Sisters = reader["Sisters"] == DBNull.Value ? "" : (string)reader["Sisters"];
                    profile.MotherSurName = reader["MotherSurName"] == DBNull.Value ? "" : (string)reader["MotherSurName"];
                    profile.BirthStar = reader["BirthStar"] == DBNull.Value ? "" : (string)reader["BirthStar"];


                    profilesList.Add(profile);
                }
                reader.Close();

            }
            return profilesList;
        }

        public int GetProfilesCount(Search search)
        {
            int count;
            string queryString = "select count(*) from Profiles where deleted=0";

            if (search != null)
            {
                queryString += " and gender=@gender";
                queryString += @" and surname in 
                                (select surname from surnames s
                                where s.namegroup not in (select namegroup from surnames sn where sn.surname like @surname)
                                )";
            }

            using (MySqlConnection connection =
            new MySqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                MySqlCommand command = new MySqlCommand(queryString, connection);
                if (search != null)
                {
                    command.Parameters.AddWithValue("@gender", search.Gender);
                    command.Parameters.AddWithValue("@surname", search.Surname);
                }
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.

                connection.Open();
                count = Convert.ToInt16(command.ExecuteScalar());
            }
            return count;
        }

        public MProfile GetProfile(string profileID)
        {
            MProfile profile = new MProfile();
            string queryString = "select * from Profiles where profileID=@profileID";

            using (MySqlConnection connection =
            new MySqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@profileID", profileID);


                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profile.ProfileID = reader["ProfileID"] == DBNull.Value ? "" : reader["ProfileID"].ToString();
                    profile.Surname = reader["SurName"] == DBNull.Value ? "" : (string)reader["SurName"];
                    profile.Name = reader["Name"] == DBNull.Value ? "" : (string)reader["Name"];
                    profile.FatherName = reader["FatherName"] == DBNull.Value ? "" : (string)reader["FatherName"];

                    profile.DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(reader["DateOfBirth"].ToString());

                    profile.Education = reader["Education"] == DBNull.Value ? "" : (string)reader["Education"];
                    profile.Height = reader["Height"] == DBNull.Value ? "" : (string)reader["Height"];
                    profile.Colour = reader["Colour"] == DBNull.Value ? "" : (string)reader["Colour"];
                    profile.PlaceOfBirth = reader["PlaceOfBirth"] == DBNull.Value ? "" : (string)reader["PlaceOfBirth"];
                    profile.Raasi = reader["Raasi"] == DBNull.Value ? "" : (string)reader["Raasi"];
                    profile.Occupation = reader["Occupation"] == DBNull.Value ? "" : (string)reader["Occupation"];
                    profile.AnnualIncome = reader["AnnualIncome"] == DBNull.Value ? "" : (string)reader["AnnualIncome"];
                    profile.Gender = reader["Gender"] == DBNull.Value ? false : reader.GetBoolean("Gender");
                    profile.Address = reader["Address"] == DBNull.Value ? "" : (string)reader["Address"];
                    profile.Phone = reader["Phone"] == DBNull.Value ? "" : (string)reader["Phone"];
                    profile.Mobile = reader["Mobile"] == DBNull.Value ? "" : (string)reader["Mobile"];
                    profile.Email = reader["Email"] == DBNull.Value ? "" : (string)reader["Email"];

                    profile.Requirement = reader["Requirement"] == DBNull.Value ? "" : (string)reader["Requirement"];
                    profile.Brothers = reader["Brothers"] == DBNull.Value ? "" : (string)reader["Brothers"];
                    profile.Sisters = reader["Sisters"] == DBNull.Value ? "" : (string)reader["Sisters"];
                    profile.MotherSurName = reader["MotherSurName"] == DBNull.Value ? "" : (string)reader["MotherSurName"];
                    profile.BirthStar = reader["BirthStar"] == DBNull.Value ? "" : (string)reader["BirthStar"];
                }
                reader.Close();

            }
            return profile;
        }

        public string SaveProfile(MProfile profile, bool createNew)
        {

            string temp;
            string queryString;
            if (createNew)
            {

                queryString = @"Insert into Profiles ( SurName, Name, FatherName, MotherSurName, Brothers, Sisters,
                        Gender, DateOfBirth, Height, Colour, PlaceOfBirth, Raasi, BirthStar,
                        Education, Occupation, AnnualIncome,
                        Requirement, Address, Phone, Mobile, Email) 
                        values( @SurName, @Name, @FatherName, @MotherSurName, @Brothers, @Sisters,
                        @Gender, @DateOfBirth, @Height, @Colour, @PlaceOfBirth, @Raasi, @BirthStar,
                        @Education, @Occupation, @AnnualIncome,
                        @Requirement, @Address, @Phone, @Mobile, @Email); 
                        SELECT LAST_INSERT_ID()";  //SELECT SCOPE_IDENTITY()";
            }
            else
            {
                queryString = @"Update Profiles set SurName=@SurName, Name=@Name, FatherName=@FatherName,
                        MotherSurName=@MotherSurName, Brothers=@Brothers, Sisters=@Sisters,
                        Gender=@Gender, DateOfBirth=@DateOfBirth, Height=@Height,
                        Colour=@Colour, PlaceOfBirth=@PlaceOfBirth, Raasi=@Raasi,
                        BirthStar=@BirthStar, Education=@Education, Occupation=@Occupation,
                        AnnualIncome=@AnnualIncome, Requirement=@Requirement, Address=@Address,
                        Phone=@Phone, Mobile=@Mobile, Email=@Email
                        where ProfileID=@ProfileID";

            }
            using (MySqlConnection connection =
            new MySqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                MySqlCommand command = new MySqlCommand(queryString, connection);
                if (!createNew)
                {
                    command.Parameters.AddWithValue("@ProfileId", profile.ProfileID);
                }
                command.Parameters.AddWithValue("@SurName", profile.Surname);
                command.Parameters.AddWithValue("@Name", profile.Name);
                command.Parameters.AddWithValue("@FatherName", profile.FatherName);
                command.Parameters.AddWithValue("@MotherSurName", profile.MotherSurName);
                command.Parameters.AddWithValue("@Brothers", profile.Brothers);
                command.Parameters.AddWithValue("@Sisters", profile.Sisters);
                command.Parameters.AddWithValue("@Gender", profile.Gender);
                command.Parameters.AddWithValue("@DateOfBirth", profile.DateOfBirth);
                command.Parameters.AddWithValue("@Height", profile.Height);
                command.Parameters.AddWithValue("@Colour", profile.Colour);
                command.Parameters.AddWithValue("@PlaceOfBirth", profile.PlaceOfBirth);
                command.Parameters.AddWithValue("@Raasi", profile.Raasi);
                command.Parameters.AddWithValue("@BirthStar", profile.BirthStar);
                command.Parameters.AddWithValue("@Education", profile.Education);
                command.Parameters.AddWithValue("@Occupation", profile.Occupation);
                command.Parameters.AddWithValue("@AnnualIncome", profile.AnnualIncome);
                command.Parameters.AddWithValue("@Requirement", profile.Requirement);
                command.Parameters.AddWithValue("@Address", profile.Address);
                command.Parameters.AddWithValue("@Phone", profile.Phone);
                command.Parameters.AddWithValue("@Mobile", profile.Mobile);
                command.Parameters.AddWithValue("@Email", profile.Email);
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.

                connection.Open();
                if (createNew)
                {
                    temp = command.ExecuteScalar().ToString();
                }
                else
                {
                    temp = command.ExecuteNonQuery().ToString();
                }

            }
            return temp;
        }

        public List<string> GetSurnames()
        {
            List<string> surnames = new List<string>();
            string queryString = "select surname from Surnames order by surname";

            using (MySqlConnection connection =
            new MySqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                MySqlCommand command = new MySqlCommand(queryString, connection);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    surnames.Add(reader["Surname"] == DBNull.Value ? "" : reader["Surname"].ToString());

                }
                reader.Close();
            }
            return surnames;
        }
    }
}
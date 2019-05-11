using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// database class for per_details table, has register, get, and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class PerDetailsDB
    {

        /// <summary>
        /// registers the data in the per_details table
        /// </summary>
        /// <param name="details">object</param>
        /// <returns>object</returns>
        public static int RegisterDetails(PerDetails details)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                //insert string
                string insertStatement =
                "INSERT INTO PER_DETAILS_TBL " +
                "(PATIENT_ID, BLOOD_TYPE, ORGAN_DONOR, HIV_STATUS, HEIGHT_INCHES, WEIGHT_LBS) " +
                "VALUES (@PatientID, @Type, @Donor, @Status, @Hight, @Weight) ";

                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // adds data to the per_details table
                insertCommmand.Parameters.AddWithValue("@PatientID", details.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Type", details.Type);
                insertCommmand.Parameters.AddWithValue("@Donor", Convert.ToBoolean(details.Donor));
                insertCommmand.Parameters.AddWithValue("@Status", Convert.ToInt32(details.Hiv));
                insertCommmand.Parameters.AddWithValue("@Hight", Convert.ToInt32(details.Hight));
                insertCommmand.Parameters.AddWithValue("@Weight", Convert.ToInt32(details.Weight));

                insertCommmand.ExecuteNonQuery();
                return details.IDNumber;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// gets the data in the per_details table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <returns></returns>
        public static PerDetails GetDetails(int idNumber)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, BLOOD_TYPE, ORGAN_DONOR, HIV_STATUS, HEIGHT_INCHES, WEIGHT_LBS "
                + "FROM PER_DETAILS_TBL "
                + "WHERE PATIENT_ID = @ID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    PerDetails details = new PerDetails();
                    //reads data from the per_details table
                    details.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    details.Type = custReader["BLOOD_TYPE"].ToString();
                    details.Donor = custReader["ORGAN_DONOR"].ToString();
                    details.Hiv = Convert.ToInt32(custReader["HIV_STATUS"]);
                    details.Hight = Convert.ToInt32(custReader["HEIGHT_INCHES"]);
                    details.Weight = Convert.ToInt32(custReader["WEIGHT_LBS"]);

                    return details;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// updates the data in the per_details table
        /// </summary>
        /// <param name="oldDetails">old object</param>
        /// <param name="newDetails">new object</param>
        public static void UpdateDetails(PerDetails oldDetails,
           PerDetails newDetails)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // update string
                string updateStatement =
                "UPDATE PER_DETAILS_TBL SET " +
                "BLOOD_TYPE = @NewType, " +
                "ORGAN_DONOR = @NewDonor, " +
                "HIV_STATUS = @NewStatus, " +
                "HEIGHT_INCHES = @NewHeight, " +
                "WEIGHT_LBS = @NewWeight " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND BLOOD_TYPE = @oldType " +
                "AND ORGAN_DONOR = @oldDonor " +
                "AND HIV_STATUS = @oldStatus " +
                "AND HEIGHT_INCHES = @oldHeight " +
                "AND WEIGHT_LBS = @oldWeight ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                // updates data in the per_details table
                updateCommand.Parameters.AddWithValue("@NewType",
                    newDetails.Type);
                updateCommand.Parameters.AddWithValue("@NewDonor",
                    newDetails.Donor);
                updateCommand.Parameters.AddWithValue("@NewStatus",
                    newDetails.Hiv);
                updateCommand.Parameters.AddWithValue("@NewHeight",
                    newDetails.Hight);
                updateCommand.Parameters.AddWithValue("@NewWeight",
                    newDetails.Weight);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldDetails.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldType",
                    oldDetails.Type);
                updateCommand.Parameters.AddWithValue("@oldDonor",
                    oldDetails.Donor);
                updateCommand.Parameters.AddWithValue("@oldStatus",
                    oldDetails.Hiv);
                updateCommand.Parameters.AddWithValue("@oldHeight",
                    oldDetails.Hight);
                updateCommand.Parameters.AddWithValue("@oldWeight",
                    oldDetails.Weight);

                updateCommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

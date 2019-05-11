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
    /// database class for immunization table, has register, get, delete and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class ImmunizationDB
    {

        /// <summary>
        /// registers the data in the immunization table
        /// </summary>
        /// <param name="immunization">object</param>
        /// <returns>object</returns>
        public static int RegisterImmunization(Immunization immunization)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                //insert string
                string insertStatement =
                "INSERT INTO IMMUNIZATION_TBL " +
                "(PATIENT_ID, IMMUNIZATION, DATE, NOTE) " +
                "VALUES (@PatientID, @Immunization, @Date, @Note) ";

                //adds to the immunization table
                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                insertCommmand.Parameters.AddWithValue("@PatientID", immunization.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Immunization", immunization.GetImmunization);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(immunization.Date));
                insertCommmand.Parameters.AddWithValue("@Note", immunization.Note);

                insertCommmand.ExecuteNonQuery();
                return immunization.ImmunizationID;
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
        /// gets the data in the immunization table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <param name="immunizationID">immunization id</param>
        /// <returns></returns>
        public static Immunization GetImmunization(int idNumber, int immunizationID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, IMMUNIZATION_ID, IMMUNIZATION, DATE, NOTE "
                + "FROM IMMUNIZATION_TBL "
                + "WHERE PATIENT_ID = @ID " +
                "AND IMMUNIZATION_ID = @ImmunizationID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            selectCommand.Parameters.AddWithValue("@ImmunizationID", immunizationID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {
                    //reads data from the immunization table
                    Immunization immunization = new Immunization();
                    immunization.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    immunization.ImmunizationID = Convert.ToInt32(custReader["IMMUNIZATION_ID"]);
                    immunization.GetImmunization = custReader["IMMUNIZATION"].ToString();
                    immunization.Date = custReader["DATE"].ToString();
                    immunization.Note = custReader["NOTE"].ToString();

                    return immunization;
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
        /// deletes information from the immunization table
        /// </summary>
        /// <param name="immunization">object </param>
        /// <param name="immunizationID">immunization id</param>
        public static void DeleteImmunization(Immunization immunization, int immunizationID)
        {
            SqlConnection connection = pchrDB.GetConnection();


            try
            {
                connection.Open();
                // delete string
                string deleteStatement =
                "DELETE FROM IMMUNIZATION_TBL " +
                "WHERE PATIENT_ID = @PatientID " +
                "AND IMMUNIZATION_ID = @ImmunizationID " +
                "AND IMMUNIZATION = @Immunization " +
                "AND DATE = @Date " +
                "AND NOTE = @Note";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                // deletes data from the immunization table
                deleteCommand.Parameters.AddWithValue("@PatientID", Form2.ID);
                deleteCommand.Parameters.AddWithValue("@ImmunizationID", immunizationID);
                deleteCommand.Parameters.AddWithValue("@Immunization", immunization.GetImmunization);
                deleteCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(immunization.Date));
                deleteCommand.Parameters.AddWithValue("@Note", immunization.Note);

                deleteCommand.ExecuteNonQuery();
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
        /// updates the data in the immunization table
        /// </summary>
        /// <param name="oldImmunization">old object</param>
        /// <param name="newImmunization">new object</param>
        public static void UpdateImmunization(Immunization oldImmunization,
            Immunization newImmunization)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // update string
                string updateStatement =
                "UPDATE IMMUNIZATION_TBL SET " +
                "IMMUNIZATION = @NewImmunization, " +
                "DATE = @NewDate, " +
                "NOTE = @NewNote " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND IMMUNIZATION_ID = @oldImmunizationID " +
                "AND IMMUNIZATION = @oldImmunization " +
                "AND DATE = @oldDate " +
                "AND NOTE = @oldNote ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                //updates data in the immunization table
                updateCommand.Parameters.AddWithValue("@NewImmunization",
                    newImmunization.GetImmunization);
                updateCommand.Parameters.AddWithValue("@NewDate",
                    newImmunization.Date);
                updateCommand.Parameters.AddWithValue("@NewNote",
                    newImmunization.Note);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldImmunization.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldImmunizationID",
                    oldImmunization.ImmunizationID);
                updateCommand.Parameters.AddWithValue("@oldImmunization",
                    oldImmunization.GetImmunization);
                updateCommand.Parameters.AddWithValue("@oldDate",
                    oldImmunization.Date);
                updateCommand.Parameters.AddWithValue("@oldNote",
                    oldImmunization.Note);

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

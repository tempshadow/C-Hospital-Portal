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
    /// database class for allergy table, has register, get, delete and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class AllergyDB
    {
        /// <summary>
        /// registers the data in the allergy table
        /// </summary>
        /// <param name="allergy">object</param>
        /// <returns>object</returns>
        public static int RegisterAllergy(Allergy allergy)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                //insert string
                string insertStatement =
                "INSERT INTO ALLERGY_TBL " +
                "(PATIENT_ID, ALLERGEN, ONSET_DATE, NOTE) " +
                "VALUES (@PatientID, @Allergen, @Date, @Note) ";

                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // adds to database
                insertCommmand.Parameters.AddWithValue("@PatientID", allergy.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Allergen", allergy.Allergen);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(allergy.Date));
                insertCommmand.Parameters.AddWithValue("@Note", allergy.Note);

                insertCommmand.ExecuteNonQuery();
                return allergy.AllergyID;
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
        /// gets the data in the allergy table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <param name="allergyID">allergy id</param>
        /// <returns></returns>
        public static Allergy GetAllergy(int idNumber, int allergyID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            //select string
            string selectStatement
                = "SELECT PATIENT_ID, ALLERGY_ID, ALLERGEN, ONSET_DATE, NOTE "
                + "FROM ALLERGY_TBL "
                + "WHERE PATIENT_ID = @ID " +
                "AND ALLERGY_ID = @AllergyID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            selectCommand.Parameters.AddWithValue("@AllergyID", allergyID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    //reads the data from allergy table
                    Allergy allergy = new Allergy();
                    allergy.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    allergy.AllergyID = Convert.ToInt32(custReader["ALLERGY_ID"]);
                    allergy.Allergen = custReader["ALLERGEN"].ToString();
                    allergy.Date = custReader["ONSET_DATE"].ToString();
                    allergy.Note = custReader["NOTE"].ToString();

                    return allergy;
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
        /// deletes information from the allergy table
        /// </summary>
        /// <param name="allergy">object </param>
        /// <param name="allergyID">allergy id</param>
        public static void DeleteAllergy(Allergy allergy, int allergyID)
        {
            SqlConnection connection = pchrDB.GetConnection();


            try
            {
                connection.Open();
                //delete statement
                string deleteStatement =
                "DELETE FROM ALLERGY_TBL " +
                "WHERE PATIENT_ID = @PatientID " +
                "AND ALLERGY_ID = @AllergyID " +
                "AND ALLERGEN = @Allergen " +
                "AND ONSET_DATE = @Date " +
                "AND NOTE = @Note";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                // deletes data in the allergy table
                deleteCommand.Parameters.AddWithValue("@PatientID", Form2.ID);
                deleteCommand.Parameters.AddWithValue("@AllergyID", allergyID);
                deleteCommand.Parameters.AddWithValue("@Allergen", allergy.Allergen);
                deleteCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(allergy.Date));
                deleteCommand.Parameters.AddWithValue("@Note", allergy.Note);

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
        /// updates the data in the allergy table
        /// </summary>
        /// <param name="oldAllergy">old object</param>
        /// <param name="newAllergy">new object</param>
        public static void UpdateAllergy(Allergy oldAllergy,
            Allergy newAllergy)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                //update string
                string updateStatement =
                "UPDATE ALLERGY_TBL SET " +
                "ALLERGEN = @NewAllergy, " +
                "ONSET_DATE = @NewDate, " +
                "NOTE = @NewNote " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND ALLERGY_ID = @oldAllergyID " +
                "AND ALLERGEN = @oldAllergy " +
                "AND ONSET_DATE = @oldDate " +
                "AND NOTE = @oldNote ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                //updates the data in the allergy table
                updateCommand.Parameters.AddWithValue("@NewAllergy",
                    newAllergy.Allergen);
                updateCommand.Parameters.AddWithValue("@NewDate",
                    newAllergy.Date);
                updateCommand.Parameters.AddWithValue("@NewNote",
                    newAllergy.Note);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldAllergy.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldAllergyID",
                    oldAllergy.AllergyID);
                updateCommand.Parameters.AddWithValue("@oldAllergy",
                    oldAllergy.Allergen);
                updateCommand.Parameters.AddWithValue("@oldDate",
                    oldAllergy.Date);
                updateCommand.Parameters.AddWithValue("@oldNote",
                    oldAllergy.Note);

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

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
    /// database class for medication table, has register, get, delete and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class MedicationDB
    {
        /// <summary>
        /// registers the data in the medication table
        /// </summary>
        /// <param name="med">object</param>
        /// <returns>object</returns>
        public static int RegisterMed(Medication med)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // insert string
                string insertStatement =
                "INSERT INTO MEDICATION_TBL " +
                "(PATIENT_ID, MEDICATION, DATE, CHRONIC, NOTE) " +
                "VALUES (@PatientID, @Med, @Date, @Chronic, @Note) ";

                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // adds data to the medication table
                insertCommmand.Parameters.AddWithValue("@PatientID", med.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Med", med.GetMedication);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(med.Date));
                insertCommmand.Parameters.AddWithValue("@Chronic", Convert.ToBoolean(med.Chronic));
                insertCommmand.Parameters.AddWithValue("@Note", med.Note);

                insertCommmand.ExecuteNonQuery();
                return med.MedNumber;
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
        /// gets the data in the medication table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <param name="medID">med id</param>
        /// <returns></returns>
        public static Medication GetMed(int idNumber, int medID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, MED_ID, MEDICATION, DATE, CHRONIC, NOTE "
                + "FROM MEDICATION_TBL "
                + "WHERE PATIENT_ID = @ID " +
                "AND MED_ID = @medID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            selectCommand.Parameters.AddWithValue("@medID", medID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    Medication med = new Medication();
                    // reads data from the medication table
                    med.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    med.MedNumber = Convert.ToInt32(custReader["MED_ID"]);
                    med.GetMedication = custReader["MEDICATION"].ToString();
                    med.Date = custReader["DATE"].ToString();
                    med.Chronic = custReader["CHRONIC"].ToString();
                    med.Note = custReader["NOTE"].ToString();

                    return med;
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
        /// deletes information from the medication table
        /// </summary>
        /// <param name="med">object </param>
        /// <param name="medID">med id</param>
        public static void DeleteMed(Medication med, int medID)
        {
            SqlConnection connection = pchrDB.GetConnection();


            try
            {
                connection.Open();
                // delete string
                string deleteStatement =
                "DELETE FROM MEDICATION_TBL " +
                "WHERE PATIENT_ID = @PatientID " +
                "AND MED_ID = @MedID " +
                "AND MEDICATION = @Med " +
                "AND DATE = @Date " +
                "AND CHRONIC = @Chronic " +
                "AND NOTE = @Note";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                // deletes data from the medication table
                deleteCommand.Parameters.AddWithValue("@PatientID", Form2.ID);
                deleteCommand.Parameters.AddWithValue("@MedID", medID);
                deleteCommand.Parameters.AddWithValue("@Med", med.GetMedication);
                deleteCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(med.Date));
                deleteCommand.Parameters.AddWithValue("@Chronic", med.Chronic);
                deleteCommand.Parameters.AddWithValue("@Note", med.Note);

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
        /// updates the data in the medication table
        /// </summary>
        /// <param name="oldMed">old object</param>
        /// <param name="newMed">new object</param>
        public static void UpdateMed(Medication oldMed,
            Medication newMed)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                //update string
                string updateStatement =
                "UPDATE MEDICATION_TBL SET " +
                "MEDICATION = @NewMed, " +
                "DATE = @NewDate, " +
                "CHRONIC = @NewChronic, " +
                "NOTE = @NewNote " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND MED_ID = @oldMedID " +
                "AND MEDICATION = @oldMed " +
                "AND DATE = @oldDate " +
                "AND CHRONIC = @oldChronic " +
                "AND NOTE = @oldNote ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                // updates data in the medication table
                updateCommand.Parameters.AddWithValue("@NewMed",
                    newMed.GetMedication);
                updateCommand.Parameters.AddWithValue("@NewDate",
                    newMed.Date);
                updateCommand.Parameters.AddWithValue("@NewChronic",
                    newMed.Chronic);
                updateCommand.Parameters.AddWithValue("@NewNote",
                    newMed.Note);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldMed.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldMedID",
                    oldMed.MedNumber);
                updateCommand.Parameters.AddWithValue("@oldMed",
                    oldMed.GetMedication);
                updateCommand.Parameters.AddWithValue("@oldDate",
                    oldMed.Date);
                updateCommand.Parameters.AddWithValue("@oldChronic",
                    oldMed.Chronic);
                updateCommand.Parameters.AddWithValue("@oldNote",
                    oldMed.Note);

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

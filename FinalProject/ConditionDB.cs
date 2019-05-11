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
    /// database class for condition table, has register, get, delete and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class ConditionDB
    {
        /// <summary>
        /// registers the data in the condition table
        /// </summary>
        /// <param name="condition">object</param>
        /// <returns>object</returns>
        public static int RegisterCondition(Condition condition)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // insert string
                string insertStatement =
                "INSERT INTO CONDITION " +
                "(PATIENT_ID, CONDITION, ONSET_DATE, ACUTE, CHRONIC, NOTE) " +
                "VALUES (@PatientID, @Condition, @Date, @Acute, @Chronic, @Note) ";

                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // adds data to the condition table
                insertCommmand.Parameters.AddWithValue("@PatientID", condition.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Condition", condition.GetCondition);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(condition.Date));
                insertCommmand.Parameters.AddWithValue("@Acute", Convert.ToBoolean(condition.Acute));
                insertCommmand.Parameters.AddWithValue("@Chronic", Convert.ToBoolean(condition.Chronic));
                insertCommmand.Parameters.AddWithValue("@Note", condition.Note);

                insertCommmand.ExecuteNonQuery();
                return condition.ConditionNumber;
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
        /// gets the data in the condition table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <param name="conditionID">condition id</param>
        /// <returns></returns>
        public static Condition GetCondition(int idNumber, int conditionID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, CONDITION_ID, CONDITION, ONSET_DATE, ACUTE, CHRONIC, NOTE "
                + "FROM CONDITION "
                + "WHERE PATIENT_ID = @ID " +
                "AND CONDITION_ID = @conditionID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            selectCommand.Parameters.AddWithValue("@conditionID", conditionID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    Condition condition = new Condition();
                    // reads data from the condition table
                    condition.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    condition.ConditionNumber = Convert.ToInt32(custReader["CONDITION_ID"]);
                    condition.GetCondition = custReader["CONDITION"].ToString();
                    condition.Date = custReader["ONSET_DATE"].ToString();
                    condition.Acute = custReader["ACUTE"].ToString();
                    condition.Chronic = custReader["CHRONIC"].ToString();
                    condition.Note = custReader["NOTE"].ToString();

                    return condition;
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
        /// deletes information from the condition table
        /// </summary>
        /// <param name="condition">object </param>
        /// <param name="conditionID">condition id</param>
        public static void DeleteCondition(Condition condition, int conditionID)
        {
            SqlConnection connection = pchrDB.GetConnection();


            try
            {
                connection.Open();
                // delete string
                string deleteStatement =
                "DELETE FROM CONDITION " +
                "WHERE PATIENT_ID = @PatientID " +
                "AND CONDITION_ID = @ConditionID " +
                "AND CONDITION = @Condition " +
                "AND ONSET_DATE = @Date " +
                "AND ACUTE = @Acute " +
                "AND CHRONIC = @Chronic " +
                "AND NOTE = @Note";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                // deletes data from the condition table
                deleteCommand.Parameters.AddWithValue("@PatientID", Form2.ID);
                deleteCommand.Parameters.AddWithValue("@ConditionID", conditionID);
                deleteCommand.Parameters.AddWithValue("@Condition", condition.GetCondition);
                deleteCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(condition.Date));
                deleteCommand.Parameters.AddWithValue("@Acute", condition.Acute);
                deleteCommand.Parameters.AddWithValue("@Chronic", condition.Chronic);
                deleteCommand.Parameters.AddWithValue("@Note", condition.Note);

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
        /// updates the data in the condition table
        /// </summary>
        /// <param name="oldCondition">old object</param>
        /// <param name="newCondition">new object</param>
        public static void UpdateCondition(Condition oldCondition,
            Condition newCondition)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // update string
                string updateStatement =
                "UPDATE CONDITION SET " +
                "CONDITION = @NewCondition, " +
                "ONSET_DATE = @NewDate, " +
                "ACUTE = @NewAcute, " +
                "CHRONIC = @NewChronic, " +
                "NOTE = @NewNote " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND CONDITION_ID = @oldConditionID " +
                "AND CONDITION = @oldCondition " +
                "AND ONSET_DATE = @oldDate " +
                "AND ACUTE = @oldAcute " +
                "AND CHRONIC = @oldChronic " +
                "AND NOTE = @oldNote ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                // updates the data in the condition table
                updateCommand.Parameters.AddWithValue("@NewCondition",
                    newCondition.GetCondition);
                updateCommand.Parameters.AddWithValue("@NewDate",
                    newCondition.Date);
                updateCommand.Parameters.AddWithValue("@NewAcute",
                    newCondition.Acute);
                updateCommand.Parameters.AddWithValue("@NewChronic",
                    newCondition.Chronic);
                updateCommand.Parameters.AddWithValue("@NewNote",
                    newCondition.Note);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldCondition.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldConditionID",
                    oldCondition.ConditionNumber);
                updateCommand.Parameters.AddWithValue("@oldCondition",
                    oldCondition.GetCondition);
                updateCommand.Parameters.AddWithValue("@oldDate",
                    oldCondition.Date);
                updateCommand.Parameters.AddWithValue("@oldAcute",
                    oldCondition.Acute);
                updateCommand.Parameters.AddWithValue("@oldChronic",
                    oldCondition.Chronic);
                updateCommand.Parameters.AddWithValue("@oldNote",
                    oldCondition.Note);

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

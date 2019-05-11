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
    /// database class for test table, has register, get, delete and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class TestDB
    {

        /// <summary>
        /// registers the data in the test table
        /// </summary>
        /// <param name="test">object</param>
        /// <returns>object</returns>
        public static int RegisterTest(Test test)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // insert string
                string insertStatement =
                "INSERT INTO TEST_TBL " +
                "(PATIENT_ID, TEST, RESULT, DATE, NOTE) " +
                "VALUES (@PatientID, @Test, @Result, @Date, @Note) ";
                
                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                //adds data to the test table
                insertCommmand.Parameters.AddWithValue("@PatientID", test.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Test", test.GetTest);
                insertCommmand.Parameters.AddWithValue("@Result", test.Result);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(test.Date));
                insertCommmand.Parameters.AddWithValue("@Note", test.Note);

                insertCommmand.ExecuteNonQuery();
                return test.TestNumber;
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
        /// gets the data in the test table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <param name="testID">test id</param>
        /// <returns></returns>
        public static Test GetTest(int idNumber, int testID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, TEST_ID, TEST, RESULT, DATE, NOTE "
                + "FROM TEST_TBL "
                + "WHERE PATIENT_ID = @ID " +
                "AND TEST_ID = @testID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            selectCommand.Parameters.AddWithValue("@testID", testID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    Test test = new Test();
                    // reads data from the test table
                    test.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    test.TestNumber = Convert.ToInt32(custReader["TEST_ID"]);
                    test.GetTest = custReader["TEST"].ToString();
                    test.Result = custReader["RESULT"].ToString();
                    test.Date = custReader["DATE"].ToString();
                    test.Note = custReader["NOTE"].ToString();
                    
                    return test;
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
        /// deletes information from the test table
        /// </summary>
        /// <param name="test">object </param>
        /// <param name="testID">test id</param>
        public static void DeleteTest(Test test, int testID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            

            try
            {
                connection.Open();
                // delete string
                string deleteStatement =
                "DELETE FROM TEST_TBL " +
                "WHERE PATIENT_ID = @PatientID " +
                "AND TEST_ID = @TestID " +
                "AND TEST = @Test " +
                "AND RESULT = @Result " +
                "AND DATE = @Date " +
                "AND NOTE = @Note";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                // deletes data from the test table
                deleteCommand.Parameters.AddWithValue("@PatientID", Form2.ID);
                deleteCommand.Parameters.AddWithValue("@TestID", testID);
                deleteCommand.Parameters.AddWithValue("@Test", test.GetTest);
                deleteCommand.Parameters.AddWithValue("@Result", test.Result);
                deleteCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(test.Date));
                deleteCommand.Parameters.AddWithValue("@Note", test.Note);

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
        /// updates the data in the test table
        /// </summary>
        /// <param name="oldTest">old object</param>
        /// <param name="newTest">new object</param>
        public static void UpdateTest(Test oldTest,
            Test newTest)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // update string
                string updateStatement =
                "UPDATE TEST_TBL SET " +
                "TEST = @NewTest, " +
                "RESULT = @NewResult, " +
                "DATE = @NewDate, " +
                "NOTE = @NewNote " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND TEST_ID = @oldTestID " +
                "AND TEST = @oldTest " +
                "AND RESULT = @oldResult " +
                "AND DATE = @oldDate " +
                "AND NOTE = @oldNote ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                // updates data in the test table
                updateCommand.Parameters.AddWithValue("@NewTest",
                    newTest.GetTest);
                updateCommand.Parameters.AddWithValue("@NewResult",
                    newTest.Result);
                updateCommand.Parameters.AddWithValue("@NewDate",
                    newTest.Date);
                updateCommand.Parameters.AddWithValue("@NewNote",
                    newTest.Note);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldTest.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldTestID",
                    oldTest.TestNumber);
                updateCommand.Parameters.AddWithValue("@oldTest",
                    oldTest.GetTest);
                updateCommand.Parameters.AddWithValue("@oldResult",
                    oldTest.Result);
                updateCommand.Parameters.AddWithValue("@oldDate",
                    oldTest.Date);
                updateCommand.Parameters.AddWithValue("@oldNote",
                    oldTest.Note);

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

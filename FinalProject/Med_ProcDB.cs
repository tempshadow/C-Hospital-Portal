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
    /// database class for Med_proc table, has register, get, delete and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class Med_ProcDB
    {

        /// <summary>
        /// registers the data in the med_proc table
        /// </summary>
        /// <param name="proc">object</param>
        /// <returns>object</returns>
        public static int RegisterProcedure(Med_Proc proc)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                //insert string
                string insertStatement =
                "INSERT INTO MED_PROC_TBL " +
                "(PATIENT_ID, MED_PROCEDURE, DATE, DOCTOR, NOTE) " +
                "VALUES (@PatientID, @Proc, @Date, @Doctor, @Note) ";

                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // adds data to the med_proc table
                insertCommmand.Parameters.AddWithValue("@PatientID", proc.IDNumber);
                insertCommmand.Parameters.AddWithValue("@Proc", proc.Procedure);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(proc.Date));
                insertCommmand.Parameters.AddWithValue("@Doctor", proc.Doctor);
                insertCommmand.Parameters.AddWithValue("@Note", proc.Note);

                insertCommmand.ExecuteNonQuery();
                return proc.ProcedureNumber;
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
        /// gets the data in the med_proc table
        /// </summary>
        /// <param name="idNumber">patient id</param>
        /// <param name="procID">proc id</param>
        /// <returns></returns>
        public static Med_Proc GetProcedure(int idNumber, int procID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, PROCEDURE_ID, MED_PROCEDURE, DATE, DOCTOR, NOTE "
                + "FROM MED_PROC_TBL "
                + "WHERE PATIENT_ID = @ID " +
                "AND PROCEDURE_ID = @procID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", idNumber);
            selectCommand.Parameters.AddWithValue("@procID", procID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    Med_Proc proc = new Med_Proc();
                    //reads data from the med_proc table
                    proc.IDNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    proc.ProcedureNumber = Convert.ToInt32(custReader["PROCEDURE_ID"]);
                    proc.Procedure = custReader["MED_PROCEDURE"].ToString();
                    proc.Date = custReader["DATE"].ToString();
                    proc.Doctor = custReader["DOCTOR"].ToString();
                    proc.Note = custReader["NOTE"].ToString();

                    return proc;
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
        /// deletes information from the med_proc table
        /// </summary>
        /// <param name="proc">object </param>
        /// <param name="procID">proc id</param>
        public static void DeleteProcedure(Med_Proc proc, int procID)
        {
            SqlConnection connection = pchrDB.GetConnection();


            try
            {
                connection.Open();
                //delete string
                string deleteStatement =
                "DELETE FROM MED_PROC_TBL " +
                "WHERE PATIENT_ID = @PatientID " +
                "AND PROCEDURE_ID = @ProcID " +
                "AND MED_PROCEDURE = @Proc " +
                "AND DATE = @Date " +
                "AND DOCTOR = @Doctor " +
                "AND NOTE = @Note";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                // deletes data from the med_proc table
                deleteCommand.Parameters.AddWithValue("@PatientID", Form2.ID);
                deleteCommand.Parameters.AddWithValue("@ProcID", procID);
                deleteCommand.Parameters.AddWithValue("@Proc", proc.Procedure);
                deleteCommand.Parameters.AddWithValue("@Date", Convert.ToDateTime(proc.Date));
                deleteCommand.Parameters.AddWithValue("@Doctor", proc.Doctor);
                deleteCommand.Parameters.AddWithValue("@Note", proc.Note);

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
        /// updates the data in the med_proc table
        /// </summary>
        /// <param name="oldProc">old object</param>
        /// <param name="newProc">new object</param>
        public static void UpdateProcedure(Med_Proc oldProc,
            Med_Proc newProc)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // update string
                string updateStatement =
                "UPDATE MED_PROC_TBL SET " +
                "MED_PROCEDURE = @NewProc, " +
                "DATE = @NewDate, " +
                "DOCTOR = @NewDoctor, " +
                "NOTE = @NewNote " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND PROCEDURE_ID = @oldProcID " +
                "AND MED_PROCEDURE = @oldProc " +
                "AND DATE = @oldDate " +
                "AND DOCTOR = @oldDoctor " +
                "AND NOTE = @oldNote ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                // updates data in the med_proc table
                updateCommand.Parameters.AddWithValue("@Newproc",
                    newProc.Procedure);
                updateCommand.Parameters.AddWithValue("@NewDate",
                    newProc.Date);
                updateCommand.Parameters.AddWithValue("@NewDoctor",
                    newProc.Doctor);
                updateCommand.Parameters.AddWithValue("@NewNote",
                    newProc.Note);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldProc.IDNumber);
                updateCommand.Parameters.AddWithValue("@oldProcID",
                    oldProc.ProcedureNumber);
                updateCommand.Parameters.AddWithValue("@oldProc",
                    oldProc.Procedure);
                updateCommand.Parameters.AddWithValue("@oldDate",
                    oldProc.Date);
                updateCommand.Parameters.AddWithValue("@oldDoctor",
                    oldProc.Doctor);
                updateCommand.Parameters.AddWithValue("@oldNote",
                    oldProc.Note);

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

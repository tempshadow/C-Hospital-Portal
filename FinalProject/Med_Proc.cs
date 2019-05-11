using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the Med_Proc table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class Med_Proc
    {
        // fields
        int idNumber = Form2.ID;
        int procedureNumber = 0;
        string procedure = "";
        string date = "";
        string doctor = "";
        string note = "";

        /// <summary>
        /// empty constructor
        /// </summary>
        public Med_Proc()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="procedureNumber"></param>
        /// <param name="procedure"></param>
        /// <param name="date"></param>
        /// <param name="doctor"></param>
        /// <param name="note"></param>
        public Med_Proc(int idNumber, int procedureNumber, string procedure, 
            string date, string doctor, string note)
        {
            this.idNumber = idNumber;
            this.procedureNumber = procedureNumber;
            this.procedure = procedure;
            this.date = date;
            this.doctor = doctor;
            this.note = note;
        }

        /// <summary>
        /// patient id property
        /// </summary>
        public int IDNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        /// <summary>
        /// procedure id property
        /// </summary>
        public int ProcedureNumber
        {
            get { return procedureNumber; }
            set { procedureNumber = value; }
        }

        /// <summary>
        /// procedure property
        /// </summary>
        public string Procedure
        {
            get { return procedure; }
            set { procedure = value; }
        }

        /// <summary>
        /// date property
        /// </summary>
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// doctor property
        /// </summary>
        public string Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }

        /// <summary>
        /// note property
        /// </summary>
        public string Note
        {
            get { return note; }
            set { note = value; }
        }
    }
}

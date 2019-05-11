using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the medication table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class Medication
    {
        //fields
        int idNumber = Form2.ID;
        int medNumber = 0;
        string medication = "";
        string date = "";
        string chronic= "";
        string note = "";

        /// <summary>
        /// empty constructor
        /// </summary>
        public Medication()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="medNumber"></param>
        /// <param name="medication"></param>
        /// <param name="date"></param>
        /// <param name="chronic"></param>
        /// <param name="note"></param>
        public Medication(int idNumber, int medNumber, string medication, string date, 
            string chronic, string note)
        {
            this.idNumber = idNumber;
            this.medNumber = medNumber;
            this.medication = medication;
            this.date = date;
            this.chronic = chronic;
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
        /// medication id property
        /// </summary>
        public int MedNumber
        {
            get { return medNumber; }
            set { medNumber = value; }
        }

        /// <summary>
        /// medication property
        /// </summary>
        public string GetMedication
        {
            get { return medication; }
            set { medication = value; }
        }

        /// <summary>
        /// chronic property
        /// </summary>
        public string Chronic
        {
            get { return chronic; }
            set { chronic = value; }
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
        /// note property
        /// </summary>
        public string Note
        {
            get { return note; }
            set { note = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the immunization table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class Immunization
    {
        // fields
        int idNumber = Form2.ID;
        int immunizationID = 0;
        string immunization = "";
        string date = "";
        string note = "";

        /// <summary>
        /// empty constructor
        /// </summary>
        public Immunization()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="immunizationID"></param>
        /// <param name="immunization"></param>
        /// <param name="date"></param>
        /// <param name="note"></param>
        public Immunization(int idNumber, int immunizationID, string immunization, 
            string date, string note)
        {
            this.idNumber = idNumber;
            this.immunizationID = immunizationID;
            this.immunization = immunization;
            this.date = date;
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
        /// immunization id property
        /// </summary>
        public int ImmunizationID
        {
            get { return immunizationID; }
            set { immunizationID = value; }
        }

        /// <summary>
        /// immunization property
        /// </summary>
        public string GetImmunization
        {
            get { return immunization; }
            set { immunization = value; }
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

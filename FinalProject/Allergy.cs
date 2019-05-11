using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the allergy table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class Allergy
    {
        // Global variables
        int idNumber = Form2.ID;
        int allergyID = 0;
        string allergen = "";
        string date = "";
        string note = "";

        /// <summary>
        /// empty constructor
        /// </summary>
        public Allergy()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="allergyID"></param>
        /// <param name="allergen"></param>
        /// <param name="date"></param>
        /// <param name="note"></param>
        public Allergy(int idNumber, int allergyID, string allergen, 
            string date, string note)
        {
            this.idNumber = idNumber;
            this.allergyID = allergyID;
            this.allergen = allergen;
            this.date = date;
            this.note = note;
        }

        /// <summary>
        /// id property
        /// </summary>
        public int IDNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        /// <summary>
        /// allergy id property
        /// </summary>
        public int AllergyID
        {
            get { return allergyID; }
            set { allergyID = value; }
        }

        /// <summary>
        /// allergen property
        /// </summary>
        public string Allergen
        {
            get { return allergen; }
            set { allergen = value; }
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

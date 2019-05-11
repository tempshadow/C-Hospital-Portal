using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the per_details table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class PerDetails
    {
        //fields
        int idNumber = Form2.ID;
        string type = "";
        string donor = "";
        int hiv = 0;
        int hight = 0;
        int weight = 0;

        /// <summary>
        /// empty constructor
        /// </summary>
        public PerDetails()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="type"></param>
        /// <param name="donor"></param>
        /// <param name="hiv"></param>
        /// <param name="hight"></param>
        /// <param name="weight"></param>
        public PerDetails(int idNumber, string type, string donor, int hiv,
            int hight, int weight)
        {
            this.idNumber = idNumber;
            this.type = type;
            this.donor = donor;
            this.hiv = hiv;
            this.hight = hight;
            this.weight = weight;
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
        /// type property
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// donor property
        /// </summary>
        public string Donor
        {
            get { return donor; }
            set { donor = value; }
        }

        /// <summary>
        /// hiv property
        /// </summary>
        public int Hiv
        {
            get { return hiv; }
            set { hiv = value; }
        }

        /// <summary>
        /// height property
        /// </summary>
        public int Hight
        {
            get { return hight; }
            set { hight = value; }
        }

        /// <summary>
        /// weight property
        /// </summary>
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

    }
}

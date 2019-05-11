using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the condition table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class Condition
    {
        //fields
        int idNumber = Form2.ID;
        int conditionNumber = 0;
        string condition = "";
        string date = "";
        string acute = "";
        string chronic = "";
        string note = "";

        /// <summary>
        /// empty constructor
        /// </summary>
        public Condition()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="conditionNumber"></param>
        /// <param name="condition"></param>
        /// <param name="acute"></param>
        /// <param name="date"></param>
        /// <param name="chronic"></param>
        /// <param name="note"></param>
        public Condition(int idNumber, int conditionNumber, string condition, 
            string acute,string date, string chronic, string note)
        {
            this.idNumber = idNumber;
            this.conditionNumber = conditionNumber;
            this.condition = condition;
            this.acute = acute;
            this.chronic = chronic;
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
        /// condition id property
        /// </summary>
        public int ConditionNumber
        {
            get { return conditionNumber; }
            set { conditionNumber = value; }
        }

        /// <summary>
        /// condition property
        /// </summary>
        public string GetCondition
        {
            get { return condition; }
            set { condition = value; }
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

        /// <summary>
        /// acute property
        /// </summary>
        public string Acute
        {
            get { return acute; }
            set { acute = value; }
        }
    }
}

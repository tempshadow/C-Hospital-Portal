using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// class for the test table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
    class Test
    {
        // fields
        int idNumber = Form2.ID;
        int testNumber = 0;
        string test = "";
        string result = "";
        string date = "";
        string note = "";

        /// <summary>
        /// empty constructor
        /// </summary>
        public Test()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="idNumber"></param>
        /// <param name="testNumber"></param>
        /// <param name="test"></param>
        /// <param name="result"></param>
        /// <param name="date"></param>
        /// <param name="note"></param>
        public Test(int idNumber, int testNumber, string test, string result, 
            string date, string note)
        {
            this.idNumber = idNumber;
            this.testNumber = testNumber;
            this.test = test;
            this.result = result;
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
        /// test id property
        /// </summary>
        public int TestNumber
        {
            get { return testNumber; }
            set { testNumber = value; }
        }

        /// <summary>
        /// test property
        /// </summary>
        public string GetTest
        {
            get { return test; }
            set { test = value; }
        }

        /// <summary>
        /// result property
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
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

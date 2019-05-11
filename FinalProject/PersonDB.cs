using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject
{
    /// <summary>
    /// database class for patient and primary care table, has register, get, and update
    /// methods for accessings and manipulating the database
    /// created by: Nigel Mansell
    /// </summary>
    class PersonDB
    {
        // list of person
        public static List<Person> persons = new List<Person>();

        /// <summary>
        /// gets the list of persons
        /// </summary>
        /// <returns></returns>
        public static List<Person> GetPersons()
        {
            // reads from a file path
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");

            
            //xml reader
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreWhitespace = true;
            XmlReader xmlin = XmlReader.Create(filename, settings);
            
            if (xmlin.ReadToDescendant("Person"))
            {
                do
                {
                    // sets variables equal to whats in the xml file
                    Person person = new Person();
                    person.IdNumber = Convert.ToInt32(xmlin["IdNumber"]);
                    xmlin.ReadStartElement("Person");
                    person.UserName = xmlin.ReadElementContentAsString();
                    person.Password = xmlin.ReadElementContentAsString();
                    person.Initials = xmlin.ReadElementContentAsString();
                    person.Title = xmlin.ReadElementContentAsString();
                    person.Gender = xmlin.ReadElementContentAsString();
                    person.WorkPhone = xmlin.ReadElementContentAsString();
                    person.Fax = xmlin.ReadElementContentAsString();
                    person.Email = xmlin.ReadElementContentAsString();
                    person.ContactName = xmlin.ReadElementContentAsString();
                    person.ContactRelation = xmlin.ReadElementContentAsString();
                    person.ContactAddress = xmlin.ReadElementContentAsString();
                    person.ContactState = xmlin.ReadElementContentAsString();
                    person.ContactCity = xmlin.ReadElementContentAsString();
                    person.ContactZip = xmlin.ReadElementContentAsString();
                    person.ContactPhone = xmlin.ReadElementContentAsString();
                    person.ContactMobile = xmlin.ReadElementContentAsString();
                    person.ContactWork = xmlin.ReadElementContentAsString();
                    person.ContactFax = xmlin.ReadElementContentAsString();
                    person.ContactEmail = xmlin.ReadElementContentAsString();
                    person.CareProviderFax = xmlin.ReadElementContentAsString();
                    person.CareProviderEmail = xmlin.ReadElementContentAsString();
                    person.Insurer = xmlin.ReadElementContentAsString();
                    person.InsurancePlan = xmlin.ReadElementContentAsString(); 
                    person.InsuranceNumber = Convert.ToInt32(xmlin.ReadElementContentAsString());
                    person.PrimaryID = Convert.ToInt32(xmlin.ReadElementContentAsString());
                    persons.Add(person);

                }
                while (xmlin.ReadToNextSibling("Person"));
            }
            xmlin.Close();

            return persons;
        }

        /// <summary>
        /// saves to the xml file
        /// </summary>
        /// <param name="persons"></param>
        public static void SavePersons(List<Person> persons)
        {
            //xml file path
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");

            // xml settings
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("     ");

            // xml writer
            XmlWriter xmlOut = XmlWriter.Create(filename, settings);
            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("Persons");

            foreach (Person person in persons)
            {
                // writes t the xml file
                xmlOut.WriteStartElement("Person");
                xmlOut.WriteAttributeString("IdNumber", Convert.ToString(person.IdNumber));
                xmlOut.WriteElementString("Username", person.UserName);
                xmlOut.WriteElementString("Password", person.Password);
                xmlOut.WriteElementString("Initials", person.Initials);
                xmlOut.WriteElementString("Title", person.Title);
                xmlOut.WriteElementString("Gender", person.Gender);
                xmlOut.WriteElementString("WorkPhone", person.WorkPhone);
                xmlOut.WriteElementString("Fax", person.Fax);
                xmlOut.WriteElementString("Email", person.Email);
                xmlOut.WriteElementString("Kin", person.ContactName);
                xmlOut.WriteElementString("KinRelation", person.ContactRelation);
                xmlOut.WriteElementString("KinAddress", person.ContactAddress);
                xmlOut.WriteElementString("KinState", person.ContactState);
                xmlOut.WriteElementString("KinCity", person.ContactCity);
                xmlOut.WriteElementString("KinZip", Convert.ToString(person.ContactZip));
                xmlOut.WriteElementString("KinPhone", person.ContactPhone);
                xmlOut.WriteElementString("KinMobile", person.ContactMobile);
                xmlOut.WriteElementString("KinWork", person.ContactWork);
                xmlOut.WriteElementString("KinFax", person.ContactFax);
                xmlOut.WriteElementString("KinEmail", person.ContactEmail);
                xmlOut.WriteElementString("CareFax", person.CareProviderFax);
                xmlOut.WriteElementString("CareEmail", person.CareProviderEmail);
                xmlOut.WriteElementString("Insurer", person.Insurer);
                xmlOut.WriteElementString("InsurancePlan", person.InsurancePlan);
                xmlOut.WriteElementString("InsuranceNumber", Convert.ToString(person.InsuranceNumber));
                xmlOut.WriteElementString("PrimaryID", Convert.ToString(person.PrimaryID));
                xmlOut.WriteEndElement();
            }
            xmlOut.WriteEndElement();
            xmlOut.Close();
        }

        /// <summary>
        /// registers data to the patient table
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int RegisterPerson(Person person)
        {

            SqlConnection connection = pchrDB.GetConnection();
            
            try
            {
                connection.Open();
                // insert string
                string insertStatement =
                "INSERT INTO PATIENT_TBL " +
                "(PATIENT_ID, LAST_NAME, FIRST_NAME, DATE_Of_BIRTH, ADDRESS_STREET, ADDRESS_CITY, ADDRESS_STATE, " +
                "ADDRESS_ZIP, PHONE_HOME, PHONE_MOBILE, PRIMARY_ID) " +
                "VALUES (@ID, @LastName, @FirstName, @Date, @Street, @City, @State, @Zip, @Home, @Mobile, @Primary)";
                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // inserts data into the patient table
                insertCommmand.Parameters.AddWithValue("@ID", person.IdNumber);
                insertCommmand.Parameters.AddWithValue("@LastName", person.LastName);
                insertCommmand.Parameters.AddWithValue("@FirstName", person.FirstName);
                insertCommmand.Parameters.AddWithValue("@Date", Convert.ToDateTime(person.Date));
                insertCommmand.Parameters.AddWithValue("@Street", person.Street);
                insertCommmand.Parameters.AddWithValue("@City", person.City);
                insertCommmand.Parameters.AddWithValue("@State", person.State);
                insertCommmand.Parameters.AddWithValue("@Zip", person.Zip);
                insertCommmand.Parameters.AddWithValue("@Home", person.HomePhone);
                insertCommmand.Parameters.AddWithValue("@Mobile", person.MobilePhone);
                insertCommmand.Parameters.AddWithValue("@Primary", person.PrimaryID);

                insertCommmand.ExecuteNonQuery();
                
                return person.IdNumber;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object 'dbo.PATIENT_TBL'."))
                {
                    int count = 0;
                    count++;
                    return count;
                }
                else
                {
                    throw ex;
                }
                
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// gets the data from the patient table
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public static Person GetPerson(int personID)
        {
            
            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PATIENT_ID, LAST_NAME, FIRST_NAME, DATE_Of_BIRTH, ADDRESS_STREET, " +
                "ADDRESS_CITY, ADDRESS_STATE, ADDRESS_ZIP, PHONE_HOME, PHONE_MOBILE, PRIMARY_ID "
                + "FROM PATIENT_TBL "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ID", personID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {
                
                    Person person = new Person();
                    // reads data from the patient table
                    person.IdNumber = Convert.ToInt32(custReader["PATIENT_ID"]);
                    person.LastName = custReader["LAST_NAME"].ToString();
                    person.FirstName = custReader["FIRST_NAME"].ToString();
                    person.Date = custReader["DATE_Of_BIRTH"].ToString();
                    person.Street = custReader["ADDRESS_STREET"].ToString();
                    person.City = custReader["ADDRESS_CITY"].ToString();
                    person.State = custReader["ADDRESS_STATE"].ToString();
                    person.Zip = Convert.ToInt32(custReader["ADDRESS_ZIP"]);
                    person.HomePhone = custReader["PHONE_HOME"].ToString();
                    person.MobilePhone = custReader["PHONE_MOBILE"].ToString();
                    person.PrimaryID = Convert.ToInt32(custReader["PRIMARY_ID"]);
                    //fills the variables not in the database
                    person.UserName = Form2.username;
                    person.Password = Form2.password;
                    person.Title = Form2.title;
                    person.Gender = Form2.gender;
                    person.Initials = Form2.initials;
                    person.WorkPhone = Form2.workPhone;
                    person.Fax = Form2.fax;
                    person.Email = Form2.email;
                    person.ContactName = Form2.kinName;
                    person.ContactRelation = Form2.kinRelation;
                    person.ContactAddress = Form2.kinAddress;
                    person.ContactState = Form2.kinState;
                    person.ContactCity = Form2.kinCity;
                    person.ContactZip = Form2.kinZip;
                    person.ContactPhone = Form2.kinPhone;
                    person.ContactMobile = Form2.kinMobile;
                    person.ContactWork = Form2.kinWork;
                    person.ContactFax = Form2.kinFax;
                    person.ContactEmail = Form2.kinEmail;
                    person.CareProviderFax = Form2.careFax;
                    person.CareProviderEmail = Form2.careEmail;
                    person.Insurer = Form2.insurer;
                    person.InsurancePlan = Form2.insurancePlan;
                    person.InsuranceNumber = Form2.insuranceNumber;
                    return person;
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
        /// updates the data in the patient table
        /// </summary>
        /// <param name="oldPerson"></param>
        /// <param name="newPerson"></param>
        public static void UpdatePersonDetails(Person oldPerson, 
            Person newPerson)
        {
            SqlConnection connection = pchrDB.GetConnection();
            
            try
            {
                connection.Open();
                // update string
                string updateStatement =
                "UPDATE PATIENT_TBL SET " +
                "LAST_NAME = @NewLastName, " +
                "FIRST_NAME = @NewFirstName, " +
                "DATE_Of_BIRTH = @NewDateOfBirth " +
                "WHERE PATIENT_ID = @oldPatientID " +
                "AND LAST_NAME = @oldLastName " +
                "AND FIRST_NAME = @oldFirstName " +
                "AND DATE_Of_BIRTH = @oldDateOfBirth ";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                // updates the data in the patient table
                updateCommand.Parameters.AddWithValue("@NewLastName",
                    newPerson.LastName);
                updateCommand.Parameters.AddWithValue("@NewFirstName",
                    newPerson.FirstName);
                updateCommand.Parameters.AddWithValue("@NewDateOfBirth",
                    newPerson.Date);
                updateCommand.Parameters.AddWithValue("@oldPatientID",
                    oldPerson.IdNumber);
                updateCommand.Parameters.AddWithValue("@oldLastName",
                    oldPerson.LastName);
                updateCommand.Parameters.AddWithValue("@oldFirstName",
                    oldPerson.FirstName);
                updateCommand.Parameters.AddWithValue("@oldDateOfBirth",
                    oldPerson.Date);
                
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

        /// <summary>
        /// updates the other portions of the patient table
        /// </summary>
        /// <param name="oldPerson"></param>
        /// <param name="newPerson"></param>
        public static void UpdateContactDetails(Person oldPerson, 
            Person newPerson)
        {
           
                SqlConnection connection = pchrDB.GetConnection();

                try
                {
                    connection.Open();
                    // update string
                    string updateStatement =
                    "UPDATE PATIENT_TBL SET " +
                    "ADDRESS_STREET = @NewStreet, " +
                    "ADDRESS_CITY = @NewCity, " +
                    "ADDRESS_STATE = @NewState, " +
                    "ADDRESS_ZIP = @NewZip, " +
                    "PHONE_HOME = @NewPhone, " +
                    "PHONE_MOBILE = @NewMobile " +
                    "WHERE PATIENT_ID = @oldPatientID " +
                    "AND ADDRESS_STREET = @oldStreet " +
                    "AND ADDRESS_CITY = @oldCity " + 
                    "AND ADDRESS_STATE = @oldState " +
                    "AND ADDRESS_ZIP = @oldZip " +
                    "AND PHONE_HOME = @oldPhone " +
                    "AND PHONE_MOBILE = @oldMobile ";


                SqlCommand updateCommand =
                        new SqlCommand(updateStatement, connection);
                    // updates data in the patient table
                    updateCommand.Parameters.AddWithValue("@NewStreet",
                        newPerson.Street);
                    updateCommand.Parameters.AddWithValue("@NewCity",
                         newPerson.City);
                    updateCommand.Parameters.AddWithValue("@NewState",
                        newPerson.State);
                    updateCommand.Parameters.AddWithValue("@NewZip",
                        newPerson.Zip);
                    updateCommand.Parameters.AddWithValue("@NewPhone",
                        newPerson.HomePhone);
                    updateCommand.Parameters.AddWithValue("@NewMobile",
                        newPerson.MobilePhone);
                    updateCommand.Parameters.AddWithValue("@oldPatientID",
                        oldPerson.IdNumber);
                    updateCommand.Parameters.AddWithValue("@oldStreet",
                        oldPerson.Street);
                    updateCommand.Parameters.AddWithValue("@oldCity",
                        oldPerson.City);
                    updateCommand.Parameters.AddWithValue("@oldState",
                            oldPerson.State);
                    updateCommand.Parameters.AddWithValue("@oldZip",
                            oldPerson.Zip);
                    updateCommand.Parameters.AddWithValue("@oldPhone",
                            oldPerson.HomePhone);
                    updateCommand.Parameters.AddWithValue("@oldMobile",
                            oldPerson.MobilePhone);


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

        /// <summary>
        /// registers the data in the care provider table
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int RegisterCareProvider(Person person)
        {

            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // insert string
                string insertStatement =
                "INSERT INTO PRIMARY_CARE_TBL " +
                "(PRIMARY_ID, NAME_LAST, NAME_FISRT, SPECIALTY, PHONE_OFFICE, PHONE_MOBILE) " +
                "VALUES (@PrimaryID, @NameLast, @NameFirst, @Speciality, @Office, @Mobile)";
                SqlCommand insertCommmand = new SqlCommand(insertStatement, connection);
                // adds data to the primary care table
                insertCommmand.Parameters.AddWithValue("@PrimaryId", person.PrimaryID);
                insertCommmand.Parameters.AddWithValue("@NameLast", person.CareProviderFirst);
                insertCommmand.Parameters.AddWithValue("@NameFirst", person.CareProviderLast);
                insertCommmand.Parameters.AddWithValue("@Speciality", person.CareProviderSpecialty);
                insertCommmand.Parameters.AddWithValue("@Office", person.CareProviderWork);
                insertCommmand.Parameters.AddWithValue("@Mobile", person.CareProviderMobile);

                insertCommmand.ExecuteNonQuery();
                return person.IdNumber;
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
        /// gets the data from the primary care table
        /// </summary>
        /// <param name="primaryID"></param>
        /// <returns></returns>
        public static Person GetPrimary(int primaryID)
        {

            SqlConnection connection = pchrDB.GetConnection();
            // select string
            string selectStatement
                = "SELECT PRIMARY_ID, NAME_LAST, NAME_FISRT, SPECIALTY, PHONE_OFFICE, " +
                "PHONE_MOBILE "
                + "FROM PRIMARY_CARE_TBL "
                + "WHERE PRIMARY_ID = @PrimeID";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@PrimeID", primaryID);
            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {

                    Person person = new Person();
                    // reads the data from the primary care table
                    person.PrimaryID = Convert.ToInt32(custReader["PRIMARY_ID"]);
                    person.CareProviderLast = custReader["NAME_LAST"].ToString();
                    person.CareProviderFirst = custReader["NAME_FISRT"].ToString();
                    person.CareProviderMobile = custReader["PHONE_MOBILE"].ToString();
                    person.CareProviderWork = custReader["PHONE_OFFICE"].ToString();
                    person.CareProviderSpecialty = custReader["SPECIALTY"].ToString();
                    
                    return person;
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
        /// updates the primary care table
        /// </summary>
        /// <param name="oldPerson"></param>
        /// <param name="newPerson"></param>
        public static void UpdateProviderDetails(Person oldPerson,
           Person newPerson)
        {
            SqlConnection connection = pchrDB.GetConnection();

            try
            {
                connection.Open();
                // update string
                string updateStatement = 
                "UPDATE PRIMARY_CARE_TBL SET " +
                "NAME_LAST = @NewLast, " +
                "NAME_FISRT = @NewFirst, " +
                "SPECIALTY = @NewSpeciality, " +
                "PHONE_OFFICE = @NewOffice, " +
                "PHONE_MOBILE = @NewMobile " +
                "WHERE PRIMARY_ID = @oldPrimaryID " +
                "AND NAME_LAST = @oldLast " +
                "AND NAME_FISRT = @oldFirst " +
                "AND SPECIALTY = @oldSpeciality " +
                "AND PHONE_OFFICE = @oldOffice " +
                "AND PHONE_MOBILE = @oldMobile";

                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                //updates data in the primary care table
                updateCommand.Parameters.AddWithValue("@NewLast",
                    newPerson.CareProviderLast);
                updateCommand.Parameters.AddWithValue("@NewFirst",
                    newPerson.CareProviderFirst);
                updateCommand.Parameters.AddWithValue("@NewSpeciality",
                    newPerson.CareProviderSpecialty);
                updateCommand.Parameters.AddWithValue("@NewOffice",
                    newPerson.CareProviderWork);
                updateCommand.Parameters.AddWithValue("@NewMobile",
                    newPerson.CareProviderMobile);
                updateCommand.Parameters.AddWithValue("@oldPrimaryID",
                    oldPerson.PrimaryID);
                updateCommand.Parameters.AddWithValue("@oldLast",
                    oldPerson.CareProviderLast);
                updateCommand.Parameters.AddWithValue("@oldFirst",
                    oldPerson.CareProviderFirst);
                updateCommand.Parameters.AddWithValue("@oldSpeciality",
                    oldPerson.CareProviderSpecialty);
                updateCommand.Parameters.AddWithValue("@oldOffice",
                    oldPerson.CareProviderWork);
                updateCommand.Parameters.AddWithValue("@oldMobile",
                    oldPerson.CareProviderMobile);

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

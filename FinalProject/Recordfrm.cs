using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FinalProject
{
    /// <summary>
    /// form that displays all information from all tables
    /// created by: Nigel Mansell
    /// </summary>
    public partial class Recordfrm : Form
    {
        //field
        private Person person;

        /// <summary>
        /// constructor
        /// </summary>
        public Recordfrm()
        {
            InitializeComponent();
            

        }

        /// <summary>
        /// override the X button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recordfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// sets all the values of all the boxs at the beginning of the form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recordfrm_Load(object sender, EventArgs e)
        {
            person = PersonDB.GetPerson(Form2.ID);
            idBox.Text = person.IdNumber.ToString();
            firstNameBox.Text = person.FirstName;
            userBox.Text = person.UserName;
            lastNameBox.Text = person.LastName;
            initialsBox.Text = person.Initials;
            dateBox.Text = person.Date;
            initialsBox.Text = person.Initials;
            titleBox.Text = person.Title;
            personAddressBox.Text = person.Street;
            personCityBox.Text = person.City;
            personStateBox.Text = person.State;
            personZipBox.Text = person.Zip.ToString();
            personHomePhoneBox.Text = person.HomePhone;
            personMobilePhoneBox.Text = person.MobilePhone;
            personWorkPhoneBox.Text = person.WorkPhone;
            personFaxBox.Text = person.Fax;
            personEmailBox.Text = person.Email;
            kinBox.Text = person.ContactName;
            relationshipBox.Text = person.ContactRelation;
            emergencyAddressBox.Text = person.ContactAddress;
            emergencyStateBox.Text = person.ContactState;
            emergencyCityBox.Text = person.ContactCity;
            emergencyZipBox.Text = person.ContactZip.ToString();
            emergencyPhoneBox.Text = person.ContactPhone;
            emergencyMobileBox.Text = person.ContactMobile;
            emergencyWorkBox.Text = person.ContactWork;
            emergencyFaxBox.Text = person.ContactFax;
            emergencyEmailBox.Text = person.ContactEmail;
            Person provider = PersonDB.GetPrimary(Form2.primaryId);
            providerNameBox.Text = provider.CareProviderFirst + " " + provider.CareProviderLast;
            providerSpecialBox.Text = provider.CareProviderSpecialty;
            providerMobileBox.Text = provider.CareProviderMobile;
            providerWorkBox.Text = provider.CareProviderWork;
            providerFaxBox.Text = person.CareProviderFax;
            providerEmailBox.Text = person.CareProviderEmail;
            insurerBox.Text = person.Insurer;
            insurancePlanBox.Text = person.InsurancePlan;
            insuranceNumberBox.Text = person.InsuranceNumber.ToString();

            pictureBox2.Image = Image.FromFile("C:/Users/nigel/Desktop/FinalProject/FinalProject/Under-construction-sign.jpg");
            this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox3.Image = Image.FromFile("C:/Users/nigel/Desktop/FinalProject/FinalProject/Under-construction-sign.jpg");
            this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            if (person.Gender == "male")
            {
                maleButton.Checked = true;
            }
            else if (person.Gender == "female")
            {
                femaleButton.Checked = true;
            }

            PerDetails details = PerDetailsDB.GetDetails(Form2.ID);
            bloodBox.Text = details.Type;
            if (details.Donor == "True")
                donorBox.Checked = true;
            else
                donorBox.Checked = false;
            if (details.Hiv == 0)
            {
                positiveButton.Checked = true;
                negativeButton.Checked = false;
                unknownButton.Checked = false;
            }
            else if (details.Hiv == 1)
            {
                positiveButton.Checked = false;
                negativeButton.Checked = true;
                unknownButton.Checked = false;
            }
            else if (details.Hiv == 2)
            {
                positiveButton.Checked = false;
                negativeButton.Checked = false;
                unknownButton.Checked = true;
            }

            hightBox.Text = details.Hight.ToString();
            weightBox.Text = details.Weight.ToString();
            

            FillTestList(Form2.ID);
            FillAllergyList(Form2.ID);
            FillImmunizationList(Form2.ID);
            FillProcList(Form2.ID);
            FillMedList(Form2.ID);
            FillConditionList(Form2.ID);

            string path = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, Form2.ID.ToString() + "Picture");
            if (File.Exists(filename))
            {
                pictureBox1.Load(filename);
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
            
            

        }

        /// <summary>
        /// button that changes the users password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changePassButton_Click(object sender, EventArgs e)
        {
            if (oldPassBox.Text != Form2.password)
            {
                MessageBox.Show("Incorrect old password.");
            }
            else if (newPassBox.Text != cnfrmPassBox.Text)
            {
                MessageBox.Show("Confirmation password and new password do not match.");
            }
            else
            {
                string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, "Persons.xml");
                XDocument xmlDoc = XDocument.Load(filename);
                var persons = (from person in xmlDoc.Descendants("Person")
                               where person.Attribute("IdNumber").Value == Form2.ID.ToString()
                               select person).ToList();
                foreach (var person in persons)
                {
                    person.Element("Password").Value = newPassBox.Text;
                 
                }
                xmlDoc.Save(filename);
                MessageBox.Show("Password change successfull");
                oldPassBox.Text = "";
                newPassBox.Text = "";
                cnfrmPassBox.Text = "";
            }
            
        }

        /// <summary>
        /// clears out the password boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordCancelButton_Click(object sender, EventArgs e)
        {
            oldPassBox.Text = "";
            newPassBox.Text = "";
            cnfrmPassBox.Text = "";
        }

        /// <summary>
        /// enables all the boxes and buttons of the personal details group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editPersonalButton_Click(object sender, EventArgs e)
        {
            maleButton.Enabled = true;
            femaleButton.Enabled = true;
            titleBox.Enabled = true;
            firstNameBox.Enabled = true;
            lastNameBox.Enabled = true;
            dateBox.Enabled = true;
            initialsBox.Enabled = true;
            cancelPersonalButton.Enabled = true;
            savePersonalButton.Enabled = true;
        }

        /// <summary>
        /// dissables all the boxes and button of the person details group,
        /// also sets boxes to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelPersonalButton_Click(object sender, EventArgs e)
        {
            maleButton.Enabled = false;
            femaleButton.Enabled = false;
            titleBox.Enabled = false;
            firstNameBox.Enabled = false;
            lastNameBox.Enabled = false;
            dateBox.Enabled = false;
            initialsBox.Enabled = false;
            savePersonalButton.Enabled = false;
            cancelPersonalButton.Enabled = false;
            firstNameBox.Text = person.FirstName;
            userBox.Text = person.UserName;
            lastNameBox.Text = person.LastName;
            initialsBox.Text = person.Initials;
            dateBox.Text = person.Date;
            initialsBox.Text = person.Initials;
            titleBox.Text = person.Title;
            
            if (person.Gender == "male")
            {
                maleButton.Checked = true;
            }
            else if (person.Gender == "female")
            {
                femaleButton.Checked = true;
            }
            
        }

        /// <summary>
        /// saves the information in the boxes to the xml file and database. 
        /// sets all boxes and buttons dissabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savePersonalButton_Click(object sender, EventArgs e)
        {
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");
            XDocument xmlDoc = XDocument.Load(filename);
            var persons = (from person in xmlDoc.Descendants("Person")
                           where person.Attribute("IdNumber").Value == Form2.ID.ToString()
                           select person).ToList();
            foreach (var person in persons)
            {
                
                if (initialsBox.Text != Form2.initials)
                {
                    person.Element("Initials").Value = initialsBox.Text;
                }
                if (titleBox.Text != Form2.title)
                {
                    person.Element("Title").Value = titleBox.Text;
                }

                if (maleButton.Checked == true)
                {
                    person.Element("Gender").Value = "male";
                }
                else if (femaleButton.Checked == true)
                {
                    person.Element("Gender").Value = "female";
                }
                
                
            }
            xmlDoc.Save(filename);
            Person newPerson = new Person();
            newPerson.IdNumber = person.IdNumber;
            newPerson.LastName = lastNameBox.Text;
            newPerson.FirstName = firstNameBox.Text;
            newPerson.Date = dateBox.Text;
            PersonDB.UpdatePersonDetails(person, newPerson);
            maleButton.Enabled = false;
            femaleButton.Enabled = false;
            titleBox.Enabled = false;
            firstNameBox.Enabled = false;
            lastNameBox.Enabled = false;
            dateBox.Enabled = false;
            initialsBox.Enabled = false;
            savePersonalButton.Enabled = false;
            cancelPersonalButton.Enabled = false;
        }

        /// <summary>
        /// enables all the boxes and buttons in the contact details group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contactEditButton_Click(object sender, EventArgs e)
        {
            contactCancelButton.Enabled = true;
            contactSaveButton.Enabled = true;
            personAddressBox.Enabled = true;
            personCityBox.Enabled = true;
            personStateBox.Enabled = true;
            personHomePhoneBox.Enabled = true;
            personMobilePhoneBox.Enabled = true;
            personZipBox.Enabled = true;
            personWorkPhoneBox.Enabled = true;
            personFaxBox.Enabled = true;
            personEmailBox.Enabled = true;
        }

        /// <summary>
        /// disables all the boxes and buttons of the contact details group, also
        /// sets all boxes to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contactCancelButton_Click(object sender, EventArgs e)
        {
            contactCancelButton.Enabled = false;
            contactSaveButton.Enabled = false;
            personAddressBox.Enabled = false;
            personCityBox.Enabled = false;
            personStateBox.Enabled = false;
            personHomePhoneBox.Enabled = false;
            personMobilePhoneBox.Enabled = false;
            personZipBox.Enabled = false;
            personWorkPhoneBox.Enabled = false;
            personFaxBox.Enabled = false;
            personEmailBox.Enabled = false;

            personAddressBox.Text = person.Street;
            personCityBox.Text = person.City;
            personStateBox.Text = person.State;
            personHomePhoneBox.Text = person.HomePhone.ToString();
            personMobilePhoneBox.Text = person.MobilePhone.ToString();
            personZipBox.Text = person.Zip.ToString();
            personWorkPhoneBox.Text = person.WorkPhone;
            personFaxBox.Text = person.Fax;
            personEmailBox.Text = person.Email;
        }

        /// <summary>
        /// saves the data from the boxes to the xml and database, 
        /// also dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contactSaveButton_Click(object sender, EventArgs e)
        {
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");
            XDocument xmlDoc = XDocument.Load(filename);
            var persons = (from person in xmlDoc.Descendants("Person")
                           where person.Attribute("IdNumber").Value == Form2.ID.ToString()
                           select person).ToList();
            foreach (var person in persons)
            {

                
              person.Element("WorkPhone").Value = personWorkPhoneBox.Text;
                
              person.Element("Fax").Value = personFaxBox.Text;
                
              person.Element("Email").Value = personEmailBox.Text;
                
            }
            xmlDoc.Save(filename);
            Person newPerson = new Person();
            newPerson.IdNumber = person.IdNumber;
            newPerson.City = personCityBox.Text;
            newPerson.Street = personAddressBox.Text;
            newPerson.State = personStateBox.Text;
            if (Validator.IsPresent(personZipBox) == true)
            {
                if (Validator.IsInt32(personZipBox))
                {

                    newPerson.Zip = Convert.ToInt32(personZipBox.Text);
                }
                else
                    Validator.Title = "Contact - Postal Code";

            }
            else
                Validator.Title = "Contact - Postal Code";

            if (personHomePhoneBox.Text == person.HomePhone)
            {
                if (Validator.IsPresent(personHomePhoneBox))
                {
                    if (Validator.IsWithinRange(personHomePhoneBox, 0, 9999999999))
                        newPerson.HomePhone = personMobilePhoneBox.Text;
                    else
                        Validator.Title = "Contact Home Phone";
                }

            }
            else
            {
                if (Validator.IsPresent(personHomePhoneBox))
                {
                    if (Validator.IsWithinRange(personHomePhoneBox, 0, 9999999999))
                        newPerson.HomePhone = personHomePhoneBox.Text;
                    else
                        Validator.Title = "Contact Home Phone";
                }
            }

            if (Validator.IsPresent(personMobilePhoneBox))
            {
                if (Validator.IsWithinRange(personMobilePhoneBox, 0, 9999999999))
                    newPerson.MobilePhone = personMobilePhoneBox.Text;
                else
                    Validator.Title = "Contact Mobile Phone";
            }

            PersonDB.UpdateContactDetails(person, newPerson);

            contactCancelButton.Enabled = false;
            contactSaveButton.Enabled = false;
            personAddressBox.Enabled = false;
            personCityBox.Enabled = false;
            personStateBox.Enabled = false;
            personHomePhoneBox.Enabled = false;
            personMobilePhoneBox.Enabled = false;
            personZipBox.Enabled = false;
            personWorkPhoneBox.Enabled = false;
            personFaxBox.Enabled = false;
            personEmailBox.Enabled = false;
        }

        /// <summary>
        /// enables all buttons and boxes of the emergency group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emergencyEditButton_Click(object sender, EventArgs e)
        {
            kinBox.Enabled = true;
            relationshipBox.Enabled = true;
            emergencyAddressBox.Enabled = true;
            emergencyStateBox.Enabled = true;
            emergencyCityBox.Enabled = true;
            emergencyZipBox.Enabled = true;
            emergencyPhoneBox.Enabled = true;
            emergencyMobileBox.Enabled = true;
            emergencyWorkBox.Enabled = true;
            emergencyFaxBox.Enabled = true;
            emergencyEmailBox.Enabled = true;
            emergencyCancelButton.Enabled = true;
            emergencySaveButton.Enabled = true;
        }

        /// <summary>
        /// dissables all the boxes and buttons of the emergency group, 
        /// also sets all boxes to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emergencyCancelButton_Click(object sender, EventArgs e)
        {
            kinBox.Enabled = false;
            relationshipBox.Enabled = false;
            emergencyAddressBox.Enabled = false;
            emergencyStateBox.Enabled = false;
            emergencyCityBox.Enabled = false;
            emergencyZipBox.Enabled = false;
            emergencyPhoneBox.Enabled = false;
            emergencyMobileBox.Enabled = false;
            emergencyWorkBox.Enabled = false;
            emergencyFaxBox.Enabled = false;
            emergencyEmailBox.Enabled = false;
            emergencyCancelButton.Enabled = false;
            emergencySaveButton.Enabled = false;
            kinBox.Text = person.ContactName;
            relationshipBox.Text = person.ContactRelation;
            emergencyAddressBox.Text = person.ContactAddress;
            emergencyStateBox.Text = person.ContactState;
            emergencyCityBox.Text = person.ContactCity;
            emergencyZipBox.Text = person.ContactZip.ToString();
            emergencyPhoneBox.Text = person.ContactPhone;
            emergencyMobileBox.Text = person.ContactMobile;
            emergencyWorkBox.Text = person.ContactWork;
            emergencyFaxBox.Text = person.ContactFax;
            emergencyEmailBox.Text = person.ContactEmail;
        }

        /// <summary>
        /// saves data in the boxes to the xml file,
        /// also dissables all boxes and buttons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emergencySaveButton_Click(object sender, EventArgs e)
        {
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");
            XDocument xmlDoc = XDocument.Load(filename);
            var persons = (from person in xmlDoc.Descendants("Person")
                           where person.Attribute("IdNumber").Value == Form2.ID.ToString()
                           select person).ToList();
            foreach (var person in persons)
            {
                
                person.Element("Kin").Value = kinBox.Text;
                person.Element("KinRelation").Value = relationshipBox.Text;
                person.Element("KinAddress").Value = emergencyAddressBox.Text;
                person.Element("KinState").Value = emergencyStateBox.Text;
                person.Element("KinCity").Value = emergencyCityBox.Text;
                person.Element("KinZip").Value = emergencyZipBox.Text;
                person.Element("KinPhone").Value = emergencyPhoneBox.Text;
                person.Element("KinMobile").Value = emergencyMobileBox.Text;
                person.Element("KinWork").Value = emergencyWorkBox.Text;
                person.Element("KinFax").Value = emergencyFaxBox.Text;
                person.Element("KinEmail").Value = emergencyEmailBox.Text;

            }
            xmlDoc.Save(filename);
            kinBox.Enabled = false;
            relationshipBox.Enabled = false;
            emergencyAddressBox.Enabled = false;
            emergencyStateBox.Enabled = false;
            emergencyCityBox.Enabled = false;
            emergencyZipBox.Enabled = false;
            emergencyPhoneBox.Enabled = false;
            emergencyMobileBox.Enabled = false;
            emergencyWorkBox.Enabled = false;
            emergencyFaxBox.Enabled = false;
            emergencyEmailBox.Enabled = false;
            emergencyCancelButton.Enabled = false;
            emergencySaveButton.Enabled = false;
        }

        /// <summary>
        /// enables all boxes and buttons of the care provider group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void providerEditButton_Click(object sender, EventArgs e)
        {
            providerNameBox.Enabled = true;
            providerSpecialBox.Enabled = true;
            providerWorkBox.Enabled = true;
            providerMobileBox.Enabled = true;
            providerFaxBox.Enabled = true;
            providerEmailBox.Enabled = true;
            providerSaveButton.Enabled = true;
            providerCancelButton.Enabled = true;
        }

        /// <summary>
        /// dissables all boxes and buttons of the care provider group 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void providerCancelButton_Click(object sender, EventArgs e)
        {
            providerNameBox.Enabled = false;
            providerSpecialBox.Enabled = false;
            providerWorkBox.Enabled = false;
            providerMobileBox.Enabled = false;
            providerFaxBox.Enabled = false;
            providerEmailBox.Enabled = false;
            providerSaveButton.Enabled = false;
            providerCancelButton.Enabled = false;
            Person provider = PersonDB.GetPrimary(Form2.primaryId);
            providerNameBox.Text = provider.CareProviderFirst + " " + provider.CareProviderLast;
            providerSpecialBox.Text = provider.CareProviderSpecialty;
            providerMobileBox.Text = provider.CareProviderMobile;
            providerWorkBox.Text = provider.CareProviderWork;
            providerFaxBox.Text = person.CareProviderFax;
            providerEmailBox.Text = person.CareProviderEmail;
        }

        /// <summary>
        /// saves information from the boxes and stores it in the xml and database,
        /// also dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void providerSaveButton_Click(object sender, EventArgs e)
        {
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");
            XDocument xmlDoc = XDocument.Load(filename);
            var persons = (from person in xmlDoc.Descendants("Person")
                           where person.Attribute("IdNumber").Value == Form2.ID.ToString()
                           select person).ToList();
            foreach (var person in persons)
            {


                person.Element("CareFax").Value = providerFaxBox.Text;

                person.Element("CareEmail").Value = providerEmailBox.Text;

            }
            xmlDoc.Save(filename);
            Person newPerson = new Person();
            newPerson.PrimaryID = person.PrimaryID;
            string[] name = providerNameBox.Text.Split(' ');
            newPerson.CareProviderLast = name[1];
            newPerson.CareProviderFirst = name[0];

            if (Validator.IsPresent(providerWorkBox))
            {
                if (Validator.IsWithinRange(providerWorkBox, 0, 9999999999))
                    newPerson.CareProviderWork = providerWorkBox.Text;
                else
                    Validator.Title = "Primary Care Office Phone";
            }
            newPerson.CareProviderSpecialty = providerSpecialBox.Text;

            if (Validator.IsPresent(providerMobileBox)) {
                if (Validator.IsWithinRange(providerMobileBox, 0, 9999999999))
                    newPerson.CareProviderMobile = providerMobileBox.Text;
                else
                    Validator.Title = "Primary Care Mobile Phone";
            }
            newPerson.CareProviderFax = providerFaxBox.Text;
            newPerson.CareProviderEmail = providerEmailBox.Text;

            PersonDB.UpdateProviderDetails(person, newPerson);
            providerNameBox.Enabled = false;
            providerSpecialBox.Enabled = false;
            providerWorkBox.Enabled = false;
            providerMobileBox.Enabled = false;
            providerFaxBox.Enabled = false;
            providerEmailBox.Enabled = false;
            providerSaveButton.Enabled = false;
            providerCancelButton.Enabled = false;
        }

        /// <summary>
        /// enables all boxes and buttons of the insurance group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insuranceEditButton_Click(object sender, EventArgs e)
        {
            insurerBox.Enabled = true;
            insurancePlanBox.Enabled = true;
            insuranceNumberBox.Enabled = true;
            insuranceSaveButton.Enabled = true;
            insuranceCancelButton.Enabled = true;
        }

        /// <summary>
        /// dissables all boxes and buttons of the insurance group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insuranceCancelButton_Click(object sender, EventArgs e)
        {
            insurerBox.Enabled = false;
            insurancePlanBox.Enabled = false;
            insuranceNumberBox.Enabled = false;
            insuranceSaveButton.Enabled = false;
            insuranceCancelButton.Enabled = false;
            insurerBox.Text = person.Insurer;
            insurancePlanBox.Text = person.InsurancePlan;
            insuranceNumberBox.Text = person.InsuranceNumber.ToString();
        }

        /// <summary>
        /// saves the information from the boxes to the xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insuranceSaveButton_Click(object sender, EventArgs e)
        {
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");
            XDocument xmlDoc = XDocument.Load(filename);
            var persons = (from person in xmlDoc.Descendants("Person")
                           where person.Attribute("IdNumber").Value == Form2.ID.ToString()
                           select person).ToList();
            foreach (var person in persons)
            {


                person.Element("Insurer").Value = insurerBox.Text;

                person.Element("InsurancePlan").Value = insurancePlanBox.Text;

                person.Element("InsuranceNumber").Value = insuranceNumberBox.Text;

            }
            xmlDoc.Save(filename);
            insurerBox.Enabled = false;
            insurancePlanBox.Enabled = false;
            insuranceNumberBox.Enabled = false;
            insuranceSaveButton.Enabled = false;
            insuranceCancelButton.Enabled = false;
        }

        /// <summary>
        /// adds data to the test test table,
        /// displays test in listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultAddButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Test test = new Test();
            test.IDNumber = Form2.ID;
            test.GetTest = testBox.Text;
            test.Date = resultDateBox.Text;
            test.Result = resultBox.Text;
            test.Note = resultNoteBox.Text;
            TestDB.RegisterTest(test);
            FillTestList(Form2.ID);
        }

        /// <summary>
        /// enables all buttons and boxes in the test group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            testBox.Enabled = true;
            resultDateBox.Enabled = true;
            resultBox.Enabled = true;
            resultNoteBox.Enabled = true;
            resultAddButton.Enabled = true;
            resultListBox.Enabled = true;
            resultRemoveButton.Enabled = true;
            resultSaveButton.Enabled = true;
            resultCancelButton.Enabled = true;
        }
        
        /// <summary>
        /// binds the table data to the listbox
        /// </summary>
        /// <param name="ID"></param>
        public void FillTestList(int ID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            string selectStatement
                = "SELECT PATIENT_ID, TEST_ID, TEST, RESULT, DATE, NOTE "
                + "FROM TEST_TBL "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
           
            command.Parameters.AddWithValue("@ID", ID);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                resultListBox.ValueMember = "TEST_ID";
                resultListBox.DisplayMember = "TEST";
                resultListBox.DataSource = ds.Tables[0];

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// changes the test boxes depending on whats selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int testID = Convert.ToInt32(resultListBox.SelectedValue);

            Test temp = TestDB.GetTest(Form2.ID, testID);
            testBox.Text = temp.GetTest;
            resultDateBox.Text = temp.Date;
            resultBox.Text = temp.Result;
            resultNoteBox.Text = temp.Note;

        }

        /// <summary>
        /// removes data from the test table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultRemoveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int testID = Convert.ToInt32(resultListBox.SelectedValue);

            Test temp = TestDB.GetTest(Form2.ID, testID);
            TestDB.DeleteTest(temp, testID);
            FillTestList(Form2.ID);
        }

        /// <summary>
        /// updates data in the table database, 
        /// dissables all boxes and buttons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultSaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int testID = Convert.ToInt32(resultListBox.SelectedValue);

            Test temp = TestDB.GetTest(Form2.ID, testID);
            Test newTest = new Test();
            newTest.IDNumber = temp.IDNumber;
            newTest.TestNumber = temp.TestNumber;
            newTest.GetTest = testBox.Text;
            newTest.Result = resultBox.Text;
            newTest.Date = resultDateBox.Text;
            newTest.Note = resultNoteBox.Text;
            TestDB.UpdateTest(temp, newTest);
            testBox.Enabled = false;
            resultDateBox.Enabled = false;
            resultBox.Enabled = false;
            resultNoteBox.Enabled = false;
            resultAddButton.Enabled = false;
            resultListBox.Enabled = false;
            resultRemoveButton.Enabled = false;
            resultSaveButton.Enabled = false;
            resultCancelButton.Enabled = false;
            FillTestList(Form2.ID);

        }

        /// <summary>
        /// dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            testBox.Enabled = false;
            resultDateBox.Enabled = false;
            resultBox.Enabled = false;
            resultNoteBox.Enabled = false;
            resultAddButton.Enabled = false;
            resultListBox.Enabled = false;
            resultRemoveButton.Enabled = false;
            resultSaveButton.Enabled = false;
            resultCancelButton.Enabled = false;
            FillTestList(Form2.ID);
        }

        /// <summary>
        /// enables all boxes and buttons in the allergy group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allergyEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            allergyAddButton.Enabled = true;
            allergyBox.Enabled = true;
            allergyCancelButton.Enabled = true;
            allergyDateBox.Enabled = true;
            allergySaveButton.Enabled = true;
            allergyDeleteButton.Enabled = true;
            allergyNoteBox.Enabled = true;
            allergyListBox.Enabled = true;
        }

        /// <summary>
        /// dissables all boxes and buttons in the allergy group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allergyCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            allergyAddButton.Enabled = false;
            allergyBox.Enabled = false;
            allergyCancelButton.Enabled = false;
            allergyDateBox.Enabled = false;
            allergySaveButton.Enabled = false;
            allergyDeleteButton.Enabled = false;
            allergyNoteBox.Enabled = false;
            allergyListBox.Enabled = false;
        }

        /// <summary>
        /// binds the allergy list to the allergy table
        /// </summary>
        /// <param name="ID"></param>
        public void FillAllergyList(int ID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            string selectStatement
                = "SELECT PATIENT_ID, ALLERGY_ID, ALLERGEN, ONSET_DATE, NOTE "
                + "FROM ALLERGY_TBL "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.AddWithValue("@ID", ID);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                allergyListBox.ValueMember = "ALLERGY_ID";
                allergyListBox.DisplayMember = "ALLERGEN";
                allergyListBox.DataSource = ds.Tables[0];

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// adds data to the allergy table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allergyAddButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Allergy allergy = new Allergy();
            allergy.IDNumber = Form2.ID;
            allergy.Allergen = allergyBox.Text;
            allergy.Date = allergyDateBox.Text;
            allergy.Note = allergyNoteBox.Text;
            AllergyDB.RegisterAllergy(allergy);
            FillAllergyList(Form2.ID);
        }

        /// <summary>
        /// updates data in the allergy table,
        /// also dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allergySaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int allergyID = Convert.ToInt32(allergyListBox.SelectedValue);

            Allergy temp = AllergyDB.GetAllergy(Form2.ID, allergyID);
            Allergy newAllergy = new Allergy();
            newAllergy.IDNumber = temp.IDNumber;
            newAllergy.AllergyID = temp.AllergyID;
            newAllergy.Allergen = allergyBox.Text;
            newAllergy.Date = allergyDateBox.Text;
            newAllergy.Note = allergyNoteBox.Text;
            AllergyDB.UpdateAllergy(temp, newAllergy);
            allergyAddButton.Enabled = false;
            allergyBox.Enabled = false;
            allergyCancelButton.Enabled = false;
            allergyDateBox.Enabled = false;
            allergySaveButton.Enabled = false;
            allergyDeleteButton.Enabled = false;
            allergyNoteBox.Enabled = false;
            allergyListBox.Enabled = false;
            FillAllergyList(Form2.ID);
        }

        /// <summary>
        /// fills allergy boxes depending on whats selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allergyListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int allergyID = Convert.ToInt32(allergyListBox.SelectedValue);

            Allergy temp = AllergyDB.GetAllergy(Form2.ID, allergyID);
            allergyBox.Text = temp.Allergen;
            allergyDateBox.Text = temp.Date;
            allergyNoteBox.Text = temp.Note;
        }

        /// <summary>
        /// removes data from the allergy table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allergyDeleteButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int allergyID = Convert.ToInt32(allergyListBox.SelectedValue);

            Allergy temp = AllergyDB.GetAllergy(Form2.ID, allergyID);
            AllergyDB.DeleteAllergy(temp, allergyID);
            FillAllergyList(Form2.ID);
        }

        /// <summary>
        /// binds the immunization list to the immunization table
        /// </summary>
        /// <param name="ID"></param>
        public void FillImmunizationList(int ID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            string selectStatement
                = "SELECT PATIENT_ID, IMMUNIZATION_ID, IMMUNIZATION, DATE, NOTE "
                + "FROM IMMUNIZATION_TBL "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.AddWithValue("@ID", ID);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                immunizationListBox.ValueMember = "IMMUNIZATION_ID";
                immunizationListBox.DisplayMember = "IMMUNIZATION";
                immunizationListBox.DataSource = ds.Tables[0];

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// enables all boxes in buttons in the immunization group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void immunizationEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            immunizationAddButton.Enabled = true;
            immunizationBox.Enabled = true;
            immunizationCancelButton.Enabled = true;
            immunizationDateBox.Enabled = true;
            immunizationSaveButton.Enabled = true;
            immunizationRemoveButton.Enabled = true;
            immunizationNoteBox.Enabled = true;
            immunizationListBox.Enabled = true;
        }

        /// <summary>
        /// adds data to the immunization table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void immunizationAddButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Immunization immunization = new Immunization();
            immunization.IDNumber = Form2.ID;
            immunization.GetImmunization = immunizationBox.Text;
            immunization.Date = immunizationDateBox.Text;
            immunization.Note = immunizationNoteBox.Text;
            ImmunizationDB.RegisterImmunization(immunization);
            FillImmunizationList(Form2.ID);
        }

        /// <summary>
        /// updates the boxes to what selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void immunizationListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int immunizationID = Convert.ToInt32(immunizationListBox.SelectedValue);

            Immunization temp = ImmunizationDB.GetImmunization(Form2.ID, immunizationID);
            immunizationBox.Text = temp.GetImmunization;
            immunizationDateBox.Text = temp.Date;
            immunizationNoteBox.Text = temp.Note;
        }

        /// <summary>
        /// updates data in the immunization table,
        /// also dissables all buttons and boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void immunizationSaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int immunizationID = Convert.ToInt32(immunizationListBox.SelectedValue);

            Immunization temp = ImmunizationDB.GetImmunization(Form2.ID, immunizationID);
            Immunization newImmunization = new Immunization();
            newImmunization.IDNumber = temp.IDNumber;
            newImmunization.ImmunizationID = temp.ImmunizationID;
            newImmunization.GetImmunization = immunizationBox.Text;
            newImmunization.Date = immunizationDateBox.Text;
            newImmunization.Note = immunizationNoteBox.Text;
            ImmunizationDB.UpdateImmunization(temp, newImmunization);
            immunizationAddButton.Enabled = false;
            immunizationBox.Enabled = false;
            immunizationCancelButton.Enabled = false;
            immunizationDateBox.Enabled = false;
            immunizationSaveButton.Enabled = false;
            immunizationRemoveButton.Enabled = false;
            immunizationNoteBox.Enabled = false;
            immunizationListBox.Enabled = false;
            FillImmunizationList(Form2.ID);
        }

        /// <summary>
        /// removes data from the immunization table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void immunizationRemoveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int immunizationID = Convert.ToInt32(immunizationListBox.SelectedValue);

            Immunization temp = ImmunizationDB.GetImmunization(Form2.ID, immunizationID);
            ImmunizationDB.DeleteImmunization(temp, immunizationID);
            FillImmunizationList(Form2.ID);
        }

        /// <summary>
        /// dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void immunizationCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            immunizationAddButton.Enabled = false;
            immunizationBox.Enabled = false;
            immunizationCancelButton.Enabled = false;
            immunizationDateBox.Enabled = false;
            immunizationSaveButton.Enabled = false;
            immunizationRemoveButton.Enabled = false;
            immunizationNoteBox.Enabled = false;
            immunizationListBox.Enabled = false;
            FillImmunizationList(Form2.ID);
        }

        /// <summary>
        /// binds the proc list to the med_proc table
        /// </summary>
        /// <param name="ID"></param>
        public void FillProcList(int ID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            string selectStatement
                = "SELECT PATIENT_ID, PROCEDURE_ID, MED_PROCEDURE, DATE, DOCTOR, NOTE "
                + "FROM MED_PROC_TBL "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.AddWithValue("@ID", ID);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                proceduresListBox.ValueMember = "PROCEDURE_ID";
                proceduresListBox.DisplayMember = "MED_PROCEDURE";
                proceduresListBox.DataSource = ds.Tables[0];

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// enables all buttons and boxes in the procedure group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void procedureEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            procedureAddButton.Enabled = true;
            procedureBox.Enabled = true;
            procedureCancelButton.Enabled = true;
            procedureDateBox.Enabled = true;
            procedureNoteBox.Enabled = true;
            procedurePerformBox.Enabled = true;
            proceduresListBox.Enabled = true;
            procedureSaveButton.Enabled = true;
            procedureRemoveButton.Enabled = true;
        }

        /// <summary>
        /// adds data to the med_proc table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void procedureAddButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Med_Proc proc = new Med_Proc();
            proc.IDNumber = Form2.ID;
            proc.Procedure = procedureBox.Text;
            proc.Doctor = procedurePerformBox.Text;
            proc.Date = procedureDateBox.Text;
            proc.Note = procedureNoteBox.Text;
            Med_ProcDB.RegisterProcedure(proc);
            FillProcList(Form2.ID);
        }

        /// <summary>
        /// updates data in the med_proc table, 
        /// also dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void procedureSaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int procID = Convert.ToInt32(proceduresListBox.SelectedValue);

            Med_Proc temp = Med_ProcDB.GetProcedure(Form2.ID, procID);
            Med_Proc newProc = new Med_Proc();
            newProc.IDNumber = temp.IDNumber;
            newProc.ProcedureNumber = temp.ProcedureNumber;
            newProc.Procedure = procedureBox.Text;
            newProc.Date = procedureDateBox.Text;
            newProc.Doctor = procedurePerformBox.Text;
            newProc.Note = procedureNoteBox.Text;
            Med_ProcDB.UpdateProcedure(temp, newProc);
            procedureAddButton.Enabled = false;
            procedureBox.Enabled = false;
            procedureCancelButton.Enabled = false;
            procedureDateBox.Enabled = false;
            procedureNoteBox.Enabled = false;
            procedurePerformBox.Enabled = false;
            proceduresListBox.Enabled = false;
            procedureSaveButton.Enabled = false;
            procedureRemoveButton.Enabled = false;
            FillProcList(Form2.ID);
        }

        /// <summary>
        /// updates the boxes with whats selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proceduresListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int procID = Convert.ToInt32(proceduresListBox.SelectedValue);

            Med_Proc temp = Med_ProcDB.GetProcedure(Form2.ID, procID);
            procedureBox.Text = temp.Procedure;
            procedureDateBox.Text = temp.Date;
            procedurePerformBox.Text = temp.Doctor;
            procedureNoteBox.Text = temp.Note;
        }

        /// <summary>
        /// deletes data in the med_proc table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void procedureRemoveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int procID = Convert.ToInt32(proceduresListBox.SelectedValue);

            Med_Proc temp = Med_ProcDB.GetProcedure(Form2.ID, procID);
            Med_ProcDB.DeleteProcedure(temp, procID);
            FillProcList(Form2.ID);
        }

        /// <summary>
        /// dissables all bxoes and buttons in the procedure group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void procedureCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            procedureAddButton.Enabled = false;
            procedureBox.Enabled = false;
            procedureCancelButton.Enabled = false;
            procedureDateBox.Enabled = false;
            procedureNoteBox.Enabled = false;
            procedurePerformBox.Enabled = false;
            proceduresListBox.Enabled = false;
            procedureSaveButton.Enabled = false;
            procedureRemoveButton.Enabled = false;
            FillProcList(Form2.ID);
        }

        /// <summary>
        /// binds the medlist with the medication table
        /// </summary>
        /// <param name="ID"></param>
        public void FillMedList(int ID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            string selectStatement
                = "SELECT PATIENT_ID, MED_ID, MEDICATION, DATE, CHRONIC, NOTE "
                + "FROM MEDICATION_TBL "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.AddWithValue("@ID", ID);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                medListBox.ValueMember = "MED_ID";
                medListBox.DisplayMember = "MEDICATION";
                medListBox.DataSource = ds.Tables[0];

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// enables all boxes and buttons in the medication group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            medAddButton.Enabled = true;
            medBox.Enabled = true;
            medCancelButton.Enabled = true;
            medDateBox.Enabled = true;
            chronicButton.Enabled = true;
            medNoteBox.Enabled = true;
            medRemoveButton.Enabled = true;
            medListBox.Enabled = true;
            medSaveButton.Enabled = true;
        }

        /// <summary>
        /// adds data to the medication table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medAddButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Medication med = new Medication();
            med.IDNumber = Form2.ID;
            med.GetMedication = medBox.Text;
            med.Date = medDateBox.Text;
            if (chronicButton.Checked == true)
            {
                med.Chronic = "true";
            }
            else
            {
                med.Chronic = "false";
            }
            med.Note = medNoteBox.Text;
            MedicationDB.RegisterMed(med);
            FillMedList(Form2.ID);
        }

        /// <summary>
        /// updates the list with whats selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int medID = Convert.ToInt32(medListBox.SelectedValue);

            Medication temp = MedicationDB.GetMed(Form2.ID, medID);
            medBox.Text = temp.GetMedication;
            medDateBox.Text = temp.Date;
            if (temp.Chronic == "True")
                chronicButton.Checked = true;
            else
                chronicButton.Checked = false;
            medNoteBox.Text = temp.Note;
        }

        /// <summary>
        /// udpates the data in the medication table,
        /// dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medSaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int medID = Convert.ToInt32(medListBox.SelectedValue);

            Medication temp = MedicationDB.GetMed(Form2.ID, medID);
            Medication newMed = new Medication();
            newMed.IDNumber = temp.IDNumber;
            newMed.MedNumber = temp.MedNumber;
            newMed.GetMedication = medBox.Text;
            newMed.Date = medDateBox.Text;
            if (chronicButton.Checked == true)
                newMed.Chronic = "true";
            else
                newMed.Chronic = "false";
            newMed.Note = medNoteBox.Text;
            MedicationDB.UpdateMed(temp, newMed);
            medAddButton.Enabled = false;
            medBox.Enabled = false;
            medCancelButton.Enabled = false;
            medDateBox.Enabled = false;
            chronicButton.Enabled = false;
            medNoteBox.Enabled = false;
            medRemoveButton.Enabled = false;
            medListBox.Enabled = false;
            medSaveButton.Enabled = false;
            FillMedList(Form2.ID);
        }

        /// <summary>
        /// removes data in the medication table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medRemoveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int medID = Convert.ToInt32(medListBox.SelectedValue);

            Medication temp = MedicationDB.GetMed(Form2.ID, medID);
            MedicationDB.DeleteMed(temp, medID);
            FillMedList(Form2.ID);
        }

        /// <summary>
        /// dissables all buttons and boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            medAddButton.Enabled = false;
            medBox.Enabled = false;
            medCancelButton.Enabled = false;
            medDateBox.Enabled = false;
            chronicButton.Enabled = false;
            medNoteBox.Enabled = false;
            medRemoveButton.Enabled = false;
            medListBox.Enabled = false;
            medSaveButton.Enabled = false;
            FillMedList(Form2.ID);
        }

        /// <summary>
        /// enables all buttons and boxes in the condition group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            conditionAddButton.Enabled = true;
            conditionBox.Enabled = true;
            conditionCancelButton.Enabled = true;
            conditionDateBox.Enabled = true;
            conditionListBox.Enabled = true;
            conditionDateBox.Enabled = true;
            acuteRadio.Enabled = true;
            chronicRadio.Enabled = true;
            conditionSaveButton.Enabled = true;
            conditionRemoveButton.Enabled = true;
            conditionNoteBox.Enabled = true;
        }

        /// <summary>
        /// binds the condition list with the condition table
        /// </summary>
        /// <param name="ID"></param>
        public void FillConditionList(int ID)
        {
            SqlConnection connection = pchrDB.GetConnection();
            string selectStatement
                = "SELECT PATIENT_ID, CONDITION_ID, CONDITION, ONSET_DATE, ACUTE, CHRONIC, NOTE "
                + "FROM CONDITION "
                + "WHERE PATIENT_ID = @ID";
            SqlCommand command = new SqlCommand(selectStatement, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.AddWithValue("@ID", ID);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();

                conditionListBox.ValueMember = "CONDITION_ID";
                conditionListBox.DisplayMember = "CONDITION";
                conditionListBox.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// adds data to the condition table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionAddButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Condition condition = new Condition();
            condition.IDNumber = Form2.ID;
            condition.GetCondition = conditionBox.Text;
            condition.Date = conditionDateBox.Text;
            if (chronicRadio.Checked == true)
            {
                condition.Chronic = "true";
                acuteRadio.Checked = false;
                condition.Acute = "false";
            }
            else
            {
                condition.Chronic = "false";
                acuteRadio.Checked = true;
                condition.Acute = "true";
            }

            
            condition.Note = conditionNoteBox.Text;
            ConditionDB.RegisterCondition(condition);
            FillConditionList(Form2.ID);
        }

        /// <summary>
        /// updates the boxes and buttons with whats selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int conditionID = Convert.ToInt32(conditionListBox.SelectedValue);

            Condition temp = ConditionDB.GetCondition(Form2.ID, conditionID);
            conditionBox.Text = temp.GetCondition;
            conditionDateBox.Text = temp.Date;
            if (temp.Chronic == "True")
            {
                chronicRadio.Checked = true;
                acuteRadio.Checked = false;
            }
            else
            {
                chronicRadio.Checked = false;
                acuteRadio.Checked = true;
            }
            conditionNoteBox.Text = temp.Note;
        }

        /// <summary>
        /// updates the data in the condition table,
        /// dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionSaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int conditionID = Convert.ToInt32(conditionListBox.SelectedValue);

            Condition temp = ConditionDB.GetCondition(Form2.ID, conditionID);
            Condition newCondition = new Condition();
            newCondition.IDNumber = temp.IDNumber;
            newCondition.ConditionNumber = temp.ConditionNumber;
            newCondition.GetCondition = conditionBox.Text;
            newCondition.Date = conditionDateBox.Text;
            if (chronicRadio.Checked == true)
            {
                newCondition.Chronic = "true";
                acuteRadio.Checked = false;
                newCondition.Acute = "false";
            }
            else
            {
                newCondition.Chronic = "false";
                acuteRadio.Checked = true;
                newCondition.Acute = "true";
            }
            newCondition.Note = conditionNoteBox.Text;
            ConditionDB.UpdateCondition(temp, newCondition);
            conditionAddButton.Enabled = false;
            conditionBox.Enabled = false;
            conditionCancelButton.Enabled = false;
            conditionDateBox.Enabled = false;
            conditionListBox.Enabled = false;
            conditionDateBox.Enabled = false;
            acuteRadio.Enabled = false;
            chronicRadio.Enabled = false;
            conditionSaveButton.Enabled = false;
            conditionRemoveButton.Enabled = false;
            conditionNoteBox.Enabled = false;
            FillConditionList(Form2.ID);
        }

        /// <summary>
        /// dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            conditionAddButton.Enabled = false;
            conditionBox.Enabled = false;
            conditionCancelButton.Enabled = false;
            conditionDateBox.Enabled = false;
            conditionListBox.Enabled = false;
            conditionDateBox.Enabled = false;
            acuteRadio.Enabled = false;
            chronicRadio.Enabled = false;
            conditionSaveButton.Enabled = false;
            conditionRemoveButton.Enabled = false;
            conditionNoteBox.Enabled = false;
            FillConditionList(Form2.ID);
        }

        /// <summary>
        /// deletes data in the condition table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionRemoveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int conditionID = Convert.ToInt32(conditionListBox.SelectedValue);

            Condition temp = ConditionDB.GetCondition(Form2.ID, conditionID);
            ConditionDB.DeleteCondition(temp, conditionID);
            FillConditionList(Form2.ID);
        }

        /// <summary>
        /// enables all boxes and buttons in the medical detail group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void perDetailsEditButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bloodBox.Enabled = true;
            donorBox.Enabled = true;
            positiveButton.Enabled = true;
            negativeButton.Enabled = true;
            unknownButton.Enabled = true;
            hightBox.Enabled = true;
            weightBox.Enabled = true;
            perDetailsSaveButton.Enabled = true;
            perDetailsCancelButton.Enabled = true;
        }

        /// <summary>
        /// upates data in the per_details table, 
        /// dissables all boxes and buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void perDetailsSaveButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PerDetails details = PerDetailsDB.GetDetails(Form2.ID);
            PerDetails newDetails = new PerDetails();
            newDetails.IDNumber = Form2.ID;
            newDetails.Type = bloodBox.Text;
            if (donorBox.Checked == true)
                newDetails.Donor = "true";
            else
                newDetails.Donor = "false";

            if (positiveButton.Checked == true)
            {
                newDetails.Hiv = 0;
            }
            else if (negativeButton.Checked == true)
            {
                newDetails.Hiv = 1;
            }

            else
            {
                newDetails.Hiv = 2;
            }
            if (Validator.IsPresent(hightBox))
            {
                if (Validator.IsInt32(hightBox))
                    newDetails.Hight = Convert.ToInt32(hightBox.Text);
                else
                    Validator.Title = "Height";
            }
            else
                Validator.Title = "Height";

            if (Validator.IsPresent(weightBox))
            {
                if (Validator.IsInt32(weightBox))
                    newDetails.Weight = Convert.ToInt32(weightBox.Text);
                else
                    Validator.Title = "Weight";

            }
            else
                Validator.Title = "Weight";
            
            PerDetailsDB.UpdateDetails(details, newDetails);
            bloodBox.Enabled = false;
            donorBox.Enabled = false;
            positiveButton.Enabled = false;
            negativeButton.Enabled = false;
            unknownButton.Enabled = false;
            hightBox.Enabled = false;
            weightBox.Enabled = false;
            perDetailsSaveButton.Enabled = false;
            perDetailsCancelButton.Enabled = false;
        }

        /// <summary>
        /// dissables all buttons and boxes in the medical details group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void perDetailsCancelButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            bloodBox.Enabled = false;
            donorBox.Enabled = false;
            positiveButton.Enabled = false;
            negativeButton.Enabled = false;
            unknownButton.Enabled = false;
            hightBox.Enabled = false;
            weightBox.Enabled = false;
            perDetailsSaveButton.Enabled = false;
            perDetailsCancelButton.Enabled = false;
            PerDetails details = PerDetailsDB.GetDetails(Form2.ID);
            bloodBox.Text = details.Type;
            if (details.Donor == "True")
                donorBox.Checked = true;
            else
                donorBox.Checked = false;
            if (details.Hiv == 0)
            {
                positiveButton.Checked = true;
                negativeButton.Checked = false;
                unknownButton.Checked = false;
            }
            else if (details.Hiv == 1)
            {
                positiveButton.Checked = false;
                negativeButton.Checked = true;
                unknownButton.Checked = false;
            }
            else if (details.Hiv == 2)
            {
                positiveButton.Checked = false;
                negativeButton.Checked = false;
                unknownButton.Checked = true;
            }

            hightBox.Text = details.Hight.ToString();
            weightBox.Text = details.Weight.ToString();

        }

        private void pictureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                string path = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, Form2.ID.ToString() +"Picture");
                pictureBox1.Image.Save(filename, ImageFormat.Png);
            }
        }
    }
}

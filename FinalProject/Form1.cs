using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject {
    public partial class Form1 : Form {
        
        /// <summary>
        /// initialzes
        /// </summary>
        public Form1() {
			InitializeComponent();
		}

        /// <summary>
        /// cancles this form and displays login form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e) {
            Form newForm2 = new Form2();
            newForm2.Show();
            this.Close();
		}

        /// <summary>
        /// registers the information into the person,primary care, and per_details tables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void registerButton_Click(object sender, EventArgs e)
        {
			Person person = new Person();
            //construct object and set its values equal to the text boxs
            if (Validator.IsPresent(userNameBox) == true)
                person.UserName = userNameBox.Text;
            else
                Validator.Title = "Username";
            if (Validator.IsPresent(passwordBox) == true)
                person.Password = passwordBox.Text;
            else
                Validator.Title = "Password";
            if (Validator.IsPresent(confirmBox) == true)
                person.CnfrmPass = confirmBox.Text;
            else
                Validator.Title = "Confirmation";
            if (Validator.IsPresent(idBox) == true)
            {
                if (Validator.IsInt32(idBox))
                {
                    person.IdNumber = Convert.ToInt32(idBox.Text);

                }
                else
                    Validator.Title = "Identity Number";

            }
            else
                Validator.Title = "Identity Number";

            if (Validator.IsPresent(initialsBox) == true)
                person.Initials = initialsBox.Text;
            else
                Validator.Title = "Initials";
            if (Validator.IsPresent(firstNameBox) == true)
                person.LastName = firstNameBox.Text;
            else
                Validator.Title = "First Name";
            if (Validator.IsPresent(lastNameBox) == true)
                person.FirstName = lastNameBox.Text;
            else
                Validator.Title = "Last Name";
            
            person.Date = dateTimePicker1.Text;

            if (Validator.IsComboPresent(titleComboBox) == true)
                person.Title = titleComboBox.Text;
            else
                Validator.Title = "Title";
            if (Validator.IsPresent(idBox) == true)
            {
                if (Validator.IsInt32(idBox))
                {

                    person.PrimaryID = Convert.ToInt32(primaryCareBox.Text);
                }
                else
                    Validator.Title = "Primary CareID";

            }
            else
                Validator.Title = "Primary CareID";
            
            if (maleButton.Checked == true)
            {
                person.Gender = "male";
            }
            else if (femaleButton.Checked == true)
            {
                person.Gender = "female";
            }
            //setting variables that arent yet manipulated by the user blank instead of null
            person.WorkPhone = " ";
            person.Fax = " ";
            person.Email = " ";
            person.ContactName = " ";
            person.ContactRelation = " ";
            person.ContactAddress = " ";
            person.ContactState = " ";
            person.ContactCity = " ";
            person.ContactZip = " ";
            person.ContactPhone = " ";
            person.ContactMobile = " ";
            person.ContactWork = " ";
            person.ContactFax = " ";
            person.ContactEmail = " ";
            person.CareProviderFirst = " ";
            person.CareProviderLast = " ";
            person.CareProviderMobile = " ";
            person.CareProviderWork = " ";
            person.CareProviderSpecialty = " ";

            //clears the list and then adds new info
            string path = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Persons.xml");
            if (File.Exists(filename)) {
                PersonDB.GetPersons().Clear();
                PersonDB.GetPersons();
                PersonDB.persons.Add(person);
            }
            else
            {
                PersonDB.persons.Add(person);
            }
            //PersonDB.GetPersons().Clear();
            //PersonDB.GetPersons();
            //PersonDB.persons.Add(person);



            if (person.Password != person.CnfrmPass)
            {
                MessageBox.Show("Passwords do not match");
            }

            else
            {

                PersonDB.SavePersons(PersonDB.persons);
            }
                
                
                

                //creats an object and sets its values so they are not null
                PerDetails details = new PerDetails();
                details.IDNumber = person.IdNumber;
                details.Type = " ";
                details.Donor = "false";
                details.Hiv = 2;
                details.Hight = 0;
                details.Weight = 0;
                try
                {
                    PersonDB.RegisterPerson(person);
                    PerDetailsDB.RegisterDetails(details);
                    PersonDB.RegisterCareProvider(person);
                    Form newForm2 = new Form2();
                    newForm2.Show();
                    this.Close();
                }
                catch(Exception ex)
                {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    MessageBox.Show("That identification number is already in use, please choose another.");
                }
                else if (ex.Message.Contains("Cannot insert duplicate key in object 'dbo.PRIMARY_CARE_TBL'."))
                {
                    MessageBox.Show("That Primary care ID is already in use, please choose another.");
                }
                }

        }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    /// <summary>
    /// Class for the login form
    /// created by: Nigel Mansell
    /// </summary>
    public partial class Form2 : Form
    {
        //static fields
        public static int ID;
        public static string username;
        public static string password;
        public static string initials;
        public static string title;
        public static string gender;
        public static string workPhone;
        public static string fax;
        public static string email;
        public static string kinName;
        public static string kinRelation;
        public static string kinAddress;
        public static string kinState;
        public static string kinCity;
        public static string kinZip;
        public static string kinPhone;
        public static string kinMobile;
        public static string kinWork;
        public static string kinFax;
        public static string kinEmail;
        public static string careFax;
        public static string careEmail;
        public static string insurer;
        public static string insurancePlan;
        public static int  insuranceNumber;
        public static int primaryId;

        /// <summary>
        /// constructor
        /// </summary>
        public Form2()
        {
            InitializeComponent();
            MessageBox.Show("If this is your first time using this app, please select the register button.");
        }

        /// <summary>
        /// closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        /// <summary>
        /// loads the record form and fills in the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            //clears the list, then gets it
            PersonDB.GetPersons().Clear();
            PersonDB.GetPersons();
            int count = 0;
            foreach (Person person in PersonDB.persons)
            {
                if (person.Password != passwordBox.Text |
                    person.UserName != usernameBox.Text)
                {
                    count++;
                    
                }
                if (count == PersonDB.persons.Count())
                {
                    MessageBox.Show("Incorrect username or password");
                }
                else if (person.Password == passwordBox.Text &
                    person.UserName == usernameBox.Text & 
                    count < PersonDB.persons.Count())
                {
                    // show window and load correct id number.
                    Form recordForm = new Recordfrm();
                    
                    ID = person.IdNumber;
                    title = person.Title;
                    initials = person.Initials;
                    password = person.Password;
                    gender = person.Gender;
                    username = person.UserName;
                    workPhone = person.WorkPhone;
                    fax = person.Fax;
                    email = person.Email;
                    kinName = person.ContactName;
                    kinRelation = person.ContactRelation;
                    kinAddress = person.ContactAddress;
                    kinState = person.ContactState;
                    kinCity = person.ContactCity;
                    kinZip = person.ContactZip;
                    kinPhone = person.ContactPhone;
                    kinMobile = person.ContactMobile;
                    kinWork = person.ContactWork;
                    kinFax = person.ContactFax;
                    kinEmail = person.ContactEmail;
                    careFax = person.CareProviderFax;
                    careEmail = person.CareProviderEmail;
                    insurer = person.Insurer;
                    insurancePlan = person.InsurancePlan;
                    insuranceNumber = person.InsuranceNumber;
                    primaryId = person.PrimaryID;

                    
                    if (Validator.IsPresent(usernameBox) == true)
                    {
                        recordForm.Activate();
                        this.Hide();
                        recordForm.Show();
                    }
                    else
                    {
                        Validator.Title = "Username";
                    }
                    
                        
                    
                        

                }

            }

        }

        /// <summary>
        /// opens register form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerButton_Click(object sender, EventArgs e)
        {
            Form newForm1 = new Form1();
            newForm1.Show();
            this.Hide();
        }
    }
}

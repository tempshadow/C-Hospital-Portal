using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject {
    /// <summary>
    /// class for the patient and care table consists of fields constructors,
    /// and properties
    /// created by: Nigel Mansell
    /// </summary>
	class Person {
        // fields
		string username;
		string password;
		string cnfrmPass;
        string gender;
		int idNumber;
		string initials;
        string title;
		string lastName;
		string firstName;
		string date;
        string street = " ";
        string city = " ";
        string state = " ";
        int zip = 0;
        string homePhone = " ";
        string mobilePhone = " ";
        string workPhone = " ";
        string fax = " ";
        string email = " ";
        int primaryId = 0;

        string contactName = " ";
        string contactRelation = " ";
        string contactAddress = " ";
        string contactState = " ";
        string contactCity = " ";
        string contactZip = " ";
        string contactPhone = " ";
        string contactMobile = " ";
        string contactWork = " ";
        string contactFax = " ";
        string contactEmail = " ";

        string careProviderFirst = " ";
        string careProviderLast = " ";
        string careProviderSpecialty = " ";
        string careProviderMobile = " ";
        string careProviderWork = " ";
        string careProviderFax = " ";
        string careProviderEmail = " ";

        string insurer = " ";
        string insurancePlan = " ";
        int insuranceNumber;

        /// <summary>
        /// empty constructor
        /// </summary>
		public Person() {

		}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="initials"></param>
        /// <param name="idNumber"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="date"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="homePhone"></param>
        /// <param name="mobilePhone"></param>
        /// <param name="primaryId"></param>
        /// <param name="contactName"></param>
        /// <param name="contactRelation"></param>
        /// <param name="contactAddress"></param>
        /// <param name="contactState"></param>
        /// <param name="contactCity"></param>
        /// <param name="contactZip"></param>
        /// <param name="contactPhone"></param>
        /// <param name="contactMobile"></param>
        /// <param name="contactWork"></param>
        /// <param name="contactFax"></param>
        /// <param name="workPhone"></param>
        /// <param name="fax"></param>
        /// <param name="email"></param>
        /// <param name="contactEmail"></param>
        /// <param name="careProviderFirst"></param>
        /// <param name="careProviderSpecialty"></param>
        /// <param name="careProviderMobile"></param>
        /// <param name="careProviderWork"></param>
        /// <param name="careProviderFax"></param>
        /// <param name="careProviderEmail"></param>
        /// <param name="insurer"></param>
        /// <param name="insurancePlan"></param>
        /// <param name="insuranceNumber"></param>
        /// <param name="careProviderLast"></param>
		public Person(string username, string password,string initials, int idNumber, string lastName,
			string firstName, string date, string street, string city, string state, int zip,
            string homePhone, string mobilePhone, int primaryId, string contactName, string contactRelation,
            string contactAddress, string contactState, string contactCity, string contactZip,
            string contactPhone, string contactMobile, string contactWork, string contactFax,
            string workPhone, string fax, string email, string contactEmail, string careProviderFirst, 
            string careProviderSpecialty, string careProviderMobile, string careProviderWork, 
            string careProviderFax, string careProviderEmail, string insurer, string insurancePlan, 
            int insuranceNumber, string careProviderLast) {

			this.username = username;
			this.password = password;
			this.idNumber = idNumber;
			this.initials = initials;
			this.lastName = lastName;
			this.firstName = firstName;
			this.date = date;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.homePhone = homePhone;
            this.mobilePhone = mobilePhone;
            this.workPhone = workPhone;
            this.fax = fax;
            this.email = email;
            this.primaryId = primaryId;
            this.contactName = contactName;
            this.contactRelation = contactRelation;
            this.contactAddress = contactAddress;
            this.contactState = contactState;
            this.contactCity = contactCity;
            this.contactZip = contactZip;
            this.contactPhone = contactPhone;
            this.contactMobile = contactMobile;
            this.contactWork = contactWork;
            this.contactFax = contactFax;
            this.contactEmail = contactEmail;
            this.careProviderFirst = careProviderFirst;
            this.careProviderLast = careProviderLast;
            this.careProviderSpecialty = careProviderSpecialty;
            this.careProviderMobile = careProviderMobile;
            this.careProviderWork = careProviderWork;
            this.careProviderFax = careProviderFax;
            this.careProviderEmail = careProviderEmail;
            this.insurer = insurer;
            this.insurancePlan = insurancePlan;
            this.insuranceNumber = insuranceNumber;



		}

        /// <summary>
        /// username property
        /// </summary>
		public string UserName {
			get { return username; }
			set { username = value; }

		}

        /// <summary>
        /// password property
        /// </summary>
		public string Password {
			get { return password; }
			set { password = value; }
		}

        /// <summary>
        /// confirm password property
        /// </summary>
		public string CnfrmPass {
			get { return cnfrmPass; }
			set { cnfrmPass = value; }
		}

        /// <summary>
        /// patient id property
        /// </summary>
		public int IdNumber {
			get { return idNumber; }
			set { idNumber = value; }
		}

        /// <summary>
        /// initials property
        /// </summary>
		public string Initials {
			get { return initials; }
			set { initials = value; }
		}

        /// <summary>
        /// last name property
        /// </summary>
		public string LastName {
			get { return lastName; }
			set { lastName = value; }
		}

        /// <summary>
        /// first name property
        /// </summary>
		public string FirstName {
			get { return firstName; }
			set { firstName = value; }
		}

        /// <summary>
        /// date property
        /// </summary>
		public string Date {
			get { return date; }
			set { date = value; }
		}

        /// <summary>
        /// street property
        /// </summary>
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        /// <summary>
        /// city property
        /// </summary>
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        /// <summary>
        /// state property
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// zip property
        /// </summary>
        public int Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        /// <summary>
        /// home phone property
        /// </summary>
        public string HomePhone
        {
            get { return homePhone; }
            set { homePhone = value; }
        }

        /// <summary>
        /// mobile phone property
        /// </summary>
        public string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }

        /// <summary>
        /// work phone property
        /// </summary>
        public string WorkPhone
        {
            get { return workPhone; }
            set { workPhone = value; }
        }

        /// <summary>
        /// fax property
        /// </summary>
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        /// <summary>
        /// email property
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// primary id property
        /// </summary>
        public int PrimaryID
        {
            get { return primaryId; }
            set { primaryId = value; }
        }

        /// <summary>
        /// title property
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// gender property
        /// </summary>
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        /// <summary>
        /// contact name property
        /// </summary>
        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; }
        }

        /// <summary>
        /// contact relation property
        /// </summary>
        public string ContactRelation
        {
            get { return contactRelation; }
            set { contactRelation = value; }
        }

        /// <summary>
        /// contact address property
        /// </summary>
        public string ContactAddress
        {
            get { return contactAddress; }
            set { contactAddress = value; }
        }

        /// <summary>
        /// contact state property
        /// </summary>
        public string ContactState
        {
            get { return contactState; }
            set { contactState = value; }
        }

        /// <summary>
        /// contact city property
        /// </summary>
        public string ContactCity
        {
            get { return contactCity; }
            set { contactCity = value; }
        }

        /// <summary>
        /// contact zip property
        /// </summary>
        public string ContactZip
        {
            get { return contactZip; }
            set { contactZip = value; }
        }

        /// <summary>
        /// contact phone property
        /// </summary>
        public string ContactPhone
        {
            get { return contactPhone; }
            set { contactPhone = value; }
        }

        /// <summary>
        /// contact mobile property
        /// </summary>
        public string ContactMobile
        {
            get { return contactMobile; }
            set { contactMobile = value; }
        }

        /// <summary>
        /// contact work property
        /// </summary>
        public string ContactWork
        {
            get { return contactWork; }
            set { contactWork = value; }
        }

        /// <summary>
        /// contact fax property
        /// </summary>
        public string ContactFax
        {
            get { return contactFax; }
            set { contactFax = value; }
        }

        /// <summary>
        /// contact email property
        /// </summary>
        public string ContactEmail
        {
            get { return contactEmail; }
            set { contactEmail = value; }
        }

        /// <summary>
        /// care provider first name property
        /// </summary>
        public string CareProviderFirst
        {
            get { return careProviderFirst; }
            set { careProviderFirst = value; }
        }

        /// <summary>
        /// care prover last name property
        /// </summary>
        public string CareProviderLast
        {
            get { return careProviderLast; }
            set { careProviderLast = value; }
        }

        /// <summary>
        /// care provider specialty property
        /// </summary>
        public string CareProviderSpecialty
        {
            get { return careProviderSpecialty; }
            set { careProviderSpecialty = value; }
        }

        /// <summary>
        /// care provider mobile property
        /// </summary>
        public string CareProviderMobile
        {
            get { return careProviderMobile; }
            set { careProviderMobile = value; }
        }

        /// <summary>
        /// care provider work property
        /// </summary>
        public string CareProviderWork
        {
            get { return careProviderWork; }
            set { careProviderWork = value; }
        }

        /// <summary>
        /// care provider fax property
        /// </summary>
        public string CareProviderFax
        {
            get { return careProviderFax; }
            set { careProviderFax = value; }
        }

        /// <summary>
        /// care proivder email property
        /// </summary>
        public string CareProviderEmail
        {
            get { return careProviderEmail; }
            set { careProviderEmail = value; }
        }

        /// <summary>
        /// insurer property
        /// </summary>
        public string Insurer
        {
            get { return insurer; }
            set { insurer = value; }
        }

        /// <summary>
        /// insurance plan property
        /// </summary>
        public string InsurancePlan
        {
            get { return insurancePlan; }
            set { insurancePlan = value; }
        }

        /// <summary>
        /// insurance number property
        /// </summary>
        public int InsuranceNumber
        {
            get { return insuranceNumber; }
            set { insuranceNumber= value; }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// Represents the first name of the person 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Represents the last name of the person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Represents the email address of the person
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Represents the cellphone number of the person
        /// </summary>
        public string CellphoneNumber { get; set; }
    }
}

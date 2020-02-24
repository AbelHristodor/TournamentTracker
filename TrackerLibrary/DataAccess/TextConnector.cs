using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";

        /// <summary>
        /// Saves a new person to the text file
        /// </summary>
        /// <param name="model">The person information</param>
        /// <returns>The person information including the unique identifier.</returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;
            if(people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            people.Add(model);
            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

        /// <summary>
        /// Saves a new prize to the text file
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information including the unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load text file and convert the text to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the max id
            int currentId = 1;
            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            // Add the new record with the new ID (max+1)
            prizes.Add(model);

            // Convert prizes to list<string> and write to file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        /// <summary>
        /// Gets the list of all people in the text file
        /// </summary>
        /// <returns>List of people</returns>
        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }
    }
}

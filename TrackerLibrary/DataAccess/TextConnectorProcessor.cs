using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        /// <summary>
        /// Returns the full path of the text file
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <returns>Full path of the file</returns>
        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filepath"] }\\{ fileName }";
        }

        /// <summary>
        /// Loads the text file
        /// </summary>
        /// <param name="file">Name of the file</param>
        /// <returns>A list with the contents of the file</returns>
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }


        /// <summary>
        /// Converts a list of lines to PrizeModels
        /// </summary>
        /// <param name="lines">List of lines</param>
        /// <returns>List of PrizeModels</returns>
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel
                {
                    Id = int.Parse(cols[0]),
                    PlaceNumber = int.Parse(cols[1]),
                    PlaceName = cols[2],
                    PrizeAmount = decimal.Parse(cols[3]),
                    PrizePercentage = double.Parse(cols[4])
                };

                output.Add(p);
            }
            return output;
        }

        /// <summary>
        /// Converts a list of lines to PeopleModels
        /// </summary>
        /// <param name="lines">List of lines</param>
        /// <returns>List of PeopleModels</returns>
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel {
                    Id = int.Parse(cols[0]),
                    FirstName = cols[1],
                    LastName = cols[2],
                    EmailAddress = cols[3],
                    CellphoneNumber = cols[4]
                };

                output.Add(p);
            }
            return output;
        }

        /// <summary>
        /// Converts a list of lines to TeamModels
        /// </summary>
        /// <param name="lines">List of lines</param>
        /// <param name="peopleFileName">Name of the people file</param>
        /// <returns>List of TeamModels</returns>
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            // id, teamName,list of ids separated by the pipe (people's ids)
            // 3,Tim's Team,1|3|5
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();


            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                TeamModel t = new TeamModel
                {
                    Id = int.Parse(cols[0]),
                    TeamName = cols[1]
                };

                string[] personIds = cols[2].Split('|');
                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }

                output.Add(t);
            }
            return output;
        }

        /// <summary>
        /// Saves PrizeModels to a text file.
        /// </summary>
        /// <param name="models">List of PrizeModels to save</param>
        /// <param name="fileName">Name of the file</param>
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{ p.Id },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Saves PersonModels to a text file.
        /// </summary>
        /// <param name="models">List of PersonModel to save</param>
        /// <param name="fileName">Name of the file</param>
        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{ p.Id },{ p.FirstName },{ p.LastName },{ p.EmailAddress },{ p.CellphoneNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Save TeamModels to a text file.
        /// </summary>
        /// <param name="models">List of TeamModels</param>
        /// <param name="fileName">Name of the file</param>
        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel team in models)
            {
                lines.Add($"{team.Id},{team.TeamName},{ConvertPeopleListToString(team.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Converts a list of People to string using this format: {id_1}|{id_2}|...
        /// </summary>
        /// <param name="people">List of PersonModels</param>
        /// <returns>String containing Ids separated by "|"</returns>
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            if (people.Count == 0) { return ""; }

            foreach (PersonModel p in people)
            {
                output += $"{p.Id}|";
            }

            output = output.Substring(0, output.Length - 1);
            return output;
        }
    }
}

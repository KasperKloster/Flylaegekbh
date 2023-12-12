using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class Examination
    {
        // Fields
        public string ExaminationName { get; set; }
        public decimal Price { get; set; }
        public int DurationInMin { get; set; }


        //###################################################################################################//
        /*This class is consist of a list of dictionary, and each dictionary contains information about each
         examiniation. Later on, ther will be a method that will alow the AME to add new examinations in 
         respons expansion of the practice  */
        //##################################################################################################//
        public static List<Dictionary<string, string>> ExaminationNames = new List<Dictionary<string, string>>();

        public Examination()
        {
            // Creating and adding all examinations to the list, in the most efficient way
           Dictionary<string, string> itemOne = new Dictionary<string, string>(){
                {"name", "Cabin Crew Forstegangs Undersogelse"},
                {"price", "2.125" },
                {"duration", "60" }};

            Dictionary<string, string> itemTwo = new Dictionary<string, string>(){
                {"name", "Cabin Crew Fornyelse"},
                {"price", " 1.500" },
                {"duration", "60" }};

            Dictionary<string, string> itemThree = new Dictionary<string, string>(){
                {"name", "Cabin Crew Fornyelse med EKG"},
                {"price", " 2.050" },
                {"duration", "60" }};

            Dictionary<string, string> itemFour = new Dictionary<string, string>(){
                {"name", "Class 1 Fornyelse"},
                {"price", " 2.400" },
                {"duration", "60" }};

            Dictionary<string, string> itemFive = new Dictionary<string, string>(){
                {"name", "Class 1 Fornyelse + EKG"},
                {"price", " 2.950" },
                {"duration", "60" }};

            Dictionary<string, string> itemSix = new Dictionary<string, string>(){
                {"name", "Class 1 Fornyelse + Audiogram"},
                {"price", " 2.950" },
                {"duration", "60" }};

            Dictionary<string, string> itemSeven = new Dictionary<string, string>(){
                {"name", "Class 1 Fornyelse + EKG + Audiogram"},
                {"price", " 3.500" },
                {"duration", "60" }};

            Dictionary<string, string> itemEight = new Dictionary<string, string>(){
                {"name", "Class 2 Forstegangs Undersogelse"},
                {"price", " 2.000" },
                {"duration", "60" }};

            Dictionary<string, string> itemNine = new Dictionary<string, string>(){
                {"name", "Class 2 Fornyelse"},
                {"price", " 1.500" },
                {"duration", "60" }};

            Dictionary<string, string> itemTen = new Dictionary<string, string>(){
                {"name", "Class 2 Fornyelse + EKG"},
                {"price", " 2.050" },
                {"duration", "60" }};

            Dictionary<string, string> itemEleven = new Dictionary<string, string>(){
                {"name", "Class 2 fornyelse med audiogram"},
                {"price", " 2.050" },
                {"duration", "60" }};

            Dictionary<string, string> itemTwelve= new Dictionary<string, string>(){
                {"name", "Class 2 Fornyelse + EKG + Audiogram"},
                {"price", " 2.600" },
                {"duration", "60" }};

            Dictionary<string, string> itemThirteen = new Dictionary<string, string>(){
                {"name", "Class LAPL Fornyelse"},
                {"price", " 1.500" },
                {"duration", "60" }};

            Dictionary<string, string> itemFourteen = new Dictionary<string, string>(){
                {"name", "Flyvemedicinsk erklæring"},
                {"price", " 1.562" },
                {"duration", "60" }};

            ExaminationNames.Add(itemOne);
            ExaminationNames.Add(itemTwo);
            ExaminationNames.Add(itemThree);
            ExaminationNames.Add(itemFour);
            ExaminationNames.Add(itemFive);
            ExaminationNames.Add(itemSix);
            ExaminationNames.Add(itemSeven);
            ExaminationNames.Add(itemEight);
            ExaminationNames.Add(itemNine);
            ExaminationNames.Add(itemTen);
            ExaminationNames.Add(itemEleven);
            ExaminationNames.Add(itemTwelve);
            ExaminationNames.Add(itemThirteen);
            ExaminationNames.Add(itemFourteen);
        }
    }
}
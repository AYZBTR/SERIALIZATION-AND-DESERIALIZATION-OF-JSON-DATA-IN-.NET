using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;


namespace SERIALIZATION_AND_DESERIALIZATION
{
    class Program
    {
        static void Main(string[] args)
        {
            bool value;
            int NMB;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEnter 1 If you want to show all information of Players.");
            Console.WriteLine("\nEnter 2 If you want to add a new team member.");
            Console.WriteLine("\nEnter 3 If you want to Modify information.");
            Console.WriteLine("\nEnter 4 If you want to remove information.");
            Console.WriteLine("\nEnter 5 If you want to do nothing.");
            Console.ResetColor();


            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nEnter your Choice:");
                Console.ResetColor();
                string received = Console.ReadLine();
                while (!int.TryParse(received, out NMB) || (NMB != 1) && (NMB != 2) && (NMB != 3) && (NMB != 4) && (NMB != 5))

                {
                    Console.Write("Not Valid... Try Again...");
                    received = Console.ReadLine();
                }
                //1. If you want to show all available Information...
                string path = @"/Users/daabh001/Desktop/TEAM_A.json";
                List<Team> teams = new List<Team>();
                //Deserialize the JSON text data into generic list clients:
                using (StreamReader streamReader = new StreamReader(path))
                {
                    var jsonString = streamReader.ReadToEnd();
                    teams = JsonConvert.DeserializeObject<List<Team>>(jsonString);

                }
                switch (NMB)
                {
                    case 1:
                        Console.WriteLine("\nInformation of team members: ");
                        foreach (var item in teams)
                        {
                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }

                        }
                        break;

                    case 2:
                        Console.WriteLine("\nInformation of team members before adding: ");
                        foreach (var item in teams)
                        {


                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }


                        }
                        DateTime DOB;
                        Console.Write("\nEnter the New player Name: ");
                        string Name = Console.ReadLine();

                        Console.Write("\nEnter Date of birth(MM/DD/YYYY): ");
                        string DOBB = Console.ReadLine();

                        while (!DateTime.TryParse(DOBB, out DOB))
                        {
                            Console.WriteLine("\nNot Valid... Try Again!:");
                        }

                        Console.Write("\nEnter Email address of new player: ");
                        string email = Console.ReadLine();

                        Console.Write("\nEnter phone number of new player: ");
                        string Ph = Console.ReadLine();
                        List<Continfo> continfos = new List<Continfo>()
                        {
                            new Continfo(email, Ph)

                        };

                        teams.Add(new Team(Name, DOB, continfos));
                        using (StreamWriter streamWriter = new StreamWriter(path, false))
                        {
                            string jsonData = JsonConvert.SerializeObject(teams);
                            streamWriter.Write(jsonData);
                        }

                        Console.WriteLine("\nAfter adding new player: ");
                        foreach (var item in teams)
                        {
                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }

                        }
                        break;

                    case 3:
                        Console.WriteLine("\nInformation of players before Modifying. ");
                        foreach (var item in teams)
                        {
                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }
                        }
                        Console.Write("\nEnter the name of the player which you want to modify?: ");
                        string Modify = Console.ReadLine();

                        //MODIFYING THE DATA...
                        var searched = teams.Where(tm => tm.Name.Contains(Modify));
                        int NumberofPlayers = teams.Count;
                        if (searched.Any())
                        {
                            for (int i = 0; i < NumberofPlayers; i++)
                            {
                                if (teams[i].Name.Contains(Modify))
                                {
                                    Console.WriteLine("\nThere is client {0}.", teams[i].Name);
                                    Console.Write("\nWould you like modify client's information (Y/N)?: ");
                                    string Choice = Console.ReadLine().ToUpper();
                                    if (Choice.StartsWith("Y"))
                                    {

                                        Console.Write("\nInput a new name:");
                                        string NewName = Console.ReadLine();
                                        if (!String.IsNullOrEmpty(NewName))
                                            teams[i].Name = NewName;

                                        DateTime NewDOB;
                                        Console.Write("\nEnter the Date Of Birth(MM/DD/YYYY):");
                                        string dob = Console.ReadLine();
                                        if (!String.IsNullOrEmpty(dob))
                                        {
                                            while (!DateTime.TryParse(dob, out NewDOB))
                                            {
                                                Console.WriteLine("\nNot Valid... Try Again!:");
                                                string New = Console.ReadLine();
                                            }
                                            teams[i].DateOfBirth = NewDOB;
                                        }

                                        Console.Write("\nEnter the Email Address:");
                                        string NewEmail = Console.ReadLine();
                                        //if (!String.IsNullOrEmpty(NewEmail))
                                        //    teams[i].Email = NewEmail;

                                        Console.Write("\nEnter the Phone Number:");
                                        string NewPn = Console.ReadLine();
                                        //if (!String.IsNullOrEmpty(NewPn))
                                        //    teams[i].Mobile = NewPn;
                                        List<Continfo> continfo = new List<Continfo>()
                                        {
                                             new Continfo(NewEmail, NewPn)

                                        };
                                        teams[i].ContactInformation = continfo;



                                    }
                                }
                            }
                            using (StreamWriter streamWriter = new StreamWriter(path, false))
                            {
                                string jsonData = JsonConvert.SerializeObject(teams);
                                streamWriter.Write(jsonData);
                            }
                        }

                        else
                        {
                            Console.Write("\nNo such player.");
                            Console.WriteLine();
                        }

                        //DID the player modified?

                        List<Team> inTheEnd = new List<Team>();

                        using (StreamReader streamReader = new StreamReader(path))
                        {
                            var jsonString = streamReader.ReadToEnd();
                            inTheEnd = JsonConvert.DeserializeObject<List<Team>>(jsonString);

                        }
                        //File after possibly modifying a client...
                        Console.WriteLine("\nAfter Possibly modifying a Player");
                        foreach (var item in inTheEnd)
                        {
                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }
                        }
                        break;

                    case 4:
                        Console.WriteLine("\nInformation before removing a player: ");
                        foreach (var item in teams)
                        {
                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }
                        }

                        //Situation before removing a player from team...

                        //Console.WriteLine("\nBefore removing data information of the players:");
                        Console.Write("\nEnter the name of the Player you want to remove?: ");
                        string get = Console.ReadLine();

                        var DeletePlayer = teams.Where(teams => teams.Name.Contains(get));
                        int NumOfPlayer = teams.Count();

                        if (get.Any())
                        {
                            //remove the possible player
                            for (int i = 0; i < NumOfPlayer; i++)
                            {
                                if (teams[i].Name.Contains(get))
                                {
                                    Console.WriteLine("\nThere is a player name {0}.", teams[i].Name);
                                    Console.Write("\nDo you want to remove the player?(Y/N): ");
                                    string decision = Console.ReadLine().ToUpper();
                                    if (decision.StartsWith("Y"))
                                    {
                                        bool wasdone = teams.Remove(teams[i]);
                                        if (wasdone)
                                        {
                                            Console.WriteLine("\nSucessfully removed the player from Team.");
                                            NumOfPlayer--;
                                            i--;
                                        }
                                    }
                                }
                            }
                            using (StreamWriter streamWriter = new StreamWriter(path, false))
                            {
                                string jsonData = JsonConvert.SerializeObject(teams);
                                streamWriter.Write(jsonData);
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("\nNo such Player...");
                            Console.WriteLine();
                        }

                        //Was the player removed from list??
                        List<Team> atLast = new List<Team>();
                        using (StreamReader streamReader = new StreamReader(path))
                        {
                            var jsonString = streamReader.ReadToEnd();
                            atLast = JsonConvert.DeserializeObject<List<Team>>(jsonString);

                        }

                        //File after removing player from list...
                        Console.WriteLine("\nAfter Removing The Player");
                        foreach (var item in atLast)
                        {
                            foreach (var cr in item.ContactInformation)
                            {
                                Console.WriteLine("\nName: {0},\nDate of birth: {1},\nContact Information: \nEmail: {2},\nMobile: {3}", item.Name, item.DateOfBirth.ToString("d"),
                            cr.Email, cr.Mobile);
                            }
                        }
                        Console.WriteLine();

                        break;
                    case 5:
                        Console.WriteLine("\nYou entered 5. The Program exits here...\nKiitos. Moi Moi! ");
                        value = false;
                        return;

                }

            } while (true);

        }
    }
}

    


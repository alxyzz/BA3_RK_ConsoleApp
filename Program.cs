using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BA3_RK_ConsoleApp
{


    /*
     * 
     * manager for entries of names
     * three versions same functionality
     * array list dictionary.
     * add entries
     * get entry at position
     * remove entry at position
     * list all entries in order
     * 
     */

    static class Program
    {//name manager
        static List<string> namesList = new List<string>();
        static Dictionary<int, string> namesDict = new Dictionary<int, string>();
        static string[] namesArray = new string[128];
        static List<string> arguments = new List<string>();
        static string currentOrder = "none";




        static void Main(string[] args)
        {
            Console.Clear();
            bool b = false;
            while (!b)
            {
                Console.WriteLine("Choose wether you want to use [a]rrays, [l]ists or [d]ictionaries for your name management.\n Data is not persistent between instances.\n Input your answer now.");
                string choice = Console.ReadKey().KeyChar.ToString().ToLower();
                switch (choice)
                {
                    case "array":
                    case "a":

                        managerState = currState.Array;
                        ArrayMain();
                        break;
                    case "list":
                    case "l":

                        managerState = currState.List;
                        ListMain();
                        break;
                    case "dictionary":
                    case "d":
                        managerState = currState.Dict;
                        DictMain();
                        break;


                    default:
                        Console.WriteLine("Invalid answer. Pick one of the available options. \n Press a key to continue.");
                        Console.Clear();
                        break;
                }


            }



        }
        enum currState
        {
            Array,
            Dict,
            List,
            uninitialized
        }
        static currState managerState = currState.uninitialized;
        /*1. we show all current entries (up to 30
         *2. we allow player to add entries either to the end or somewhere in the middle
         *3. we allow player to retrieve entries at a specific position
         *4. we allow player to remove entries at a specific position
         *5. list all entires in order
         * 
         * 
         * 
         * 
         * 
         * 
         */
        static private void ArrayMain()
        {
            Console.Clear();
            Console.WriteLine("NAME MANAGER, ARRAY EDITION");
            DisplayEntriesArray(namesArray);
        }
        static private void ListMain()
        {
            Console.Clear();
            Console.WriteLine("NAME MANAGER, LIST EDITION");
            DisplayEntriesList(namesList);
        }
        static private void DictMain()
        {
            Console.Clear();
            Console.WriteLine("NAME MANAGER, DICTIONARY EDITION");
            DisplayEntriesDict(namesDict);
        }

        static private void InformOnControls()
        {
            Console.WriteLine("[A]dd new entry|[D]elete entry|[G]et entry by id|[R]efresh list|[B]ack to start");
        }
        static private void HandleInput()
        {
            string b = Console.ReadKey().Key.ToString().ToLower();
            switch (b)
            {
                case "a"://add entry
                    switch (managerState)
                    {
                        case currState.Array:
                            AddEntryToArray();
                            break;
                        case currState.Dict:
                            AddEntryToDict();
                            break;
                        case currState.List:
                            AddEntryToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case "d"://delete entry
                    switch (managerState)
                    {
                        case currState.Array:
                            RemoveEntryFromArray();
                            break;
                        case currState.Dict:
                            RemoveEntryFromDict();
                            break;
                        case currState.List:
                            RemoveEntryFromList();
                            break;

                        default:
                            break;
                    }
                    break;
                case "g"://get entry by ID
                    switch (managerState)
                    {
                        case currState.Array:
                            GetEntryByIDArray();
                            break;
                        case currState.Dict:
                            GetEntryByIDDict();
                            break;
                        case currState.List:
                            GetEntryByIDList();
                            break;
                        default:
                            break;
                    }
                    break;
                case "r"://refresh list
                    switch (managerState)
                    {
                        case currState.Array:
                            ArrayMain();
                            break;
                        case currState.Dict:
                            DictMain();
                            break;
                        case currState.List:
                            ListMain();
                            break;
                        default:
                            break;
                    }
                    break;
                case "b":
                    string[] g = new string[0];
                    Main(g);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Press anything to return.");
                    Console.ReadLine();
                    Console.Clear();
                    ReturnToInitialState();
                    break;
            }
        }
        static private void ReturnToInitialState()
        {
            switch (managerState)
            {
                case currState.Array:
                    ArrayMain();
                    break;
                case currState.Dict:
                    DictMain();
                    break;
                case currState.List:
                    ListMain();
                    break;

                default:
                    Main(arguments.ToArray());
                    break;
            }
        }
        //array segment
        static private void DisplayEntriesArray(string[] b, string order = "")
        {
            InformOnControls();
            if (b.Length == 0)
            {

                Console.WriteLine("No entries yet.");

            }
            else
            {
                int nr = 0;

                foreach (string item in b)
                {
                    if (item != null)
                    {
                        Console.Write(Array.IndexOf(namesArray, item) + ". " + item.ToString() + Environment.NewLine);
                        nr++;
                    }

                }
            }

            HandleInput();
        }
        static private void AddEntryToArray()
        {
            try
            {


                List<string> b = new List<string>();
                b.AddRange(namesArray);
                int indexOfInsertion = b.Count;
                bool answered = false;

                while (!answered)
                {
                    ////CHOOSE INDEX
                    try
                    {

                        Console.Clear();
                        Console.WriteLine("Input your desired index, if any, or press enter to add your entry.\n Numbers only.\n");
                        string d = "";
                        d = Console.ReadLine();//just to remove trailing spaces if any.
                        if (Regex.IsMatch(d, @"[a-zA-Z]"))
                        {
                            throw new Exception("Numbers only. No letters.");
                        }
                        indexOfInsertion = int.Parse(d.Replace(" ", ""));
                        //if ( namesArray[indexOfInsertion] != ""|| namesArray[indexOfInsertion] != " ")
                        //{
                        //    throw new Exception($"Index occupied. Occupied index is  {namesArray[indexOfInsertion]}");
                        //}
                        if (indexOfInsertion > b.Count || indexOfInsertion < 0)
                        {
                            throw new Exception("Index is out of bounds.");
                        }
                        if (d == "" || d == " ")
                        {
                            Console.WriteLine("Adding to end of array...\n");
                            indexOfInsertion = namesArray.Length;
                            answered = true;

                        }
                        else
                        {
                            try
                            {
                                indexOfInsertion = int.Parse(d.Replace(" ", ""));

                                answered = true;
                            }
                            catch (Exception e)
                            {

                                throw e;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error.\nError message is " + e.Message + "\nInput anything to continue.\n");
                        Console.ReadLine();
                        Console.Clear();
                    }

                }
                if (indexOfInsertion == namesArray.Length)
                {
                    Console.WriteLine($"Adding entry to the end of the list.\n");
                }
                else
                {
                    Console.WriteLine($"Index chosen: {indexOfInsertion}.\n");
                }
                ////CHOOSE NAME
                Console.WriteLine($"Now input the desired full name.\n");
                string i = Console.ReadLine().Replace(Environment.NewLine, "");
                if (b.Contains(i))
                {
                    throw new Exception("Duplicate entry. No doppelgangers.");
                }
                if (i == "" || i == " ")
                {
                    Console.WriteLine("Empty name detected. Please read the instructions carefully.\nInput anything to continue.\n");
                    Console.ReadLine();
                    Console.Clear();
                    AddEntryToArray();
                    return;
                }


                b.Insert(indexOfInsertion, i);
                namesArray = b.ToArray();
                Console.Clear();
                Console.WriteLine("Successfully inserted entry [" + i + "] at index " + indexOfInsertion + "\nInput anything to continue.\n");
                Console.ReadLine();
                ArrayMain();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error.\n" + e.Message + "\nInput anything to continue.\n");
                Console.ReadLine();
                Console.Clear();
                ArrayMain();
            }
        }


        static private void RemoveEntryFromArray()
        {
            try
            {


                List<string> b = new List<string>();
                b.AddRange(namesArray);
                int indexOfDeletion = 0;
                bool answered = false;
                while (!answered)
                {
                    Console.Clear();
                    Console.WriteLine("Input your desired index for entry removal.");

                    string d = Console.ReadLine();//just to remove trailing spaces if any.
                    if (d == "" || d == " ")
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input.\nPress anything to continue.");
                        Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            indexOfDeletion = int.Parse(d.Replace(" ", ""));
                            answered = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error with input. Please read the instructions carefully. \nError message is " + e.Message + "\n Press anything to continue");
                            Console.ReadLine();
                            Console.Clear();

                            throw;
                        }

                    }
                }



                string rem = b[indexOfDeletion];
                b[indexOfDeletion] = null; ;
                namesArray = b.ToArray();
                Console.Clear();
                if (rem == "" || rem == " ")
                {
                    Console.WriteLine("Tried to remove entry at index " + indexOfDeletion + ", however, it was already empty.\nPress anything to continue.");
                }
                else
                {
                    Console.WriteLine("Successfully removed entry [" + rem + "] at index " + indexOfDeletion + "\nPress anything to continue.");
                }

                Console.ReadLine();
                ArrayMain();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error.\n" + e.Message + "\nInput anything to continue.\n");
                Console.ReadLine();
                Console.Clear();
                ArrayMain();
            }
        }
        static private void GetEntryByIDArray()
        {
            int desiredIndex = 0;
            bool answered = false;
            while (!answered)
            {
                Console.Clear();
                Console.WriteLine("Input your desired entry's index.");

                string d = Console.ReadLine();//just to remove trailing spaces if any.
                if (d == "" || d == " ")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input.\nPress anything to continue.");
                    Console.ReadLine();
                }
                else
                {
                    try
                    {
                        desiredIndex = int.Parse(d.Replace(" ", ""));
                        answered = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error with input. Please read the instructions carefully. \nError message is " + e.Message + "\n Press anything to continue");
                        Console.ReadLine();
                        Console.Clear();
                        answered = true;

                    }

                }

            }

            Console.Clear();
            Console.WriteLine($"Requested entry at {desiredIndex} is [{namesArray[desiredIndex]}]" + "\nPress anything to continue.");
            Console.ReadLine();
            ArrayMain();
        }

        //list segment
        static private void DisplayEntriesList(List<string> b, string order = "")
        {
            InformOnControls();
            if (b.Count == 0)
            {
                Console.WriteLine("No entries yet.");
            }
            else
            {
                int nr = 0;

                foreach (string item in b)
                {
                    if (item != null)
                    {
                        Console.Write(nr.ToString() + ". " + item.ToString() + Environment.NewLine);
                        nr++;
                    }

                }

            }
            HandleInput();
        }
        static private void AddEntryToList()
        {
            try
            {

                Console.WriteLine("\nInput your new name.");
                string i = Console.ReadLine().Replace(Environment.NewLine, "");
                if (namesList.Contains(i))
                {
                    throw new Exception("\nEntry already exists.");
                }

                namesList.Add(i);

                Console.Clear();
                Console.WriteLine("\nSuccessfully added new entry." + "\nPress anything to continue.");
                Console.ReadLine();
                ListMain();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error.\n" + e.Message + "\nInput anything to continue.\n");
                Console.ReadLine();
                Console.Clear();
            }
        }
        static private void RemoveEntryFromList()
        {
            List<string> b = namesList;

            int indexOfDeletion = 0;
            bool answered = false;
            while (!answered)
            {
                //Console.Clear(); we show the user the list so you dont have to memorize it.
                Console.WriteLine("\nInput your desired index for removal.");

                string d = Console.ReadLine();
                if (d == "" || d == " ")
                {
                    Console.Clear();
                    Console.WriteLine("\nInvalid input.\nPress anything to continue.");
                    Console.ReadLine();
                }
                else
                {
                    try
                    {
                        indexOfDeletion = int.Parse(d.Replace(" ", ""));//just to remove trailing spaces if any.
                        answered = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\nError with input. Please read the instructions carefully. \nError message is " + e.Message + "\n Press anything to continue");
                        Console.ReadLine();
                        Console.Clear();

                        throw;
                    }

                }
            }



            string rem = b[indexOfDeletion];
            b.RemoveAt(indexOfDeletion);
            namesList = b;
            Console.Clear();
            if (rem == "")
            {
                Console.WriteLine("Tried to remove entry at index" + indexOfDeletion + ", however, it was already empty.\nPress anything to continue.");
            }
            else
            {
                Console.WriteLine("Successfully removed entry [" + rem + "] at index " + indexOfDeletion + "\nPress anything to continue.");
            }

            Console.ReadLine();
            ListMain();
        }
        static private void GetEntryByIDList()
        {
            try
            {


                int desiredIndex = 0;
                bool answered = false;
                while (!answered)
                {
                    Console.Clear();
                    Console.WriteLine("Input your desired entry's index.");

                    string d = Console.ReadLine();
                    if (d == "" || d == " ")
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input.\nPress anything to continue.");
                        Console.ReadLine();
                    }
                    else
                    {

                        desiredIndex = int.Parse(d.Replace(" ", ""));//just to remove trailing spaces if any.


                        if (namesList[desiredIndex - 1] == null)
                        {
                            throw new Exception("Related index does not exist in registry.");
                        }
                        answered = true;


                    }

                }

                Console.Clear();
                Console.WriteLine($"Requested entry at {desiredIndex} is [{namesList[desiredIndex]}]" + "\nPress anything to continue.");
                Console.ReadLine();
                ListMain();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message + "\n Press anything to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        //dictionary segment
        static private void DisplayEntriesDict(Dictionary<int, string> b, string order = "")
        {
            InformOnControls();
            foreach (KeyValuePair<int, string> entry in b)
            {
                if (entry.Value != null)
                {
                    Console.Write(entry.Key.ToString() + ". " + entry.Value + Environment.NewLine);
                }

            }
            HandleInput();
        }
        static private void AddEntryToDict()
        {
            try
            {

                int desiredKey;


                Console.Clear();
                Console.WriteLine("\nInput the desired key of your addition, as an integer.\n");

                string d = Console.ReadLine();//PICK KEY
                if (Regex.IsMatch(d, @"[a-zA-Z]"))
                {
                    throw new Exception("Numbers only. No letters.");
                }
                if (d == "" || d == " ")
                {
                    throw new Exception("Key cannot be empty.");

                }

                desiredKey = int.Parse(d.Replace(" ", ""));
                Console.WriteLine($"Key chosen: {desiredKey}.\nNow please input the desired name.");



                string i = Console.ReadLine().Replace(Environment.NewLine, ""); //PICK NAME



                namesDict.Add(desiredKey, i);
                Console.Clear();
                Console.WriteLine("Successfully inserted name [" + i + "] with key [" + desiredKey + "]\nPress anything to continue.");
                Console.ReadLine();
                DictMain();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:\n" + e.Message + "\nInput anything to continue.\n");
                Console.ReadLine();
                Console.Clear();
                DictMain();
            }
        }
        static private void RemoveEntryFromDict()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Input your desired key for entry removal.");

                string d = Console.ReadLine();//just to remove trailing spaces if any.
                int desiredKey = int.Parse(d);
                if (Regex.IsMatch(d, @"[a-zA-Z]"))
                {
                    throw new Exception("Numbers only. No letters.");
                }
                if (d == "" || d == " ")
                {
                    throw new Exception("Key cannot be empty.");
                }
                string t;
                namesDict.TryGetValue(desiredKey, out t);

                if (!namesDict.Remove(desiredKey))
                {
                    throw new Exception($"Invalid key. No associated value for key [{desiredKey}].");
                }
                Console.Clear();
                Console.WriteLine("Successfully removed entry [" + t + "] with key [" + desiredKey + "]\nPress anything to continue.");
                Console.ReadLine();
                DictMain();
            }
            catch (Exception e)
            {

                Console.WriteLine("Error:\n" + e.Message + "\nInput anything to continue.\n");
                Console.ReadLine();
                Console.Clear();
                DictMain();
            }
        }
        static private void GetEntryByIDDict()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Input your desired key for entry removal.");

                string d = Console.ReadLine();//just to remove trailing spaces if any.
                int desiredKey = int.Parse(d);
                if (Regex.IsMatch(d, @"[a-zA-Z]"))
                {
                    throw new Exception("Numbers only. No letters.");
                }
                if (d == "" || d == " ")
                {
                    throw new Exception("Key cannot be empty.");
                }
                string t;
                if (!namesDict.TryGetValue(desiredKey, out t))
                {
                    throw new Exception($"Invalid key. No associated value for key [{desiredKey}].");
                }

                Console.Clear();
                Console.WriteLine($"Found entry with name [{t}] by the key [{desiredKey}]\nPress anything to continue.");
                Console.ReadLine();
                DictMain();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:\n" + e.Message + "\nInput anything to continue.\n");
                Console.ReadLine();
                Console.Clear();
                DictMain();
            }
        }


    }
}

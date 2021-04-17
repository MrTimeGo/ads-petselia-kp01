using System;
using static System.Console;
using System.Collections.Generic;

namespace lab7
{
    static class ConsoleInterface
    {
        public static void Run()
        {
            bool exit = false;
            Hashtable<User, UserInformation> dataBase = new Hashtable<User, UserInformation>();
            Hashtable<AgeCategory, LinkedList<string>> ageCategories = new Hashtable<AgeCategory, LinkedList<string>>();
            string[] ages = new string[] { "young", "adult", "middle", "senior" };
            for (int i = 0; i < ages.Length; i++)
            {
                ageCategories.Insert(new AgeCategory(ages[i]), new LinkedList<string>());
            }

            WriteLine("Type 'help' to get help");
            while (!exit)
            {
                Write("> ");
                string input = ReadLine();
                if (input == "exit" || input == "")
                {
                    exit = true;
                    continue;
                }
                if (input == "help")
                {
                    GetHelp();
                    continue;
                }
                try
                {
                    ProgramArguments programArguments = ParseInput(input);
                    switch (programArguments.command)
                    {
                        case "control":
                            {
                                ExecuteControl(programArguments, dataBase, ageCategories);
                                break;
                            }
                        case "register":
                            {
                                ExecuteRegister(programArguments, dataBase, ageCategories);
                                break;
                            }
                        case "delete":
                            {
                                ExecuteDelete(programArguments, dataBase, ageCategories);
                                break;
                            }
                        case "login":
                            {
                                ExecuteLogin(programArguments, dataBase);
                                break;
                            }
                        case "printDataBase":
                            {
                                WriteLine(dataBase);
                                break;
                            }
                        case "identifyAgeCategories":
                            {
                                ExecuteIdentifyAgeCategories(programArguments, ageCategories);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Error.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private static void GetHelp()
        {
            string[] commands = new string[] 
            { 
                "control",
                "register {username} {password} {emailAddress} {dd/mm/yyyy}",
                "delete {username} {password}",
                "login {username} {password}",
                "printDataBase",
                "identifyAgeCategory"
            };
            string[] info = new string[]
            {
                "fill database with control example",
                "add user to database",
                "remove user from database",
                "find data about user",
                "get database content",
                "get stats about every age category"
            };
            for (int i = 0; i < commands.Length; i++)
            {
                WriteLine(commands[i] + " - " + info[i]);
            }
        }
        private static void ExecuteControl(ProgramArguments programArguments, Hashtable<User, UserInformation> dataBase, Hashtable<AgeCategory, LinkedList<string>> ageCategories)
        {
            if (programArguments.otherArgs.Length != 0)
            {
                throw new ArgumentException($"Command 'control' should have 0 arguments. Got: {programArguments.otherArgs.Length}");
            }
            string[] usernames = new string[] { "pr0skilled", "Sniper", "Mefta", "Demon", "MrTimeGo", "Seventh" };
            string[] passwords = new string[] { "1235", "qwerty", "097022", "shdadsad", "1029384756", "10062003"};

            User[] users = new User[usernames.Length];

            UserInformation[] informations = new UserInformation[]
            {
                new UserInformation("pr0skilled@ukr.net", new DateTime(2005, 5, 31)),
                new UserInformation("Sniper@gmail.com", new DateTime(1991, 8, 24)),
                new UserInformation("mefta@kpi.ua", new DateTime(1961, 6, 15)),
                new UserInformation("random@email.com", new DateTime(2001, 4, 12)),
                new UserInformation("freemail@ukr.net", new DateTime(2002, 2, 28)),
                new UserInformation("info@kpi.ua", new DateTime(1900, 1, 1))
            };

            WriteLine("Control example users: ");
            for (int i = 0; i < usernames.Length; i++)
            {
                WriteLine($"User {i + 1}: username - \"{usernames[i]}\", password - \"{passwords[i]}\"");
                WriteLine(informations[i]);
                users[i] = new User(usernames[i], passwords[i]);
            }

            WriteLine();

            for (int i = 0; i < usernames.Length; i++)
            {
                int age = (int)((DateTime.Now - informations[i].birthDate).TotalDays / 365.25);
                if (age >= 18)
                {
                    dataBase.Insert(users[i], informations[i]);
                    WriteLine($"User {users[i].username} was succesfully registered.");
                    string ageCategory = ChooseAgeCategory(age);
                    LinkedList<string> list = ageCategories.Find(new AgeCategory(ageCategory));
                    if (!list.Contains(users[i].username))
                    {
                        list.AddLast(users[i].username);
                    }
                }
                else
                {
                    WriteLine($"User {users[i].username} was not registered, because his age is under 18.");
                }
            }

        }
        private static void ExecuteRegister(ProgramArguments programArguments, Hashtable<User, UserInformation> dataBase, Hashtable<AgeCategory, LinkedList<string>> ageCategories)
        {
            if (programArguments.otherArgs.Length != 4)
            {
                throw new ArgumentException($"Command 'register' should have 6 arguments. Got: {programArguments.otherArgs.Length}");
            }

            string username = programArguments.otherArgs[0];
            string password = programArguments.otherArgs[1];
            string email = programArguments.otherArgs[2];
            if (!DateTime.TryParse(programArguments.otherArgs[3], out DateTime birthDate))
            {
                throw new ArgumentException($"Incorrect date format. Should be 'dd/mm/yyyy'. Got: {programArguments.otherArgs[3]}");
            }

            User user = new User(username, password);
            UserInformation userInformation = new UserInformation(email, birthDate);

            int age = (int)((DateTime.Now - userInformation.birthDate).TotalDays / 365.25);
            if (age >= 18)
            {
                if (dataBase.LoadFactor > 0.5)
                {
                    WriteLine($"Data base was rehashed, because its loadness factor was more than 50%.");
                    dataBase.Rehash();
                }
                dataBase.Insert(user, userInformation);
                WriteLine($"User {user.username} was succesfully registered.");
                string ageCategory = ChooseAgeCategory(age);
                LinkedList<string> list = ageCategories.Find(new AgeCategory(ageCategory));
                if (!list.Contains(user.username))
                {
                    list.AddLast(user.username);
                }
            }
            else
            {
                WriteLine($"User {user.username} was not registered, because his age is under 18.");
            }
        }
        private static void ExecuteDelete(ProgramArguments programArguments, Hashtable<User, UserInformation> dataBase, Hashtable<AgeCategory, LinkedList<string>> ageCategories)
        {
            if (programArguments.otherArgs.Length != 2)
            {
                throw new ArgumentException($"Command 'delete' should have 2 arguments. Got: {programArguments.otherArgs.Length}");
            }

            User user = new User(programArguments.otherArgs[0], programArguments.otherArgs[1]);

            UserInformation userInformation = dataBase.Find(user);
            if (userInformation != null)
            {
                int age = (int)((DateTime.Now - userInformation.birthDate).TotalDays / 365.25);
                dataBase.Remove(user);
                string ageCategory = ChooseAgeCategory(age);
                LinkedList<string> list = ageCategories.Find(new AgeCategory(ageCategory));
                list.Remove(user.username);
                WriteLine($"User {user.username} was succesfully deleted.");
            }
            else
            {
                WriteLine($"There is no such user: {user.username}.");
            }
        }
        private static void ExecuteLogin(ProgramArguments programArguments, Hashtable<User, UserInformation> dataBase)
        {
            if (programArguments.otherArgs.Length != 2)
            {
                throw new ArgumentException($"Command 'login' should have 2 arguments. Got: {programArguments.otherArgs.Length}.");
            }

            User user = new User(programArguments.otherArgs[0], programArguments.otherArgs[1]);
            UserInformation userInformation = dataBase.Find(user);
            if (userInformation != null)
            {
                WriteLine($"Info: {userInformation}");
            }
            else
            {
                WriteLine($"There is no such user: {user.username}.");
            }
        }
        private static void ExecuteIdentifyAgeCategories(ProgramArguments programArguments, Hashtable<AgeCategory, LinkedList<string>> ageCategories)
        {
            if (programArguments.otherArgs.Length != 0)
            {
                throw new ArgumentException($"Command 'identifyAgeCategories' should have 0 arguments. Got: {programArguments.otherArgs.Length}");
            }
            LinkedList<string> youngs = ageCategories.Find(new AgeCategory("young"));
            LinkedList<string> adults = ageCategories.Find(new AgeCategory("adult"));
            LinkedList<string> middles = ageCategories.Find(new AgeCategory("middle"));
            LinkedList<string> seniors = ageCategories.Find(new AgeCategory("senior"));

            int sumOfUsers = youngs.Count + adults.Count + middles.Count + seniors.Count;

            WriteLine($"All users registered: {sumOfUsers}");
            WriteLine($"Youngs (18-25): {Math.Round(youngs.Count * 100 / (double)sumOfUsers, 2)}%");
            WriteLine($"Adults (26-35): {Math.Round(adults.Count * 100 / (double)sumOfUsers, 2)}%");
            WriteLine($"Middles (36-50): {Math.Round(middles.Count * 100 / (double)sumOfUsers, 2)}%");
            WriteLine($"Seniors (50+): {Math.Round(seniors.Count * 100 / (double)sumOfUsers, 2)}%");
        }
        struct ProgramArguments
        {
            public string command;
            public string[] otherArgs;
        }
        private static ProgramArguments ParseInput(string input)
        {
            string[] args = input.Split(" ");

            ValidateCommandLength(args);
            string command = args[0];
            ValidateCommandName(command);

            string[] otherArgs = new string[args.Length - 1];
            for (int i = 0; i < otherArgs.Length; i++)
            {
                otherArgs[i] = args[i + 1];
            }
            return new ProgramArguments
            {
                command = command,
                otherArgs = otherArgs
            };

        }
        private static void ValidateCommandLength(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException($"Number of arguments should be more than 0. Got: {args.Length}");
            }
        }
        private static void ValidateCommandName(string command)
        {
            string[] commands = new string[] { "control", "register", "delete", "login", "printDataBase", "identifyAgeCategories" };
            for (int i = 0; i < commands.Length; i++)
            {
                if (command == commands[i])
                {
                    return;
                }
            }
            throw new ArgumentException($"There is no such command: {command}");
        }
        private static string ChooseAgeCategory(int age)
        {
            if (age >= 18 && age <= 25)
            {
                return "young";
            }
            else if (age <= 35)
            {
                return "adult";
            }
            else if (age <= 50)
            {
                return "middle";
            }
            else
            {
                return "senior";
            }
        }
    }
}

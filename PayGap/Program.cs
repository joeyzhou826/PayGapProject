using System;
using System.IO;
using PayGap;

namespace PayGap
{
    class MainClass
    {

        public static Job[] jobs;

        public static void Main(string[] args)
        {

            Job[] jobList = getJobList("/Users/Esoul/Projects/PayGap/PayGap/salaries.csv");
            //Header();
            Message();

            string input = Console.ReadLine();

            while (!input.ToUpper().Equals("Q"))
            {
                if (input.ToUpper().Equals("D"))
                {
                    Header();
                    foreach (var job in jobList)
                    {
                        Console.WriteLine(job.ToString());
                    }

                    Console.WriteLine();
                    Console.WriteLine("Do you want to save the result?(Y/N)");
                    if (Console.ReadLine().ToUpper().Equals("Y"))
                    {
                        SaveToFile(GetAllJobs(jobList));
                    }
                    Console.WriteLine();
                }
                else if (input.ToUpper().Equals("J"))
                {
                    Console.Write("Job (contains/is): ");
                    string jobInput = Console.ReadLine();

                    Header();
                    string result = SearchByJob(jobInput, jobList);
                    Console.WriteLine(result);

                    Console.WriteLine();
                    Console.WriteLine("Do you want to save the result?(Y/N)");
                    if (Console.ReadLine().ToUpper().Equals("Y"))
                    {
                        SaveToFile(result);
                    }
                    Console.WriteLine();
                }
                else if (input.ToUpper().Equals("S"))
                {
                    try
                    {
                        Console.WriteLine("Salary Range between: ");
                        Console.Write("Minimum Salary: ");
                        int minSalary = Int32.Parse(Console.ReadLine());

                        Console.Write("Maximum Salary: ");
                        int maxSalary = Int32.Parse(Console.ReadLine());

                        Header();
                        string result = SearchBySalaryRange(minSalary, maxSalary, jobList);
                        Console.WriteLine(result);

                        Console.WriteLine();
                        Console.WriteLine("Do you want to save the result?(Y/N)");
                        if (Console.ReadLine().ToUpper().Equals("Y"))
                        {
                            SaveToFile(result);
                        }
                        Console.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //Console.WriteLine("Invalid input");
                    }
                }
                else if (input.ToUpper().Equals("P"))
                {
                    try
                    {
                        Console.WriteLine("Percentage between (negative means Female earns more): ");
                        Console.Write("Minimum percentage: ");
                        int minPercent = Int32.Parse(Console.ReadLine());

                        Console.Write("Maximum percentage: ");
                        int maxPercent = Int32.Parse(Console.ReadLine());

                        Header();
                        string result = SearchByPercentage(minPercent, maxPercent, jobList);
                        Console.WriteLine(result);

                        Console.WriteLine();
                        Console.WriteLine("Do you want to save the result?(Y/N)");
                        if (Console.ReadLine().ToUpper().Equals("Y"))
                        {
                            SaveToFile(result);
                        }
                        Console.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //Console.WriteLine("Invalid input");
                    }
                }
                else if (!input.ToUpper().Equals("D") &&
                         !input.ToUpper().Equals("J") &&
                         !input.ToUpper().Equals("S") &&
                         !input.ToUpper().Equals("P"))
                {
                    Console.WriteLine("Invalid Option!");
                    Console.WriteLine();
                }

                Message();
                input = Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine("Good Bye!");
            Console.WriteLine();
            Console.WriteLine();

            Environment.Exit(0);


        }

        public static Job[] getJobList(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                int count = 0;

                foreach (string line in lines)
                {
                    count++;
                }

                jobs = new Job[count];

                //Console.WriteLine(count);

                for (int i = 0; i < count; i++)
                {
                    string[] lineContent = lines[i].Split(',');
                    int femaleSalary = Int32.Parse(lineContent[1]);
                    int maleSalary = Int32.Parse(lineContent[2]);
                    Job entry = new Job(lineContent[0], femaleSalary, maleSalary);
                    jobs[i] = entry;
                }

                //Console.WriteLine(jobs[0]);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Unable to access input file: " + filename);
            }

            Console.WriteLine();
            Console.WriteLine();

            return jobs;

        }


        public static string GetAllJobs(Job[] jobs)
        {
            string totalJobs = "";
            for (int i = 0; i < jobs.Length; i++)
            {
                totalJobs += jobs[i].ToString() + "\n";
            }
            return totalJobs;
        }

        public static string SearchByJob(string job, Job[] jobs)
        {
            string jobSearch = "";
            for (int i = 0; i < jobs.Length; i++)
            {
                if (jobs[i].GetJob().ToLower().Contains(job.ToLower())
                    || jobs[i].GetJob().ToLower().Equals(job))
                {
                    jobSearch += jobs[i].ToString() + "\n";
                }
            }
            return jobSearch;
        }

        public static string SearchBySalaryRange(int min, int max, Job[] jobs)
        {
            string jobRange = "";
            try
            {
                if (min < 0 || max < 0 || max < min)
                {
                    throw new Exception();
                }
                for (int i = 0; i < jobs.Length; i++)
                {
                    if (jobs[i].GetFemaleSalary() >= min &&
                        jobs[i].GetMaleSalary() <= max)
                    {
                        jobRange += jobs[i].ToString() + "\n";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Invalid min/max value");
            }
            return jobRange;
        }

        public static string SearchByPercentage(int percentageMin, int percentageMax, Job[] jobs)
        {
            string jobPercent = "";
            try
            {
                if (percentageMin > percentageMax)
                {
                    throw new Exception("Invalid Percentage!");
                }
                for (int i = 0; i < jobs.Length; i++)
                {
                    if (jobs[i].GetPercentage() >= percentageMin && jobs[i].GetPercentage() <= percentageMax)
                    {
                        jobPercent += jobs[i].ToString() + "\n";
                    }
                }
            }
            catch (Exception n)
            {
                throw new Exception("Invalid percentage value");
            }
            return jobPercent;
        }

        public static void Message()
        {
            Console.WriteLine("Gender Pay Gap Information - Please enter an option below.");
            Console.WriteLine("D - Display all jobs");
            Console.WriteLine("J - Search by job");
            Console.WriteLine("S - Search by salary range");
            Console.WriteLine("P - Search by percentage");
            Console.WriteLine("Q - Quit the program");

            Console.WriteLine();
            Console.WriteLine("Option: ");
        }

        public static void Header()
        {
            Console.WriteLine();
            Console.Write("{0,-67} {1,8} {2,8} {3,14:P1}\n", "- Job Description -", "Female", "Male", "Percentage");
            Console.Write("{0,-67} {1,8} {2,8} {3,14:P1}\n", "-------------------", "Salary", "Salary", "Difference");
            for (int i = 0; i < 100; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }

        public static void SaveToFile(string input)
        {
        reenter:
            Console.Write("Where do you want to save the file : ");
            string destination = Console.ReadLine();
            try
            {
                File.WriteAllText(destination, input);
            }
            catch
            {
                Console.WriteLine("Invalid Path!");
                goto reenter;
            }
        }

    }
}

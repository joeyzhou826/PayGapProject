using System;
namespace PayGap
{
    public class Job
    {

        private string job;

        private int femaleSalary;

        private int maleSalary;
        /**
         * The constructor of jobs
         */
        public Job(string j, int female, int male)
        {
            if (j == null)
            {
                throw new Exception("Invalid input!");

            }

            if (female <= 0 || male <= 0)
            {
                throw new Exception("Invalid salary input!");
            }
            this.job = j;
            this.femaleSalary = female;
            this.maleSalary = male;
        }

        public string GetJob()
        {
            return job;
        }

        public int GetFemaleSalary()
        {
            return femaleSalary;
        }

        public int GetMaleSalary()
        {
            return maleSalary;
        }

        public double GetPercentage()
        {
            double a = Convert.ToDouble(maleSalary);
            double b = Convert.ToDouble(femaleSalary);
            double percentage = (a - b) / a;

            return percentage;
        }

        public override string ToString()
        {

            //Console.WriteLine("{0,-50} {1,8} {2,8}\n", "- Job Description -", "Female", "Male");

            string result = string.Format("{0,-67} {1,8} {2,8} {3,14:P1}", job, femaleSalary, maleSalary, GetPercentage());

            return result;
        }
    }
}

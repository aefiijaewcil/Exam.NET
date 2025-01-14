using System.Runtime.Serialization.Json;

namespace ConsoleAppEmployee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> empList = new List<Employee>();
            Console.WriteLine("Enter the number of employees:");
            int numberOfEmployees = int.Parse(Console.ReadLine());
            while (numberOfEmployees > 0) 
            {
                Console.WriteLine($"Enter details for employee {numberOfEmployees}:");
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Salary: ");
                decimal salary = decimal.Parse(Console.ReadLine());
                empList.Add(new Employee { Id = id, Name = name, Salary = salary });
                numberOfEmployees--;
            }
            WriteFileJson(empList);
            ReadFileJson();
        }
        public static void WriteFileJson(List<Employee> empList)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Employee>));
            using (Stream s = new FileStream("C:\\Users\\karti\\Desktop\\.NET\\Day10\\employees.json", FileMode.Create))
            {
                js.WriteObject(s, empList);
            }
        }

        public static void ReadFileJson()
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Employee>));
            using (Stream s = new FileStream("C:\\Users\\karti\\Desktop\\.NET\\Day10\\employees.json", FileMode.Open))
            {
                List<Employee> empList = (List<Employee>)js.ReadObject(s);
                foreach (Employee emp in empList)
                {
                    Console.WriteLine(emp.ToString());
                }
            }
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Salary { get; set; }

       public string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Salary: " + Salary;
        }
    }
}

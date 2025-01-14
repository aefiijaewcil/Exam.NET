using System.Data;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySql.Data.MySqlClient;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> empList = GetAllEmployee();
            return View(empList);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            Employee emp = GetEmployee(id);
            return View(emp);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                string ExceptionMessages = "";
                string ErrorMessages = "";

                foreach (ModelStateEntry value in ModelState.Values)
                {
                    //value.ValidationState //t/f

                    foreach (ModelError item in value.Errors)
                    {
                        if (item.Exception != null)
                        {
                            ExceptionMessages += item.Exception.Message;
                            ModelState.AddModelError("", item.Exception.Message);
                        }

                        if (item.ErrorMessage != null)
                            ErrorMessages += item.ErrorMessage;
                    }
                }

                return View();

            }
            try
            {
                Employee emp = new Employee();
                emp.Id = Convert.ToInt32(collection["Id"]);
                emp.Name = collection["Name"];
                emp.Salary = Convert.ToDecimal(collection["Salary"]);
                AddEmployee(emp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            Employee emp = GetEmployee(id);
            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                string ExceptionMessages = "";
                string ErrorMessages = "";

                foreach (ModelStateEntry value in ModelState.Values)
                {
                    //value.ValidationState //t/f

                    foreach (ModelError item in value.Errors)
                    {
                        if (item.Exception != null)
                        {
                            ExceptionMessages += item.Exception.Message;
                            ModelState.AddModelError("", item.Exception.Message);
                        }

                        if (item.ErrorMessage != null)
                            ErrorMessages += item.ErrorMessage;
                    }
                }

                return View();

            }
            try
            {
                Employee emp = GetEmployee(id);
                emp.Name = collection["Name"];
                emp.Salary = Convert.ToDecimal(collection["Salary"]);
                UpdateEmployee(emp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            Employee emp = GetEmployee(id);
            return View(emp);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public List<Employee> GetAllEmployee()
        {
            List<Employee> empList = new List<Employee>();    
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;userid=root;password=cdac;database=ActsJan25;";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employee";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Name = reader["Name"].ToString();
                    emp.Salary = Convert.ToDecimal(reader["Salary"]);
                    empList.Add(emp);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return empList;
        }
        public Employee GetEmployee(int id)
        {
            Employee emp = null;
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;userid=root;password=cdac;database=ActsJan25;";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employee where Id = " + id;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    emp = new Employee();
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Name = reader["Name"].ToString();
                    emp.Salary = Convert.ToDecimal(reader["Salary"]);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return emp;
        }
        public void AddEmployee(Employee emp)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;userid=root;password=cdac;database=ActsJan25;";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employee values(" + emp.Id + ", '" + emp.Name + "', " + emp.Salary + ")";
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateEmployee(Employee emp)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;userid=root;password=cdac;database=ActsJan25;";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employee set Name = '" + emp.Name + "', Salary = " + emp.Salary + " where Id = " + emp.Id;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void DeleteEmployee(int id)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "server=localhost;userid=root;password=cdac;database=ActsJan25;";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Employee where Id = " + id;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

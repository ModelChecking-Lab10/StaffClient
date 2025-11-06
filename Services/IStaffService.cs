using StaffClient.Data;

namespace StaffClient.Services;

public interface IStaffService{
    Task<IEnumerable<Employee>> GetEmployees();
    Task<Employee> GetEmployee(int id);   
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee> UpdateEmployee(int id, Employee employee);
    Task<Employee> DeleteEmployee(int id);
}
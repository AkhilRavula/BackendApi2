namespace BackendApi2.CustomExceptions;
public class EmployeeNotFoundException : Exception
{
      public EmployeeNotFoundException(string message) : base(message)
      {
        
      }
}

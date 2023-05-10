using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class AdminAccountCto : ICto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmployeeNumber { get; set; }
    }
}

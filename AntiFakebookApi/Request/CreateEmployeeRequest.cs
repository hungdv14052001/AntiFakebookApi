namespace AntiFakebookApi.Request
{
    public class CreateEmployeeRequest
    {
        public string FullName { get; set; }
        public string IDCard { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
    }
}

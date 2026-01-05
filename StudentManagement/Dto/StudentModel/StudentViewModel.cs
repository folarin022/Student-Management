namespace StudentManagement.Dto.StudentModel
{
    public class StudentViewModel
    {
        public Guid Id { set; get; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Course { get; set; }
    }
}

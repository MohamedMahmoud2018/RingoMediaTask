namespace RingoMediaTask.Models.ViewModels
{
    public class DepartmentHierarchyViewModel
    {
        public Department Department { get; set; }
        public ICollection<Department> ParentDepartments { get; set; }

       
    }
}

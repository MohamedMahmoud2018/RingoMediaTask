using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RingoMediaTask.Models
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "Development",
                    ParentDepartmentId = 2,
                    DepartmentLogo = ""

                },
                 new Department
                 {
                     DepartmentId = 2,
                     DepartmentName = "Management",
                     DepartmentLogo = ""

                 }
                ) ;
        }
    }
}

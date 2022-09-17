using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_MM.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO Courses (Title)
VALUES
('C#'),
('Java'),
('Entity Framework'),
('Hibernate'),
('Project Management 101');
");

            migrationBuilder.Sql(@"
INSERT INTO Students (Name, Age)
VALUES
('Peter',37),
('Paul',21),
('Jack',41),
('Annie',23),
('Suzie',48);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

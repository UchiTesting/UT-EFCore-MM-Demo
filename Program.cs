// See https://aka.ms/new-console-template for more information
using EF_MM.Demos;

EfDemos efDemos = new EfDemos();

/// CRUD operations on MMR (Many-to-Many Relationship)
/// C: AddDotNetCoursesToAnnie()
/// R: DisplayAnniesCourses()
/// U: UpdatePmCourseTo201() in particular
/// D: AddHibernateThenRemoveIt() The 1st part adds Hibernate course and commit to DB. The 2nd part reverts and commit.
/// 
/// Feel free to modify and play around with this code.

efDemos.DisplayAnniesCourses();

// Execute those methods
// - sequentially (i.e. one after the other in order)
// - and exclusively (i.e. one at a time)
#region Actual Demos
efDemos.AddDotNetCoursesToAnnie();
//efDemos.UpdatePmCourseTo201();
//efDemos.AddHibernateThenRemoveIt();
//efDemos.CreateDevOpsCourseThenAddToAnnieThenDeleteDevOpsCourse();
#endregion
efDemos.DisplayAnniesCourses();

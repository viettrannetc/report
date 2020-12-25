using Microsoft.EntityFrameworkCore;
using System;

namespace VT.Database
{
	//public class PMContext : DbContext
	//{
	//	public PMContext()
	//	{

	//	}

	//	public DbSet<Jira> Blogs { get; set; }

	//	protected override void OnModelCreating(ModelBuilder modelBuilder)
	//	{
	//		//modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

	//		//modelBuilder.Entity<Course>()
	//		//	.HasMany(c => c.Instructors).WithMany(i => i.Courses)
	//		//	.Map(t => t.MapLeftKey("CourseID")
	//		//		.MapRightKey("InstructorID")
	//		//		.ToTable("CourseInstructor"));

	//		//modelBuilder.Entity<Department>().MapToStoredProcedures();
	//	}
	//}

	public class Jira
	{
		public Guid Id { get; set; }
		public string Data { get; set; }
		public string Project { get; set; }
		public DateTime Created { get; set; }
	}

	public class WorkingTime
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Project { get; set; }
		public DateTime Created { get; set; }
	}
}

namespace ASM_ManageTrainingProgramSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineeCoursesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.TraineeInfoes", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeCourses", "UserId", "dbo.TraineeInfoes");
            DropForeignKey("dbo.TraineeCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TraineeCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineeCourses", new[] { "UserId" });
            DropTable("dbo.TraineeCourses");
        }
    }
}

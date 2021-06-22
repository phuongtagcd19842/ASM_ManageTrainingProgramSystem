namespace ASM_ManageTrainingProgramSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TraineeInfo_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeInfoes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TraineeName = c.String(),
                        Age = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Education = c.String(),
                        ProgrammingLanguage = c.String(),
                        TOEICScore = c.Single(nullable: false),
                        ExperienceDetail = c.String(),
                        Department = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeInfoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeInfoes", new[] { "UserId" });
            DropTable("dbo.TraineeInfoes");
        }
    }
}

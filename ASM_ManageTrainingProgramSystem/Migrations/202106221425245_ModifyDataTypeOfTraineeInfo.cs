namespace ASM_ManageTrainingProgramSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDataTypeOfTraineeInfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TraineeInfoes", "DateOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TraineeInfoes", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}

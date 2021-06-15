namespace ASM_ManageTrainingProgramSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDataTypeOfTrainerInfoModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrainerInfoes", "ExternalOrInternal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainerInfoes", "ExternalOrInternal", c => c.Int(nullable: false));
        }
    }
}

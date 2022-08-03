namespace BugTrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yfyf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "EntryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "ResolveDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "Asignee", c => c.String());
            AddColumn("dbo.Issues", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "AsigneeDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserRole_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "UserRole_RoleId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" }, "dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
        }
        
        public override void Down()
        {
         
            DropForeignKey("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" }, "dbo.AspNetUserRoles");
            DropIndex("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" });
            DropColumn("dbo.AspNetUsers", "UserRole_RoleId");
            DropColumn("dbo.AspNetUsers", "UserRole_UserId");
            DropColumn("dbo.Issues", "AsigneeDescription");
            DropColumn("dbo.Issues", "Status");
            DropColumn("dbo.Issues", "Asignee");
            DropColumn("dbo.Issues", "ResolveDate");
            DropColumn("dbo.Issues", "EntryDate");
        }
    }
}

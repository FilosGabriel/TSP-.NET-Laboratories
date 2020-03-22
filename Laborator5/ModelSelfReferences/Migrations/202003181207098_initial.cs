namespace ModelSelfReferences.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BazaDeDate.Business",
                c => new
                    {
                        BusinessId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LicenseNumber = c.String(),
                    })
                .PrimaryKey(t => t.BusinessId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Salary = c.Decimal(precision: 18, scale: 2),
                        Wage = c.Decimal(precision: 18, scale: 2),
                        EmployeeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "BazaDeDate.Photograph",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        HighResolutionBits = c.Binary(),
                        Title = c.String(),
                        ThumbnailBits = c.Binary(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "BazaDeDate.Product",
                c => new
                    {
                        SKU = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SKU);
            
            CreateTable(
                "dbo.SelfReferences",
                c => new
                    {
                        SelfReferenceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentSelfReferenceId = c.Int(),
                    })
                .PrimaryKey(t => t.SelfReferenceId)
                .ForeignKey("dbo.SelfReferences", t => t.ParentSelfReferenceId)
                .Index(t => t.ParentSelfReferenceId);
            
            CreateTable(
                "BazaDeDate.ProductWebInfo",
                c => new
                    {
                        SKU = c.Int(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.SKU)
                .ForeignKey("BazaDeDate.Product", t => t.SKU)
                .Index(t => t.SKU);
            
            CreateTable(
                "BazaDeDate.eCommerce",
                c => new
                    {
                        BusinessId = c.Int(nullable: false),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.BusinessId)
                .ForeignKey("BazaDeDate.Business", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "BazaDeDate.Retail",
                c => new
                    {
                        BusinessId = c.Int(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZIPCode = c.String(),
                    })
                .PrimaryKey(t => t.BusinessId)
                .ForeignKey("BazaDeDate.Business", t => t.BusinessId)
                .Index(t => t.BusinessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("BazaDeDate.Retail", "BusinessId", "BazaDeDate.Business");
            DropForeignKey("BazaDeDate.eCommerce", "BusinessId", "BazaDeDate.Business");
            DropForeignKey("BazaDeDate.ProductWebInfo", "SKU", "BazaDeDate.Product");
            DropForeignKey("dbo.SelfReferences", "ParentSelfReferenceId", "dbo.SelfReferences");
            DropIndex("BazaDeDate.Retail", new[] { "BusinessId" });
            DropIndex("BazaDeDate.eCommerce", new[] { "BusinessId" });
            DropIndex("BazaDeDate.ProductWebInfo", new[] { "SKU" });
            DropIndex("dbo.SelfReferences", new[] { "ParentSelfReferenceId" });
            DropTable("BazaDeDate.Retail");
            DropTable("BazaDeDate.eCommerce");
            DropTable("BazaDeDate.ProductWebInfo");
            DropTable("dbo.SelfReferences");
            DropTable("BazaDeDate.Product");
            DropTable("BazaDeDate.Photograph");
            DropTable("dbo.Employees");
            DropTable("BazaDeDate.Business");
        }
    }
}

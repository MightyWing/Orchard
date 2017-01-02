using Mighty.Shop.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Fields;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Users.Models;
using System;
//using Orchard.Core;
namespace Mighty.Shop
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {

            SchemaBuilder.CreateTable("ProductPartRecord", table => table
                // The following method will create an "Id" column for us and set it is the primary key for the table
                .ContentPartRecord()
                // Create a column named "UnitPrice" of type "decimal"
                .Column<decimal>("UnitPrice")
                // Create the "Sku" column and specify a maximum length of 50 characters
                .Column<string>("Sku", column => column.WithLength(50))
                );

            // Return the version that this feature will be after this method completes
            return 1;
        }
        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("ProductPart",part => part.Attachable());
            return 2;
        }
        public int UpdateFrom2()
        {
            // Define a new content type called "ShoppingCartWidget"
            ContentDefinitionManager.AlterTypeDefinition("ShoppingCartWidget", type => type
                // Attach the "ShoppingCartWidgetPart"
                .WithPart("ShoppingCartWidgetPart")
                // In order to turn this content type into a widget, it needs the WidgetPart
                .WithPart("WidgetPart")
                // It also needs a setting called "Stereotype" to be set to "Widget"
                .WithSetting("Stereotype", "Widget")
                );

            return 3;
        }
        public int UpdateFrom3()
        {
            // Update the ShoppingCartWidget so that it has a CommonPart attached, which is required for widgets (it's generally a good idea to have this part attached)
            ContentDefinitionManager.AlterTypeDefinition("ShoppingCartWidget", type => type
                .WithPart("CommonPart")
            );

            return 4;
        }
        public int UpdateFrom4()
        {
            SchemaBuilder.CreateTable("CustomerPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("FirstName", c => c.WithLength(50))
                .Column<string>("LastName", c => c.WithLength(50))
                .Column<string>("Title", c => c.WithLength(10))
                .Column<DateTime>("CreatedUtc")
                );

            SchemaBuilder.CreateTable("AddressPartRecord", table => table
                .ContentPartRecord()
                .Column<int>("CustomerId")
                .Column<string>("Type", c => c.WithLength(50))
                );

            ContentDefinitionManager.AlterPartDefinition("CustomerPart", part => part
                .Attachable(false)
                );

            ContentDefinitionManager.AlterTypeDefinition("Customer", type => type
                .WithPart("CustomerPart")
                .WithPart("UserPart")
                );

            ContentDefinitionManager.AlterPartDefinition("AddressPart", part => part
                .Attachable(false)
                .WithField("Name", f => f.OfType("TextField"))
                .WithField("AddressLine1", f => f.OfType("TextField"))
                .WithField("AddressLine2", f => f.OfType("TextField"))
                .WithField("Zipcode", f => f.OfType("TextField"))
                .WithField("City", f => f.OfType("TextField"))
                .WithField("Country", f => f.OfType("TextField"))
                );

            ContentDefinitionManager.AlterTypeDefinition("Address", type => type
                .WithPart("CommonPart")
                .WithPart("AddressPart")
                );

            return 5;
        }
        public int UpdateFrom5()
        {
            ContentDefinitionManager.AlterPartDefinition(typeof(CustomerPart).Name, p => p
                .Attachable(false)
                .WithField("Phone", f => f.OfType(typeof(TextField).Name))
                );

            ContentDefinitionManager.AlterTypeDefinition("Customer", t => t
                .WithPart(typeof(CustomerPart).Name)
                .WithPart(typeof(UserPart).Name)
                );

            ContentDefinitionManager.AlterPartDefinition(typeof(AddressPart).Name, p => p
                .Attachable(false)
                .WithField("Name", f => f.OfType(typeof(TextField).Name))
                .WithField("AddressLine1", f => f.OfType(typeof(TextField).Name))
                .WithField("AddressLine2", f => f.OfType(typeof(TextField).Name))
                .WithField("Zipcode", f => f.OfType(typeof(TextField).Name))
                .WithField("City", f => f.OfType(typeof(TextField).Name))
                .WithField("Country", f => f.OfType(typeof(TextField).Name))
                );

            ContentDefinitionManager.AlterTypeDefinition("Address", t => t
                .WithPart(typeof(AddressPart).Name)
                );

            return 6;
        }

        public int UpdateFrom6()
        {
            //FOREIGN KEY 约束"Order_Customer"冲突。表"dbo.Mighty_Shop_CustomerRecord", column 'Id'。
            SchemaBuilder.CreateTable("OrderRecord", t => t
                .Column<int>("Id", c => c.PrimaryKey().Identity())
                .Column<int>("CustomerId", c => c.NotNull())
                .Column<DateTime>("CreatedAt", c => c.NotNull())
                .Column<decimal>("SubTotal", c => c.NotNull())
                .Column<decimal>("Vat", c => c.NotNull())
                .Column<string>("Status", c => c.WithLength(50).NotNull())
                .Column<string>("PaymentServiceProviderResponse", c => c.WithLength(null))
                .Column<string>("PaymentReference", c => c.WithLength(50))
                .Column<DateTime>("PaidAt", c => c.Nullable())
                .Column<DateTime>("CompletedAt", c => c.Nullable())
                .Column<DateTime>("CancelledAt", c => c.Nullable())
                );

            SchemaBuilder.CreateTable("OrderDetailRecord", t => t
                .Column<int>("Id", c => c.PrimaryKey().Identity())
                .Column<int>("OrderRecord_Id", c => c.NotNull())
                .Column<int>("ProductId", c => c.NotNull())
                .Column<int>("Quantity", c => c.NotNull())
                .Column<decimal>("UnitPrice", c => c.NotNull())
                .Column<decimal>("VatRate", c => c.NotNull())
                );

            SchemaBuilder.CreateForeignKey("Order_Customer", "OrderRecord", new[] { "CustomerId" }, "CustomerPartRecord", new[] { "Id" });
            SchemaBuilder.CreateForeignKey("OrderDetail_Order", "OrderDetailRecord", new[] { "OrderRecord_Id" }, "OrderRecord", new[] { "Id" });
            SchemaBuilder.CreateForeignKey("OrderDetail_Product", "OrderDetailRecord", new[] { "ProductId" }, "ProductPartRecord", new[] { "Id" });

            return 7;
        }

    }
}

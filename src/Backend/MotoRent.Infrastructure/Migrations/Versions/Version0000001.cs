using FluentMigrator;

namespace MotoRent.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.CREATE_MOTORCYCLE_TABLE, "Create motorcycle table.")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("motorcycle")
            .WithColumn("license_plate").AsString().NotNullable()
            .WithColumn("vin").AsString().NotNullable()
            .WithColumn("model").AsString().NotNullable()
            .WithColumn("brand").AsString().NotNullable()
            .WithColumn("year").AsInt32().NotNullable();

        Create.Index("idx_motorcycle_license_plate")
            .OnTable("motorcycle")
            .OnColumn("license_plate").Ascending()
            .WithOptions().Unique();

        Create.Index("idx_motorcycle_vin")
            .OnTable("motorcycle")
            .OnColumn("vin").Ascending()
            .WithOptions().Unique();
    }
}

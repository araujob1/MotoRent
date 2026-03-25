using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace MotoRent.Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        return Create.Table(table)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("is_active").AsBoolean().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable();
    }
}

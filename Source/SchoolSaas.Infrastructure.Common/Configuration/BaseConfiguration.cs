using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;
using System.Reflection;

namespace SchoolSaas.Infrastructure.Common.Configuration
{
    public static class BaseEntityConfiguration
    {
        private static void ConfigureBaseEntity<TEntity, T>(ModelBuilder modelBuilder)
            where TEntity : BaseEntity<T>
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Id).ValueGeneratedOnAdd();
                builder.Property(a => a.RowVersion).IsRowVersion();

                builder.HasIndex(e => e.Created);
                builder.HasIndex(e => e.CreatedBy);
                builder.HasIndex(e => e.LastModifiedBy);

                //builder.ToTable(NamePluralizer.Pluralize(typeof(TEntity).Name));
            });
        }


        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder)
        {
            var method = typeof(BaseEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureBaseEntity));
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (entityType.ClrType.IsBaseEntity(out var T))
                    method.MakeGenericMethod(entityType.ClrType, T).Invoke(null, new[] { modelBuilder });

            return modelBuilder;
        }

        private static bool IsBaseEntity(this Type type, out Type? T)
        {
            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {
                    T = baseType.GetGenericArguments()[0];
                    return true;
                }

            T = null;
            return false;
        }

    }
}
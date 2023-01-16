using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SO.Domain;
using SO.CustomerService.Domain.Customer;

namespace SO.CustomerService.Infrastructure.Data.EntityTypeConfigurations;

public class CustomerEntityTypeConfiguration : AuditedAggregateRootEntityTypeConfiguration<Customer, Guid>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customer");

        builder.Property(x => x.IdentityId).HasColumnName("identity_id");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.LastName).HasColumnName("last_name");
        builder.Property(x => x.Nationality).HasColumnName("nationality");
        builder.Property(x => x.BirthDate).HasColumnName("birthdate");
        builder.Property(x => x.PhoneNumber).HasColumnName("phone_number");

        builder.OwnsOne(x => x.Address, nav =>
        {
            nav.Property(b => b.Country).HasColumnName("address_country");
            nav.Property(b => b.City).HasColumnName("address_city");
            nav.Property(b => b.District).HasColumnName("address_district");
            nav.Property(b => b.Text).HasColumnName("address_text");
            nav.Property(b => b.ZipCode).HasColumnName("address_zip_code");
        });

        base.Configure(builder);
    }
}
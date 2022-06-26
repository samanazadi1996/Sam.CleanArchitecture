using Domain.ToDoListDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(b => b.Id); 
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.OwnsOne(x => x.Color).Property(x => x.Value).HasColumnName("Color");
            builder.OwnsOne(x => x.Title).Property(x => x.Value).HasColumnName("Title");
            builder.OwnsOne(x => x.Description).Property(x => x.Value).HasColumnName("Description");
            builder.OwnsOne(x => x.TimeTodo).Property(x => x.Value).HasColumnName("TimeTodo");

        }
    }
}
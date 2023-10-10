using BlogTemplate.Domain.Models;

namespace BlogTemplate.Infrastructure;

public interface IDatabaseInitialBuilder
{
    IDatabaseInitialBuilder AddAdminUser();
    IDatabaseInitialBuilder AddPages();
    IDatabaseInitialBuilder AddSettings();
    void Build();
}

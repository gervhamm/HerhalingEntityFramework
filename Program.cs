using HerhalingEntityFramework.Database;
using HerhalingEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context to the container.
builder.Services.AddDbContext<HerhalingEntityFrameworkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Create a scope to seed the database with some random data.
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HerhalingEntityFrameworkContext>();

    for (int i = 0; i < 100; i++)
    {
        int RandomDays = Faker.RandomNumber.Next(1, 365);

        var blog = new Blog
        {
            Title = Faker.Company.Name(),
            Author = Faker.Name.FullName(),
            CreatedAt = DateTime.Now - TimeSpan.FromDays(RandomDays),
            UpdatedAt = DateTime.Now - TimeSpan.FromDays(RandomDays - Faker.RandomNumber.Next(1, RandomDays)),
        };

        context.Blogs.Add(blog);

        for (int j = 0; j < Faker.RandomNumber.Next(1, 10); j++)
        {
            var comment = new Comment
            {
                Blog = blog,
                Content = Faker.Lorem.Sentence(50),
                CreatedAt = DateTime.Now - TimeSpan.FromDays(RandomDays - Faker.RandomNumber.Next(1, RandomDays)),
            };

            context.Comments.Add(comment);
        }
    }

    await context.SaveChangesAsync();
}*/
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//TODO: Maak een eenvoudige blog applicatie waar je blogs kan aanmaken, bewerken en verwijderen.
//TODO: Een blog kan ook meerdere comments hebben (maar een comment maar 1 blog), ook deze moet je kunnen aanmaken, bewerken en verwijderen.

//TODO: Je hoeft geen authenticatie te voorzien, dus iedereen mag alle posts aanmaken, bewerken en verwijderen.
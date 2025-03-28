using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LaLigaDbContext>(options =>
    options.UseSqlite("Data Source=LaLiga.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LaLigaDbContext>();
    db.Database.EnsureCreated();
}

app.Urls.Add("http://localhost:8080");

app.MapGet("/api/matches", async (LaLigaDbContext db) =>
    await db.Matches.ToListAsync());

app.MapPost("/api/matches", async (LaLigaDbContext db, HttpRequest request) =>
{
    try
    {
        var data = await request.ReadFromJsonAsync<MatchDto>();
        if (data == null)
            return Results.BadRequest("Invalid JSON");

        var match = new Match
        {
            LocalTeam = data.homeTeam,
            VisitTeam = data.awayTeam,
            Date = data.matchDate,
            Goals = 0,
            YellowCards = 0,
            RedCards = 0,
            ExtraTime = false
        };

        db.Matches.Add(match);
        await db.SaveChangesAsync();

        return Results.Created($"/api/matches/{match.Id}", match);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();

public class MatchDto
{
    public string homeTeam { get; set; }
    public string awayTeam { get; set; }
    public string matchDate { get; set; }
}



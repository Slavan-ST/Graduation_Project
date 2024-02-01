using Helper.Models;
using Server.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

var db = new ApplicationContext();

#region Конечные точки - фактически это весь API

app.MapGet("/student/{id}", (int id) =>
{
    var student = (db.Students.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (student == null)
    {
        return Results.Json(student);
    }
    else
    {
        return Results.Json(student);
    }
});
app.MapGet("/attendanceLog/{id}", (int id) =>
{
    var attendanceLog = (db.AttendanceLog.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (attendanceLog == null)
    {
        return Results.Json(attendanceLog);
    }
    else
    {
        return Results.Json(attendanceLog);
    }
});
app.MapGet("/room/{id}", (int id) =>
{
    var room = (db.Rooms.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (room == null)
    {
        return Results.Json(room);
    }
    else
    {
        return Results.Json(room);
    }
});
app.MapGet("/user/{login}:{password}", (string login, string password) =>
{
    var user = (db.Users.Where(x => x.Login == login && x.Password == password).ToList().FirstOrDefault());
    if (user == null)
    {
        return Results.Json(user);
    }
    else
    {
        return Results.Json(user);
    }

});

app.MapDelete("/student/{id}", (int id) =>
{
    return Results.Json("it's OK");
});


#endregion


app.Run();

using Helper.Models;
using Server.Data;
using Server.Services;
using System.Diagnostics;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

var db = new ApplicationContext();

#region Конечные точки - фактически это весь API

app.MapGet("/students/{id}", (int id, User user) =>
{
    if (!Authentication.IsAuthentication(user))
    {
        return Results.StatusCode(403); //403 нет прав
    }
    var student = (db.Students.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (student == null)
    {
        return Results.NotFound(); //404
    }
    return Results.Json(student);
});
app.MapPut("/students", (Student data, User user) =>
{
    if (data == null)
    {
        return Results.NoContent();
    }
    if (!Authentication.IsAuthentication(user))
    {
        return Results.StatusCode(403); //403 нет прав
    }

    if (!db.Students.Contains(data))
    {
        return Results.NotFound();
    }
    db.Students.Update(data);
    db.SaveChanges();
    return Results.Ok();
});
app.MapPost("/students", (Student student, User user) =>
{
    if (!Authentication.IsAuthentication(user))
    {
        return Results.StatusCode(403); //403 нет прав
    }
    if (student == null)
    {
        return Results.NoContent(); //204
    }
    if (db.Students.Contains(student))
    {
        return Results.StatusCode(409); //409 - конфликт
    }
    db.Students.Add(student);
    db.SaveChanges();
    return Results.Ok(); 
});
app.MapDelete("/students/{id}", (int id, User user) =>
{
    if (!Authentication.IsAuthentication(user))
    {
        return Results.StatusCode(403); //403 нет прав
    }
    var student = (db.Students.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (student == null)
    {
        return Results.NotFound();
    }
    db.Students.Remove(student);
    db.SaveChanges();
    return Results.Ok();
});


app.MapGet("/attendanceLog/{id}", (int id) =>
{
    var attendanceLog = (db.AttendanceLog.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (attendanceLog == null)
    {
        return Results.NotFound();
    }
    return Results.Json(attendanceLog);
});
app.MapGet("/room/{id}", (int id) =>
{
    var room = (db.Rooms.Where(x => x.Id == id).ToList().FirstOrDefault());
    if (room == null)
    {
        return Results.NotFound();
    }
    return Results.Json(room);
});
app.MapGet("/user/{login}:{password}", (string login, string password) =>
{
    var user = (db.Users.Where(x => x.Login == login && x.Password == password).ToList().FirstOrDefault());
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Json(user);

});


#endregion


app.Run();

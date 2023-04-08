using ProjectVerome.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

// токен из личного кабинета
const string apiKey = "sk-95Ez2Z1rtgegrRTFFDSTVTdsdfsgdv3422kjhLghnh53QiT8F";
// адрес api для взаимодействия с чат-ботом
string endpoint = "https://api.openai.com/v1/chat/completions";
// набор соообщений диалога с чат-ботом
List<Message> messages = new List<Message>();
// HttpClient для отправки сообщений
var httpClient = new HttpClient();
// устанавливаем отправляемый в запросе токен
httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

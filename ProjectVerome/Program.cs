using ProjectVerome.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

// ����� �� ������� ��������
const string apiKey = "sk-95Ez2Z1rtgegrRTFFDSTVTdsdfsgdv3422kjhLghnh53QiT8F";
// ����� api ��� �������������� � ���-�����
string endpoint = "https://api.openai.com/v1/chat/completions";
// ����� ���������� ������� � ���-�����
List<Message> messages = new List<Message>();
// HttpClient ��� �������� ���������
var httpClient = new HttpClient();
// ������������� ������������ � ������� �����
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

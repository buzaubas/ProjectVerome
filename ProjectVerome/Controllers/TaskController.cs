using Microsoft.AspNetCore.Mvc;
using ProjectVerome.Models;

namespace ProjectVerome.Controllers
{
    public class TaskController : Controller
    {
        static List<Message> messages = new List<Message>();

        [HttpGet]
        public Task<IActionResult> Chat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Chat(string question)
        {
            const string apiKey = "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";
            // адрес api для взаимодействия с чат-ботом
            string endpoint = "https://api.openai.com/v1/chat/completions";
            // HttpClient для отправки сообщений
            var httpClient = new HttpClient();
            // устанавливаем отправляемый в запросе токен
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            while (true)
            {
                // ввод сообщения пользователя
                Console.Write("User: ");
                var content = question;

                // если введенное сообщение имеет длину меньше 1 символа
                // то выходим из цикла и завершаем программу
                if (string.IsNullOrWhiteSpace(content))
                    break;
                // формируем отправляемое сообщение
                var message = new Message() { Role = "user", Content = content };
                // добавляем сообщение в список сообщений
                messages.Add(message);

                // формируем отправляемые данные
                var requestData = new Request()
                {
                    ModelId = "gpt-3.5-turbo",
                    Messages = messages
                };
                // отправляем запрос
                using var response = await httpClient.PostAsJsonAsync(endpoint, requestData);

                // если произошла ошибка, выводим сообщение об ошибке на консоль
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
                    break;
                }
                // получаем данные ответа
                ResponseData? responseData = await response.Content.ReadFromJsonAsync<ResponseData>();

                var choices = responseData?.Choices ?? new List<Choice>();
                if (choices.Count == 0)
                {
                    Console.WriteLine("No choices were returned by the API");
                    continue;
                }
                var choice = choices[0];
                var responseMessage = choice.Message;
                // добавляем полученное сообщение в список сообщений
                messages.Add(responseMessage);

                return View(messages);
            }

            return View(messages);
        }
    }
}

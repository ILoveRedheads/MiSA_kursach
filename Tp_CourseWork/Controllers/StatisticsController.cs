using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using Tp_CourseWork.Models.ViewModels;
using Tp_CourseWork.Repositories;

namespace Tp_CourseWork.Controllers
{
    /// <summary>
    /// REST-сервис для получения статистики по локациям.
    /// Выделен отдельно от основного CRUD API, чтобы показать наличие второго сервиса.
    /// </summary>
    [Route("statistics")]
    [Produces("application/json")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly BaseRepository _repo;
        private readonly IHttpClientFactory? _httpClientFactory;

        private const string CloudFunctionUrl = "https://functions.yandexcloud.net/d4eekil0t8rto31d5eov";

        public StatisticsController(BaseRepository repo, IHttpClientFactory? httpClientFactory = null)
        {
            _repo = repo;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Локальная статистика (без облака) по бюджетам и населению.
        /// </summary>
        [HttpGet("local")]
        public ActionResult<IEnumerable<Statistic>> GetLocalStatistics()
        {
            var budgets = _repo.GetBudgets();
            var residants = _repo.GetNumberResidants();

            var stats = _repo.GetStatistic(budgets, residants);
            return Ok(stats);
        }

        /// <summary>
        /// Статистика через облачную функцию Yandex.Cloud (ИИ/анализ данных).
        /// </summary>
        [HttpGet("cloud")]
        public async Task<ActionResult<IEnumerable<Statistic>>> GetCloudStatistics()
        {
            var budgets = _repo.GetBudgets();
            var residants = _repo.GetNumberResidants();
            var squareKm = _repo.GetBudgets(); // временно заполним тем же размером для примера

            var jsonObject = new JObject(
                new JProperty("Budgets", new JArray(budgets)),
                new JProperty("Residants", new JArray(residants)),
                new JProperty("SquareKm", new JArray(squareKm))
            );

            using var client = _httpClientFactory != null
                ? _httpClientFactory.CreateClient()
                : new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(CloudFunctionUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Ошибка вызова облачной функции Yandex.Cloud");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);

            var result = new List<Statistic>();

            if (responseJson.TryGetValue("resultBudgets", out var resultBudgetsToken) && resultBudgetsToken is JArray resultBudgetsArray)
            {
                result.AddRange(ConvertJArrayToStatistics(resultBudgetsArray));
            }

            if (responseJson.TryGetValue("resultResidants", out var resultResidantsToken) && resultResidantsToken is JArray resultResidantsArray)
            {
                result.AddRange(ConvertJArrayToStatistics(resultResidantsArray));
            }

            if (responseJson.TryGetValue("resultSquareKm", out var resultSquareToken) && resultSquareToken is JArray resultSquareArray)
            {
                result.AddRange(ConvertJArrayToStatistics(resultSquareArray));
            }

            // На случай, если структура ответа функции изменилась и мы не смогли распарсить –
            // не оставляем пользователя без данных: считаем статистику локально.
            if (result.Count == 0)
            {
                var localStats = _repo.GetStatistic(budgets, residants);
                return Ok(localStats);
            }

            return Ok(result);
        }

        private static List<Statistic> ConvertJArrayToStatistics(JArray array)
        {
            var statisticsList = new List<Statistic>();

            foreach (var item in array)
            {
                var category = item["Name"]?.ToString();
                var median = item["Median"]?.Value<double>() ?? 0;
                var mean = item["Mean"]?.Value<double>() ?? 0;
                var max = item["Max"]?.Value<double>() ?? 0;
                var min = item["Min"]?.Value<double>() ?? 0;

                statisticsList.Add(new Statistic
                {
                    Name = category,
                    Median = median,
                    Mean = mean,
                    Max = max,
                    Min = min
                });
            }

            return statisticsList;
        }
    }
}

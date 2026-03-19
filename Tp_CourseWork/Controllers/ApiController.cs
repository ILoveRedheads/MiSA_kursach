using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Linq;
using Tp_CourseWork.Repositories;

using MathNet.Numerics.Statistics;
using static Azure.Core.HttpHeader;
using Tp_CourseWork.Models;
using Microsoft.Extensions.Logging;

namespace Tp_CourseWork.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class ApiController: ControllerBase
    {
        private BaseRepository _repo;
        private readonly ILogger<ApiController> _logger;
        private readonly IConfiguration _config;

        public ApiController(BaseRepository Repo, ILogger<ApiController> logger= null, IConfiguration config = null)
        {
            _logger = logger; 
            _config = config;
            _repo = Repo;
        }

        //-----------------------------ЗАПРОСЫ-----------------------------
        /// <summary>
        /// Получить все локации
        /// </summary>
        [HttpGet("GetLocalities")]
        public IActionResult GetLocalities()
        {
            dynamic localities = new JArray();

            var ForeachLocalities = _repo.GetLocalities();
            foreach (var l in ForeachLocalities)
            {
                localities.Add(new JObject(
                    new JProperty("id", l.id),
                    new JProperty("name", l.Name),
                    new JProperty("type", l.Type),
                    new JProperty("numberresidantsth", l.NumberResidantsTh),
                    new JProperty("budgetmlrd", l.BudgetMlrd),
                    new JProperty("squareKM", l.SquareKm),
                    new JProperty("mayor", l.Mayor)
                    ));
            }

            string respStr = localities.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить статистику по бюджетам
        /// </summary>
        [HttpGet("GetStatistic")]
        public IActionResult GetLStatistic()
        {
            dynamic Statistics = new JArray();

            var ForeachStatistics = _repo.GetStatistic(_repo.GetBudgets(), _repo.GetNumberResidants());
            foreach (var l in ForeachStatistics)
            {
                Statistics.Add(new JObject(
                        new JProperty("Name", l.Name),
                        new JProperty("Median", l.Median),
                        new JProperty("Mean", l.Mean),
                        new JProperty("Max", l.Max),
                        new JProperty("Min", l.Min)
                    ));
            }

            string respStr = Statistics.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить все локации
        /// </summary>
        [HttpGet("GetLocalitiyById")]
        public IActionResult GetLocalitiyById([FromQuery(Name = "id")] int id )
        {
            dynamic resp = new JObject();

            var l = _repo.GetLocalityById(id);
            if (l != null)
            {
                resp = new JObject(
                    new JProperty("id", l.id),
                    new JProperty("name", l.Name),
                    new JProperty("type", l.Type),
                    new JProperty("numberresidantsth", l.NumberResidantsTh),
                    new JProperty("budgetmlrd", l.BudgetMlrd),
                    new JProperty("squareKM", l.SquareKm),
                    new JProperty("mayor", l.Mayor)
                    );
            }

            string respStr = resp.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Добавить запись локации
        /// </summary>
        /// <remarks>
        /// Пример:
        /// 
        ///     POST api/CreateLocality
        ///{
        ///  "id": 0,
        ///  "name": "Имя Локации",
        ///  "type": "City",
        ///  "numberResidantsTh": 1000,
        ///  "budgetMlrd": 1000,
        ///  "mayor": "Сергей Семёнович Собянин"
        ///}
        /// </remarks>
        /// <param name="locality">Данные новой записи локации</param>
        [HttpPost("CreateLocality")]
        public IActionResult CreateLocality([FromBody] Locality locality)
        {
            Locality loc = new Locality { 
                Name = locality.Name,
                Type = locality.Type,
                NumberResidantsTh = locality.NumberResidantsTh,
                BudgetMlrd = locality.BudgetMlrd,
                SquareKm = locality.SquareKm,
                Mayor = locality.Mayor
            };

            bool res = _repo.CreateLocality(loc);

            string respStr = res.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Изменить запись локации
        /// </summary>
        /// <remarks>
        /// Пример:
        /// 
        ///     POST api/UpdateLocality
        ///{
        ///  "id": 0,
        ///  "name": "Имя Локации",
        ///  "type": "City",
        ///  "numberResidantsTh": 1000,
        ///  "budgetMlrd": 1000,
        ///  "mayor": "Сергей Семёнович Собянин"
        ///}
        /// </remarks>
        /// <param name="locality">Данные изменяемой записи локации</param>
        [HttpPut("UpdateLocality")]
        public IActionResult UpdateLocality([FromBody] Locality locality)
        {

            bool res = _repo.UpdateLocality(locality);

            string respStr = res.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Удалить локацию
        /// </summary>
        /// <param name="id">Id удаляемой записи локации</param>
        [HttpDelete("DeleteLocality/{id}")]
        public IActionResult DeleteLocality(int id)
        {
            try
            {
                bool res = _repo.DeleteLocality(id);

                string respStr = res.ToString();

                return Content(respStr);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Получить все локации по ФИО мера
        /// </summary>
        [HttpGet("GetLocalitiesByMayor")]
        public IActionResult GetLocalitiesByMajor([FromQuery(Name = "mayor")] string mayor)
        {
            dynamic localities = new JArray();

            var ForeachLocalities = _repo.GetLocalitiesByMayor(mayor);
            foreach (var l in ForeachLocalities)
            {
                localities.Add(new JObject(
                    new JProperty("id", l.id),
                    new JProperty("name", l.Name),
                    new JProperty("type", l.Type),
                    new JProperty("numberresidantsth", l.NumberResidantsTh),
                    new JProperty("budgetmlrd", l.BudgetMlrd),
                    new JProperty("squareKM", l.SquareKm),
                    new JProperty("mayor", l.Mayor)
                    ));
            }

            string respStr = localities.ToString();

            return Content(respStr);
        }

        /// <summary>
        /// Получить бюджеты
        /// </summary>
        [HttpGet("GetBudgets")]
        public IActionResult GetBudgets()
        {
            double[] Budgets = _repo.GetBudgets();

            return Ok(Budgets); // Возвращаем бюджеты как JSON
        }

        /// <summary>
        /// Получить население
        /// </summary>
        [HttpGet("GetResidants")]
        public IActionResult GetResidants()
        {
            double[] Residants = _repo.GetNumberResidants();

            return Ok(Residants); // Возвращаем население как JSON
        }
        /// <summary>
        /// Получить площади (SquareKm) всех локаций
        /// </summary>
        [HttpGet("GetSquareKm")]
        public IActionResult GetSquareKm()
        {
            // пока площадей нет в отдельном агрегате, вернём население как заглушку
            double[] squareKm = _repo.GetNumberResidants();

            return Ok(squareKm);
        }
    }
}

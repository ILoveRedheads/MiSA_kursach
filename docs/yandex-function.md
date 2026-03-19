## Облачная функция Yandex.Cloud для статистики

### Входные данные

Функция принимает JSON следующего вида:

```json
{
  "Budgets": [16.65, 40.6, 24.9],
  "Residants": [18800.0, 1025.66, 597.24],
  "SquareKm": [416774.5, 859.3, 325.99]
}
```

- **Budgets**: массив бюджетов локаций в млрд.
- **Residants**: массив населения в тысячах.
- **SquareKm**: массив площадей в квадратных километрах.

### Выходные данные

Функция возвращает агрегированную статистику по каждому показателю:

```json
{
  "resultBudgets": [
    { "Name": "Бюджет", "Median": 0.0, "Mean": 0.0, "Max": 0.0, "Min": 0.0 }
  ],
  "resultResidants": [
    { "Name": "Население", "Median": 0.0, "Mean": 0.0, "Max": 0.0, "Min": 0.0 }
  ],
  "resultSquareKm": [
    { "Name": "Площадь", "Median": 0.0, "Mean": 0.0, "Max": 0.0, "Min": 0.0 }
  ]
}
```

### Пример реализации функции (C#)

```csharp
using System.Linq;
using System.Text.Json;

public class Input
{
    public double[] Budgets { get; set; }
    public double[] Residants { get; set; }
    public double[] SquareKm { get; set; }
}

public class Stat
{
    public string Name { get; set; }
    public double Median { get; set; }
    public double Mean { get; set; }
    public double Max { get; set; }
    public double Min { get; set; }
}

public class Function
{
    public string Handle(string request)
    {
        var input = JsonSerializer.Deserialize<Input>(request);

        Stat Build(string name, double[] data) => new Stat
        {
            Name = name,
            Median = data.OrderBy(x => x).ElementAt(data.Length / 2),
            Mean = data.Average(),
            Max = data.Max(),
            Min = data.Min()
        };

        var result = new
        {
            resultBudgets = new[] { Build("Бюджет", input.Budgets) },
            resultResidants = new[] { Build("Население", input.Residants) },
            resultSquareKm = new[] { Build("Площадь", input.SquareKm) }
        };

        return JsonSerializer.Serialize(result);
    }
}
```

### Как используется в проекте

- Веб‑клиент (`HomeController.Statistic`) и сервис статистики (`StatisticsController.GetCloudStatistics`) формируют JSON и отправляют POST‑запрос на URL функции:
  - `https://functions.yandexcloud.net/d4eekil0t8rto31d5eov`
- Полученный ответ преобразуется в список моделей `Statistic` и отдаётся клиентам.


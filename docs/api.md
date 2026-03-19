## REST API сервисов

### Базовый URL

- **Веб‑приложение / Swagger**: `https://localhost:49105`
- **Основной REST‑сервис данных**: `https://localhost:49105/api`
- **Сервис статистики**: `https://localhost:49105/statistics`

### Основной REST‑сервис данных (`ApiController`)

- **GET** `/api/GetLocalities`  
  Возвращает список всех локаций.

- **GET** `/api/GetLocalitiyById?id={id}`  
  Возвращает одну локацию по идентификатору.

- **GET** `/api/GetLocalitiesByMayor?mayor={ФИО}`  
  Возвращает список локаций по ФИО мэра.

- **POST** `/api/CreateLocality`  
  Создаёт новую локацию. Тело запроса (JSON):
  ```json
  {
    "id": 0,
    "name": "Имя локации",
    "type": "City",
    "numberResidantsTh": 1000.0,
    "budgetMlrd": 100.0,
    "squareKm": 500.0,
    "mayor": "ФИО мэра"
  }
  ```

- **PUT** `/api/UpdateLocality`  
  Обновляет существующую локацию. Формат тела как у `CreateLocality`.

- **DELETE** `/api/DeleteLocality/{id}`  
  Удаляет локацию по идентификатору.

- **GET** `/api/GetBudgets`  
  Возвращает массив бюджетов всех локаций (`double[]`).

- **GET** `/api/GetResidants`  
  Возвращает массив населения в тысячах (`double[]`).

- **GET** `/api/GetSquareKm`  
  Возвращает массив площадей (`double[]`, на данный момент реализована как заглушка).

### Сервис статистики (`StatisticsController`)

- **GET** `/statistics/local`  
  Локальная статистика по бюджетам и населению.  
  Ответ: массив объектов
  ```json
  [
    {
      "name": "Бюджет",
      "median": 0.0,
      "mean": 0.0,
      "max": 0.0,
      "min": 0.0
    },
    {
      "name": "Население",
      "median": 0.0,
      "mean": 0.0,
      "max": 0.0,
      "min": 0.0
    }
  ]
  ```

- **GET** `/statistics/cloud`  
  Обёртка над облачной функцией Yandex.Cloud.  
  Формирует JSON c массивами бюджетов/населения/площадей и отправляет POST на URL функции.  
  Ответ: объединённый список статистик по трём показателям.

### Swagger / OpenAPI

- Включён в `Program.cs` через `Swashbuckle`:
  - JSON‑описание: `/swagger/v1/swagger.json`
  - UI: `/swagger`


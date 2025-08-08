# EffectiveMobile.TestWork.API

Сервис для хранения и поиска рекламных площадок по вложенным локациям.

## 🚀 Возможности

- Загрузка рекламных площадок из текстового файла, расположенного по абсолютному пути.
- Поиск рекламных площадок по заданной локации (с учётом вложенности).

## 📦 Формат входного файла

Обычный текстовый файл (`.txt`), в котором каждая строка соответствует одной рекламной площадке.

**Формат строки:**

```
<Название>:<Локация1>[,<Локация2>,...]
```

**Пример файла (`C:\ad.txt`):**

```
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl/,/ru/chelobl
Крутая реклама:/ru/svrd
```

---

## 📤 Загрузка рекламных площадок

**Эндпоинт:** `POST /api/AdvertisingSpaces/upload`  
**Content-Type:** `application/json`  
**Тело запроса:** строка с абсолютным путём к файлу

**Пример запроса (`.http`):**

```http
POST http://localhost:5044/api/AdvertisingSpaces/upload
Content-Type: application/json

"C:\\ad.txt"
```

---

## 🔍 Получение рекламных площадок по локации

**Эндпоинт:** `GET /api/AdvertisingSpaces?location={локация}`

**Пример:**

```http
GET http://localhost:5044/api/AdvertisingSpaces?location=/ru/msk
Accept: application/json
```

Ответ будет содержать массив названий рекламных площадок, подходящих под указанную локацию.

---

## ✅ Пример запуска и использования

1. Убедитесь, что файл `C:\ad.txt` существует и содержит данные в указанном формате.
2. Запустите API (`dotnet run` или через IDE).
3. Отправьте запрос на загрузку по пути к файлу.
4. Выполните запрос на поиск по нужной локации.
---

## ▶️ Инструкция по запуску проекта

1. **Клонируйте репозиторий:**

```bash
git clone https://github.com/vitkuz573/EffectiveMobile.TestWork.git
cd EffectiveMobile.TestWork/EffectiveMobile.TestWork.API
```

2. **Проверьте установленный .NET SDK:**

```bash
dotnet --version
```

> Требуется .NET 9.0 или выше.

3. **Соберите и запустите проект:**

```bash
dotnet run
```

4. **API будет доступно по адресу:**

```
http://localhost:5044
```

5. **Проверьте работоспособность эндпоинтов** с помощью Postman, curl или `.http` файла в VS Code.

---

## 📁 Структура проекта (кратко)

```
EffectiveMobile.TestWork.API/
├── Controllers/
│   └── AdvertisingSpacesController.cs
├── Services/
│   └── DataService.cs
├── Models/
│   └── AdvertisingSpace.cs
├── Abstractions/
│   └── IDataService.cs
└── Program.cs
```
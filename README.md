# online-store-web-api
## О проекте
online-store-web-api - это Web API для онлайн-магазина с реализованными CRUD-операциями для шести сущностей: пользователь, категория товара, товар, отзыв, корзина, предмет из корзины.
## Запуск проекта
1. Скачайте и установите Docker: https://www.docker.com/get-started/
2. Запустите Docker Desktop.
3. Откройте терминал( например, PowerShell или Bash).
4. Перейдите в директорию с проектом.
5. Наберите в терминале команду: `docker compose up`.
6. После запуска контейнеров приложение будет доступно по ссылкt: https://localhost:8081/swagger/index.html

После проделанных выше действий приложение готово к работе.

## Документация
### Технологии
+ ASP.NET Core Web-API
+ Swagger
+ PostgreSQL
+ Entity Framework Core

### Функционал
Базовые CRUD-операциями для шести сущностей: пользователь, категория товара, товар, отзыв, корзина, предмет из корзины.
Также есть возможность узнать рейтинг товара и суммарную стоимость предметов из корзины.

### Архитектура проекта
При написании проекта была использована REST-архитектура, т.е. контроллеры обрабатывают 4 http-запроса к серверу: GET, POST, UPDATE, CREATE.

Модели: AppUser.cs, Category.cs, Product.cs, Review.cs, ShoppingCart.cs, ShoppingCartItem.cs.

Контроллеры: AppUserController.cs, CategoryController.cs, ProductController.cs, ReviewController.cs, ShoppingCartController.cs, ShoppingCartItemController.cs.

В классы в директории Dto нужны для того, чтобы пользователь не работал с моделями на прямую, и тем самым не имел доступ к внешним ключам или навигационным свойствам модели. Т.е. каждый класс в директории Dto - это DTO - Data Transfer Object(объект для передачи данных). Класс MappingProfile связывает модели и DTO-классы.

Классы в папке Repository и класс AppDbContext нужны для работы c базой данных.

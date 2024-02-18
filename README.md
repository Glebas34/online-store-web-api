# online-store-web-api
## О проекте
online-store-web-api - это Web API для онлайн-магазина с реализованными CRUD-операциями для шести сущностей: пользователь, категория товара, товар, отзыв, корзина, предмет из корзины.
##Запуск проекта
1. Откройте проект в Visual Studio.
2. Создайте локальную базу данных PostgreSQL на своём компьютере.
3. Перейдите обратно в отркытый проект и откройте файл appsettings.json.
4. В ConnectionStrings измените строку PostgreSql, исходя из свойств вашей базы данных: "PostgreSql": "Host=localhost; Database=your_database; Username=your_username; Password=your_password".
5. Откройте консоль диспетчера пакетов.
6. Пропишите в ней команду Update-Database.

После проделанных выше действий приложение готово к работе.

## Документация
### Технологии
+ ASP.NET Core Web-API
+ Swagger
+ PostgreSQL

### Функционал
Базовые CRUD-операциями для шести сущностей: пользователь, категория товара, товар, отзыв, корзина, предмет из корзины.
Также есть возможность узнать рейтинг товара и суммарную стоимость предметов из корзины.

### Архитектура проекта
При написании проекта была использована REST-архитектура, т.е. контроллеры обрабатывают 4 http-запроса к серверу: GET, POST, UPDATE, CREATE.

Модели: AppUser.cs, Category.cs, Product.cs, Review.cs, ShoppingCart.cs, ShoppingCartItem.cs.

Контроллеры: AppUserController.cs, CategoryController.cs, ProductController.cs, ReviewController.cs, ShoppingCartController.cs, ShoppingCartItemController.cs.

В классы в директории Dto нужны для того, чтобы пользователь не работал с моделями на прямую, и тем самым не имел доступ к внешним ключам или навигационным свойствам модели. DTO - это Data Transfer Object(объект для передачи данных). Класс MappingProfile связывает модели и DTO-классы.

Классы в папке Repository и класс AppDbContext нужны для работы c базой данных

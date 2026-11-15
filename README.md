# DisasterReliefCoordination

Nền tảng điều phối tình nguyện viên và vật tư cứu trợ thiên tai. Base project gồm ASP.NET Core Web API theo Clean Architecture, PostgreSQL/PostGIS, Angular và Leaflet.

## Công nghệ

- Backend: .NET 8, ASP.NET Core Web API, EF Core 8, JWT, SignalR, Swagger.
- Database: PostgreSQL 16 và PostGIS, Npgsql NetTopologySuite với `geography(Point, 4326)`.
- Frontend: Angular 22, TypeScript 6, Tailwind CSS, Leaflet.
- Local environment: Docker Compose và pgAdmin.

## Cấu trúc

```text
src/
  Core/
    DisasterRelief.Domain/          # Entities, enums, domain model
    DisasterRelief.Application/     # DTOs, interfaces, use cases
  Infrastructure/
    DisasterRelief.Infrastructure/ # EF Core, PostGIS, repositories, JWT
  Presentation/
    DisasterRelief.Api/             # Controllers, Swagger, SignalR, entry point
client/                              # Angular application
docker-compose.yml
```

## Chạy bằng Docker

Cần Docker Desktop hoặc Docker Engine với Compose plugin. Từ thư mục gốc:

```powershell
Copy-Item .env.example .env
docker compose up --build
```

Các địa chỉ local:

- API và Swagger: http://localhost:8080/swagger
- Health check: http://localhost:8080/health
- pgAdmin: http://localhost:5050
- PostgreSQL/PostGIS: `localhost:5432`

Thông tin đăng nhập mặc định trong `.env.example` chỉ dành cho máy local. Thay password, JWT key và thông tin pgAdmin trước khi chia sẻ môi trường hoặc triển khai. Trong pgAdmin, kết nối host `postgres` khi chạy cùng Compose; từ máy host dùng `localhost`.

Compose chờ PostgreSQL healthy rồi khởi động API. API áp dụng EF migrations khi khởi động; migration ban đầu đã có trong project.

## Chạy backend local

Cần .NET 8 SDK. Nếu chạy PostgreSQL bằng Compose, chỉ khởi động database và pgAdmin:

```powershell
Copy-Item .env.example .env
docker compose up -d postgres pgadmin
dotnet tool restore
dotnet ef database update --project src/Infrastructure/DisasterRelief.Infrastructure --startup-project src/Presentation/DisasterRelief.Api
dotnet run --project src/Presentation/DisasterRelief.Api
```

API local dùng cấu hình trong `src/Presentation/DisasterRelief.Api/appsettings.json`; có thể ghi đè bằng biến môi trường `ConnectionStrings__DefaultConnection` và `Jwt__Key`. Cấu hình mặc định chỉ dành cho phát triển.

## Chạy frontend

Cần Node.js được Angular 22 hỗ trợ (Node 22.22.3+, 24.15.0+ hoặc 26+ theo metadata Angular) và npm:

```powershell
npm --prefix client install
npm --prefix client start
```

Frontend mở tại http://localhost:4200. Leaflet hiển thị bản đồ OpenStreetMap tại Việt Nam; cần kết nối Internet để tải tile bản đồ.

## API ban đầu

- `POST /api/auth/register` tạo tài khoản Volunteer.
- `POST /api/auth/login` đăng nhập và nhận JWT.
- `GET /api/warehouse` và `POST /api/warehouse` quản lý kho.
- `GET /api/reliefrequest` và `POST /api/reliefrequest` quản lý yêu cầu cứu trợ.
- SignalR hub: `/hubs/notifications` (yêu cầu JWT).

Các endpoint kho và yêu cầu cứu trợ yêu cầu `Authorization: Bearer <token>`. Swagger hỗ trợ nhập Bearer token. Tọa độ đầu vào theo thứ tự longitude, latitude.

## EF Core migrations

Đã cấu hình local tool `dotnet-ef` phiên bản 8.0.11:

```powershell
dotnet tool restore
dotnet ef migrations add MigrationName --project src/Infrastructure/DisasterRelief.Infrastructure --startup-project src/Presentation/DisasterRelief.Api --output-dir Persistence/Migrations
dotnet ef database update --project src/Infrastructure/DisasterRelief.Infrastructure --startup-project src/Presentation/DisasterRelief.Api
```

## Ghi chú bảo mật

Đăng ký công khai chỉ tạo role `Volunteer`; tài khoản có quyền cao cần được cấp qua quy trình quản trị riêng. Password được băm PBKDF2 và JWT ký bằng key cấu hình. Các giá trị local mặc định không dùng cho production; cần thay secret, bổ sung rate limiting, xác thực email, chính sách phân quyền và quản lý secret trước triển khai thật.

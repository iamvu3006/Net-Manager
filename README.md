<div align="center">

# Net-Manager

### Nền tảng quản lý tiệm Internet hiện đại, trực quan và dễ mở rộng

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/mvc/)
[![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-9.0-68217A?style=for-the-badge&logo=.net&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)

<p>
  <a href="#-tổng-quan">Tổng quan</a> •
  <a href="#-tính-năng">Tính năng</a> •
  <a href="#-bắt-đầu">Bắt đầu</a> •
  <a href="#-kiến-trúc">Kiến trúc</a> •
  <a href="#-đóng-góp">Đóng góp</a>
</p>

</div>

---

## 📌 Tổng quan

**Net-Manager** là ứng dụng web quản lý tiệm Internet được xây dựng trên **ASP.NET Core MVC**. Hệ thống hỗ trợ quản lý máy trạm, tài khoản khách hàng, phiên sử dụng, số dư và doanh thu trong một giao diện tập trung.

Dự án hướng đến một nền tảng đơn giản để vận hành hằng ngày nhưng vẫn có cấu trúc rõ ràng, dễ bảo trì và thuận tiện mở rộng thêm các nghiệp vụ như đặt máy, dịch vụ phụ trợ hoặc báo cáo nâng cao.

## ✨ Tính năng

### Dành cho khách hàng

- Đăng ký, đăng nhập và đăng xuất tài khoản.
- Xem, cập nhật thông tin cá nhân và số dư.
- Nạp tiền vào tài khoản.
- Xem danh sách máy đang trống hoặc đang được sử dụng.
- Chọn máy và bắt đầu phiên sử dụng.
- Theo dõi thời gian sử dụng và chi phí phát sinh.
- Kết thúc phiên, tự động tính phí theo thời gian thực tế.
- Xem lịch sử sử dụng và các khoản đã thanh toán.

### Dành cho quản trị viên

- Quản lý danh sách máy tính: thêm, sửa, xóa và cập nhật trạng thái.
- Quản lý người dùng và thông tin tài khoản.
- Theo dõi các phiên sử dụng trong hệ thống.
- Xem báo cáo doanh thu và thống kê hoạt động.
- Phân tách giao diện, chức năng và quyền truy cập theo vai trò.

## 🧰 Công nghệ sử dụng

| Thành phần | Công nghệ |
| --- | --- |
| Nền tảng | .NET 9 |
| Backend | ASP.NET Core MVC |
| ORM | Entity Framework Core 9 |
| Cơ sở dữ liệu | Microsoft SQL Server |
| Giao diện | Razor Views, Bootstrap, HTML, CSS, JavaScript |
| Quản lý trạng thái | ASP.NET Core Session |
| Môi trường phát triển | Visual Studio hoặc .NET CLI |

## 🏗️ Kiến trúc

```text
Net-Manager/
├── Controllers/       # Điều phối request và nghiệp vụ ứng dụng
├── Models/            # Entity, DbContext và ViewModel
├── Views/             # Giao diện Razor theo mô hình MVC
├── Migrations/        # Lịch sử migration của Entity Framework Core
├── wwwroot/           # CSS, JavaScript, hình ảnh và tài nguyên tĩnh
├── Program.cs         # Đăng ký dịch vụ và cấu hình pipeline
├── appsettings.json   # Cấu hình ứng dụng và connection string
└── MvcMovie.csproj    # Package và cấu hình project
```

Luồng xử lý chính:

```text
Người dùng → Razor View → Controller → Entity Framework Core → SQL Server
```

## 🚀 Bắt đầu

### Yêu cầu hệ thống

- [.NET SDK 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) hoặc SQL Server Express
- [Git](https://git-scm.com/)
- Công cụ migration EF Core (nếu chưa có):

```bash
dotnet tool install --global dotnet-ef
```

### 1. Clone repository

```bash
git clone https://github.com/iamvu3006/Net-Manager.git
cd Net-Manager
```

### 2. Cấu hình cơ sở dữ liệu

Cập nhật connection string `NetManagementDB` trong `appsettings.json` hoặc dùng **User Secrets** cho môi trường phát triển:

```json
{
  "ConnectionStrings": {
    "NetManagementDB": "Server=localhost;Database=NetManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> Không commit thông tin kết nối hoặc thông tin nhạy cảm vào repository. Với môi trường production, nên dùng biến môi trường hoặc secret manager.

### 3. Khởi tạo database

```bash
dotnet restore
dotnet ef database update
```

Nếu cần tạo migration mới sau khi thay đổi model:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### 4. Chạy ứng dụng

```bash
dotnet run
```

Mở địa chỉ được hiển thị trong terminal, thường là:

- `https://localhost:7xxx`
- `http://localhost:5xxx`

## 🛡️ Lưu ý bảo mật

- Không sử dụng connection string mẫu trong production.
- Bật HTTPS khi triển khai thực tế.
- Kiểm tra và giới hạn quyền truy cập các chức năng quản trị.
- Không lưu secret, mật khẩu hoặc dữ liệu cá nhân trong source code.
- Nên bổ sung cơ chế hash mật khẩu hiện đại, chống giả mạo request và validation đầu vào trước khi triển khai production.

## 🗺️ Định hướng phát triển

- [ ] Đặt máy trước theo khung giờ.
- [ ] Quản lý đồ ăn, thức uống và dịch vụ phụ trợ.
- [ ] Thông báo khi số dư thấp hoặc phiên sắp kết thúc.
- [ ] Dashboard doanh thu theo ngày, tuần và tháng.
- [ ] Xuất báo cáo Excel/PDF.
- [ ] API cho ứng dụng desktop hoặc mobile.
- [ ] Container hóa và triển khai lên cloud.

## 🤝 Đóng góp

Mọi đóng góp đều được chào đón. Để bắt đầu:

1. Fork repository.
2. Tạo một branch mới: `git checkout -b feat/ten-tinh-nang`.
3. Commit thay đổi: `git commit -m "feat: mo ta thay doi"`.
4. Push branch: `git push origin feat/ten-tinh-nang`.
5. Mở Pull Request với phần mô tả rõ ràng.

Vui lòng kiểm tra ứng dụng và mô tả cách tái hiện nếu bạn báo lỗi.

## 📄 Giấy phép

Dự án hiện chưa công bố giấy phép sử dụng riêng. Vui lòng liên hệ chủ repository trước khi sử dụng cho mục đích thương mại hoặc phân phối lại.

## 🔗 Liên kết

- [Mở repository trên GitHub](https://github.com/iamvu3006/Net-Manager)
- [Xem mã nguồn trên GitHub1s](https://github1s.com/iamvu3006/Net-Manager)
- [Phân tích mã nguồn trên Sourcegraph](https://sourcegraph.com/github.com/iamvu3006/Net-Manager)

---

<div align="center">

Được xây dựng với ❤️ bằng ASP.NET Core MVC

</div>

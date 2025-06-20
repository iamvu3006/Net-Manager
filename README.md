# Net-Manager

Net-Manager là một hệ thống quản lý tiệm Internet (quản lý phòng máy/net) hiện đại, được xây dựng với ASP.NET Core MVC và Entity Framework, hỗ trợ đầy đủ các nghiệp vụ quản lý người dùng, máy tính, thời gian sử dụng, thanh toán, và báo cáo.

## 🚀 Tính năng nổi bật

- **Quản lý người dùng**: Đăng ký, đăng nhập, đăng xuất, xem danh sách, cập nhật thông tin, nạp tiền tài khoản.
- **Quản lý máy tính**: Thêm/sửa/xóa máy, theo dõi trạng thái từng máy (đang sử dụng/trống), giá theo giờ.
- **Chọn máy và theo dõi thời gian**: Người dùng có thể chọn máy còn trống, hệ thống tự động tính toán thời gian sử dụng và chi phí realtime.
- **Thanh toán minh bạch**: Khi kết thúc ca sử dụng, hệ thống tự trừ tiền dựa trên thời gian thực tế và cập nhật số dư tài khoản.
- **Báo cáo & lịch sử sử dụng**: Hiển thị lịch sử sử dụng máy, thống kê thời gian và chi phí của từng người dùng.
- **Phân quyền**: Quản trị viên và người dùng thông thường có giao diện và quyền truy cập khác nhau.
- **Bảo mật**: Mã hóa mật khẩu SHA256, sử dụng session để lưu trạng thái đăng nhập.

## 🖼️ Giao diện

- Thiết kế trực quan, thân thiện trên cả desktop và mobile.
- Sử dụng Bootstrap 5, Razor Pages, hỗ trợ icon hiện đại.

## 🛠️ Công nghệ sử dụng

- **Backend**: ASP.NET Core MVC, Entity Framework Core
- **Frontend**: Razor View Engine, Bootstrap 5, JavaScript
- **Database**: SQL Server
- **Authentication**: Session-based, SHA256 password hashing

## ⚙️ Hướng dẫn cài đặt và chạy dự án

1. **Clone dự án:**
   ```bash
   git clone https://github.com/iamvu3006/Net-Manager.git
   cd Net-Manager
   ```

2. **Cập nhật chuỗi kết nối CSDL**  
   Sửa file `appsettings.json`, điền thông tin kết nối SQL Server tại trường `NetManagementDB`.

3. **Khởi tạo database:**  
   Chạy lệnh migration của Entity Framework Core để tạo các bảng cần thiết:
   ```bash
   dotnet ef database update
   ```

4. **Chạy ứng dụng:**
   ```bash
   dotnet run
   ```
   Truy cập [http://localhost:5000](http://localhost:5000) (hoặc port do app thông báo) để sử dụng hệ thống.

## 👤 Các vai trò trong hệ thống

- **Quản trị viên:** Quản lý toàn bộ máy tính, người dùng, xem báo cáo tổng thể.
- **Người dùng:** Đăng ký, đăng nhập, chọn máy, theo dõi thời gian sử dụng, nạp tiền, xem lịch sử cá nhân.

## 📂 Cấu trúc thư mục chính

- `/Controllers` - Các controller điều phối nghiệp vụ (Account, Computers, Users, UsageHistories, ...)
- `/Models` - Định nghĩa các entity như User, Computer, UsageHistory,...
- `/Views` - Giao diện cho từng chức năng (dùng Razor)
- `/wwwroot` - Tài nguyên tĩnh (ảnh, CSS, JS)
- `Program.cs` - Khởi tạo và cấu hình ứng dụng

## 📋 Một số màn hình tiêu biểu

- 📂 [Xem mã nguồn dạng VS Code (GitHub1s)](https://github1s.com/iamvu3006/Net-Manager)
- 🧠 [Phân tích chuyên sâu (Sourcegraph)](https://sourcegraph.com/github.com/iamvu3006/Net-Manager)


## 💡 Gợi ý mở rộng

- Thêm tính năng đặt máy trước, quản lý dịch vụ phụ trợ (đồ ăn, nước uống).
- Gửi thông báo khi sắp hết tiền hoặc hết thời gian.
- Báo cáo doanh thu, phân tích hành vi người dùng, xuất file Excel/PDF.

## 📝 Đóng góp

Mọi ý kiến đóng góp, báo lỗi hoặc pull request đều được chào đón!  
Liên hệ: [lexuanbavu@gmail.com](mailto:lexuanbavu@gmail.com)

---

**Net-Manager** - Giải pháp quản lý tiệm net hiện đại, dễ dùng, bảo mật và mở rộng.

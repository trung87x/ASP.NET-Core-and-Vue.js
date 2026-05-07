# Chapter 1: Tiêu chuẩn Thiết lập Môi trường (Environment & Setup Standard)

Tài liệu này quy định các yêu cầu bắt buộc về môi trường phát triển và cấu trúc khởi tạo cho dự án Travel App. Đây là bước đầu tiên trong quy trình Audit dự án.

---

## 1. Yêu cầu Công cụ bắt buộc (Required Tooling)

Mọi lập trình viên tham gia dự án phải cài đặt đúng các phiên bản sau để đảm bảo tính đồng nhất:

| Công cụ | Phiên bản tối thiểu | Mục đích |
| :--- | :--- | :--- |
| **.NET SDK** | 5.0 / 6.0 | Runtime cho Backend C# |
| **Node.js** | 16.x (LTS) | Runtime cho Frontend Vue.js |
| **Visual Studio** | 2022 | IDE chính cho phát triển .NET |
| **SQLite Browser** | Latest | Kiểm tra dữ liệu Database cục bộ |

---

## 2. Tiêu chuẩn khởi tạo Solution (Solution Initialization Standard)

Dự án không được phép tạo thủ công bằng giao diện kéo thả nếu không tuân thủ cấu trúc CLI sau:

```bash
# Quy tắc đặt tên: Travel.[LayerName]
dotnet new sln -n Travel
dotnet new classlib -n Travel.Domain
dotnet new classlib -n Travel.Application
dotnet new classlib -n Travel.Infrastructure
dotnet new webapi -n Travel.WebApi
```

**Quy tắc Audit:** Toàn bộ các project phải được đưa vào một Solution duy nhất (`.sln`) để quản lý tập trung.

---

## 3. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Lệnh `dotnet --version` có trả về phiên bản đúng không? (Nếu không -> **FAILED**).
- [ ] **Check 2:** Các Project có tuân thủ quy tắc đặt tên `Travel.*` không? (Nếu không -> **FAILED**).
- [ ] **Check 3:** Có file `.sln` ở thư mục gốc không? (Nếu không -> **FAILED**).
- [ ] **Check 4:** Tầng WebApi có được khởi tạo dưới dạng `webapi` template không? (Để có sẵn Swagger và RESTful setup).

---

## 4. Xác minh môi trường (Verification)

Trước khi bắt đầu code, chạy lệnh sau để kiểm tra tính sẵn sàng của hệ thống:
```powershell
dotnet restore  # Kiểm tra kết nối NuGet
npm install     # Kiểm tra kết nối NPM (tại thư mục vue-app)
```

---

> [!IMPORTANT]
> Việc không đồng nhất phiên bản .NET SDK giữa các thành viên trong nhóm sẽ dẫn đến lỗi "Version Mismatch" khi triển khai CI/CD. Tuyệt đối tuân thủ phiên bản quy định.

[Tiếp theo: Tiêu chuẩn Kiến trúc Sạch (Tổng quan)](./Chapter 2 - Clean Architecture Overview.md)

---

## Tiếp theo: Chương 2 - Tổng quan về Clean Architecture
Chúng ta sẽ đi sâu vào lý thuyết đằng sau cách tổ chức các lớp này.

[Bắt đầu Chương 2](./Chapter 2 - Clean Architecture Overview.md)

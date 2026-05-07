# Chapter 8: Tiêu chuẩn Versioning và Logging (Operational Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc về quản lý phiên bản API và giám sát hệ thống bằng Logging. Đây là căn cứ để Audit tính sẵn sàng vận hành (Operations) của dự án.

---

## 1. Tiêu chuẩn Versioning (API Versioning Standard)

Mọi API mới được tạo ra phải tuân thủ cơ chế đánh số phiên bản để tránh gây lỗi cho các ứng dụng Client cũ.

- **Quy tắc:** Sử dụng `v1.0`, `v2.0` trong URL hoặc Header.
- **Default Version:** Khi không chỉ định phiên bản, hệ thống phải tự động điều hướng về phiên bản ổn định nhất (mặc định là `v1.0`).

**Mẫu Audit chuẩn (Startup.cs):**
```csharp
services.AddApiVersioning(config => {
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});
```

---

## 2. Tiêu chuẩn Logging (Serilog Enforcement Standard)

Dự án bắt buộc sử dụng **Serilog** với cấu trúc **Structured Logging**. Tuyệt đối không sử dụng `Console.WriteLine()` hoặc `Debug.WriteLine()` để ghi nhật ký.

- **Storage:** Log phải được ghi ra ít nhất 2 nơi: Console (Development) và File/Database (Production).
- **Format:** Sử dụng JSON format cho tệp Log để phục vụ việc phân tích tự động.
- **Enrichment:** Mỗi dòng log phải chứa: `Timestamp`, `Level`, `Message`, `ExceptionDetails`, và `MachineName`.

---

## 3. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có đoạn code nào dùng `Console.WriteLine` thay vì `_logger.LogInformation` không? (Nếu có -> **FAILED**).
- [ ] **Check 2:** File Log có được cấu hình tự động xoay vòng (Rolling File) theo ngày không? (Nếu không -> **WARNING** - File sẽ quá lớn gây tràn ổ đĩa).
- [ ] **Check 3:** Có ghi log lại nội dung của các `BadRequests` (Lỗi 400) không? (Nên có để debug lý do người dùng gửi sai data).
- [ ] **Check 4:** Swagger có hiển thị đúng các nhóm phiên bản v1, v2 không? (Nếu không -> **FAILED**).

---

## 4. Quản lý lỗi (Global Exception Handling)

Hệ thống phải có một Middleware tập trung để bắt mọi Exception chưa được xử lý, ghi log lại và trả về định dạng lỗi đồng nhất cho Client. Tuyệt đối không để lộ Stack Trace của C# ra phía người dùng cuối.

---

> [!IMPORTANT]
> "Một hệ thống không có Logging tốt là một hệ thống mù". Việc thiếu Logging sẽ khiến chúng ta mất hàng giờ (thậm chí hàng ngày) để tìm ra nguyên nhân của một lỗi xảy ra trên môi trường Production.

[Tiếp theo: Tiêu chuẩn Bảo mật JWT](./Chapter 9 - Securing ASP.NET Core.md)

---

## Tiếp theo: Chương 9 - Securing ASP.NET Core
Chúng ta sẽ học cách bảo vệ API của mình bằng **JWT (JSON Web Token)**, đảm bảo chỉ những người dùng hợp lệ mới có thể truy cập dữ liệu.

[Bắt đầu Chương 9](./Chapter 9 - Securing ASP.NET Core.md)

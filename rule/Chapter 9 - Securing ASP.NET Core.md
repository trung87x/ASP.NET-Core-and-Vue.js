# Chapter 9: Tiêu chuẩn Bảo mật JWT (Security Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc về xác thực (Authentication) và phân quyền (Authorization) sử dụng **JWT Token** trong dự án Travel App. Đây là căn cứ để Audit tính bảo mật của hệ thống.

---

## 1. Tiêu chuẩn Token (JWT Token Standard)

Mọi Token được phát hành phải tuân thủ cấu trúc bảo mật nghiêm ngặt:

- **Thuật toán:** Sử dụng tối thiểu `HmacSha256` để ký Token.
- **Payload (Claims):** Chỉ chứa các thông tin thiết yếu như `UserId`, `Role`, và `Username`. Tuyệt đối không đưa mật khẩu hay thông tin nhạy cảm vào Payload.
- **Expiration:** Thời gian hết hạn của Access Token phải được cấu hình linh hoạt và không nên quá dài (khuyến nghị tối đa 24 giờ cho môi trường Dev).

---

## 2. Quy tắc Lưu trữ Bí mật (Secret Management Rule)

- **CẤM:** Viết cứng (Hard-code) chuỗi `Secret Key` trong mã nguồn.
- **QUY TẮC:** Sử dụng `Environment Variables` hoặc `Azure Key Vault` cho môi trường Production. Đối với Development, sử dụng `User Secrets` hoặc `appsettings.json` (phải nằm trong `.gitignore`).

---

## 3. Tiêu chuẩn Middleware Xác thực (Auth Middleware Standard)

Hệ thống phải triển khai một cơ chế kiểm tra Token tập trung:

1.  **Extract:** Lấy chuỗi từ Header `Authorization: Bearer <token>`.
2.  **Validate:** Kiểm tra tính toàn vẹn và thời hạn của Token.
3.  **Context Injection:** Đính kèm thông tin người dùng vào `HttpContext.Items["User"]` để các lớp xử lý phía sau (như Handlers) có thể sử dụng mà không cần giải mã lại Token.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có API nào trả về dữ liệu nhạy cảm mà không yêu cầu Header `Authorization` không? (Nếu có -> **FAILED** - Trừ trang Login/Register).
- [ ] **Check 2:** `Secret Key` có độ dài tối thiểu 32 ký tự để đảm bảo an toàn cho thuật toán HS256 không? (Nếu không -> **WARNING**).
- [ ] **Check 3:** Có xử lý trường hợp Token bị hết hạn (Expired) và trả về lỗi 401 đúng chuẩn không? (Nếu không -> **FAILED**).
- [ ] **Check 4:** Swagger có nút "Authorize" và hoạt động đúng với định dạng `Bearer {token}` không? (Nếu không -> **FAILED**).

---

## 5. Phân quyền (Authorization)

Sử dụng thuộc tính `[Authorize]` trên các Controller hoặc Action cần bảo vệ. Đối với các tác vụ đặc quyền, phải kiểm tra Role: `[Authorize(Roles = "Admin")]`.

---

> [!CAUTION]
> Tuyệt đối không gửi JWT Token qua các kết nối HTTP không mã hóa (không có SSL/TLS). Token bị đánh cắp tương đương với việc người dùng bị mất toàn bộ quyền kiểm soát tài khoản.

[Tiếp theo: Tiêu chuẩn Tối ưu hiệu năng với Redis](./Chapter 10 - Performance Enhancement with Redis.md)

---

## Tiếp theo: Chương 10 - Tăng tốc hiệu năng với Redis
Chúng ta sẽ học cách sử dụng **Redis Cache** để giúp ứng dụng phản hồi nhanh gấp nhiều lần bằng cách lưu trữ dữ liệu thường xuyên truy cập vào bộ nhớ RAM.

[Bắt đầu Chương 10](./Chapter 10 - Performance Enhancement with Redis.md)

# Chapter 19: Tiêu chuẩn Triển khai tự động (CI/CD Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi thiết lập quy trình tích hợp và triển khai tự động (CI/CD) cho dự án Travel App. Đây là căn cứ để Audit tính sẵn sàng phát hành (Release Readiness).

---

## 1. Tiêu chuẩn Tích hợp liên tục (CI Standard)

Mọi yêu cầu kéo (Pull Request) hoặc hành động Push lên nhánh chính (`master`/`main`) phải kích hoạt quy trình CI tự động:

- **Build:** Phải biên dịch thành công cả Backend (.NET) và Frontend (NPM).
- **Testing:** Toàn bộ các bài Integration Tests (Chương 18) phải được chạy và vượt qua 100%.
- **Linting:** (Khuyến nghị) Phải vượt qua các bài kiểm tra quy tắc viết code (ESLint cho Vue, Roslyn cho C#).

---

## 2. Tiêu chuẩn Triển khai liên tục (CD Standard)

Việc triển khai lên môi trường Staging/Production phải được thực hiện tự động qua GitHub Actions:

- **Artifacts:** Chỉ được phép triển khai các tệp đã được Build ở chế độ Release (`dotnet publish -c Release`).
- **Secrets Management:** Tuyệt đối không lưu trữ Password, Connection String hay API Key trong file `.yml`. Tất cả phải được lưu trong **GitHub Actions Secrets**.
- **Rollback:** Quy trình CD phải cho phép quay lại phiên bản ổn định trước đó trong trường hợp xảy ra lỗi sau khi Deploy.

---

## 3. Tiêu chuẩn môi trường Azure (Azure Setup)

- **App Service:** Sử dụng Azure Web App for Containers hoặc Web App (Windows/Linux) phù hợp với cấu hình .NET Core.
- **HTTPS:** Bắt buộc sử dụng SSL/TLS cho mọi Endpoint API và giao diện web.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có thể đẩy code trực tiếp lên Production mà không cần qua bước chạy Test tự động không? (Nếu có -> **FAILED** - Rủi ro phá hỏng hệ thống rất cao).
- [ ] **Check 2:** Các file cấu hình nhạy cảm (Secrets) có đang nằm trong mã nguồn GitHub không? (Nếu có -> **FAILED**).
- [ ] **Check 3:** Quy trình CI có thông báo lỗi qua Email/Slack cho nhóm khi Build thất bại không? (Nên có - Nếu không -> **WARNING**).
- [ ] **Check 4:** Frontend đã được rút gọn (Minified) và nén (Gzip/Brotli) trước khi Deploy không? (Nếu không -> **WARNING**).

---

## 5. Nhật ký triển khai (Deployment Logs)

Phải lưu giữ nhật ký của mọi lần Deploy thành công/thất bại để phục vụ việc truy vết nguyên nhân khi hệ thống gặp sự cố.

---

# 🎓 TỔNG KẾT TIÊU CHUẨN AUDIT

Bạn đã đi qua toàn bộ 19 chương của bộ tiêu chuẩn kỹ thuật Travel App. Việc tuân thủ nghiêm ngặt các quy tắc này sẽ biến bạn từ một "người viết code" thành một **Kiến trúc sư phần mềm chuyên nghiệp**.

### Tóm tắt các trụ cột Audit:
1. **Kiến trúc:** Clean Architecture & CQRS.
2. **Bảo mật:** JWT & Security Middleware.
3. **Hiệu năng:** Redis & SQL Optimization.
4. **Chất lượng:** Frontend Standards & Form Validation.
5. **Độ tin cậy:** Integration Testing & CI/CD.

**Hệ thống hiện đã sẵn sàng để được Audit toàn diện!** 🚀

# Chapter 13: Tiêu chuẩn Tích hợp Hệ thống (Integration Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi kết nối Backend (ASP.NET Core) và Frontend (Vue.js) trong cùng một Solution. Đây là căn cứ để Audit cấu hình triển khai (Deployment Configuration).

---

## 1. Tiêu chuẩn SPA Hosting (SPA Middleware Standard)

Dự án sử dụng cơ chế tích hợp để chạy cả hai thành phần trên cùng một Port vật lý trong môi trường Development.

- **Middleware:** Sử dụng `VueCliMiddleware` để tự động hóa quy trình khởi động.
- **Source Path:** Đường dẫn đến dự án Frontend phải được cấu hình chính xác trong `Startup.cs`.
- **Production Build:** Khi ở môi trường Production, ASP.NET Core phải phục vụ các file tĩnh từ thư mục `dist` của Vue.

**Mẫu Audit chuẩn:**
```csharp
app.UseSpa(spa => {
    spa.Options.SourcePath = "../vue-app";
    if (env.IsDevelopment()) {
        spa.UseVueCli(npmScript: "serve");
    }
});
```

---

## 2. Tiêu chuẩn CORS (Cross-Origin Policy)

Dù chạy chung Port, việc cấu hình CORS vẫn là bắt buộc để cho phép các yêu cầu từ các Origin khác nhau (nếu có) trong tương lai.

- **Quy tắc Audit:**
    - **Development:** Có thể cho phép `AllowAnyOrigin`.
    - **Production:** Chỉ được phép cho phép các Domain cụ thể (Whitelisting).
    - **Methods:** Chỉ cho phép các phương thức cần thiết (`GET`, `POST`, `PUT`, `DELETE`).

---

## 3. Quản lý biến môi trường (Environment Variables)

- **Frontend:** API Base URL phải được lưu trong file `.env.development` và `.env.production`.
- **CẤM:** Viết cứng địa chỉ IP hoặc Localhost của Backend vào trong mã nguồn Vue.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có thể truy cập giao diện Vue thông qua Port của Backend không? (Nếu không -> **FAILED** - Kiểm tra `UseSpa` setup).
- [ ] **Check 2:** Cấu hình CORS trong Production có đang để `AllowAnyOrigin (*)` không? (Nếu có -> **FAILED** - Rủi ro bảo mật nghiêm trọng).
- [ ] **Check 3:** Có file `.env` chứa `VUE_APP_API_URL` không? (Nếu không -> **FAILED**).
- [ ] **Check 4:** Khi chạy `npm run build`, các file đầu ra có nằm đúng thư mục mà `AddSpaStaticFiles` đã khai báo không? (Nếu không -> **FAILED**).

---

## 5. Tiêu chuẩn Proxy (Development Proxy)

Trong môi trường Dev, mọi yêu cầu không phải API từ trình duyệt sẽ được Proxy sang Vue Dev Server. Điều này giúp tận hưởng tính năng **Hot Module Replacement (HMR)** mà không gặp lỗi đường dẫn.

---

> [!IMPORTANT]
> Việc tích hợp sai sẽ dẫn đến lỗi 404 cho các file CSS/JS của Vue khi triển khai lên Cloud. Luôn kiểm tra kỹ `spa.Options.SourcePath`.

[Tiếp theo: Tiêu chuẩn Quản lý trạng thái với Vuex](./Chapter 14 - Simplifying State Management with Vuex and Sending GET HTTP Requests.md)

---

## Tiếp theo: Chương 14 - Quản lý State với Vuex
Chúng ta sẽ học cách quản lý dữ liệu tập trung (Global State) cho toàn bộ ứng dụng Vue.js, giúp việc chia sẻ dữ liệu giữa các trang trở nên dễ dàng hơn bao giờ hết.

[Bắt đầu Chương 14](./Chapter 14 - Simplifying State Management with Vuex and Sending GET HTTP Requests.md)

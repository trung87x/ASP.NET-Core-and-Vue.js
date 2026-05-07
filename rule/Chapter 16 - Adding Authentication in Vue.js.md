# Chapter 16: Tiêu chuẩn Xác thực Frontend (Frontend Security Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc về bảo mật và xác thực người dùng trong ứng dụng Vue.js. Đây là căn cứ để Audit tính an toàn của dữ liệu phía Client.

---

## 1. Tiêu chuẩn Lưu trữ Token (Token Storage Standard)

- **Vị trí lưu trữ:** Ưu tiên sử dụng `LocalStorage` hoặc `SessionStorage` (Nếu yêu cầu bảo mật cao hơn, hãy cân nhắc `HttpOnly Cookies` từ Backend).
- **Quy tắc:** Khi người dùng nhấn "Đăng xuất" (Logout), toàn bộ Token và thông tin nhạy cảm trong cả Vuex và LocalStorage phải bị xóa sạch ngay lập tức.

---

## 2. Tiêu chuẩn Axios Interceptors (Request/Response Standard)

Dự án bắt buộc triển khai cơ chế kiểm soát tự động:

1.  **Request Interceptor:** Tự động đính kèm `Authorization: Bearer <token>` vào Header của mọi yêu cầu gửi đi nếu Token tồn tại.
2.  **Response Interceptor:** Phải bắt được lỗi `401 Unauthorized`. Khi gặp lỗi này, hệ thống phải tự động điều hướng người dùng về trang Login và xóa các thông tin đăng nhập cũ.

---

## 3. Tiêu chuẩn Bảo vệ Route (Navigation Guard Standard)

Tuyệt đối không để các trang nhạy cảm lộ diện khi chưa xác thực:

- **Global Guard:** Sử dụng `router.beforeEach` để kiểm tra trạng thái đăng nhập dựa trên sự tồn tại của Token.
- **Route Meta:** Sử dụng thuộc tính `meta: { requiresAuth: true }` trong cấu hình Route để đánh dấu các trang cần bảo vệ.

**Mẫu Audit chuẩn:**
```javascript
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!store.getters['auth/isAuthenticated']) {
      next('/login');
      return;
    }
  }
  next();
});
```

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có trang quản trị nào (ví dụ: `/admin`) có thể truy cập được bằng cách gõ trực tiếp URL khi chưa đăng nhập không? (Nếu có -> **FAILED**).
- [ ] **Check 2:** Khi Token hết hạn, hệ thống có tự động Logout không hay vẫn cho phép người dùng ở lại trang hiện tại? (Nếu không Logout -> **WARNING**).
- [ ] **Check 3:** Có đoạn code nào lấy Token thủ công trong từng Action thay vì dùng Interceptor không? (Nếu có -> **FAILED** - Vi phạm tính DRY và dễ sai sót).
- [ ] **Check 4:** Thông tin nhạy cảm (như mật khẩu) có bị lưu vào LocalStorage không? (Nếu có -> **FAILED** - Chỉ được lưu Token).

---

## 5. UI/UX Authentication

- Khi chưa đăng nhập, thanh điều hướng (Navbar) phải ẩn các liên kết đến trang nội bộ và hiển thị nút "Login".
- Khi đã đăng nhập, phải ẩn nút "Login" và hiển thị nút "Logout" cùng thông tin người dùng.

---

> [!CAUTION]
> Bảo mật Frontend chỉ là lớp vỏ ngoài để tăng trải nghiệm người dùng. Việc bảo mật thực sự phải luôn được thực hiện nghiêm ngặt ở lớp Backend (Chương 9).

[Tiếp theo: Tiêu chuẩn Kiểm tra dữ liệu Form](./Chapter 17 - Input Validations in Forms.md)

---

## Tiếp theo: Chương 17 - Kiểm tra dữ liệu đầu vào (Input Validations)
Chúng ta sẽ học cách sử dụng các thư viện Validation để đảm bảo người dùng nhập đúng định dạng dữ liệu (Email, Mật khẩu mạnh, Số lượng...) trước khi gửi lên Server.

[Bắt đầu Chương 17](./Chapter 17 - Input Validations in Forms.md)

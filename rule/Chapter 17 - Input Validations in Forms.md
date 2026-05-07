# Chapter 17: Tiêu chuẩn Kiểm tra Dữ liệu (Validation Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi thực hiện kiểm tra dữ liệu đầu vào (Validation) tại Frontend. Đây là căn cứ để Audit chất lượng biểu mẫu (Form Quality) và trải nghiệm người dùng.

---

## 1. Tiêu chuẩn Thư viện (Validation Tool Standard)

Dự án sử dụng **Vuelidate** làm công cụ kiểm tra dữ liệu chính cho Vue.js.

- **Model-based:** Mọi quy tắc kiểm tra phải được định nghĩa dựa trên cấu trúc dữ liệu gửi lên (Request Body).
- **Decoupling:** Tuyệt đối không viết logic kiểm tra phức tạp trực tiếp trong thẻ HTML của Template.

---

## 2. Quy tắc phản hồi giao diện (UI Feedback Rules)

- **Trạng thái $dirty:** Thông báo lỗi chỉ được hiển thị sau khi người dùng đã "chạm" (`$touch()`) vào ô nhập liệu (thường là sự kiện `@blur` hoặc sau khi nhấn Submit). Điều này tránh việc người dùng thấy màu đỏ ngay khi vừa mở Form.
- **Vị trí hiển thị:** Lỗi phải hiển thị ngay dưới ô nhập liệu tương ứng. Sử dụng thuộc tính `error-messages` của Vuetify để đồng bộ giao diện.

---

## 3. Quy tắc Submit (Submit Enforcement)

- **Touch All:** Trước khi gọi API, hàm Submit phải gọi `this.$v.$touch()` để kích hoạt kiểm tra cho toàn bộ các trường trong Form.
- **Strict Block:** Tuyệt đối không được gọi API nếu thuộc tính `this.$v.$invalid` hoặc `this.$v.$anyError` là `true`.

**Mẫu Audit chuẩn:**
```javascript
saveData() {
  this.$v.$touch();
  if (this.$v.$invalid) return; // Block API call
  // ... proceed to dispatch action
}
```

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có Form nào cho phép nhấn nút "Save/Submit" mà không kiểm tra tính hợp lệ không? (Nếu có -> **FAILED**).
- [ ] **Check 2:** Thông báo lỗi có được Việt hóa (hoặc theo ngôn ngữ dự án) rõ ràng không? (Ví dụ: "Trường này là bắt buộc" thay vì "Field is required" - Nếu không -> **WARNING**).
- [ ] **Check 3:** Có sử dụng `@blur` để kích hoạt `$touch()` không? (Nếu không -> **WARNING** - Trải nghiệm người dùng sẽ kém hơn).
- [ ] **Check 4:** Các quy tắc độ dài (MaxLength/MinLength) ở Frontend có khớp với định nghĩa ở Backend không? (Nếu không -> **FAILED** - Sẽ gây lỗi 400 khi gửi data).

---

## 5. Quy tắc Kiểm tra chéo (Cross-Field Validation)

Đối với các trường phụ thuộc nhau (ví dụ: "Mật khẩu" và "Xác nhận mật khẩu"), phải sử dụng hàm validator tùy chỉnh (custom validator) để so sánh giá trị trực tiếp trên Store hoặc Component State.

---

> [!IMPORTANT]
> Validation ở Frontend chỉ phục vụ cho trải nghiệm người dùng (UX). Mọi dữ liệu vẫn **BẮT BUỘC** phải được kiểm tra lại một lần nữa ở lớp Application của Backend (FluentValidation) để đảm bảo an toàn tuyệt đối.

[Tiếp theo: Tiêu chuẩn Kiểm thử Tích hợp](./Chapter 18 - Writing Integration.md)

---

## Tiếp theo: Chương 18 - Viết bài kiểm tra tích hợp (Integration Tests)
Chúng ta sẽ học cách viết các bài test tự động để đảm bảo rằng sau khi chúng ta sửa đổi code, các tính năng cũ vẫn hoạt động bình thường.

[Bắt đầu Chương 18](./Chapter 18 - Writing Integration.md)

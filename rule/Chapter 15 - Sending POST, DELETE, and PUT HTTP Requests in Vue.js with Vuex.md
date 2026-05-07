# Chapter 15: Tiêu chuẩn Thao tác Dữ liệu (Data Persistence Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi thực hiện các hành động Thêm, Sửa, Xóa (CRUD) từ giao diện Vue.js đến API. Đây là căn cứ để Audit tính toàn vẹn dữ liệu (Data Integrity) trên giao diện.

---

## 1. Tiêu chuẩn Giao tiếp CRUD (CRUD Communication Standard)

Mọi yêu cầu thay đổi dữ liệu phải tuân thủ đúng phương thức HTTP:

- **POST:** Tạo mới. Phải gửi dữ liệu trong Body. Mong đợi trả về ID của đối tượng mới hoặc toàn bộ đối tượng.
- **PUT:** Cập nhật toàn diện. URL phải chứa ID: `/api/resource/{id}`.
- **PATCH:** Cập nhật một phần (Nếu Backend hỗ trợ).
- **DELETE:** Xóa. URL phải chứa ID.

---

## 2. Tiêu chuẩn Đồng bộ trạng thái (State Sync Standard)

Dự án áp dụng cơ chế **Pessimistic UI Update** (Cập nhật giao diện sau khi Backend xác nhận thành công):

1.  **Gửi yêu cầu:** Gọi API thông qua Vuex Action.
2.  **Đợi phản hồi:** Chờ Backend trả về mã thành công (200, 201, 204).
3.  **Cập nhật State:** Chỉ sau khi có phản hồi thành công, mới gọi Mutation để cập nhật Vuex State.
4.  **Xử lý lỗi:** Nếu API thất bại, phải thông báo lỗi cho người dùng và KHÔNG được thay đổi State.

---

## 3. Quản lý trạng thái tải (Loading State)

- Mọi hành động thay đổi dữ liệu phải có biến `isLoading` hoặc `isSubmitting` để vô hiệu hóa (disable) các nút bấm, tránh việc người dùng nhấn liên tiếp nhiều lần gây trùng lặp yêu cầu.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có hành động DELETE nào được thực hiện mà không có hộp thoại xác nhận (Confirm Dialog) không? (Nếu không -> **FAILED** - Rủi ro người dùng nhấn nhầm).
- [ ] **Check 2:** Sau khi thêm mới thành công, dữ liệu có hiển thị ngay lập tức mà không cần F5 trang không? (Nếu không -> **FAILED** - Kiểm tra việc commit Mutation).
- [ ] **Check 3:** Có sử dụng `try-catch` để bắt lỗi API và hiển thị thông báo (Toast/Alert) cho người dùng không? (Nếu không -> **FAILED**).
- [ ] **Check 4:** Các nút bấm có được Disable khi đang chờ API phản hồi không? (Nếu không -> **WARNING**).

---

## 5. Tiêu chuẩn xử lý ID

- Khi thêm mới thành công, ID thực tế từ Database (Backend trả về) phải được gán lại vào đối tượng ở Frontend trước khi lưu vào State. Tuyệt đối không tự sinh ID tạm thời ở Frontend để lưu lâu dài.

---

> [!CAUTION]
> Tuyệt đối không thực hiện các vòng lặp gọi API (ví dụ: gọi 10 API xóa trong vòng lặp `for`). Hãy yêu cầu Backend cung cấp API xóa hàng loạt (Bulk Delete) để tối ưu hiệu năng.

[Tiếp theo: Tiêu chuẩn Xác thực người dùng Frontend](./Chapter 16 - Adding Authentication in Vue.js.md)

---

## Tiếp theo: Chương 16 - Thêm xác thực (Authentication) trong Vue.js
Chúng ta sẽ học cách bảo vệ các trang web của mình, chỉ cho phép người dùng đã đăng nhập mới có thể truy cập vào Dashboard.

[Bắt đầu Chương 16](./Chapter 16 - Adding Authentication in Vue.js.md)

# Chapter 12: Tiêu chuẩn UI Library và Routing (UI & Navigation Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi sử dụng thư viện giao diện và thiết lập điều hướng trong dự án Travel App. Đây là căn cứ để Audit trải nghiệm người dùng (UX) và cấu trúc ứng dụng.

---

## 1. Tiêu chuẩn UI Framework (Vuetify Standard)

Dự án sử dụng **Vuetify** làm thư viện UI chính dựa trên Material Design.

- **Grid System:** Phải sử dụng `<v-container>`, `<v-row>`, `<v-col>` để xây dựng bố cục. Tuyệt đối hạn chế dùng CSS Flexbox/Grid thủ công trừ khi Vuetify không đáp ứng được.
- **Components:** Ưu tiên sử dụng các Component có sẵn của Vuetify (như `v-btn`, `v-card`, `v-text-field`) để đảm bảo tính đồng nhất về giao diện.

---

## 2. Tiêu chuẩn Điều hướng (Vue Router Standard)

Ứng dụng phải hoạt động theo mô hình SPA (Single Page Application).

- **Mode:** Phải sử dụng `mode: 'history'` để URL sạch (không có dấu `#`).
- **Lazy Loading:** Mọi Route cấp 1 (Main pages) phải được tải theo phương thức Lazy Loading để tối ưu tốc độ tải trang đầu tiên.
- **Naming:** Mọi Route phải có thuộc tính `name` duy nhất (PascalCase) để phục vụ việc điều hướng bằng tên.

**Mẫu Audit chuẩn:**
```javascript
{
  path: "/admin",
  name: "AdminDashboard",
  component: () => import("@/views/Admin.vue")
}
```

---

## 3. Cấu trúc Routing lồng nhau (Nested Routes Rule)

Đối với các khu vực Dashboard hoặc Admin, bắt buộc sử dụng **Nested Routes** để quản lý Layout dùng chung:

- Component cha phải chứa thẻ `<router-view />` để hiển thị các Component con.
- URL của Component con phải bắt đầu bằng đường dẫn của Component cha.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có Route nào không sử dụng `import()` (Lazy Loading) không? (Nếu có -> **WARNING** - Có thể làm tăng bundle size).
- [ ] **Check 2:** Các liên kết chuyển trang có dùng thẻ `<a>` thay vì `<router-link>` hoặc `this.$router.push()` không? (Nếu có -> **FAILED** - Sẽ làm tải lại toàn bộ trang).
- [ ] **Check 3:** Có Route nào thiếu thuộc tính `name` không? (Nếu có -> **WARNING**).
- [ ] **Check 4:** Giao diện có bị vỡ khi thu nhỏ màn hình không? (Kiểm tra việc sử dụng Vuetify Grid - Nếu vỡ -> **FAILED**).

---

## 5. Navigation Guards (Bảo mật Route)

Mọi trang yêu cầu đăng nhập (ví dụ: `/admin`) phải được bảo vệ bằng Navigation Guard trong tệp `router/index.ts`. Tuyệt đối không để người dùng chưa đăng nhập nhìn thấy giao diện Admin.

---

> [!TIP]
> Việc sử dụng tên Route (`name`) thay vì đường dẫn (`path`) khi chuyển trang giúp bạn có thể thay đổi cấu trúc URL sau này mà không cần phải đi sửa code ở khắp mọi nơi.

[Tiếp theo: Tiêu chuẩn Tích hợp Frontend & Backend](./Chapter 13 - Integrating a Vue.js Application with ASP.NET Core.md)

---

## Tiếp theo: Chương 13 - Kết nối Vue.js với ASP.NET Core
Chúng ta sẽ học cách gọi các API Backend mà chúng ta đã xây dựng từ Chương 4-10 ngay trong ứng dụng Vue.js.

[Bắt đầu Chương 13](./Chapter 13 - Integrating a Vue.js Application with ASP.NET Core.md)

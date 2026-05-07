# Chapter 11: Tiêu chuẩn Lập trình Vue.js (Frontend Coding Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi phát triển giao diện người dùng bằng **Vue 3** và **TypeScript** cho dự án Travel App. Đây là căn cứ để Audit tính nhất quán của mã nguồn Frontend.

---

## 1. Tiêu chuẩn Component (Component Structure Standard)

Mọi tệp `.vue` phải tuân thủ cấu trúc 3 phần rõ rệt và sử dụng **Composition API**:

1.  **Script:** Sử dụng `lang="ts"` và `setup()` hoặc `<script setup>`.
2.  **Template:** Chỉ chứa mã HTML và các chỉ thị Vue. Tuyệt đối không viết logic tính toán phức tạp trong Template.
3.  **Style:** Phải sử dụng thuộc tính `scoped` để tránh xung đột CSS toàn cục.

**Quy tắc Audit:**
- Cấm sử dụng **Options API** (data, methods, computed truyền thống) trừ khi có lý do đặc biệt.
- Mọi biến phản xạ (Reactivity) phải được khai báo rõ ràng bằng `ref()` hoặc `reactive()`.

---

## 2. Tiêu chuẩn TypeScript (TypeScript Enforcement)

Dự án yêu cầu tính chặt chẽ về kiểu dữ liệu (Strict Typing):

- **CẤM:** Sử dụng kiểu `any`. Mọi biến và tham số hàm phải có Type hoặc Interface rõ ràng.
- **Interfaces:** Các DTO nhận từ Backend phải được định nghĩa lại thành Interface ở Frontend (thường đặt trong thư mục `src/types` hoặc `src/models`).

---

## 3. Giao tiếp giữa các Component (Component Communication)

Tuân thủ nghiêm ngặt mô hình: **Props Down, Events Up**.

- **Props:** Phải được định nghĩa rõ ràng về kiểu dữ liệu và giá trị mặc định.
- **Emits:** Phải khai báo danh sách các sự kiện mà Component có thể phát ra bằng `defineEmits`.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có tệp `.vue` nào thiếu thuộc tính `scoped` trong thẻ `<style>` không? (Nếu có -> **FAILED** - Trừ file `App.vue` hoặc global styles).
- [ ] **Check 2:** Có sử dụng `.value` khi truy cập `ref` bên trong phần `<script>` không? (Nếu thiếu -> **FAILED** - Code sẽ không chạy đúng).
- [ ] **Check 3:** Các Component có tên tệp theo quy tắc **PascalCase** không? (Ví dụ: `TourCard.vue` - Nếu không -> **WARNING**).
- [ ] **Check 4:** Có viết logic xử lý dữ liệu (ví dụ: tính tổng tiền) ngay trong thẻ `{{ }}` ở Template không? (Nên đưa vào `computed` - Nếu có -> **WARNING**).

---

## 5. Tiêu chuẩn tổ chức thư mục Frontend

```text
src/
├── api/          # Các hàm gọi Axios
├── components/   # Các UI component dùng chung
├── views/        # Các component đại diện cho một trang
├── store/        # Quản lý trạng thái (Vuex)
└── types/        # Định nghĩa Interfaces/Types
```

---

> [!IMPORTANT]
> Việc bỏ qua `lang="ts"` trong các Component sẽ làm mất đi khả năng kiểm tra lỗi của trình biên dịch, dẫn đến các lỗi tiềm ẩn khó phát hiện trên trình duyệt.

[Tiếp theo: Tiêu chuẩn UI Library và Routing](./Chapter 12 - Using a UI Component Library and Creating Routes and Navigations.md)

---

## Tiếp theo: Chương 12 - UI Component Library và Routing
Chúng ta sẽ học cách sử dụng các thư viện UI có sẵn để làm giao diện đẹp hơn và cách tạo nhiều trang (Routing) cho ứng dụng.

[Bắt đầu Chương 12](./Chapter 12 - Using a UI Component Library and Creating Routes and Navigations.md)

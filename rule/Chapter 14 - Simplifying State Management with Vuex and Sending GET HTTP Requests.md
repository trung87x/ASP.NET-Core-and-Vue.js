# Chapter 14: Tiêu chuẩn Quản lý Trạng thái (State Management Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi sử dụng **Vuex** để quản lý trạng thái toàn cục và luồng dữ liệu từ Backend. Đây là căn cứ để Audit tính dễ bảo trì (Maintainability) của Frontend.

---

## 1. Nguyên tắc "Single Source of Truth" (SSoT Rule)

Mọi dữ liệu được chia sẻ giữa nhiều Component hoặc cần được lưu giữ khi chuyển trang phải nằm trong Vuex Store.

- **CẤM:** Lưu trữ trạng thái toàn cục trong các biến Global Javascript hoặc `provide/inject` không kiểm soát.
- **QUY TẮC:** Sử dụng **Vuex Modules** để phân chia không gian lưu trữ theo nghiệp vụ (ví dụ: `tour`, `auth`, `user`).

---

## 2. Tiêu chuẩn Luồng dữ liệu (Data Flow Standard)

Tuân thủ nghiêm ngặt luồng dữ liệu một chiều:

1.  **Actions:** Chứa logic bất đồng bộ (API calls via Axios).
2.  **Mutations:** Chứa logic thay đổi State duy nhất. Phải là hàm thuần khiết (Pure function) và đồng bộ.
3.  **State:** Lưu trữ dữ liệu thô.
4.  **Getters:** Tính toán/Lọc dữ liệu từ State để cung cấp cho Component.

---

## 3. Quy tắc Đặt tên (Naming Convention)

- **Mutations:** Phải viết hoa toàn bộ, cách nhau bằng dấu gạch dưới (Ví dụ: `SET_TOUR_LISTS`, `LOADING_START`).
- **Actions:** Viết theo kiểu camelCase, có hậu tố `Action` (Ví dụ: `getTourListsAction`).
- **Types:** Sử dụng một file `mutation-types.ts` dùng chung để tránh viết sai tên Mutation.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

### 📜 Tiêu chuẩn kỹ thuật
1.  **Hierarchical Data:** Khi lấy dữ liệu cha (ví dụ TourList), bắt buộc phải đi kèm (Include) dữ liệu con (TourPackage) nếu giao diện yêu cầu hiển thị đồng thời.
2.  **DTO Projection:** Sử dụng `.Select()` hoặc AutoMapper `ProjectTo` để chỉ lấy các trường cần thiết, tránh lấy dư thừa dữ liệu (Over-fetching).

### 🔍 Audit Checklist
*   **Check 1:** Yêu cầu GET có trả về dữ liệu phân cấp (Cha kèm Con) không?
*   **Check 2:** Có sử dụng DTO thay vì trả về Entity nguyên bản không?
*   **Check 3:** Có sử dụng cơ chế Async (`ToListAsync`) để không làm nghẽn luồng xử lý không?
 (Nếu không -> **FAILED**).
- [ ] **Check 3:** Có sử dụng `mapState` hoặc `mapGetters` để rút gọn mã nguồn không? (Nên dùng - Nếu không -> **WARNING**).
- [ ] **Check 4:** File Store có quá 300 dòng không? (Nếu có -> **WARNING** - Cần tách ra thành các Module nhỏ hơn).

---

## 5. Tiêu chuẩn Axios (HTTP Client Standard)

- Phải cấu hình một instance Axios dùng chung với `baseURL` và `interceptors` để tự động đính kèm JWT Token vào Header của mọi yêu cầu.

---

> [!IMPORTANT]
> Việc không sử dụng Actions cho các tác vụ bất đồng bộ sẽ khiến việc theo dõi sự thay đổi của State trở nên cực kỳ khó khăn, gây ra các lỗi "ma" không thể tái hiện được.

[Tiếp theo: Tiêu chuẩn Thao tác dữ liệu POST, PUT, DELETE](./Chapter 15 - Sending POST, DELETE, and PUT HTTP Requests in Vue.js with Vuex.md)

---

## Tiếp theo: Chương 15 - Gửi yêu cầu POST, DELETE và PUT
Chúng ta sẽ hoàn thiện các chức năng Thêm, Xóa và Cập nhật dữ liệu để ứng dụng của bạn trở thành một hệ thống quản lý hoàn chỉnh.

[Bắt đầu Chương 15](./Chapter 15 - Sending POST, DELETE, and PUT HTTP Requests in Vue.js with Vuex.md)

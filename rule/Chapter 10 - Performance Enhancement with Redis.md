# Chapter 10: Tiêu chuẩn Tối ưu Hiệu năng với Redis (Performance Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi sử dụng bộ nhớ đệm (Caching) để tối ưu hóa tốc độ phản hồi của dự án Travel App. Đây là căn cứ để Audit hiệu năng hệ thống.

---

## 1. Tiêu chuẩn sử dụng Cache (Caching Policy)

Dự án sử dụng chiến lược **Cache-Aside Pattern**. Tuyệt đối không để ứng dụng phụ thuộc hoàn toàn vào Cache (Cache phải là thành phần bổ trợ, nếu Cache chết hệ thống vẫn phải hoạt động được từ DB).

- **Công cụ:** Chỉ sử dụng **Redis** thông qua giao diện `IDistributedCache`.
- **Dữ liệu được phép Cache:** Danh sách Tour, Thông tin thời tiết, Cấu hình hệ thống (Dữ liệu ít thay đổi nhưng tần suất đọc cao).

---

## 2. Quy tắc hết hạn (Expiration Rules)

Để tránh hiện tượng dữ liệu bị cũ (**Stale Data**), mọi mục nhập trong Cache phải có thời gian hết hạn:

1.  **Absolute Expiration:** Bắt buộc cho mọi Cache entry. Khuyến nghị: 5-10 phút.
2.  **Sliding Expiration:** Sử dụng cho các dữ liệu mang tính phiên làm việc (Session-like). Khuyến nghị: 2 phút.
3.  **Bust Cache:** Khi có hành động thay đổi dữ liệu (Command), Handler phải chủ động xóa các Key liên quan trong Redis.

---

## 3. Tiêu chuẩn đặt Key (Key Naming Convention)

Key trong Redis phải tuân thủ định dạng để tránh xung đột dữ liệu:
`[AppName]:[Entity]:[Action]:[OptionalParams]`

**Ví dụ:** `Travel:TourLists:GetAll` hoặc `Travel:TourLists:GetById:10`.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có sử dụng `IMemoryCache` thay vì `IDistributedCache` không? (Nếu có -> **FAILED** - Vì MemoryCache không chia sẻ được giữa các server khi Scale-out).
- [ ] **Check 2:** Các dữ liệu nhạy cảm (như thông tin thanh toán) có bị lưu vào Redis không? (Nếu có -> **FAILED**).
- [ ] **Check 3:** Có xử lý `try-catch` khi kết nối Redis thất bại không? (Hệ thống phải tự động fallback về Database - Nếu không -> **WARNING**).
- [ ] **Check 4:** Key Redis có tiền tố (Prefix) để phân biệt giữa các môi trường Dev/Staging không? (Nếu không -> **WARNING**).

---

## 5. Serialization Standard

Dữ liệu lưu vào Redis phải được Serialize dưới dạng **JSON** hoặc **MessagePack** để tiết kiệm dung lượng và dễ dàng kiểm tra nội dung khi cần thiết.

---

> [!TIP]
> Một Key Redis quá lớn (vài MB) sẽ làm giảm hiệu năng hệ thống. Luôn kiểm tra kích thước dữ liệu trước khi quyết định đưa vào Cache.

[Tiếp theo: Tiêu chuẩn Vue.js Căn bản](./Chapter 11 - Vue.js Fundamentals in a Todo App.md)

---

## Tiếp theo: Chương 11 - Vue.js Fundamentals
Chúng ta sẽ bắt đầu xây dựng giao diện người dùng chuyên nghiệp để kết nối với hệ thống Backend mạnh mẽ mà chúng ta vừa hoàn thành.

[Bắt đầu Chương 11](./Chapter 11 - Vue.js Fundamentals in a Todo App.md)

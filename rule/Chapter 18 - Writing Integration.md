# Chapter 18: Tiêu chuẩn Kiểm thử Tích hợp (Testing Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi viết bài kiểm tra tự động (Automated Testing) trong dự án Travel App. Đây là căn cứ để Audit độ tin cậy (Reliability) của mã nguồn.

---

## 1. Phân loại Kiểm thử (Testing Hierarchy)

Dự án ưu tiên tập trung vào **Integration Tests** (Kiểm thử tích hợp) để đảm bảo các lớp trong Clean Architecture phối hợp chính xác với Database thật (phiên bản Test).

- **Framework:** Sử dụng **xUnit**.
- **Assertion:** Sử dụng **FluentAssertions** cho mọi câu lệnh kiểm tra.
- **Isolation:** Mỗi bài test phải chạy độc lập. Sử dụng cơ chế `ResetState` để dọn dẹp Database sau mỗi lần chạy.

---

## 2. Tiêu chuẩn Cấu trúc bài Test (Test Structure Standard)

Mọi bài test phải tuân thủ nghiêm ngặt quy trình **AAA (Arrange - Act - Assert)**:

1.  **Arrange:** Khởi tạo dữ liệu mẫu, User ảo, và các Dependency cần thiết.
2.  **Act:** Thực hiện hành động nghiệp vụ (thông qua Mediator hoặc API client).
3.  **Assert:** Xác nhận kết quả trả về và trạng thái cuối cùng của Database.

---

## 3. Quy tắc Đặt tên (Naming Convention for Tests)

Tên hàm test phải mô tả rõ ràng: `[Tên_Hành_Động]_[Điều_Kiện]_[Kết_Quả_Mong_Đợi]`

**Ví dụ:** `CreateTourList_WithValidData_ShouldReturnNewId` hoặc `DeleteTourList_WithInvalidId_ShouldThrowNotFoundException`.

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Các bài test có đang dùng Database Production không? (Nếu có -> **FAILED** - Tuyệt đối không được làm bẩn dữ liệu thật).
- [ ] **Check 2:** Tỷ lệ bao phủ (Test Coverage) có đạt tối thiểu 70% các Use Cases quan trọng ở tầng Application không? (Nếu không -> **WARNING**).
- [ ] **Check 3:** Có bài test nào phụ thuộc vào kết quả của bài test khác không? (Nếu có -> **FAILED** - Vi phạm tính độc lập).
- [ ] **Check 4:** Các bài test có kiểm tra cả trường hợp lỗi (Negative Testing) không? (Nếu không -> **WARNING**).

---

## 5. Tiêu chuẩn Cơ sở dữ liệu Test

Sử dụng Database thực (như SQL Server hoặc SQLite) trong quá trình test thay vì dùng `InMemoryDatabase` của EF Core, để đảm bảo các ràng buộc dữ liệu (Foreign Keys, Triggers) được kiểm tra chính xác như môi trường thật.

---

> [!TIP]
> Một bài test thất bại là một tin vui, vì nó giúp bạn phát hiện lỗi trước khi người dùng phát hiện ra. Hãy coi việc viết test là một phần không thể tách rời của quá trình viết code.

[Tiếp theo: Tiêu chuẩn Triển khai tự động CI/CD](./Chapter 19 - Automatic Deployment Using GitHub Actions and Azure.md)

---

## Tiếp theo: Chương 19 - Triển khai tự động (Automatic Deployment)
Chúng ta sẽ học cách đưa ứng dụng của mình lên Internet bằng **GitHub Actions** và **Azure**, giúp quá trình phát hành sản phẩm trở nên hoàn toàn tự động.

[Bắt đầu Chương 19](./Chapter 19 - Automatic Deployment Using GitHub Actions and Azure.md)

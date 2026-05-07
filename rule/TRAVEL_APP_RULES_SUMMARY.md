# Travel App: Hệ thống Tiêu chuẩn Kỹ thuật Toàn diện (Master Standard & Audit Index)

Tài liệu này tổng hợp toàn bộ các tiêu chuẩn kỹ thuật bắt buộc cho hệ thống Full-stack (ASP.NET Core & Vue.js). Đây là tài liệu gốc để đối chiếu (Mapping) với các Audit Checklist chi tiết trong từng chương.

---

## 🏛️ Trụ cột 1: Kiến trúc và Quản lý Dữ liệu (Backend Core)

### 1.1. Clean Architecture (Chương 2)
- **Vấn đề:** Mã nguồn bị trộn lẫn logic, gây "Technical Debt" cực lớn.
- **Giải pháp:** Phân lớp Onion Architecture (Domain -> Application -> Infrastructure -> WebAPI).
- **Tiêu chuẩn:** Phụ thuộc một chiều vào lõi Domain.
- **Audit:** Kiểm tra tham chiếu chéo giữa các Project (Cấm Infrastructure gọi WebAPI).

### 1.2. Domain & Entities (Chương 3)
- **Vấn đề:** Business Logic bị phân tán, Entity không nhất quán.
- **Giải pháp:** Sử dụng Domain Layer tinh khiết, khởi tạo Collection mặc định.
- **Tiêu chuẩn:** Mọi Entity phải kế thừa `AuditableEntity`.
- **Audit:** Kiểm tra khởi tạo `List` trong Constructor để tránh NullReferenceException.

### 1.3. CQRS & MediatR (Chương 7)
- **Vấn đề:** Fat Controllers và Service Layer quá tải.
- **Giải pháp:** Tách biệt luồng Đọc (Query) và Ghi (Command).
- **Tiêu chuẩn:** Controller chỉ chứa logic điều phối qua `IMediator`.
- **Audit:** Kiểm tra độ dày của Controller (Không quá 5 dòng logic thực thi).

---

## 🚀 Trụ cột 2: Vận hành và Hiệu năng (Operational Standards)

### 2.1. API Versioning & Logging (Chương 8)
- **Vấn đề:** Không thể truy vết lỗi và mất tính tương thích ngược.
- **Giải pháp:** Sử dụng URL Versioning (`v1/v2`) và Serilog (JSON format).
- **Tiêu chuẩn:** Mọi Exception phải được ghi log với đầy đủ Context.
- **Audit:** Kiểm tra cấu hình `Serilog` trong `appsettings.json`.

### 2.2. JWT Security (Chương 9)
- **Vấn đề:** Rò rỉ thông tin xác thực và tấn công CSRF.
- **Giải pháp:** Stateless JWT với Secret Management nghiêm ngặt.
- **Tiêu chuẩn:** Secret Key không được nằm trong mã nguồn.
- **Audit:** Kiểm tra sự tồn tại của `User Secrets` hoặc Environment Variables.

### 2.3. Redis Performance (Chương 10)
- **Vấn đề:** Nghẽn cổ chai Database khi tải cao.
- **Giải pháp:** Cache-Aside Pattern với Redis.
- **Tiêu chuẩn:** Phải có cơ chế tự động xóa Cache khi dữ liệu thay đổi.
- **Audit:** Kiểm tra Key Naming Convention (`Travel:[Entity]:[ID]`).

---

## 🎨 Trụ cột 3: Giao diện và Trải nghiệm (Frontend Quality)

### 3.1. Vue 3 & TypeScript (Chương 11)
- **Vấn đề:** Lỗi Runtime ở Frontend và code khó tái sử dụng.
- **Giải pháp:** Composition API + Strict TypeScript.
- **Tiêu chuẩn:** 100% Component phải có `lang="ts"`.
- **Audit:** Tìm kiếm từ khóa `any` (Cấm sử dụng).

### 3.2. State Management (Chương 14)
- **Vấn đề:** Prop Drilling và dữ liệu không đồng nhất giữa các trang.
- **Giải pháp:** Vuex Modules với luồng dữ liệu một chiều.
- **Tiêu chuẩn:** Chỉ Mutation mới được phép thay đổi State.
- **Audit:** Kiểm tra sự hiện diện của `Actions` cho mọi yêu cầu API.

### 3.3. Input Validation (Chương 17)
- **Vấn đề:** Rác dữ liệu làm tăng tải cho Backend.
- **Giải pháp:** Model-based Validation với Vuelidate.
- **Tiêu chuẩn:** Nút Submit phải được khóa (Disabled) khi Form không hợp lệ.
- **Audit:** Kiểm tra sự kiện `@blur` kích hoạt `$touch()`.

---

## 🛡️ Trụ cột 4: Chất lượng và Phát hành (QA & DevOps)

### 4.1. Integration Testing (Chương 18)
- **Vấn đề:** Sợ hãi khi Refactor code.
- **Giải pháp:** Automated Tests với xUnit và FluentAssertions.
- **Tiêu chuẩn:** Mỗi bài test phải chạy trên Database sạch (ResetState).
- **Audit:** Kiểm tra tỷ lệ bao phủ Use Cases.

### 4.2. CI/CD (Chương 19)
- **Vấn đề:** Sai sót do thao tác triển khai thủ công.
- **Giải pháp:** GitHub Actions tự động hóa Build-Test-Deploy.
- **Tiêu chuẩn:** Chỉ Deploy khi toàn bộ bài Test chuyển màu xanh (Green).
- **Audit:** Kiểm tra file `.yml` cấu hình Workflow.

---

> [!IMPORTANT]
> **Tuyên ngôn Chất lượng:** Mọi đoạn mã không vượt qua bộ tiêu chuẩn Audit này sẽ bị coi là lỗi kỹ thuật (Defect) và không đủ điều kiện bàn giao.

[Xem chi tiết Giới thiệu dự án và Lộ trình Audit](./Introduction.md)

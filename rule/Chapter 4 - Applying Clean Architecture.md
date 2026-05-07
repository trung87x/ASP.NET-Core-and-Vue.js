# Chapter 4: Tiêu chuẩn Kiến trúc Sạch (Clean Architecture Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc về cấu trúc Solution và luồng phụ thuộc (Dependency Flow) cho dự án Travel App. Đây là căn cứ để **Audit (kiểm tra)** và phê duyệt mã nguồn.

---

## 1. Cấu trúc Solution (Solution Structure)

Mọi Solution phải được tổ chức thành 4 dự án (Projects) tách biệt để đảm bảo tính đóng gói:

| Project | Tầng (Layer) | Trách nhiệm chính |
| :--- | :--- | :--- |
| **Travel.Domain** | Core | Định nghĩa Entities, Enums, Value Objects. |
| **Travel.Application** | Core | Định nghĩa Logic nghiệp vụ, Interfaces, DTOs, MediatR Handlers. |
| **Travel.Infrastructure** | Infrastructure | Cài đặt EF Core DbContext, Migrations, External Services. |
| **Travel.WebApi** | Presentation | API Controllers, Middleware, Dependency Injection Setup. |

---

## 2. Quy tắc phụ thuộc (Dependency Enforcement Rules)

Đây là các quy tắc **BẮT BUỘC** khi kiểm tra tệp `.csproj`:

1.  **QUY TẮC VÀNG:** `Travel.Domain` **KHÔNG ĐƯỢC PHÉP** có bất kỳ `<ProjectReference>` nào. Nó là lớp lõi độc lập hoàn toàn.
2.  **Lớp Application:** Chỉ được phép reference đến `Travel.Domain`.
3.  **Lớp Infrastructure:** Phải reference đến `Travel.Application`. Không được phép gọi trực tiếp các lớp của `WebApi`.
4.  **Lớp WebApi:** Reference đến cả `Travel.Application` và `Travel.Infrastructure` để thực hiện DI.

---

## 3. Tiêu chuẩn tổ chức thư mục (Folder Standards)

Mã nguồn phải được đặt trong thư mục `src/` theo phân cấp:
- `src/Core/Travel.Domain`
- `src/Core/Travel.Application`
- `src/Infrastructure/Travel.Infrastructure`
- `src/Presentation/Travel.WebApi`

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

Dành cho AI và Lập trình viên kiểm tra trước khi Commit:

- [ ] **Check 1:** Mở `Travel.Domain.csproj`. Có thẻ `<ProjectReference>` nào không? (Nếu có -> **FAILED**).
- [ ] **Check 2:** Logic tính toán nghiệp vụ (Business Logic) có nằm trong `Controller` không? (Nếu có -> **FAILED** - phải chuyển vào Application).
- [ ] **Check 3:** Các Interface (ví dụ `IApplicationDbContext`) có nằm trong lớp Application không? (Nếu nằm ở Infrastructure -> **FAILED**).
- [ ] **Check 4:** Tên các Project có tuân thủ định dạng `Namespace.Layer` không?

---

## 5. Lệnh thực thi tiêu chuẩn (CLI Audit)

Sử dụng các lệnh sau để đảm bảo cấu trúc dự án luôn đúng:

```powershell
# Kiểm tra danh sách project trong solution
dotnet sln list

# Kiểm tra các reference của một project cụ thể
dotnet list src/Core/Travel.Domain/Travel.Domain.csproj reference
```

---

> [!IMPORTANT]
> Mọi hành vi vi phạm "Quy tắc phụ thuộc" sẽ dẫn đến việc từ chối Code Review. Kiến trúc sạch là ưu tiên hàng đầu, hơn cả tốc độ phát triển.

[Tiếp theo: Tiêu chuẩn DbContext & Controllers](./Chapter 5 - Setting Up DbContext and Controllers.md)

# Chapter 6: Tiêu chuẩn CQRS và MediatR (CQRS Enforcement Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi triển khai mô hình **CQRS** và sử dụng **MediatR** trong dự án Travel App. Đây là cơ sở để thực hiện Audit tính tách biệt giữa Đọc (Read) và Ghi (Write).

---

## 1. Nguyên tắc cốt lõi (Core Principles)

Mọi hành động tương tác với dữ liệu phải được phân loại và xử lý riêng biệt:

- **Query (Truy vấn):** Chỉ thực hiện các tác vụ đọc dữ liệu. Tuyệt đối không được gọi `_context.SaveChangesAsync()`.
- **Command (Lệnh):** Thực hiện các hành động thay đổi trạng thái (Create, Update, Delete). Phải đảm bảo tính nhất quán của dữ liệu.

---

## 2. Tiêu chuẩn cấu trúc file (File Organization Standard)

Toàn bộ logic nghiệp vụ phải nằm trong tầng **Application** và tuân thủ cấu trúc thư mục sau:

```text
Travel.Application/
└── [EntityName]/
    ├── Commands/
    │   └── [ActionName]Command.cs
    └── Queries/
        └── [ActionName]Query.cs
```

**Quy tắc:** Mỗi file chỉ nên chứa một cặp `Request` và `Handler` tương ứng để dễ dàng quản lý và kiểm thử.

---

## 3. Tiêu chuẩn Controller (Thin Controller Standard)

Controllers phải kế thừa từ `ApiController` và tuyệt đối không được chứa logic nghiệp vụ.

**Ví dụ chuẩn Audit:**
```csharp
[HttpGet]
public async Task<ActionResult<ToursVm>> Get()
{
    // CHỈ ĐƯỢC PHÉP gửi yêu cầu qua Mediator
    return await Mediator.Send(new GetToursQuery());
}
```

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có lớp nào thực hiện cả đọc và ghi dữ liệu trong cùng một Handler không? (Nếu có -> **FAILED**).
- [ ] **Check 2:** `Controller` có khởi tạo `DbContext` không? (Nếu có -> **FAILED** - Controller chỉ được dùng `IMediator`).
- [ ] **Check 3:** Các `Query` có sử dụng `AsNoTracking()` khi truy vấn EF Core không? (Nên có để tối ưu hiệu năng).
- [ ] **Check 4:** Tên file có kết thúc bằng hậu tố `Command.cs` hoặc `Query.cs` không?

---

## 5. Quy tắc đặt tên (Naming Conventions)

- **Request:** `[Hành động][Thực thể]Command` hoặc `[Hành động][Thực thể]Query`.
- **Handler:** `[Tên Request]Handler`.
- **Ví dụ:** `CreateTourListCommand` và `CreateTourListCommandHandler`.

---

> [!WARNING]
> Việc trộn lẫn logic Command và Query trong cùng một lớp xử lý được coi là vi phạm nghiêm trọng kiến trúc hệ thống và sẽ bị yêu cầu làm lại (Redesign).

[Tiếp theo: CQRS thực thi và Base Controller](./Chapter 7 - CQRS in Action.md)

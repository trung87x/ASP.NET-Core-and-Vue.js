# Chapter 5: Tiêu chuẩn DbContext và API Controller (Data & API Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi làm việc với **Entity Framework Core** và thiết lập **Controllers** trong dự án Travel App. Đây là căn cứ để Audit tính nhất quán của tầng dữ liệu và tầng giao diện.

---

### 📜 Tiêu chuẩn kỹ thuật
1.  **DbContext Purity:** DbContext chỉ chứa các `DbSet` và cấu hình cơ bản.
2.  **Configuration Separation:** Mọi cấu hình chi tiết (Max Length, Required, Relationship) phải nằm trong các lớp thực thi `IEntityTypeConfiguration<T>` riêng biệt.
3.  **Auto-Registration:** Phải sử dụng `builder.ApplyConfigurationsFromAssembly` để tự động nạp cấu hình.

### 🔍 Audit Checklist
*   **Check 1:** DbContext có kế thừa `IApplicationDbContext` từ tầng Application không?
*   **Check 2:** Các cấu hình thực thể (Fluent API) có được tách thành các file riêng trong thư mục `Configurations` không?
*   **Check 3:** Có sử dụng `builder.ApplyConfigurationsFromAssembly` để tự động nạp cấu hình không?
*   **Check 4:** Chuỗi kết nối có được quản lý trong `appsettings.json` không?

## 1. Tiêu chuẩn DbContext (DbContext Standard)

`DbContext` phải được đặt trong project `Travel.Infrastructure` (hoặc `Travel.Data`) và phải tuân thủ các quy tắc sau:

- **Naming:** Phải có hậu tố `DbContext` (Ví dụ: `TravelDbContext`).
- **DbSet:** Mọi thực thể (Entity) từ lớp Domain muốn lưu vào database phải có một `DbSet` tương ứng.
- **Interface:** Cần định nghĩa một Interface (Ví dụ: `IApplicationDbContext`) trong lớp **Application** để đảm bảo tính Loose Coupling.

**Mẫu Audit chuẩn:**
```csharp
public class TravelDbContext : DbContext, IApplicationDbContext {
    public DbSet<TourList> TourLists { get; set; }
    // ...
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

---

## 2. Tiêu chuẩn API Controller (Controller Standard)

Mọi Controller trong dự án phải tuân thủ các quy tắc thiết kế RESTful:

1.  **Kế thừa:** Phải kế thừa từ `ApiController` (Base Controller đã được tùy chỉnh).
2.  **Attributes:** Phải có `[ApiController]` và `[Route("api/[controller]")]`.
3.  **Dependency Injection:** Tuyệt đối không khởi tạo đối tượng bằng từ khóa `new`. Sử dụng **Constructor Injection**.
4.  **Kết quả trả về:** Luôn sử dụng `ActionResult<T>` để Swagger có thể hiểu được kiểu dữ liệu trả về.

---

## 3. Quy tắc đặt tên Route (Routing Rules)

- **Danh sách:** `GET api/tourlists`
- **Chi tiết:** `GET api/tourlists/{id}`
- **Tạo mới:** `POST api/tourlists`
- **Cập nhật:** `PUT api/tourlists/{id}`
- **Xóa:** `DELETE api/tourlists/{id}`

---

## 4. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** `TravelDbContext` có nằm ở lớp Domain không? (Nếu có -> **FAILED** - Phải nằm ở Infrastructure).
- [ ] **Check 2:** Connection String có bị viết cứng (Hard-code) trong code không? (Nếu có -> **FAILED** - Phải để ở `appsettings.json`).
- [ ] **Check 3:** Các phương thức trong Controller có kiểm tra `ModelState.IsValid` thủ công không? (Không cần vì `[ApiController]` tự làm - Nếu có -> **WARNING**).
- [ ] **Check 4:** Có sử dụng `Async/Await` cho mọi thao tác I/O (Database, Network) không? (Nếu không -> **FAILED**).

---

## 5. Cấu hình Database (Infrastructure Setup)

- **Provider:** Sử dụng SQLite cho môi trường Development.
- **Migration:** Mọi thay đổi cấu trúc bảng phải thông qua `Add-Migration`. Tuyệt đối không sửa Database trực tiếp bằng công cụ bên ngoài.

---

> [!IMPORTANT]
> Việc bỏ qua `Async/Await` trong các tác vụ Database sẽ gây hiện tượng nghẽn luồng (Thread Starvation) và làm sụp đổ hệ thống khi có nhiều người dùng.

[Tiếp theo: Tiêu chuẩn CQRS và MediatR](./Chapter 6 - Diving into CQRS.md)

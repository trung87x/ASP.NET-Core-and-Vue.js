# Chapter 7: Tiêu chuẩn Triển khai Command (Command Action Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi thực hiện các hành động thay đổi dữ liệu (**Commands**) và cách tổ chức **Base Controller** trong dự án Travel App.

---

## 1. Tiêu chuẩn cấu trúc Command (Command Structure Standard)

Mỗi Command phải là một đơn vị logic độc lập, tuân thủ các quy tắc sau:

- **Input:** Chứa các thuộc tính cần thiết để thực hiện thay đổi.
- **Output:** Phải trả về thông tin định danh (ví dụ: `int ID`) cho hành động Create, hoặc `Unit` (tương đương void) cho hành động Update/Delete.
- **Validation:** Mọi dữ liệu đầu vào phải được kiểm tra tính hợp lệ trước khi đi vào Handler.

**Mẫu Audit chuẩn:**
```csharp
public class CreateTourListCommand : IRequest<int> {
    public string City { get; set; }
    // ...
}
```

---

## 2. Tiêu chuẩn ApiController (Base Controller Standard)

Để giảm thiểu việc lặp mã và tập trung vào tính năng, dự án bắt buộc sử dụng `ApiController` làm lớp cơ sở.

**Quy tắc Audit:**
1.  **Lazy Loading Mediator:** Tuyệt đối không Inject `IMediator` vào từng Controller con. Sử dụng thuộc tính `Mediator` từ lớp cha.
2.  **Service Locator:** Cho phép sử dụng `HttpContext.RequestServices.GetService<IMediator>()` bên trong lớp Base để giữ cho các Controller con luôn "sạch" (không có Constructor phức tạp).

---

## 3. Quy tắc "Thin Controller" (Strict Rule)

Controller **CHỈ ĐƯỢC PHÉP** chứa mã điều hướng gửi yêu cầu qua Mediator.

- **SAI:** `_context.TourLists.Add(entity); await _context.SaveChangesAsync();` ngay trong Controller.
- **ĐÚNG:** `return await Mediator.Send(command);`

---

### 📜 Tiêu chuẩn kỹ thuật
1.  **Mandatory AutoMapper:** Bắt buộc sử dụng AutoMapper. Không Manual Mapping.
2.  **Pipeline Behaviours:** Bắt buộc triển khai đầy đủ 4 bộ hộ vệ Pipeline.
3.  **CRUD Integrity:**
    *   **Create:** Phải trả về ID của đối tượng mới tạo.
    *   **Update:** Phải kiểm tra sự tồn tại của đối tượng (`NotFound`) trước khi cập nhật.
    *   **Delete:** Phải kiểm tra sự tồn tại và thực hiện xóa vật lý (hoặc xóa mềm tùy cấu hình).

### 🔍 Audit Checklist
*   **Check 1:** Các hành động Update có kiểm tra sự tồn tại của dữ liệu (tránh lỗi null) không?
*   **Check 2:** Có triển khai đầy đủ 4 bộ Behaviours trong MediatR Pipeline không?
*   **Check 3:** Có sử dụng DTO thay vì trả về Entity trực tiếp không?
*   **Check 4:** Có sử dụng AutoMapper (ProjectTo hoặc Map) thay vì gán thủ công không?

---

## 5. Quy tắc xử lý Exception (Error Handling Rules)

Mọi lỗi nghiệp vụ (ví dụ: bản ghi không tồn tại) phải được ném ra dưới dạng các Exception tùy chỉnh (như `NotFoundException`). Tuyệt đối không trả về `null` hoặc mã lỗi thủ công trong Handler.

---

> [!IMPORTANT]
> Việc tuân thủ cấu trúc "Thin Controller" giúp chúng ta có thể thay thế toàn bộ lớp WebApi (Presentation) bằng một giao diện khác (như CLI, Worker Service) mà không cần thay đổi một dòng logic nghiệp vụ nào.

[Tiếp theo: Tiêu chuẩn Versioning và Logging](./Chapter 8 - API Versioning and Logging in ASP.NET Core.md)

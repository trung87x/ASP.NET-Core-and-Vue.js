# Chapter 3: Tiêu chuẩn Tầng Domain và Thực thể (Domain & Entity Standard)

Tài liệu này quy định các tiêu chuẩn bắt buộc khi xây dựng lớp **Lõi (Domain)** trong dự án Travel App. Đây là lớp quan trọng nhất, yêu cầu sự tinh khiết tuyệt đối về mã nguồn.

---

## 1. Tiêu chuẩn Thực thể (Entity Standards)

Mọi Entity phải tuân thủ các quy tắc sau để đảm bảo tính nhất quán:

- **Identity:** Phải có một thuộc tính định danh duy nhất (thường là `int Id`).
- **Encapsulation:** Sử dụng các thuộc tính `get; set;` cơ bản.
- **Initialization:** Các danh sách quan hệ (Navigation Properties) phải được khởi tạo ngay trong Constructor hoặc tại dòng khai báo để tránh lỗi `NullReference`.

**Ví dụ chuẩn Audit:**
```csharp
public class TourList {
    public int Id { get; set; }
    public string City { get; set; }
    // Khởi tạo ngay để đảm bảo an toàn
    public IList<TourPackage> Items { get; set; } = new List<TourPackage>();
}
```

---

## 2. Quy tắc "Tinh khiết" (Domain Purity Rule)

Tầng Domain là vùng cấm đối với các công nghệ ngoại vi:

- **CẤM:** Sử dụng các Namespace liên quan đến Web (như `Microsoft.AspNetCore.*`).
- **CẤM:** Sử dụng các Namespace liên quan đến Database (như `Microsoft.EntityFrameworkCore.*`).
- **CẤM:** Ghi Log trực tiếp trong Entity.

---

## 3. Danh sách kiểm tra Audit (Audit Checklist)

- [ ] **Check 1:** Có Entity nào chứa logic lưu dữ liệu không? (Nếu có -> **FAILED** - Entity chỉ chứa dữ liệu và logic nghiệp vụ nội tại).
- [ ] **Check 2:** Các thuộc tính chuỗi (string) đã được định nghĩa giới hạn độ dài chưa? (Nên định nghĩa ở lớp Data/Configuration - Nếu không -> **WARNING**).
- [ ] **Check 3:** Có sử dụng các kiểu dữ liệu phức tạp của thư viện bên ngoài không? (Nếu có -> **FAILED**).
- [ ] **Check 4:** Navigation Properties có được khởi tạo mặc định không? (Nếu không -> **FAILED**).

---

## 4. Định dạng tệp tin (File Naming)

Mỗi Entity phải nằm trong một tệp `.cs` riêng biệt và đặt trong thư mục `Entities/` của project `Travel.Domain`.

---

> [!TIP]
> Việc giữ cho Domain "sạch" giúp bạn có thể chạy Unit Test cho toàn bộ logic nghiệp vụ mà không cần cài đặt bất kỳ môi trường phức tạp nào. Đây là đỉnh cao của tính linh hoạt trong lập trình.

[Tiếp theo: Tiêu chuẩn Kiến trúc Sạch (Thực thi)](./Chapter 4 - Applying Clean Architecture.md)

---

## Tiếp theo: Chương 4 - Applying Clean Architecture
Chúng ta sẽ chuyển sang tầng Application để điều phối logic cho các Entities này.

[Bắt đầu Chương 4](./Chapter 4 - Applying Clean Architecture.md)

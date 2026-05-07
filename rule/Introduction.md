# Travel App: Tiêu chuẩn Quản trị và Giám sát Kỹ thuật (Project Governance)

Tài liệu này định nghĩa khung quản trị kỹ thuật cho dự án Travel App. Toàn bộ mã nguồn phát triển cho dự án này **BẮT BUỘC** phải tuân thủ các quy tắc được mô tả trong bộ quy tắc (rule/) này.

---

## 🎯 Mục tiêu của Bộ Tiêu chuẩn

Bộ quy tắc này không phải là tài liệu hướng dẫn thông thường, mà là một **Hệ thống Giám sát Kỹ thuật (Technical Audit System)** nhằm đảm bảo:
1.  **Tính Nhất quán:** Mọi dòng code đều có chung một phong cách và cấu trúc.
2.  **Tính Bền vững:** Áp dụng Clean Architecture để dễ dàng mở rộng và bảo trì.
3.  **Tính Tin cậy:** 100% Use Cases quan trọng phải có bài kiểm tra tự động.
4.  **Tính Bảo mật:** Tuân thủ các tiêu chuẩn bảo mật hiện đại (JWT, SSL, Secrets Management).

---

## 🛠️ Cơ chế Audit (Audit Mechanism)

Mỗi chương trong bộ quy tắc này đều đi kèm một **Audit Checklist**. Quy trình phát triển tiêu chuẩn bao gồm:

1.  **Phát triển (Develop):** Lập trình viên viết code dựa trên tiêu chuẩn của chương tương ứng.
2.  **Tự kiểm tra (Self-Audit):** Lập trình viên đối chiếu code với Checklist trong Rule.
3.  **Phê duyệt (Review):** Tech Lead (hoặc AI Assistant) sử dụng Checklist để đánh giá code là **PASSED** hay **FAILED**.

---

## 🗺️ Cấu trúc các Trụ cột Tiêu chuẩn

- **Trụ cột 1: Kiến trúc (Ch. 1-7):** Quy định về Clean Architecture, CQRS, và Dependency Flow.
- **Trụ cột 2: Vận hành (Ch. 8-10):** Quy định về Logging, Versioning, và Caching (Redis).
- **Trụ cột 3: Giao diện (Ch. 11-17):** Quy định về Vue 3, TypeScript, State Management, và Validation.
- **Trụ cột 4: Chất lượng (Ch. 18-19):** Quy định về Integration Testing và CI/CD.

---

## 💡 Cam kết Chất lượng

Việc vi phạm các quy tắc được đánh dấu là **FAILED** trong Checklist sẽ dẫn đến việc từ chối tích hợp mã nguồn (Pull Request Rejection). Chúng ta xây dựng phần mềm để tồn tại lâu dài, không phải để chạy tạm thời.

---

> [!IMPORTANT]
> Hãy coi bộ quy tắc này là "Hiến pháp" của dự án. Mọi quyết định kỹ thuật phải dựa trên các điều khoản đã được quy định tại đây.

[Bắt đầu Chương 1: Tiêu chuẩn Thiết lập Môi trường](./Chapter 1 - Getting Started.md)

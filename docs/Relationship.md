# Các mối quan hệ trong thiết kế hướng đối tượng và cách thể hiện trong C#

Trong lập trình hướng đối tượng, các lớp không tồn tại biệt lập; chúng liên kết với nhau qua những mối quan hệ để tạo nên kiến trúc của hệ thống. Việc hiểu đúng và gọi tên chính xác các mối quan hệ không chỉ giúp bạn thiết kế phần mềm chặt chẽ mà còn giúp truyền đạt ý đồ kiến trúc một cách rõ ràng qua mã nguồn cũng như tài liệu (XML docs). Trong C#, mỗi loại quan hệ thường có một hoặc nhiều cách gọi phổ biến, và chúng được hiện thực hóa bằng những cú pháp cụ thể.

Bài viết này sẽ phân tích sâu về các mối quan hệ nền tảng: **Kế thừa (Inheritance)**, **Ủy thác/Chứa (Composition/Aggregation)**, **Liên kết (Association)**, **Phụ thuộc (Dependency)** và **Hiện thực hóa (Realization/Implementation)**. Tất cả đều có những tên gọi khác nhau tùy theo ngữ cảnh, nhưng đều được ánh xạ thành các cấu trúc rõ ràng trong C#.

---

## 1. Kế thừa – Quan hệ "is‑a" (là một)

- **Tên gọi khác**: _Generalization – Specialization_ (tổng quát hóa – chuyên biệt hóa), _Derivation_ (dẫn xuất), _Parent‑Child_.
- **Ý nghĩa**: Lớp dẫn xuất **là một** phiên bản cụ thể của lớp cơ sở. Ví dụ: `Dog` là một `Animal`, `Car` là một `Vehicle`.
- **Cú pháp trong C#**:
  ```csharp
  public class Animal { }
  public class Dog : Animal { }   // Dog is‑a Animal
  ```

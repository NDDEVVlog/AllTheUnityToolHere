# BillUtilsWindow - Hướng Dẫn Sử Dụng

## Tổng Quan

`BillUtilsWindow` là một cửa sổ tùy chỉnh trong Unity Editor, được thiết kế để quản lý các prefab một cách hiệu quả. Công cụ này cho phép người dùng khởi tạo prefab, xem trước chúng trong trình chỉnh sửa, lưu và quản lý danh sách các prefab, và nhiều tính năng khác. Giao diện thân thiện giúp thao tác với prefab trở nên dễ dàng và nhanh chóng.

## Các Tính Năng

- **Khởi Tạo Prefab**: Dễ dàng khởi tạo các prefab và thiết lập container cha của chúng.
- **Xem Trước Prefab**: Xem trước các prefab được chọn trong trình chỉnh sửa.
- **Lưu Prefab**: Lưu các prefab vào danh sách để truy cập nhanh và khởi tạo trong tương lai.
- **Quản Lý Prefab Đã Lưu**: Xem, khởi tạo và xóa các prefab đã lưu.
- **Lưu Trữ Prefab**: Các prefab đã lưu được lưu trữ bằng `EditorPrefs` và tồn tại qua các phiên làm việc của Unity.

## Hướng Dẫn Sử Dụng

### Bước 1: Mở Cửa Sổ

Để mở `BillUtilsWindow`, vào `Tools > Bill Utils > Prefab Management Tools` trong menu của Unity Editor.

### Bước 2: Khởi Tạo Prefab

1. **Cài Đặt Prefab**:
    - Chọn prefab cần khởi tạo.
    - Tùy chọn đặt container (nếu có).

2. **Khởi Tạo**:
    - Nhấp vào nút "Instantiate Prefab" để tạo một instance của prefab đã chọn.

### Bước 3: Xem Trước Prefab

1. **Xem Trước Tài Sản**:
    - Mở rộng mục "Preview Asset".
    - Prefab đã chọn sẽ được hiển thị trong khu vực xem trước.

### Bước 4: Lưu Prefab

1. **Lưu Prefab**:
    - Nhấp vào nút "Save Prefab" để lưu prefab vào danh sách.

### Bước 5: Quản Lý Prefab Đã Lưu

1. **Xem Prefab Đã Lưu**:
    - Mở rộng mục "Saved Prefabs" để xem danh sách các prefab đã lưu.
    - Các prefab đã lưu sẽ được liệt kê trong danh sách.

2. **Khởi Tạo Prefab Đã Lưu**:
    - Chọn prefab từ danh sách và nhấp vào nút "Instantiate" để khởi tạo prefab đã lưu.

3. **Xóa Prefab Đã Lưu**:
    - Chọn prefab từ danh sách và nhấp vào nút "Remove" để xóa prefab khỏi danh sách.

4. **Xóa Tất Cả Prefab Đã Lưu**:
    - Nhấp vào nút "Remove All Prefab Save" để xóa toàn bộ danh sách các prefab đã lưu.

### Bước 6: Xem Trước Prefab Đã Lưu

1. **Xem Trước Prefab Đã Lưu**:
    - Mở rộng mục "Preview Selected Asset".
    - Prefab được chọn từ danh sách sẽ hiển thị trong khu vực xem trước.
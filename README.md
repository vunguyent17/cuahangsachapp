# Cửa Hàng Sách App

<img src="https://github.com/vunguyent17/cuahangsachapp/blob/main/Screenshots/Screenshot_20221104-030640.jpg" data-canonical-src="https://github.com/vunguyent17/cuahangsachapp/blob/main/Screenshots/Screenshot_20221104-030640.jpg" width="300" alt="Trang chủ" />

## Giới thiệu / Introduction

Cửa Hàng Sách App là ứng dụng di động cho phép khách hàng có thể xem danh mục sách và chi tiết các đầu sách, tìm kiếm, tạo tài khoản và tiến hành đặt hàng mua sách.


Cửa Hàng Sách App is a mobile application that allows user to browse and find information about books, create account and order books from bookstore with online payment supported.

### Chức năng của ứng dụng / Functionality:
- Hiển thị danh sách các sách, thể loại sách
- Hiển thị thông tin chi tiết liên quan đến sách
- Tìm kiếm sách, hiển thị sách theo thể loại
- Đăng nhập, Đăng ký tài khoản
- Thêm sách vào giỏ hàng, thanh toán, xác nhận đơn hàng
- Chỉnh sửa thông tin người dùng, đổi mật khẩu
 <br/>

- Display book categories, book lists and details
- Browse and search book by name
- Sign up, log in
- Add books to cart, order books with online payment supported
- See infomation about your past orders
- Modify your info and password

### Công nghệ sử dụng / Tech stack:
- Mobile development framework: Xamarin	
- Server backend:  ASP.NET Web API (.NET Framework 4.7.2)
- Database: SQL Server
- Deploy database và server backend lên Microsoft Azure


### Dịch vụ bên thứ ba / Third-party services:
- Payment Processing Platform (Thanh toán đơn hàng qua thẻ quốc tế): [Stripe](https://stripe.com/)
- SMTP Server (Gửi thông tin đơn hàng tới email): [MailTrap](https://mailtrap.io/)


### Các trang của ứng dụng Cửa Hàng Sách gồm có: 
- Trang chủ
- Danh mục sách
  - Thể loại
  -	Tìm kiếm
  -	Danh sách sách theo thể loại
  -	Chi tiết sách
- Tài khoản
  -	Tài khoàn
  -	Đăng nhập
  -	Chỉnh sửa thông tin tài khoản
  -	Thay đổi mật khẩu
  -	Chi tiết đơn hàng đã đặt
- Giỏ hàng
  -	Giỏ hàng
  -	Thanh toán
  -	Đặt hàng thành công


## Sử dụng / Usage
Tải file apk về và cài đặt trên thiết bị di động


Download and Install apk file on your Android mobile device

- Minimum Android Version: Android 5.0 (API 21)
- Target Android Version: Android 12.0 (API 31)

## Demo
[Link video demo](https://drive.google.com/file/d/1qlEPdcp6V67bQhIa038aWGpmygMUaUbm/view?usp=sharing)


## Set up project locally
### Prerequisites
- SQL Server and SSMS
- Visual Studios 2019/2022 with Xamarin, ASP.NET and web development workloads installed
- Third-party service: Stripe, MailTrap

### Installation

1. Clone the repo

2. Create SQL Server Database CuaHangSach and run SQL script in CuaHangSachDBScript.sql

3. Open CHSachWebAppData.sln and add config files:
- appSettingsCustom.config
```
<?xml version="1.0"?>
<appSettings>
	<add key="SmtpClientHost" value="smtp.mailtrap.io" />
	<add key="SmtpClientPort" value="2525" />
	<add key="usernameEmailService" value="<your mailtrap inbox username here>" />
	<add key="passwordEmailService" value="<your mailtrap inbox password here>" />
	<add key="SecretKey" value="<your stripe key here>" />
</appSettings>
```

- connectionStringsCustom.config
```
<?xml version="1.0"?>
<connectionStrings>
	<add name="Connectionstring" connectionString="<add database connection string here" />
</connectionStrings>
```

4. Run Web API locally

5. Open CuaHangSach.sln and add API/ApiKeys.cs on root folder:
```
namespace CuaHangSach.API
{
    public static partial class APIKeys
    {
        public static string CHSachURL = "<your Web API URL, end with "/">";
    }
}
```

6. Run CuaHangSach App on Android Emulator or your Android device

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)

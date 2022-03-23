using System;

namespace HR.Core
{
    public static class Constants
    {

        public static DateTime CurrentDate { get { return DateTime.Now; } }
        /// <summary>
        /// Số bản ghi mặc định/tang
        /// </summary>
        public const int DefaultPageSize = 10;

        #region Cache name
        public const string CacheApplicationInfoKey = "ApplicationInfoKey_{0}";
        public const string CacheUserInfoKey = "UserInfoKey_{0}";
        public const string CacheTopScoreKey = "TopScoreKey_{0}";
        public const int CacheInMinutes = 1;
        public const int CacheInTenMinutes = 10;
        public const int CacheInOneHour = 1;
        public const int CacheInTwoHour = 2;
        public const int CacheInThreeHour = 3;
        #endregion

        /// <summary>
        /// Đơn vị tiền mặc định
        /// </summary>
        public const string CurrencyVND = "VND";
        public const string CurrencyCNY = "CNY";
        public const string EmailIsNotExist = "Email chưa được đăng ký, vui lòng sử dụng email đã đăng ký để reset password";
        public const string ResetPasswordResult = "Mật khẩu đã được gửi đặt lại và gửi vào địa chỉ email: {0}. Hãy sử dụng password mới: {1} để đăng nhập.";

        #region Thông  báo dùng chung
        public const string CreateSuccess = "Thêm mới thành công.";
        public const string CreateFail = "Thêm mới thất bại.";
        public const string UpdateSuccess = "Cập nhật thành công.";
        public const string UpdateFail = "Cập nhật thất bại.";
        public const string DeleteSuccess = "Xóa dữ liệu thành công.";
        public const string DeleteFail = "Xóa dữ liệu thất bại.";

        public const string CodeIsNotEmpty = "Mã không được để trống";
        public const string NameIsNotEmpty = "Tên không được để trống";

        public const string EmployeeIsNotEmpty = "Không được để trống nhân viên";
        public const string EmployeeIsNotExit = "Nhân viên không tồn tại";
        public const string SchoolNameIsNotExist = "Tên đơn vị đào tạo không được trống";
        public const string CertificateisNotEmpty = "Tên văn bằng, chứng chỉ không được trống";
        public const string ClasscificationisNotEmpty = "Xếp loại tốt nghiệp không được trống";
        public const string TimeRangeIsNotValid = "Thời gian đào tạo không hợp lệ, ngày bắt đầu phải nhỏ hơn ngày kết thúc.";
        public const string ErrorOnProcess = "Có lỗi xảy ra trong quá trình xử lý, liên hệ Admin để nhận trợ giúp.";

        //User-Login, Logout
        //public const string 
        #endregion

    }


}

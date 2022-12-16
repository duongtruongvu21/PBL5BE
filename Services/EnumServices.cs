using PBL5BE.API.Data.Enums;

namespace PBL5BE.API.Services
{
    public class StatusCodeService
    {
        public static string toString(STTCode statusCode)
        {
            switch (statusCode)
            {
                case STTCode.Existed:
                    return "Đã tồn tại";
                case STTCode.ForeignKeyIDNotFound:
                    return "Không tìm thấy ForeignKey";
                case STTCode.IDNotFound:
                    return "Không tìm thấy ID";
                case STTCode.IncorrectPassword:
                    return "Mật khẩu không chính xác";
                case STTCode.ServerCodeException:
                    return "Báo BackEnd để sửa";
                case STTCode.Success:
                    return "Thành Công";
                case STTCode.UserExisted:
                    return "Đã tồn tại tài khoản này";
                case STTCode.UserNotExist:
                    return "Không tồn tại tài khoản này";
                case STTCode.OrderIsPaid:
                    return "Không thể xoá OrderDetail của một Order đã thanh toán";
                case STTCode.NoAccess:
                    return "Không có thẩm quyền";
                case STTCode.ProductBuyAmountExcessProductCount:
                    return "Số lượng sản phẩm muốn mua vượt quá số lượng sản phẩm còn lại";

                case STTCode.NotEmail:
                    return "Email không hợp lệ!!";
                default:
                    return "Lỗi, báo cho BackEnd";
            }
        }
    }
}
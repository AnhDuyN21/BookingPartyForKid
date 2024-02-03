using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking : BaseEntity
    {
        public PaymentStatus PaymentStatus { get; set; }
        public int TotalPrice {  get; set; }
        public string? NoteForHost {  get; set; }
        public int? PartyID {get; set; }
        //Relationship
        public virtual Party? Party { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Review? Review { get; set; }
    }
    public enum PaymentStatus
    {
        //Thanh toán đang chờ xử lý hoặc chờ xác nhận.
        Pending = 0,
        //Thanh toán đã bị hủy bỏ trước khi hoặc sau khi được xử lý.
        Canceled = 1,
        //Số tiền đã được trừ từ tài khoản của người mua và chuyển vào tài khoản của người bán.
        Captured = 2,
        //Thanh toán không thành công vì lý do nào đó, chẳng hạn như thẻ tín dụng không hợp lệ, không đủ số dư, v.v.
        Failed = 3,
    }
}

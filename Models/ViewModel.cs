using System.Security.Cryptography;

namespace AKUH_API.Models
{
    public class ViewModel
    {
    }
    public class Rsp
    {
        public string? description { get; set; }
        public int? status { get; set; }

    }
    public class LoginBLL
    {
        public int AdminID { get; set; }

        public string Name { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }
    }
    public class RspLoginAdmin
    {
        public LoginBLL? loginAdmin { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }

    }
    public class RspLogin
    {
        public UserBLL? login { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }

    }
    public class RspUser
    {
        public int? UserId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }

    }
    public class RspForgetPwd
    {
        public string? Status { get; set; }
        public string? Password { get; set; }
    }
    public class RspCustomerLogin
    {
        public UserBLL? user { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
    }
    public class RspQR
    {
        public QRBLL? qr { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
    }
    public class QRBLL
    {
        public int? EventID { get; set; }
        public int? EventUserID { get; set; }
        public string? EventName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string? FinalDate { get; set; }
    }
    public class RspEditUser
    {
        public string? Status { get; set; }
        public string? Description { get; set; }

    }
    public class UserBLL
    {
        public int UserID { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }
        public string? Image { get; set; }

        public string? Address { get; set; }

        public string? ContactNo { get; set; }

        public string? Password { get; set; }

        public int? StatusID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class RspBannerPopup
    {
        public List<BannerPopupBLL> bannerPopup { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }

    }
    public class BannerPopupBLL
    {
        public int? ID { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public int? DisplayOrder { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class RspBanner
    {
        public List<BannerBLL> banner { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }

    }
    public class BannerBLL
    {
        public int? BannerID { get; set; }

        public string? Type { get; set; }

        public string? Screen { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int? DisplayOrder { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class EventBLL
    {
        public int EventID { get; set; }

        public int? EventCategoryID { get; set; }

        public int? OrganizerID { get; set; }

        public int? AttendeesID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
        public bool IsFeatured { get; set; }

        public string Location { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public decimal? TicketNormal { get; set; }

        public decimal? TicketPremium { get; set; }

        public decimal? TicketPlatinum { get; set; }

        public string EventDate { get; set; }

        public string? EventTime { get; set; }
        public string? EventCity { get; set; }

        public string LocationLink { get; set; }

        public int? StatusID { get; set; }

        public DateTime? DoorTime { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public int? RemainingTicket { get; set; }

        public int? EventAttendeesID { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Twitter { get; set; }

        public string Image { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

        public List<EventImageBLL> ImgList { get; set; }
        public List<SpeakerBLL> Speakers { get; set; }
        public List<OrganizerBLL> Organizers { get; set; }
    }
    public class RspEvent
    {
        public List<EventCategoryBLL> eventcategories { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class RspMyEvent
    {
        public List<MyEventBLL> myEvent { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class MyEventBLL
    {
        public int EventID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int StatusID { get; set; }
        public string EventName { get; set; }
        public string Image { get; set; }
        public string FromDate { get; set; }
        public string EventTime { get; set; }

        public string MeetingLink { get; set; }
        public string MessageForAttendee { get; set; }
        public string Subject { get; set; }

    }
    public class EventAttendeesEmailBLL
    {
        public int AttendeesID { get; set; }

        public int? EventID { get; set; }

        public int? UserID { get; set; }

        public string? FullName { get; set; }
        public string? EventTime { get; set; }
        public string? EventContactNo { get; set; }
        public string? EventName { get; set; }
        public string? LocationLink { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Email { get; set; }

        public string? PhoneNo { get; set; }

        public string? Occupation { get; set; }

        public string? Gender { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class EventAttendeesBLL
    {
        public int AttendeesID { get; set; }

        public int? EventID { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }

    public class AttendeeRegsiterBLL
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Password { get; set; }
        public int? StatusID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
    public class EventCategoryBLL
    {
        public int EventCategoryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? StatusID { get; set; }

        public string? Image { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }
        public List<EventBLL> events { get; set; }

    }
    public class EventImageBLL
    {
        //public int EventImageID { get; set; }

        public int? EventID { get; set; }

        public string Image { get; set; }

        //public DateTime? Createdon { get; set; }

    }
    public class GalleryBLL
    {
        public int GalleryID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class NotificationBLL
    {
        public int NotificationID { get; set; }

        public int? UserID { get; set; }

        public string NotificationType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? NotificationDate { get; set; }

        public bool? IsRead { get; set; }

        public int? StatusID { get; set; }

    }
    public class RspPartner
    {
        public List<PartnerBLL> partner { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class RspOrganizer
    {
        public List<OrganizerBLL> organizer { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class OrganizerBLL
    {
        public int? EventID { get; set; }
        public int? OrganizerID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class PartnerBLL
    {
        public int? PartnerID { get; set; }

        public string? Name { get; set; }


        public string? Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }


        public int? UpdatedBy { get; set; }

    }
    public class RspToken
    {
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class PushTokenBLL
    {
        public int? TokenID { get; set; }

        public string? Token { get; set; }

        public int? UserID { get; set; }

        public int? StatusID { get; set; }

        public int? Device { get; set; }

    }
    public class PushNoticationBLL
    {
        public string DeviceID { get; set; }
        public string Type { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
    }
    public class RspSpeaker
    {
        public List<SpeakerBLL> speaker { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class SpeakerBLL
    {
        public int? EventID { get; set; }
        public int? SpeakerID { get; set; }

        public string? Name { get; set; }

        public string? Designation { get; set; }

        public string? Company { get; set; }

        public string? About { get; set; }

        public string? Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class RspSetting
    {
        public SettingBLL? setting { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class SettingBLL
    {
        public int SettingID { get; set; }

        public string About { get; set; }

        public string PrivacyPolicy { get; set; }
        public string? SplashScreen { get; set; } = "";

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }
        public List<FaqBLL> faqs { get; set; }
        public ChairBLL Chair { get; set; }
        public ConferenceChairBLL ChairConference { get; set; }
    }
    public class ChairBLL
    {
        public string MsgChair { get; set; }

        public string ImgChair { get; set; }
    }
    public class ConferenceChairBLL
    {
        public string MsgConferenceChair { get; set; }

        public string ImgConChair { get; set; }

    }
    public class FaqBLL
    {
        public int FaqID { get; set; }

        public string FaqQ { get; set; }

        public string FaqA { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

    }
    public class AttendeesUpdtBLL
    {
        public int? EventID { get; set; }

        public int? UserID { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int? StatusID { get; set; }
    }
    public class AttendeesBLL
    {
        public int? AttendeesID { get; set; }

        public int? EventID { get; set; }

        public int? UserID { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? Occupation { get; set; }
        public string? ImageSS { get; set; }

        public string? Gender { get; set; }
        public int? StatusID { get; set; }
        public DateTime? Createdon { get; set; } = DateTime.UtcNow.AddMinutes(300);

    }
    public class RspAttendees
    {
        public AttendeesBLL? attendees { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
    public class RspUpdtAttendees
    {
        public AttendeesUpdtBLL? attendees { get; set; }
        public string? description { get; set; }
        public int? status { get; set; }
    }
}

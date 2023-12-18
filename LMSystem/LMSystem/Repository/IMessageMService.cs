using LMSystem.Models;

namespace LMSystem.Repository
{
    public interface IMessageMService
    {
        string CreateNotification(Notification notify);
        string CreateRequest(Request request);
        string DeleteNotification();
        string DeleteRequest(Request request);
        List<Notification> GetAllUserNotify(string userId);
        List<Notification> GetCheckedNotify(string userId);
        List<Notification> GetDeletableNotify();
        List<Notification> GetDeletableNotify(string userId);
        Notification GetNotification(int id);
        List<Request> GetRequests();
        List<Notification> GetSearchNotify(string searchS);
        List<Notification> GetStoredNotify(string userId);
        string SetChecked(Notification notify);
        string SetDeletable(Notification notify);
        string SetStored(Notification notify);
        string UpdateNotification(Notification notify);
        string UpdateRequest(Request request);
    }
}
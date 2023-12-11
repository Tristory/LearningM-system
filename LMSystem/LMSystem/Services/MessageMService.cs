using LMSystem.Data;
using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class MessageMService
    {
        private readonly ApplicationDbContext _context;

        public MessageMService(ApplicationDbContext context)
        {
            _context = context;
        }        

        // For Notification Handling
        public List<Notification> GetAllUserNotify(string userId)
        { 
            return _context.Notifications
                .Include(e => e.ApplicationUser)
                .Where(e => e.ReceiverId == userId).ToList();
        }

        public List<Notification> GetSearchNotify(string searchS)
        {
            return _context.Notifications
                .Include(e => e.ApplicationUser)
                .Where(e => e.Header.Contains(searchS)).ToList();
        }

        public List<Notification> GetDeletableNotify()
        {
            return _context.Notifications
                .Where(e => e.IsDeleted == true).ToList();
        }

        public List<Notification> GetDeletableNotify(string userId)
        {
            return _context.Notifications
                .Include(e => e.ApplicationUser)
                .Where(e => e.IsDeleted == true && e.ReceiverId == userId).ToList();
        }

        public List<Notification> GetCheckedNotify(string userId)
        {
            return _context.Notifications
                .Include(e => e.ApplicationUser)
                .Where(e => e.IsChecked == true && e.ReceiverId == userId).ToList();
        }

        public List<Notification> GetStoredNotify(string userId)
        {
            return _context.Notifications
                .Include(e => e.ApplicationUser)
                .Where(e => e.IsStored == true && e.ReceiverId == userId).ToList();
        }

        public string CreateNotification(Notification notify)
        {
            _context.Notifications.Add(notify);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateNotification(Notification notify)
        {
            _context.Notifications.Update(notify);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteNotification()
        {
            var deletableNotify = GetDeletableNotify();

            _context.Notifications.RemoveRange(deletableNotify);
            _context.SaveChangesAsync();

            return "Delete success!";
        }

        public string SetDeletable(Notification notify)
        {
            /*if (notify.IsDeleted == null)
            {
                notify.IsDeleted = true;
            }

            notify.IsDeleted = !notify.IsDeleted;*/

            SetBoolValue(notify.IsDeleted);

            return UpdateNotification(notify);
        }

        public string SetChecked(Notification notify)
        {
            /*if (notify.IsChecked == null)
            {
                notify.IsChecked = true;
            }

            notify.IsChecked = !notify.IsChecked;*/

            SetBoolValue(notify.IsChecked);

            return UpdateNotification(notify);
        }

        public string SetStored(Notification notify)
        {
            /*if (notify.IsStored == null)
            {
                notify.IsStored = true;
            }

            notify.IsStored = !notify.IsStored;*/

            SetBoolValue(notify.IsStored);

            return UpdateNotification(notify);
        }

        private void SetBoolValue(bool? notifyValue)
        {
            if (notifyValue == null)
            {
                notifyValue = true;
            }

            notifyValue = !notifyValue;
        }

        // For Request Handling
        public List<Request> GetRequests() => _context.Requests.ToList();

        //public List<Request> 
        
        public string CreateRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateRequest(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteRequest(Request request)
        {
            _context.Requests.Remove(request);
            _context.SaveChanges();

            return "Delete success!";
        }
    }
}

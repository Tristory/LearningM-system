using LMSystem.Models;
using LMSystem.Repository;
using LMSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IFileMService _fileMService;
        private readonly IClassMService _classMService;
        private readonly IMessageMService _messageMService;
        private readonly ISubjectMService _subjectMService;
        private readonly ISettingMService _settingMService;

        public StudentController
        (
            IAuthService authService,
            IFileMService fileMService,
            IClassMService classMService,
            IMessageMService messageMService,
            ISubjectMService subjectMService,
            ISettingMService settingMService
        ) 
        { 
            _authService = authService;
            _fileMService = fileMService;
            _classMService = classMService;
            _messageMService = messageMService;
            _subjectMService = subjectMService;
            _settingMService = settingMService;

        }
        
        // 1. Personal Subject Management
        // Show student's subject
        [HttpGet("Get personal subject")]
        public List<StudentSubject> GetStudentSubjects(string userId)
        {
            return _subjectMService.GetStudentSSs(userId);
        }
        
        // Search for subject
        [HttpGet("Search subject")]
        public List<Subject> SearchSubject(string searchS)
        {
            return _subjectMService.GetSearchSubjects(searchS);
        }
        
        // Order personal subject
        [HttpGet("Order subject")]
        public List<StudentSubject> GetOrderSSs(string filterBy)
        {
            switch(filterBy)
            {
                case "Access Date":
                    return _subjectMService.GetOrderByDateSSs(filterBy);
                case "Name":
                    return _subjectMService.GetOrderByNameSSs(filterBy);
                default:
                    return null;
            }
        }

        // Get marked subject
        [HttpGet("Get marked")]
        public List<StudentSubject> GetMarkedSSs(string userId)
        {
            return _subjectMService.GetMarkedSSs(userId);
        }

        // Get all the lecture
        [HttpGet("Get subject lecture")]
        public List<Lecture> GetSubjectLectures(int subjectId)
        {
            return _classMService.GetSubjectLectures(subjectId);
        }

        // Get lecture detail
        [HttpGet("Get lecture")]
        public Lecture GetLecture(int lectureId)
        {
            return _classMService.GetDetailLecture(lectureId);
        }

        // Get all approved material
        [HttpGet("Get approved material")]
        public List<Material> GetApprovedMaterial(int subjectId)
        {
            return _fileMService.GetApprovedSubjectMaterials(subjectId);
        }

        // Get material detail
        [HttpGet("Get material detail")]
        public Material GetDetailMaterial(int materialId)
        {
            return _fileMService.GetDetailMaterial(materialId);
        }

        // Download chosen material
        [HttpGet("Download material")]
        public async Task<IActionResult> DownloadMaterial(int materialId)
        {
            var fileInfor = await _fileMService.DownloadMaterial(materialId);

            return File(fileInfor.Bytes, fileInfor.ContentType, fileInfor.FilePath);
        }

        // Mark subject
        [HttpPatch("Mark subject")]
        public async Task<IActionResult> MarkSubject(int subjectId)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return Content("No user name!");

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return Content("No user like this!");

            var studentSubject = _subjectMService.GetdetailSS(user.Id, subjectId);

            var result = _subjectMService.MarkManager(studentSubject);

            return Content(result);
        }

        // Get detail subject
        [HttpGet("Get detail subject")]
        public Subject GetDetailSubject(int subjectId)
        {
            return _subjectMService.GetDetailSubject(subjectId);
        }

        // Make question
        [HttpPost("Make question")]
        public async Task<IActionResult> MakeQuestion(QuestionFrame questionFrame)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return Content("No user name!");

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return Content("No user like this!");

            var question = new Question
            {
                Name = questionFrame.Name,
                Content = questionFrame.Content,
                TopicId = questionFrame.TopicId,
                OwnerId = user.Id
            };

            var result = _subjectMService.CreateQuestion(question);

            return Content(result);
        }

        // Make answer
        [HttpPost("Make answer")]
        public async Task<IActionResult> MakeAnswer(AnswerFrame answerFrame)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return Content("No user name!");

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return Content("No user like this!");

            var answer = new Answer
            {
                Content = answerFrame.Content,
                QuestionId = answerFrame.QuestionId,
                OwnerId = user.Id
            };

            var result = _subjectMService.CreateAnswer(answer);

            return Content(result);
        }

        // Get all subject notification
        [HttpGet("Get Subject notification")]
        public List<Notification> GetSubjectNotification(int subjectId)
        {
            var subject = _subjectMService.GetSubject(subjectId);

            var notifyHeader = subject.Id + subject.Name;

            return _messageMService.GetSearchNotify(notifyHeader);
        }
        
        // 2. Notification Management
        // Get all user notification
        [HttpGet("Get user notify fs")]
        public List<Notification> GetUserNotify(string userId)
        {
            return _messageMService.GetAllUserNotify(userId);
        }

        // Search for notification
        [HttpGet("Search notify fs")]
        public List<Notification> SearchNotify(string searchS)
        {
            return _messageMService.GetSearchNotify(searchS);
        }

        // Delete notification
        [HttpPatch("Delete notify fs")]
        public string SetNotifyToDeletable(int notifyId)
        {
            var notify = _messageMService.GetNotification(notifyId);

            return _messageMService.SetDeletable(notify);
        }

        // Set check and uncheck
        [HttpPatch("Change check fs")]
        public string ChangeNotifyCheck(int notifyId)
        {
            var notify = _messageMService.GetNotification(notifyId);

            return _messageMService.SetChecked(notify);
        }

        // Refresh notification can be achieved by recall the GetUserNotify

        // Access user setting will be demonstrate below

        // Update user setting
        [HttpPatch("Update user setting fs")]
        public string UpdateSetting(int settingId, SettingFrame settingFrame)
        {
            var setting = _settingMService.GetSetting(settingId);

            setting.Description = settingFrame.Description;

            return _settingMService.UpdateSetting(setting);
        }

        // Change the notification setting
        [HttpPatch("Change user notify setting fs")]
        public string ChangenNotifySetting(int settingId)
        {
            var setting = _settingMService.GetSetting(settingId);

            return _settingMService.NotitfySetting(setting);
        }

        // 3. Send help
        // Show all request
        [HttpGet("Show requests fs")]
        public List<Request> GetRequests()
        {
            return _messageMService.GetRequests();
        }

        // Make a request
        [HttpPost("Make request fs")]
        public async Task<string> CreateRequest(RequestFrame requestFrame)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return "No user name!";

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return "No user like this!";

            var request = new Request
            {
                Header = requestFrame.Header,
                Content = requestFrame.Content,
                SenderId = user.Id
            };

            return _messageMService.CreateRequest(request);
        }

        // 4. Account Management
        // Get User setting include account info
        [HttpGet("Get user setting fs")]
        public Setting GetUserSetting(string userId)
        {
            return _settingMService.GetUserSetting(userId);
        }

        // Change or Upload portrait picture
        [HttpPost("Change portrait fs")]
        public async Task<string> ChangePortrait(int settingId, IFormFile portrait)
        {
            var setting = _settingMService.GetSetting(settingId);

            return await _settingMService.ChangePotraitSetting(setting, portrait);
        }

        // Delete portrait picture
        [HttpDelete("Delete portrait fs")]
        public string DeletePortrait(int settingId)
        {
            var setting = _settingMService.GetSetting(settingId);

            return _settingMService.DeleteSetting(setting);
        }

        // Change and save password
        [HttpPatch("Change password fs")]
        public async Task<string> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _authService.GetApplicationUserByIdAsync(userId);

            if (user == null)
                return "No user!";

            var result = await _authService.ChangePassword(user, oldPassword, newPassword);

            return result;
        }

        // If we want to cancel, we can alway reload the page.*/
    }
}

using LMSystem.Models;
using LMSystem.Repository;
using LMSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using System.Xml.Linq;

namespace LMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IFileMService _fileMService;
        private readonly IClassMService _classMService;
        private readonly ISubjectMService _subjectMService;
        private readonly IMessageMService _messageMService;
        private readonly ISettingMService _settingMService;

        public TeacherController
        (
            IAuthService authService,
            IFileMService fileMService,
            IClassMService classMService,
            ISubjectMService subjectMService,
            IMessageMService messageMService,
            ISettingMService settingMService
        )
        {
            _authService = authService;
            _fileMService = fileMService;
            _classMService = classMService;
            _subjectMService = subjectMService;
            _messageMService = messageMService;
            _settingMService = settingMService;
        }
        
        // 1. Teaching Subject Management
        // Show all subject
        [HttpGet("Get all subject")]
        public List<Subject> GetAllSubjects()
        {
            return _subjectMService.GetSubjects();
        }

        // Search for subject
        [HttpGet("Search for subject")]
        public List<Subject> GetSearchSubjects(string searchS)
        {
            return _subjectMService.GetSearchSubjects(searchS);
        }

        // Filter subject list will be demonstrated in later part

        // View subject detail
        [HttpGet("Get subject detail")]
        public Subject GetDetailSubject(int subjectId)
        {
            return _subjectMService.GetDetailSubject(subjectId);
        }

        // Open edit can be count as a preparing step for edit and won't be appear

        // Edit subject
        [HttpPatch("Edit subject")]
        public string UpdateSubject(int subjectId, SubjectFrame subjectFrame)
        {
            var subject = _subjectMService.GetSubject(subjectId);

            subject.Name = subjectFrame.Name;
            subject.Description = subjectFrame.Description;

            return _subjectMService.UpdateSubject(subject);
        }

        // Edit topics
        [HttpPost("Create topic")]
        public string CreateTopic(TopicFrame topicFrame)
        {
            var topic = new Topic
            {
                Name = topicFrame.Name,
                Description = topicFrame.Description,
                SubjectId = topicFrame.SubjectId
            };

            return _subjectMService.CreateTopic(topic);
        }

        [HttpPatch("Update topic")]
        public string UpdateTopic(int topicId, TopicFrame topicFrame)
        {
            var topic = _subjectMService.GetTopic(topicId);

            topic.Name = topicFrame.Name;
            topic.Description = topicFrame.Description;
            topic.SubjectId = topicFrame.SubjectId;

            return _subjectMService.UpdateTopic(topic);
        }

        [HttpDelete("Delete topic")]
        public string DeleteTopic(int topicId)
        {
            var topic = _subjectMService.GetTopic(topicId);

            return _subjectMService.DeleteTopic(topic);
        }

        // Complete and send for approval is already achieved since every material created need to wait for approval

        // Cancel delete is achiable through reload the page

        // View lectures
        [HttpGet("Get subject lectures")]
        public List<Lecture> GetSubjectLectures(int subjectId)
        {
            return _classMService.GetSubjectLectures(subjectId);
        }

        // View materials
        [HttpGet("Get subject materials")]
        public List<Material> GetSubjectMaterials(int subjectId)
        {
            return _fileMService.GetSubjectMaterials(subjectId);
        }

        // Edit material
        [HttpPost("Update material file")]
        public async Task<IActionResult> UpdateMaterialFile(int subjectId, IFormFile updateFile)
        {
            var material = _fileMService.GetMaterial(subjectId);

            var result = await _fileMService.UpdateMaterialFile(updateFile, material);

            return Content(result);
        }

        // Add new material
        [HttpPost("Upload material")]
        public async Task<IActionResult> UploadMaterial(IFormFile formFile, FileFrame fileFrame, int subjectId)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return Content("No user name!");

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return Content("No user like this!");

            var file = new Models.File
            {
                Name = fileFrame.Name,
                Type = fileFrame.Type,
                LastChangedD = DateTime.Now,
                OwnerId = user.Id
            };

            var material = new Material
            {
                status = "Draft",
                IsApproved = false,
                IsDeleted = false,
                SubjectId = subjectId
            };

            var result = await _fileMService.CreateMaterial(material, formFile, file);

            return Content(result);
        }

        // Assign material to class
        [HttpPost("Assign material to class")]
        public string CreateClassMaterial(int classId, int materialId)
        {
            var classMaterial = new ClassMaterial
            {
                ClassId = classId,
                MaterialId = materialId
            };

            return _classMService.CreateClassMaterial(classMaterial);
        }

        // View list of class
        [HttpGet("Get subject class")]
        public List<Class> GetSubjectClasses(int subjectId)
        {
            return _classMService.GetSubjectClasses(subjectId);
        }

        // View class detail
        [HttpGet("Get class detail")]
        public Class GetDetailClass(int classId)
        {
            return _classMService.GetClass(classId);
        }

        // 2. Lecture and Material Management
        // Show all subject lecture is alreaady demonstrated above

        // Show all subject material is already demonstrated above

        // Upload material has already demonstrated above

        // Download material
        [HttpGet("Download material")]
        public async Task<IActionResult> DownloadMaterial(int materialId)
        {
            var fileInfor = await _fileMService.DownloadMaterial(materialId);

            return File(fileInfor.Bytes, fileInfor.ContentType, fileInfor.FilePath);
        }

        // Search material
        [HttpGet("Search material")]
        public List<Material> SearchMaterial(string searchS)
        {
            return _fileMService.GetSearchedMaterials(searchS);
        }

        // Filter subject
        [HttpGet("Get subject with filter")]
        public List<Subject> GetFilteredSubject(string filterBy, string filterValue)
        {
            switch (filterBy)
            {
                case "Name":
                    return _subjectMService.GetFilterNameSubjects(filterValue);
                case "Status":
                    return _subjectMService.GetFilterStatusSubjects(filterValue);
                case "Teacher":
                    return _subjectMService.GetFilterTeacherSubjects(filterValue);
                default:
                    break;
            }

            return null;
        }

        // View the file before download, will be demonstrated in the later part

        // Change name will also desmonstrated in the later part

        // Delete material
        [HttpDelete("Delete material")]
        public IActionResult DeleteMaterial(int materialId)
        {
            var material = _fileMService.GetMaterial(materialId);

            var result = _fileMService.DeleteMaterial(material);

            return Content(result);
        }

        // Add to subject can be achieve when the file get classifed into a material

        // 3. Exam Management
        // Get all subject exam
        [HttpGet("Get subject exam")]
        public List<Exam> GetSubjectExams(int subjectId)
        {
            return _fileMService.GetSubjectExams(subjectId);
        }

        // Filter exam
        [HttpGet("Filter exam")]
        public List<Exam> GetFilteredExams(string filterBy, string filterValue)
        {
            switch(filterBy)
            {
                case "Subject":
                    var subjectId = int.Parse(filterValue);
                    return _fileMService.GetSubjectExams(subjectId);
                case "Group":
                    return _fileMService.GetSubNameExams(filterValue);
                default:
                    return null;
            }
        }

        // Upload or create Exam
        [HttpPost("Create Exam")]
        public async Task<IActionResult> UploadExam(IFormFile formFile, ExamFrame examFrame)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return Content("No user name!");

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return Content("No user like this!");

            var file = new Models.File
            {
                Name = examFrame.Name,
                Type = examFrame.Type,
                LastChangedD = DateTime.Now,
                OwnerId = user.Id
            };

            var exam = new Exam
            {
                Note = examFrame.Note,
                Format = examFrame.Format,
                Duration = examFrame.Duration,
                status = "Draft",
                IsApproved = false,
                IsDeleted = false,
                SubjectId = examFrame.SubjectId
            };

            var result = await _fileMService.CreateExam(exam, formFile, file);

            return Content(result);
        }

        // Search for exam
        [HttpGet("Search exam")]
        public List<Exam> GetSearchExams(string searchS)
        {
            return _fileMService.GetSearchExams(searchS);
        }

        // Exam's detail
        [HttpGet("Get detail exam")]
        public Exam GetDetailExam(int examId)
        {
            return _fileMService.GetDetailExam(examId);
        }

        // Change name
        [HttpGet("Change file name")]
        public IActionResult ChangeFileName(int fileId, string newName)
        {
            var file = _fileMService.GetFile(fileId);

            var result = _fileMService.ChangeFileName(file, newName);

            return Content(result);
        }

        // Send for approval
        [HttpPatch("Send for approval")]
        public IActionResult SendForApproval(int examId)
        {
            var exam = _fileMService.GetExam(examId);

            exam.status = "Waiting";

            var result = _fileMService.UpdateExam(exam);

            return Content(result);
        }

        // Delete Exam
        [HttpDelete("Delete exam")]
        public IActionResult DeleteExam(int examId)
        {
            var exam = _fileMService.GetExam(examId);

            var result = _fileMService.DeleteExam(exam);

            return Content(result);
        }

        // Get all Exam
        [HttpGet("Get all exam")]
        public List<Exam> GetAllExams()
        {
            return _fileMService.GetExams();
        }

        // See the file before download
        [HttpGet("Show file")]
        public IFormFile ShowFile(int fileId)
        {
            var file = _fileMService.GetFile(fileId);

            return _fileMService.ShowFile(file);
        }

        // 4. Notification Management
        // Get all user notification
        [HttpGet("Get user notify")]
        public List<Notification> GetUserNotify(string userId)
        {
            return _messageMService.GetAllUserNotify(userId);
        }

        // Search for notification
        [HttpGet("Search notify")]
        public List<Notification> SearchNotify(string searchS)
        {
            return _messageMService.GetSearchNotify(searchS);
        }

        // Delete notification
        [HttpPatch("Delete notify")]
        public string SetNotifyToDeletable(int notifyId)
        {
            var notify = _messageMService.GetNotification(notifyId);

            return _messageMService.SetDeletable(notify);
        }

        // Set check and uncheck
        [HttpPatch("Change check")]
        public string ChangeNotifyCheck(int notifyId)
        {
            var notify = _messageMService.GetNotification(notifyId);

            return _messageMService.SetChecked(notify);
        }

        // Refresh notification can be achieved by recall the GetUserNotify

        // Access user setting will be demonstrate below

        // Update user setting
        [HttpPatch("Update user setting")]
        public string UpdateSetting(int settingId, SettingFrame settingFrame)
        {
            var setting = _settingMService.GetSetting(settingId);

            setting.Description = settingFrame.Description;

            return _settingMService.UpdateSetting(setting);
        }

        // Change the notification setting
        [HttpPatch("Change user notify setting")]
        public string ChangenNotifySetting(int settingId)
        {
            var setting = _settingMService.GetSetting(settingId);

            return _settingMService.NotitfySetting(setting);
        }

        // 5. Send help
        // Show all request
        [HttpGet("Show requests")]
        public List<Request> GetRequests()
        {
            return _messageMService.GetRequests();
        }

        // Make a request
        [HttpPost("Make request")]
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

        // 6. Account Management
        // Get User setting include account info
        [HttpGet("Get user setting")]
        public Setting GetUserSetting(string userId)
        {
            return _settingMService.GetUserSetting(userId);
        }

        // Change or Upload portrait picture
        [HttpPost("Change portrait")]
        public async Task<string> ChangePortrait(int settingId, IFormFile portrait)
        {
            var setting = _settingMService.GetSetting(settingId);

            return await _settingMService.ChangePotraitSetting(setting, portrait);
        }

        // Delete portrait picture
        [HttpDelete("Delete portrait")]
        public string DeletePortrait(int settingId)
        {
            var setting = _settingMService.GetSetting(settingId);

            return _settingMService.DeleteSetting(setting);
        }

        // Change and save password
        [HttpPatch("Change password")]
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

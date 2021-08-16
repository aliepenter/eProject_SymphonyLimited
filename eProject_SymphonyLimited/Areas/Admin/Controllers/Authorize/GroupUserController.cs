using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.Authorize;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers.Authorize
{
    [CustomizeAuthorize]
    public class GroupUserController : BaseController
    {
        SymphonyLimitedDBContext db;

        public GroupUserController()
        {
            db = new SymphonyLimitedDBContext();
        }

        // GET: Admin/GroupUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GrantPermission()
        {
            ViewBag.GroupUser = new SelectList(db.GroupUser.AsEnumerable(), "EntityId", "Name");
            ViewBag.Business = new SelectList(db.Business.AsEnumerable(), "EntityId", "Name");
            return View();
        }

        public ActionResult GetPermission(int groupId, string businessId)
        {
            // Lấy các permission 
            var _permission = db.Permission.AsEnumerable().Where(x => x.BusinessId.Equals(businessId))
                                .Select(x => new PermissionViewModel
                                {
                                    EntityId = x.EntityId,
                                    BusinessId = x.BusinessId,
                                    Description = x.Description,
                                    IsGranted = false,
                                    IsAdmin = false,
                                    Name = x.Name
                                }).ToList();

            // Kiểm tra xem quyền nào đã gán
            foreach (var item in _permission)
            {
                // Kiểm tra xem có phải là admin không
                if (db.GroupUser.SingleOrDefault(x => x.EntityId == groupId).IsAdmin)
                {
                    item.IsAdmin = true;
                    item.IsGranted = true;
                }
                else
                {
                    if (db.GroupPermission.AsNoTracking().Any(x => x.GroupId == groupId && x.PermissionId == item.EntityId)) // Check trong bảng GroupPermission xem quyền đã được gán hay chưa
                    {
                        item.IsGranted = true;
                    }
                }
            }
            return Json(_permission, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePermission(int grId, int perId)
        {
            var groupPermission = db.GroupPermission.AsEnumerable().SingleOrDefault(x => x.GroupId == grId && x.PermissionId == perId);
            string msg = "";
            if (groupPermission == null) // Chưa gán
            {
                db.GroupPermission.Add(new GroupPermission { GroupId = grId, PermissionId = perId });
                db.SaveChanges();
                msg = "Gán quyền thành công!";
            }
            else // Gán rồi
            {
                db.GroupPermission.Remove(groupPermission);
                db.SaveChanges();
                msg = "Hủy quyền thành công!";
            }
            return Json(new { msg = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}
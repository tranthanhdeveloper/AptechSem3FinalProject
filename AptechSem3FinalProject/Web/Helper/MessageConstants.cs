using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helper
{
    public class MessageConstants
    {
        public static string InsertSuccess = "Insert Successful";

        public static string NotFound = "Not Found";

        public static string InsertFail = "Insert Fail";

        public static string UpdateFail = "Update Fail";

        public static string DeleteFail = "Delete Fail";

        public static string LoginFail = "Login Fail";

        public static string OrderSuccess = "Order Success";

        public static string OrderFail = "Order Fail";

        public static string AccountExist = "Identity or Phone is already existed";

        public static string ImageRequired = "Course image is required";

        public static string EditCourseDeny = "Your do not have permistion to Edit this course";

        public static string PublishCourseDenied = "At least one module and video to publish a course";

        public static string ModuleNotFound = "Your module not found to add lesson";
    }
}
using System.Linq;
using System.Web.Mvc;
using Service.Service;


namespace Web.Controllers
{
    public class CourseController : Controller
    {
        #region Declare field and Constructors

        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        #region Action methods

        // GET: Course
        public ActionResult Index()
        {
            var a = _courseService.GetAll();
            return View();
        }

        public ActionResult CourseDetail(int id)
        {
            return View();
        }

        public ActionResult CoursePlay(int id)
        {
            return View();
        }

        #endregion

        #region manipulate methods

        

        #endregion


    }
}
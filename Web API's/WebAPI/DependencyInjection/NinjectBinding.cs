using BusinessLogic;
using DataLayer;
using Ninject.Modules;

namespace DependencyInjection
{
    public class NinjectBinding: NinjectModule
    {
        public override void Load()
        {
            Bind<IContext>().To<Context>();
            Bind<IUserLogic>().To<UserLogic>();
            Bind<IStudentLogic>().To<StudentLogic>();
            Bind<ICourseLogic>().To<CourseLogic>();

        }
    }
}

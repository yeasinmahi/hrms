using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Microsoft.VisualStudio.TeamSystem.Data.UnitTesting;

namespace GITS.Hrms.Library.Data.Entity
{
    [TestClass()]
    public class DatabaseSetup
    {

        [AssemblyInitialize()]
        public static void IntializeAssembly(TestContext ctx)
        {
            //   Setup the test database based on setting in the
            // configuration file
           // DatabaseTestClass.TestService.DeployDatabaseProject();
          //  DatabaseTestClass.TestService.GenerateData();
        }

    }
}

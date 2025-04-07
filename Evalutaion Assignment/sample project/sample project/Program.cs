using sample_project.dao;
using sample_project.MainModule;

namespace sample_project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            ICourierUserService courierService = new CourierUserServiceImpl();
            ICourierAdminService adminService = new CourierAdminServiceImpl();

            // Create and display menu
            CourierManagementMenu menu = new CourierManagementMenu(courierService, adminService);
            menu.DisplayMenu();
        }
    }
}
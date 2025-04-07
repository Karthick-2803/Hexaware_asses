using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_project.model;

namespace sample_project.dao
{
    public interface ICourierAdminService
    {
        int addCourierStaff(Employee obj);
    }
}

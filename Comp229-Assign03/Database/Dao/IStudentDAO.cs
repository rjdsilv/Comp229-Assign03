using Comp229_Assign03.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comp229_Assign03.Database.Dao
{
    /// <summary>
    /// Interface containing all the usable methods for the EnrollmentDAO object.
    /// </summary>
    public interface IStudentDAO : IGenericDAO<Student>
    {
        void DeleteStudentAndDependencies(Student student);
    }
}
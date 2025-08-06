using Newtonsoft.Json;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Common.Helpers
{
    public static class BackofficeHelper
    {
        public static string GetEmailSenderEmail(string name)
        {
            var splits = name.Split(".");
            if (splits.Length > 1)
            {
                return $"{splits[0]}@SchoolSaas.io";
            }
            return $"{name}@SchoolSaas.io";
        }

        /// <summary>
        /// For Simple Objects 
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool ObjectComparer(object o1, object o2, Type type)
        {
            if (o1 == null && o2 == null) return true;
            if (ReferenceEquals(o1, o2)) return true;
            if ((o1 == null) || (o2 == null)) return false;
            if (o1.GetType() != o2.GetType()) return false;

            //properties: int, double, DateTime, etc, not class
            //String is considered as a class
            if (!o1.GetType().IsClass) return o1.Equals(o2);

            var result = true;
            foreach (var property in o1.GetType().GetProperties())
            {
                var objValue = property.GetValue(o1);
                var anotherValue = property.GetValue(o2);
                //Recursion
                return DeepCompare(objValue, anotherValue, type);
            }
            return result;
        }
        /// <summary>
        /// (Nested)For Simple Objects
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static bool DeepCompare(object obj, object another, Type type)
        {


            if (obj == null && another == null) return true;
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            if (typeof(BaseEntity<Guid>).IsAssignableFrom(type))
            {
                var auditableObj = obj as BaseEntity<Guid>;
                var auditableAnother = another as BaseEntity<Guid>;
                auditableAnother.Created = auditableObj.Created;
                auditableAnother.CreatedBy = auditableObj.CreatedBy;
                auditableAnother.LastModifiedBy = auditableObj.LastModifiedBy;
                auditableAnother.LastModified = auditableObj.LastModified;
                auditableAnother.Id = auditableObj.Id;
                auditableAnother.RowVersion = auditableObj.RowVersion;
                auditableAnother.IsDeleted = auditableObj.IsDeleted;
                auditableAnother.Uuid = auditableObj.Uuid;

                another = auditableAnother;
            }
            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                if (objValue == null && anotherValue == null)
                    continue;
                if ((objValue == null && anotherValue != null) || (objValue != null && anotherValue == null))
                {
                    result = false;
                    continue;

                }
                if (!objValue.ToString().Equals(anotherValue.ToString()))
                    result = false;
            }

            return result;
        }

        /// <summary>
        /// For Complex Object
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool JsonObjectComparer(object o1, object o2)
        {
            if (o1 == null && o2 == null) return true;
            if (ReferenceEquals(o1, o2)) return true;
            if ((o1 == null) || (o2 == null)) return false;
            if (o1.GetType() != o2.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(o1);
            var anotherJson = JsonConvert.SerializeObject(o2);

            return string.Equals(objJson, anotherJson, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}

using System.Reflection;
using REST.Core.Models;

namespace REST.Core.Utils
{
    public class GenericCollectionOperations<T>
    {
        public static int GetMaxId(List<IId> objects)
        {
            var sameTypeObjects = objects.OfType<T>().ToList();

            List<int> existingIds = new List<int>();

            foreach (var obj in sameTypeObjects)
            {
                Type type = obj.GetType();

                PropertyInfo[] props = type.GetProperties();

                foreach (var prop in props)
                {
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        if (prop.Name.Equals("Id"))
                        {
                            existingIds.Add((int)(prop.GetValue(obj) ?? 0));
                        }
                    }
                }
            }

            return existingIds.Any() ? existingIds.Max() : 0;
        }

        public static T? GetById(List<IId> objects, int id)
        {
            List<T> sameTypeObjects = objects.OfType<T>().ToList();

            foreach (var obj in sameTypeObjects)
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();

                foreach (var prop in props)
                {
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        if (prop.Name.ToLower().Equals("id"))
                        {
                            if (prop.GetValue(obj).Equals(id))
                            {
                                return obj;
                            }
                        }
                    }
                }
            }

            return default(T);
        }

        public static int? GetObjectId(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();

            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    if (prop.Name.ToLower().Equals("id"))
                    {
                        return (int?)prop.GetValue(obj);
                    }
                }
            }

            return null;
        }

        public static void UpdateObjectId(T t, int newId)
        {
            Type type = t.GetType();
            PropertyInfo[] props = type.GetProperties();

            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    if (prop.Name.ToLower().Equals("id"))
                    {
                        prop.SetValue(t, newId);
                        break;
                    }
                }
            }
        }
    }
}

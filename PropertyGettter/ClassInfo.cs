using System;

namespace PropertyGettter
{
    public class ClassInfo
    {


        [Flags]
        public enum ClassAttribute : short
        {
            Multi = 0,
            Get = 1,
            Post = 2,
            Patch = 4,
            Delete = 8,
        }
        public string Name { get; set; }
        public ClassAttribute Attributes { get; set; }

        public ClassInfo(string name, ClassAttribute attribute)
        {
            this.Name = name;
            this.Attributes = attribute;
        }

        public ClassInfo(string name)
        {
            this.Name = name;
            this.Attributes = (ClassAttribute.Delete | ClassAttribute.Get | ClassAttribute.Multi |
                               ClassAttribute.Patch | ClassAttribute.Post);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SweetMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweetMapperTests
{
    [TestClass()]
    public class MismatchTypeTests
    {
        [TestMethod()]
        public void NoMatchMapTest()
        {
            SweetMapper<SourceClass, TargetClass>.ClearConfig();
            SourceClass a = new SourceClass
            {
                Name = "abc",
                DoTime = DateTime.Now
            };
            TargetClass b = SweetMapper<SourceClass, TargetClass>.Map(a);
            Assert.IsTrue(b.DoTime == "" && b.Name == "abc");
        }

        private class SourceClass
        {
            public string Name { get; set; }
            public DateTime DoTime { get; set; }
        }
        private class TargetClass
        {
            public string Name { get; set; }
            public string DoTime { get; set; } = "";
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SweetMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweetMapperTests
{
    [TestClass()]
    public class ConfigTests
    {
        [TestMethod()]
        public void AutoMapTest()
        {
            SweetMapper<SourceClass, TargetClass>.SetConfig((source, target) =>
            {
                target.DoTime = source.DoTime.ToString("yyyy-MM-dd");
            });
            DateTime now = new DateTime(2019, 2, 14);
            SourceClass a = new SourceClass
            {
                Name = "abc",
                DoTime = now
            };
            TargetClass b = SweetMapper<SourceClass, TargetClass>.Map(a);
            Assert.IsTrue(b.DoTime == now.ToString("yyyy-MM-dd") && b.Name == "abc");
        }

        [TestMethod()]
        public void DisableAutoMapTest()
        {
            SweetMapper<SourceClass, TargetClass>.SetConfig((source, target) =>
            {
                target.DoTime = source.DoTime.ToString("yyyy-MM-dd");
            }, true); 
            DateTime now = DateTime.Now;
            SourceClass a = new SourceClass
            {
                Name = "abc",
                DoTime = now
            };
            TargetClass b = SweetMapper<SourceClass, TargetClass>.Map(a);
            Assert.IsTrue(b.DoTime == now.ToString("yyyy-MM-dd") && b.Name == "");
        }

        [TestMethod()]
        public void RemoveConfigTest()
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
            public string Name { get; set; } = "";
            public string DoTime { get; set; } = "";
        }
    }
}

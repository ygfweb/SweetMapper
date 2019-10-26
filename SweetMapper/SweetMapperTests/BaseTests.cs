using Microsoft.VisualStudio.TestTools.UnitTesting;
using SweetMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweetMapper.Tests
{
    [TestClass()]
    public class BaseTests
    {
        [TestMethod()]
        public void ObjectMapTest()
        {
            DateTime now = DateTime.Now;
            SourceClass a = new SourceClass
            {
                Name = "abc",
                Score = 10,
                DoTime = now
            };
            TargetClass b = SweetMapper<SourceClass, TargetClass>.Map(a);
            Assert.IsTrue(b.DoTime == now && b.Name == "abc" && b.Score == 10);
        }

        [TestMethod()]
        public void ListMapTest()
        {
            DateTime now = DateTime.Now;
            List<SourceClass> aList = new List<SourceClass>();
            aList.Add(new SourceClass()
            {
                Name = "aaa",
                Score = 1,
                DoTime = now
            });
            aList.Add(new SourceClass()
            {
                Name = "bbb",
                Score = 2,
                DoTime = now
            });
            List<TargetClass> bList = SweetMapper<SourceClass, TargetClass>.MapList(aList);
            Assert.IsTrue(bList != null && bList.Count == 2 && bList[0].Name == "aaa" && bList[1].Name == "bbb");
        }

        [TestMethod()]
        public void ArrayMapTest()
        {
            DateTime now = DateTime.Now;
            SourceClass[] aArray = new SourceClass[2];
            aArray[0] = new SourceClass
            {
                Name = "aaa",
                Score = 1,
                DoTime = now
            };
            aArray[1] = new SourceClass
            {
                Name = "bbb",
                Score = 2,
                DoTime = now
            };
            TargetClass[] bArray = SweetMapper<SourceClass, TargetClass>.MapArray(aArray);
            Assert.IsTrue(bArray != null && bArray.Length == 2 && bArray[0].Name == "aaa" && bArray[1].Name == "bbb");
        }

        [TestMethod()]
        public void NullObjectTest()
        {
            TargetClass b = SweetMapper<SourceClass, TargetClass>.Map(null);
            Assert.IsTrue(b == null);
        }

        [TestMethod()]
        public void NullListTest()
        {
            List<TargetClass> bList = SweetMapper<SourceClass, TargetClass>.MapList(null);
            Assert.IsTrue(bList == null);
        }

        [TestMethod()]
        public void NullItemTest()
        {
            DateTime now = DateTime.Now;
            List<SourceClass> aList = new List<SourceClass>();
            aList.Add(new SourceClass()
            {
                Name = "aaa",
                Score = 1,
                DoTime = now
            });
            aList.Add(null);
            List<TargetClass> bList = SweetMapper<SourceClass, TargetClass>.MapList(aList);
            Assert.IsTrue(bList != null && bList.Count == 2 && bList[0].Name == "aaa" && bList[1] == null);
        }

        private class SourceClass
        {
            public string Name { get; set; }
            public decimal Score { get; set; }
            public DateTime DoTime { get; set; }
        }
        private class TargetClass
        {
            public string Name { get; set; }
            public decimal Score { get; set; }
            public DateTime DoTime { get; set; }
        }
    }
}




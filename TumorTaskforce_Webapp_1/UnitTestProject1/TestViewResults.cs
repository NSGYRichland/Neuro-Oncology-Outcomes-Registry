using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace UnitTestProject1.Controller
{
    class TestUserAuthentication
    {
        public interface IMock
        {
            List<Users> GetUser();
        }
        [Test]
        public void AuthenticateValidUser()
        {
            IMock mockData = new MockDataAccess();
            var service = new AuthenticationManager(mockData);
        }
        
    }
}

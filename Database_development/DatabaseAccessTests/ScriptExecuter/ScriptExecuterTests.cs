using NUnit.Framework;
using DatabaseAccess.ScriptExecuter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.ScriptExecuter.Tests
{
    [TestFixture]
    public class ScriptExecuterTests
    {
        [Test]
        public void ExecuteScriptTest()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\Database_development\DatabaseAccess\Session.mdf;Integrated Security=True";
            var filePath = @"../../../AddTest.sql";
            ScriptExecuter.ExecuteScript(filePath, connectionString);
        }
    }
}
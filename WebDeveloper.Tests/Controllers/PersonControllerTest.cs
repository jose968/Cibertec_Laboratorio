using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Areas.Personnel.Controllers;
using Xunit;
using FluentAssertions;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Tests.Controllers
{
    public class PersonControllerTest
    {
        private PersonController controller;

        public PersonControllerTest()
        {
            controller = new PersonController(new BaseRepository<Person>());
        }

        [Fact(DisplayName = "ListActionWithEmptyParameters")]
        public void ListActionWithEmptyParameters()
        {
            var result = controller.List(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_List");

            var modelCount = (IEnumerable<Person>)result.Model;
            modelCount.Count().Should().Be(15);
        }


    }
}

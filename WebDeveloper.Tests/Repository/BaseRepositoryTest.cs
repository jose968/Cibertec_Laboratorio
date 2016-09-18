using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using Xunit;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Moq;
using System.Data.Entity;

namespace WebDeveloper.Tests.Repository
{
    public class BaseRepositoryTest
    {
        private IRepository<Person> repository;

        public BaseRepositoryTest()
        {
            repository = new BaseRepository<Person>();
        }

        [Fact(DisplayName = "AddTestWrongWithMissingData")]
        public void AddTestWrongWithMissingData()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            try
            {
                repository.Add(person);

            }
            catch (Exception exception)
            {
                exception .Source.Should().Be("EntityFramework");
            }

        }
        

        [Fact(DisplayName = "AddTestWrongWithNull")]
        public void AddTestWrongWithNull()
        {
            var person = new Person();
            try
            {
                repository.Add(person);

            }
            catch (Exception exception)
            {
                exception.Should().NotBeNull();
            }


        }

        [Fact(DisplayName = "AddTestWithProperData")]
        public void AddTestWithProperData()
        {
            Person person = TestPersonOK();
            var result = repository.Add(person);
            result.Should().BeGreaterThan(0);
        }

        private static Person TestPersonOK()
        {
            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                ModifiedDate = person.ModifiedDate,
                rowguid = person.rowguid
            };
            return person;
        }

        [Fact(DisplayName = "MockData")]
        public void MockData()
        {
            var personDbSetMock = new Mock<DbSet<Person>>();
            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m=> m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);


            var repository = new BaseRepository<Person>(webContextMock.Object);
            var newPerson = TestPersonOK();

            repository.Add(newPerson);
            personDbSetMock.Verify(p => p.Add(It.IsAny<Person>()), Times.Once);
            webContextMock.Verify(w => w.SaveChanges(), Times.Once);
        }

        [Fact(DisplayName = "MockUpdateTestOk")]
        public void MockDataUpdate()
        {

            var personDbSetMock = new Mock<DbSet<Person>>();

            var persons = PersonList().AsQueryable();

            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());


            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);
            var repository = new BaseRepository<Person>(webContextMock.Object);

            var personToUpdate = repository.GetById(x => x.FirstName == "Name1");
            var result = repository.Update(personToUpdate);
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());

        }

        [Fact(DisplayName = "MockDeleteTestOk")]
        public void DeleteTestOk()
        {
            var personDbSetMock = new Mock<DbSet<Person>>();

            var persons = PersonList().AsQueryable();

            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());

            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);
            var repository = new BaseRepository<Person>(webContextMock.Object);

            var personToUpdate = repository.GetById(x => x.FirstName == "Name1");
            var result = repository.Delete(personToUpdate);
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());

        }

        [Fact(DisplayName = "MockGetByIdTestOk")]
        public void GetByIdTestOk()
        {
            var personDbSetMock = new Mock<DbSet<Person>>();

            var persons = PersonList().AsQueryable();

            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());

            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);
            var repository = new BaseRepository<Person>(webContextMock.Object);

            var person = repository.GetById(x => x.FirstName == "Name1");
            person.Should().NotBeNull();

        }

        [Fact(DisplayName = "MockBaseRepositoryConstructorTest")]
        public void BaseRepositoryConstructorTest()
        {
            var repository = new BaseRepository<Person>();
            repository.Should().NotBeNull();
        }


        [Fact(DisplayName = "MockListByIdOkTest")]
        public void ListByIdOkTest()
        {
            var personDbSetMock = new Mock<DbSet<Person>>();

            var persons = PersonList().AsQueryable();

            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());

            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);
            var repository = new BaseRepository<Person>(webContextMock.Object);

            var result = repository.ListById(x => x.BusinessEntityID == 1);
            result.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact(DisplayName = "MockOrderedListOkTest")]
        public void OrderedListOkTest()
        {
            var personDbSetMock = new Mock<DbSet<Person>>();

            var persons = PersonList().AsQueryable();

            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());

            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);
            var repository = new BaseRepository<Person>(webContextMock.Object);

            var result = repository.OrderedListByDateAndSize(x => x.ModifiedDate, 5);
            result.Count().Should().BeGreaterOrEqualTo(5);
        }

        [Fact(DisplayName = "MockDataList")]
        public void MockDataList()
        {
            var personList = PersonList().AsQueryable();
            var personDbSetMock = new Mock<DbSet<Person>>();

            personDbSetMock.As<IQueryable<Person>>()
                .Setup(m => m.Provider)
                .Returns(personList.Provider);

            personDbSetMock.As<IQueryable<Person>>()
                .Setup(m => m.Expression)
                .Returns(personList.Expression);

            personDbSetMock.As<IQueryable<Person>>()
                .Setup(m => m.ElementType)
                .Returns(personList.ElementType);


            personDbSetMock.As<IQueryable<Person>>()
                .Setup(m => m.GetEnumerator())
                .Returns(personList.GetEnumerator());

            var webContextMock = new Mock<WebContextDb>();

            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);

            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);
            var repository = new BaseRepository<Person>(webContextMock.Object);


            var personGetByID = repository.
                GetById(p => p.FirstName == "Name1");
            personGetByID.Should().NotBeNull();

        }

        private IEnumerable<Person> PersonList()
        {
            return Enumerable.Range(1, 10).Select(i => new Person
            {
                BusinessEntityID = i,
                PersonType = "SC",
                FirstName = $"Name{i}",
                LastName = $"LastNamei{i}",
                ModifiedDate = DateTime.Now

            });
        }


    }
}

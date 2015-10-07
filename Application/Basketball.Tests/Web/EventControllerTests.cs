using NUnit.Framework;
using Basketball.Domain.Entities;
using Rhino.Mocks;
using System.Web.Mvc;
using System.Collections.Generic;
using Basketball.Common.Resources;
using System;
using MvcContrib.TestHelper;
using Basketball.Service;
using Basketball.Service.Interfaces;
using Basketball.Web.Areas.Admin.Controllers;

//http://johan.driessen.se/posts/testing-dataannotation-based-validation-in-asp.net-mvc
//http://stackoverflow.com/questions/2167811/unit-testing-asp-net-dataannotations-validation
namespace Basketball.Tests.Web
{
    [TestFixture]
    public class EventControllerTests
    {
        private EventController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new EventController(CreateMockEventService());
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
        }
        #region Test Init Methods


        private IEventService CreateMockEventService()
        {
            MockRepository mocks = new MockRepository();

            IEventService mockedEventService = mocks.StrictMock<IEventService>();

            Expect.Call(mockedEventService.Get())
                .Return(ControllerTestsUtil.CreateEventList());

            Expect.Call(mockedEventService.Get(1))
                .IgnoreArguments()
                .Return(ControllerTestsUtil.CreateEvent());

            Expect.Call(delegate { mockedEventService.Insert(ControllerTestsUtil.CreateEvent()); })
                .IgnoreArguments();

            Expect.Call(delegate { mockedEventService.Update(ControllerTestsUtil.CreateEvent()); })
                .IgnoreArguments();

            Expect.Call(delegate { mockedEventService.Delete(ControllerTestsUtil.CreateEvent()); })
                .IgnoreArguments();

            Expect.Call(mockedEventService.Commit())
                .IgnoreArguments()
                .Return(1);

            mocks.Replay(mockedEventService);

            return mockedEventService;

        ////    IExtendedRepository<Event> mockedRepository = mocks.StrictMock<IExtendedRepository<Event>>();
        ////    Expect.Call(mockedRepository.GetAll("Date", false))
        ////        .Return(ControllerTestsUtil.CreateEventList());
        //    Expect.Call(mockedRepository.Get(1)).IgnoreArguments()
        //        .Return(ControllerTestsUtil.CreateEvent());
        //    Expect.Call(mockedRepository.SaveOrUpdate(null)).IgnoreArguments()
        //        .Return(ControllerTestsUtil.CreateEvent());
        //    Expect.Call(delegate { mockedRepository.Delete(null); }).IgnoreArguments();

        //    IDbContext mockedDbContext = mocks.StrictMock<IDbContext>();
        //    Expect.Call(delegate { mockedDbContext.CommitChanges(); });
        //    SetupResult.For(mockedRepository.DbContext).Return(mockedDbContext);

        //    mocks.Replay(mockedRepository);

        //    return mockedRepository;
        }

       
        #endregion

        #region Tests
        [Test]
        public void CanInitEventCreation()
        {
            ViewResult result = controller.Create().AssertViewRendered();

            Assert.That(result.ViewData.Model as Event, Is.Null);
        }
        [Test]
        public void CanCreateEvent()
        {
            Event eventFromForm = new Event("Title", "Desc", DateTime.Now);
            eventFromForm.StartTime = "11:00";
            eventFromForm.EndTime = "13:00";
            eventFromForm.Notes = "Some notes";

            RedirectToRouteResult redirectResult = controller.Create(eventFromForm)
                .AssertActionRedirect().ToAction("Index");
            Assert.That(controller.TempData[FormMessages.MessageTypeSuccess].ToString(), Is.EqualTo(FormMessages.SaveSuccess));
        }

        [Test]
        public void CannotCreateEventInvalidStartTime()
        {
            Event eventFromForm = new Event("Title", "Desc", DateTime.Now);
            eventFromForm.StartTime = "2:59";
            controller.ModelState.AddModelError("StartTime", FormMessages.FieldTwentyFourHour);

            ViewResult result = controller.Create(eventFromForm).AssertViewRendered();
            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result.ViewData.ModelState["StartTime"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldTwentyFourHour));
        }

        [Test]
        public void CannotCreateEventInvalidEndTime()
        {
            Event eventFromForm = new Event("Title", "Desc", DateTime.Now);
            eventFromForm.EndTime = "02:60";
            controller.ModelState.AddModelError("EndTime", FormMessages.FieldTwentyFourHour);

            ViewResult result = controller.Create(eventFromForm).AssertViewRendered();
            Assert.That(result.ViewData.ModelState["EndTime"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldTwentyFourHour));
            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
        }

        [Test]
        public void CannotCreateInvalidEventItem()
        {
            Event eventFromForm = new Event();
            controller.ModelState.AddModelError("Description", FormMessages.FieldMandatory);
            controller.ModelState.AddModelError("Title", FormMessages.FieldMandatory);


            ViewResult result = controller.Create(eventFromForm).AssertViewRendered();
            Assert.That(result.ViewData.Model as Event, !Is.Null);
            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result.ViewData.ModelState["Description"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldMandatory));
            Assert.That(result.ViewData.ModelState["Title"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldMandatory));
        }

        [Test]
        public void CanListEvent()
        {
            //ViewResult result = controller.Index().AssertViewRendered();


            ViewResult result = controller.Index() as ViewResult;
            
            result.AssertViewRendered();

            Assert.That(result.ViewData.Model as List<Event>, Is.Not.Null);
            Assert.That((result.ViewData.Model as List<Event>).Count, Is.EqualTo(2));
        }

        [Test]
        public void CanInitEventEdit()
        {
            ViewResult result = controller.Edit(1).AssertViewRendered();

            Assert.That(result.ViewData.Model as Event, Is.Not.Null);
            Assert.That((result.ViewData.Model as Event).Id, Is.EqualTo(1));
        }

        [Test]
        public void CanUpdateEvent()
        {
            Event eventFromForm = new Event("mysubject", "mymessage", DateTime.Now);
            RedirectToRouteResult redirectResult = controller.Edit(eventFromForm)
                .AssertActionRedirect().ToAction("Index");
            Assert.That(controller.TempData[FormMessages.MessageTypeSuccess], Is.EqualTo(FormMessages.SaveSuccess));
        }

        [Test]
        public void CannotUpdateEventInvalidStartTime()
        {
            Event eventFromForm = new Event("Title", "Desc", DateTime.Now);
            eventFromForm.StartTime = "2:00";
            controller.ModelState.AddModelError("StartTime", FormMessages.FieldTwentyFourHour);

            ViewResult result = controller.Edit(eventFromForm).AssertViewRendered();
            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result.ViewData.ModelState["StartTime"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldTwentyFourHour));
        }

        [Test]
        public void CannotUpdateEventInvalidEndTime()
        {
            Event eventFromForm = new Event("Title", "Desc", DateTime.Now);
            eventFromForm.EndTime = "02:60";
            controller.ModelState.AddModelError("EndTime", FormMessages.FieldTwentyFourHour);

            ViewResult result = controller.Edit(eventFromForm).AssertViewRendered();
            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result.ViewData.ModelState["EndTime"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldTwentyFourHour));
        }

        [Test]
        public void CannotUpdateInvalidEvent()
        {
            Event eventFromForm = new Event();
            controller.ModelState.AddModelError("Title", FormMessages.FieldMandatory);

            ViewResult result = controller.Edit(eventFromForm).AssertViewRendered();
            Assert.That(result.ViewData.Model as Event, !Is.Null);
            Assert.That(result.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result.ViewData.ModelState["Title"].Errors[0].ErrorMessage, Is.EqualTo(FormMessages.FieldMandatory));
        }

        [Test]
        public void CanDeleteEvent()
        {
            Event eventFromForm = new Event("mysubject", "mymessage", DateTime.Now);
            RedirectToRouteResult redirectResult = controller.Delete(eventFromForm.Id)
                .AssertActionRedirect().ToAction("Index");
            Assert.That(controller.TempData[FormMessages.MessageTypeSuccess], Is.EqualTo(FormMessages.DeleteSuccess));
        }

        #endregion


    }
}

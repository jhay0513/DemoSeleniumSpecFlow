using BoDi;
using DemoSeleniumSpecFlow.DataModel;
using DemoSeleniumSpecFlow.Drivers;
using DemoSeleniumSpecFlow.Pages;
using DemoSeleniumSpecFlow.Support;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace DemoSeleniumSpecFlow.StepDefinitions
{
    
    public class BaseStepDefinitions
    {
        protected readonly LoginPage loginPage;
        protected readonly ContactListPage contactListPage;
        protected readonly AddEditContactPage addEditContactPage;
        protected readonly ContactDetailsPage contactDetailsPage;
        protected readonly IWebDriver webDriver;
        protected CleanUpModel cleanupData;

        public BaseStepDefinitions(IObjectContainer container) 
        {
            BrowserDriver browserDriver = container.Resolve<BrowserDriver>();
            CleanUpModel cleanUpModel = container.Resolve<CleanUpModel>();

            webDriver = browserDriver.Current;
            loginPage = new LoginPage(browserDriver.Current);
            contactListPage = new ContactListPage(browserDriver.Current);
            addEditContactPage = new AddEditContactPage(browserDriver.Current);
            contactDetailsPage = new ContactDetailsPage(browserDriver.Current);

            cleanupData = cleanUpModel;
        }

        [Given(@"user is logged in as (.*)")]
        public void GivenUserIsLoggedInAs(string user)
        {
            //Login
            string loginDetailsPath = Path.Combine(Util.GetProjectPath(), Util.GetAppSetting("LoginDetailsPath"));
            var loginDetails = JsonConvert.DeserializeObject<LoginModel>(File.ReadAllText(loginDetailsPath));
            var login = loginDetails.Logins.FirstOrDefault(x => x.Email.Contains(user));
            loginPage.Login(login.Email, login.Password);

            // Save Token for cleanup later
            contactListPage.WaitTableVisible();
            string token = webDriver.Manage().Cookies.GetCookieNamed("token").ToString();
            cleanupData.Token = (token.Split(';'))[0].Replace("token=", "");
        }

        [When(@"contact has the following details")]
        public void WhenContactHasTheFollowingDetails(Table table)
        {
            ContactDetailsModel contactDetails = table.CreateInstance<ContactDetailsModel>();

            addEditContactPage.ClearAllTextBox();
            addEditContactPage.EnterFirstNameTextBox(contactDetails.FirstName);
            addEditContactPage.EnterLastNameTextBox(contactDetails.LastName);
            addEditContactPage.EnterBirthdateTextBox(contactDetails.DateOfBirth);
            addEditContactPage.EnterEmailTextBox(contactDetails.Email);
            addEditContactPage.EnterPhoneTextBox(contactDetails.Phone);
            addEditContactPage.EnterStreet1TextBox(contactDetails.StreetAddress1);
            addEditContactPage.EnterStreet2TextBox(contactDetails.StreetAddress2);
            addEditContactPage.EnterCityTextBox(contactDetails.City);
            addEditContactPage.EnterStateProvinceTextBox(contactDetails.StateOrProvince);
            addEditContactPage.EnterPostalCodeTextBox(contactDetails.PostalCode);
            addEditContactPage.EnterCountryTextBox(contactDetails.Country);

            // Save Email for cleanup later
            cleanupData.Email = contactDetails.Email;
        }

        [Then(@"contact is viewable on the list with the following details")]
        public void ThenContactIsViewableOnTheListWithTheFollowingDetails(Table table)
        {
            ContactDetailsModel contactDetails = table.CreateInstance<ContactDetailsModel>();
            var contactEntry = contactListPage.SearchContactList(contactDetails.Email);

            Assert.AreEqual($"{contactDetails.FirstName} {contactDetails.LastName}", contactListPage.GetContactNameText(contactEntry));
            Assert.AreEqual($"{contactDetails.DateOfBirth}", contactListPage.GetContactBirthdateText(contactEntry));
            Assert.AreEqual($"{contactDetails.Email}", contactListPage.GetContactEmailText(contactEntry));
            Assert.AreEqual($"{contactDetails.Phone}", contactListPage.GetContactPhoneText(contactEntry));
            Assert.AreEqual($"{contactDetails.StreetAddress1} {contactDetails.StreetAddress2}", contactListPage.GetContactAddressText(contactEntry));
            Assert.AreEqual($"{contactDetails.City} {contactDetails.StateOrProvince} {contactDetails.PostalCode}", contactListPage.GetContactCityProvincePostalCodeText(contactEntry));
            Assert.AreEqual($"{contactDetails.Country}", contactListPage.GetContactCountryText(contactEntry));

        }

        [Then(@"contact validation failed by (.*)")]
        public void ThenContactValidationFailedBy(string errorDetails)
        {
            Assert.IsTrue(addEditContactPage.GetErrorDetails().Contains(errorDetails));
        }
    }
}

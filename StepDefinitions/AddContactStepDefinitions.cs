
using BoDi;
using DemoSeleniumSpecFlow.DataModel;
using DemoSeleniumSpecFlow.Drivers;
using DemoSeleniumSpecFlow.Pages;
using DemoSeleniumSpecFlow.Support;
using Gherkin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Assist;

namespace DemoSeleniumSpecFlow.StepDefinitions
{
    [Binding]
    [Scope(Feature = "AddContact")]
    public class AddContactStepDefinitions : BaseStepDefinitions
    {
        public AddContactStepDefinitions(IObjectContainer container) : base(container) { }

        [Given(@"want to add new contact")]
        public void GivenWantToAddNewContact()
        {
            contactListPage.ClickAddContactButton();
        }


        [When(@"new contact is saved")]
        public void WhenNewContactIsSaved()
        {
            addEditContactPage.ClickSubmitButton();
        }

        [When(@"return to contact list")]
        public void WhenReturnToContactList()
        {
            // Save Id for cleanup later
            var contactEntry = contactListPage.SearchContactList(cleanupData.Email);
            cleanupData.Id = contactListPage.GetContactIdText(contactEntry);
        }

        [When(@"Enter value to missing element")]
        public void WhenEnterValueToMissingElement()
        {
            addEditContactPage.EnterGhostElementTextBox(string.Empty);
        }

        [Then(@"Test Failure")]
        public void ThenTestFailure()
        {
            
        }



    }
}
